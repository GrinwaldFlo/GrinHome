using GrinHome.Data;
using GrinHome.Data.Models;
using GrinHome.Data.Services;
using Microsoft.EntityFrameworkCore;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace GrinHome.Server
{
    public class MqttTask : BackgroundService
    {
        private readonly DataService dataService;
        private int MqttClientId;
        private IList<Sensor> sensorsMQTT = new List<Sensor>();

        private int testIncrement = 0;

        private ValueShown[] CacheSensorsLiveHome = Array.Empty<ValueShown>();

        public MqttTask(IServiceProvider serviceProvider, DataService dataService)
        {
            ServiceProvider = serviceProvider;
            this.dataService = dataService;

            dataService.TestCtoS.Subscribe(x => NewEvent());
        }

        public ApplicationDbContext? Db
        {
            get
            {
                try
                {
                    return ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
                }
                catch (Exception ex)   // If there is an issue, takes the last available
                {
                    Serilog.Log.Error(ex, "Get ApplicationDbContext");
                }
                return null;
            }
        }

        public int TestIncrement { get { testIncrement++; return testIncrement; } }
        private IServiceProvider ServiceProvider { get; }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var db = Db;
            if (db == null)
                return;

            db.Database.Migrate();
            await PopulateInitialData();
            await ImportData();
            await Task.Delay(1, stoppingToken);

            CacheSensorsLiveHome = await dataService.GetSensorsLive("Home");
            dataService.CacheSensorsLiveHome.OnNext(CacheSensorsLiveHome);
            sensorsMQTT = db.Sensors.Where(x => x.CommType == CommType.MQTT).Include(x => x.ValueDefinitions).ThenInclude(x => x.DataType).ToList();

            //await TreatData(JObject.Parse(File.ReadAllText("ttn_message_temperature_sample.json")));

            foreach (var item in db.ServerConnexions.Where(x => x.ServerType == CommType.MQTT))
            {
                await StartMqtt(item);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private static void OnSubscriberConnected(MqttClientConnectedEventArgs x)
        {
            Log.Information("MQTT OnSubscriberConnected");
        }

        private static void OnSubscriberDisconnected(MqttClientDisconnectedEventArgs x)
        {
            Log.Information("MQTT OnSubscriberDisconnected");
        }

        private async Task ImportData()
        {
            var db = Db;
            if (db == null || db.SensorValues.Any())
                return;
            string file = "temperature_data.json";
            if (!File.Exists(file))
                return;

            JsonReader jsonReader = new JsonTextReader(new StreamReader(file));
            var j = JObject.Load(jsonReader);

            var a = j["temperature"]?.ToArray();
            if (a == null)
                return;

            var sensors = new Sensor[4];
            sensors[1] = db.Sensors.Include(x => x.ValueDefinitions).First(x => x.Name == "bureau");
            sensors[2] = db.Sensors.Include(x => x.ValueDefinitions).First(x => x.Name == "balcon");
            sensors[3] = db.Sensors.Include(x => x.ValueDefinitions).First(x => x.Name == "frigo");

            var temperature = db.DataTypes.First(x => x.Name == "Temperature");
            var humidity = db.DataTypes.First(x => x.Name == "Humidity");

            foreach (var item in a)
            {
                int? sensorID = (int?)item["Sensor"];
                DateTime? date = (DateTime?)item["EventTime"];
                float? value = (float?)item["Value"];
                float? humidityValue = (float?)item["Humidity"];
                if (sensorID == null || date == null || value == null)
                    continue;
                await db.SensorValues.AddAsync(new SensorValue()
                {
                    ValueDefinition = sensors[(int)sensorID].ValueDefinitions.Where(x => x.DataType.Name == "Temperature").First(),
                    Date = (DateTime)date,
                    Value = (float)value
                });
                if (humidityValue == null)
                    continue;
                await db.SensorValues.AddAsync(new SensorValue()
                {
                    ValueDefinition = sensors[(int)sensorID].ValueDefinitions.Where(x => x.DataType.Name == "Humidity").First(),
                    Date = (DateTime)date,
                    Value = (float)humidityValue
                });
            }
            await db.SaveChangesAsync();
        }

        private void NewEvent()
        {
            dataService.TestStoC.OnNext(TestIncrement);
        }
        private async void OnSubscriberMessageReceived(MqttApplicationMessageReceivedEventArgs x)
        {
            var item = $"Timestamp: {DateTime.Now:O} | Topic: {x.ApplicationMessage.Topic} | Payload: {x.ApplicationMessage.ConvertPayloadToString()} | QoS: {x.ApplicationMessage.QualityOfServiceLevel}";
            Log.Information(item);
            var message = JObject.Parse(x.ApplicationMessage.ConvertPayloadToString());
            await TreatData(message);
            dataService.NotificationUpdate.OnNext(CommType.MQTT);
        }

        private async Task PopulateInitialData()
        {
            var db = Db;

            if (db == null || db.DataTypes.Any() || db.Sensors.Any())
                return;

            var tempInternal = new Data.Models.DataType()
            {
                Name = "Temperature",
                Unit = "°C",
                JsonPath = "uplink_message.decoded_payload.TempC_SHT"
            };
            await db.DataTypes.AddAsync(tempInternal);
            var tempExternal = new Data.Models.DataType()
            {
                Name = "Temperature external",
                Unit = "°C",
                JsonPath = "uplink_message.decoded_payload.TempC_DS"
            };
            await db.DataTypes.AddAsync(tempExternal);
            var humidity = new Data.Models.DataType()
            {
                Name = "Humidity",
                Unit = "%",
                JsonPath = "uplink_message.decoded_payload.Hum_SHT"
            };
            await db.DataTypes.AddAsync(humidity);

            await db.Sensors.AddAsync(new Data.Models.Sensor()
            {
                Name = "Bureau",
                DeviceName = "bureau",
                CommType = CommType.MQTT,
                ValueDefinitions = {
                    new ValueDefinition(){
                    DataType = tempInternal,
                    Max = 25,
                    Min = 21,
                    Name = "Bureau"
                    },
                    new ValueDefinition(){
                    DataType = humidity,
                    Max = 100,
                    Min = 0,
                    Name = "Bureau"
                    }
                }
            });

            await db.Sensors.AddAsync(new Data.Models.Sensor()
            {
                Name = "Frigo",
                DeviceName = "frigo",
                CommType = CommType.MQTT,
                ValueDefinitions = {
                    new ValueDefinition(){
                    DataType = tempInternal,
                    Max = 12,
                    Min = 2,
                    Name = "Frigo haut"
                    },
                    new ValueDefinition(){
                    DataType = humidity,
                    Max = 100,
                    Min = 0,
                    Name = "Frigo"
                    }
                    ,
                    new ValueDefinition(){
                    DataType = tempExternal,
                    Max = 12,
                    Min = 2,
                    Name = "Frigo bas"
                    }
                }
            });

            await db.Sensors.AddAsync(new Data.Models.Sensor()
            {
                Name = "Balcon",
                DeviceName = "balcon",
                CommType = CommType.MQTT,
                ValueDefinitions = {
                    new ValueDefinition(){
                    DataType = tempInternal,
                    Max = 30,
                    Min = -2,
                    Name = "Balcon"
                    },
                    new ValueDefinition(){
                    DataType = humidity,
                    Max = 100,
                    Min = 0,
                    Name = "Balcon"
                    }
                    ,
                    new ValueDefinition(){
                    DataType = tempExternal,
                    Max = 30,
                    Min = -2,
                    Name = "Balcon sol"
                    }
                }
            });


            await db.ServerConnexions.AddAsync(new ServerConnexion()
            {
                Name = "MQTT Temperature",
                ServerType = CommType.MQTT,
                URL = "eu1.cloud.thethings.network",
                Port = 8883,
                User = "grintemp@ttn",
                Password = "NNSXS.NZOOM34ENNO622BUD6KYDGCHSXX5LCMFE5H2KRY.CZCECZVRJLF3ETJFBU6BESRT22UPCXJ35IPJ6W2V6PZEWDGWSCWQ".Encrypt(),
                Topic = "v3/grintemp@ttn/devices/#"
            });


            if (await db.SaveChangesAsync() == 0)
                Log.Information("Failed to save first template");
            else
                Log.Information("Basic data saved");


            var sensors = db.Sensors.ToList();
            ushort cnt = 1;
            foreach (var sensor in sensors)
            {
                foreach (var valueDefinition in sensor.ValueDefinitions)
                {
                    await db.ValuesShown.AddAsync(new ValueShown()
                    {
                        Order = cnt++,
                        Page = "Home",
                        ValueDefinition = valueDefinition
                    }
                       );
                }
            }
            if (await db.SaveChangesAsync() == 0)
                Log.Information("Failed to save first template");
            else
                Log.Information("Basic data saved");
        }

        private async Task StartMqtt(ServerConnexion mqttOption)
        {
            Log.Information("Connecting to MQTT server");
            var password = mqttOption.Password.Decrypt();
            var options = new MqttClientOptionsBuilder()
                .WithClientId("Client" + MqttClientId++)
                .WithTcpServer(mqttOption.URL, mqttOption.Port)
                .WithCredentials(mqttOption.User, mqttOption.Password.Decrypt())
                .WithTls()
                .WithCleanSession()
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(5))
                .Build();

            var mqttFactory = new MqttFactory();
            var managedMqttClientSubscriber = mqttFactory.CreateManagedMqttClient();
            managedMqttClientSubscriber.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnSubscriberConnected);
            managedMqttClientSubscriber.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnSubscriberDisconnected);
            managedMqttClientSubscriber.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(this.OnSubscriberMessageReceived);

            await managedMqttClientSubscriber.StartAsync(
                new ManagedMqttClientOptions
                {
                    ClientOptions = options,
                });

            var topicFilter = new MqttTopicFilter { Topic = mqttOption.Topic };
            await managedMqttClientSubscriber.SubscribeAsync(topicFilter);
        }
        private async Task TreatData(JObject data)
        {
            if (data == null)
                return;
            var deviceID = (string?)data.SelectToken("end_device_ids.device_id");
            if (deviceID == null)
                return;
            var sensor = sensorsMQTT.FirstOrDefault(x => x.DeviceName == deviceID);
            if (sensor == null)
            {
                Log.Information($"MQTT Device not found {deviceID}");
                return;
            }

            foreach (var item in sensor.ValueDefinitions)
            {
                try
                {
                    var value = (float?)data.SelectToken(item.DataType.JsonPath);
                    if (value != null)
                    {
                        await new SensorValue()
                        {
                            ValueDefinitionID = item.ID,
                            Value = (float)value,
                            Date = DateTime.Now
                        }.AddAsync(Db);

                        var cacheValue = CacheSensorsLiveHome.Where(x => x.ValueDefinition.ID == item.ID).FirstOrDefault();
                        if (cacheValue != null)
                            cacheValue.SetValue(value.Value);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("MQTT, failed to get data", ex);
                }
            }
            dataService.CacheSensorsLiveHome.OnNext(CacheSensorsLiveHome);
        }
    }
}