﻿// <auto-generated />
using System;
using GrinHome.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GrinHome.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220226095550_vps")]
    partial class vps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GrinHome.Data.Models.DataType", b =>
                {
                    b.Property<ushort>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("JsonPath")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.HasKey("ID");

                    b.ToTable("DataTypes");
                });

            modelBuilder.Entity("GrinHome.Data.Models.Postfix", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Bounced")
                        .HasColumnType("int");

                    b.Property<int>("BytesDelivered")
                        .HasColumnType("int");

                    b.Property<int>("BytesReceived")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePostfix")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Deferred")
                        .HasColumnType("int");

                    b.Property<int>("Delivered")
                        .HasColumnType("int");

                    b.Property<int>("Discarded")
                        .HasColumnType("int");

                    b.Property<int>("Forwarded")
                        .HasColumnType("int");

                    b.Property<int>("Held")
                        .HasColumnType("int");

                    b.Property<string>("MessageRejectDetail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Received")
                        .HasColumnType("int");

                    b.Property<int>("RecipientHostDomain")
                        .HasColumnType("int");

                    b.Property<int>("Recipients")
                        .HasColumnType("int");

                    b.Property<int>("Rejected")
                        .HasColumnType("int");

                    b.Property<int>("RejectedWarning")
                        .HasColumnType("int");

                    b.Property<int>("Senders")
                        .HasColumnType("int");

                    b.Property<int>("SendingHostDomain")
                        .HasColumnType("int");

                    b.Property<int>("SmtpdWarnings")
                        .HasColumnType("int");

                    b.Property<string>("TopMessageCount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TopMessageDelivery")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TopMessageReceived")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TopRecipientsCount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TopRecipientsSize")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TopSendersCount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TopSendersSize")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Postfixes");
                });

            modelBuilder.Entity("GrinHome.Data.Models.Sensor", b =>
                {
                    b.Property<ushort>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint unsigned");

                    b.Property<int>("CommType")
                        .HasColumnType("int");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("GrinHome.Data.Models.SensorValue", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("Value")
                        .HasColumnType("float");

                    b.Property<ushort>("ValueDefinitionID")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("ID");

                    b.HasIndex("ValueDefinitionID");

                    b.ToTable("SensorValues");
                });

            modelBuilder.Entity("GrinHome.Data.Models.ServerConnexion", b =>
                {
                    b.Property<ushort>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<int>("ServerType")
                        .HasColumnType("int");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ID");

                    b.ToTable("ServerConnexions");
                });

            modelBuilder.Entity("GrinHome.Data.Models.ValueDefinition", b =>
                {
                    b.Property<ushort>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint unsigned");

                    b.Property<ushort>("DataTypeID")
                        .HasColumnType("smallint unsigned");

                    b.Property<float>("LimitMax")
                        .HasColumnType("float");

                    b.Property<float>("LimitMin")
                        .HasColumnType("float");

                    b.Property<float>("Max")
                        .HasColumnType("float");

                    b.Property<float>("Min")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<ushort>("SensorID")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("ID");

                    b.HasIndex("DataTypeID");

                    b.HasIndex("SensorID");

                    b.ToTable("ValueDefinitions");
                });

            modelBuilder.Entity("GrinHome.Data.Models.ValueShown", b =>
                {
                    b.Property<ushort>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint unsigned");

                    b.Property<ushort>("Order")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("Page")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<ushort>("ValueDefinitionID")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("ID");

                    b.HasIndex("ValueDefinitionID");

                    b.ToTable("ValuesShown");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GrinHome.Data.Models.SensorValue", b =>
                {
                    b.HasOne("GrinHome.Data.Models.ValueDefinition", "ValueDefinition")
                        .WithMany("SensorValues")
                        .HasForeignKey("ValueDefinitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ValueDefinition");
                });

            modelBuilder.Entity("GrinHome.Data.Models.ValueDefinition", b =>
                {
                    b.HasOne("GrinHome.Data.Models.DataType", "DataType")
                        .WithMany()
                        .HasForeignKey("DataTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GrinHome.Data.Models.Sensor", "Sensor")
                        .WithMany("ValueDefinitions")
                        .HasForeignKey("SensorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DataType");

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("GrinHome.Data.Models.ValueShown", b =>
                {
                    b.HasOne("GrinHome.Data.Models.ValueDefinition", "ValueDefinition")
                        .WithMany()
                        .HasForeignKey("ValueDefinitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ValueDefinition");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GrinHome.Data.Models.Sensor", b =>
                {
                    b.Navigation("ValueDefinitions");
                });

            modelBuilder.Entity("GrinHome.Data.Models.ValueDefinition", b =>
                {
                    b.Navigation("SensorValues");
                });
#pragma warning restore 612, 618
        }
    }
}