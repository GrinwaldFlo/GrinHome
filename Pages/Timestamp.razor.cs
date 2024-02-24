
namespace GrinHome.Pages;
public partial class Timestamp
{
	private readonly string _filename = "timestamp.log";
	private readonly string _filenameConfig = "timestamp.conf";
	private List<string> _log = new();
	private List<string> conf = new();
	private string? curNewConfig;

	public Timestamp()
	{

	}

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();

		if (File.Exists(_filename))
		{
			_log.AddRange(File.ReadAllLines(_filename));
		}
		if (File.Exists(_filenameConfig))
		{
			conf.AddRange(File.ReadAllLines(_filenameConfig));
		}
		if (conf.Count == 0)
			conf.Add("Default");
	}
	private void AddConf()
	{
		if (curNewConfig == null)
		{
			return;
		}

		if (curNewConfig == "clearall")
		{
			conf.Clear();
			_log.Clear();
			File.WriteAllLines(_filename, _log);
		}
		else
			conf.Add(curNewConfig);
		File.WriteAllLines(_filenameConfig, conf);
		StateHasChanged();
	}

	private void AddTimestamp(string confItem)
	{
		_log.Add($"{DateTime.Now.AddHours(1)} | {confItem}");
		File.WriteAllLines(_filename, _log);
	}
}