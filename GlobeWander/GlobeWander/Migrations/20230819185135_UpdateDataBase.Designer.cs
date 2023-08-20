﻿// <auto-generated />
using System;
using GlobeWander.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GlobeWander.Migrations
{
    [DbContext(typeof(GlobeWanderDbContext))]
<<<<<<<< HEAD:GlobeWander/GlobeWander/Migrations/20230819185135_UpdateDataBase.Designer.cs
    [Migration("20230819185135_UpdateDataBase")]
    partial class UpdateDataBase
========
    [Migration("20230819222829_dd")]
    partial class dd
>>>>>>>> 835d49d (test Hotel Room And Room):GlobeWander/GlobeWander/Migrations/20230819222829_dd.Designer.cs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GlobeWander.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GlobeWander.Models.BookingRoom", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

<<<<<<<< HEAD:GlobeWander/GlobeWander/Migrations/20230819185135_UpdateDataBase.Designer.cs
========
                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

>>>>>>>> 835d49d (test Hotel Room And Room):GlobeWander/GlobeWander/Migrations/20230819222829_dd.Designer.cs
                    b.HasKey("ID");

                    b.HasIndex("HotelID", "RoomNumber")
                        .IsUnique();

                    b.ToTable("BookingRooms");
                });

            modelBuilder.Entity("GlobeWander.Models.BookingTrip", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("CostPerPerson")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPersons")
                        .HasColumnType("int");

                    b.Property<int>("TripID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TripID");

                    b.ToTable("bookingTrips");
                });

            modelBuilder.Entity("GlobeWander.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TourSpotID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourSpotID");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A unique hotel that you can't find in this place",
                            Name = "Paradise",
                            TourSpotID = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "A unique hotel that you can't find in this place",
                            Name = "Wander ",
                            TourSpotID = 2
                        },
                        new
                        {
                            Id = 3,
                            Description = "A unique hotel that you can't find in this place",
                            Name = "Amazing",
                            TourSpotID = 3
                        });
                });

            modelBuilder.Entity("GlobeWander.Models.HotelRoom", b =>
                {
                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.HasKey("HotelID", "RoomNumber");

                    b.HasIndex("RoomID");

                    b.ToTable("HotelRooms");
                });

            modelBuilder.Entity("GlobeWander.Models.Rate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TripID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TripID");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("GlobeWander.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Layout = 2,
                            Name = "Small Room"
                        },
                        new
                        {
                            ID = 2,
                            Layout = 3,
                            Name = "Suite Room"
                        },
                        new
                        {
                            ID = 3,
                            Layout = 1,
                            Name = "Studio room"
                        });
                });

            modelBuilder.Entity("GlobeWander.Models.TourSpot", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.ToTable("TourSpots");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Category = 3,
                            City = "Petra",
                            Country = "Jordan",
                            Description = "a place before thousands years",
                            Name = "Petra",
                            PhoneNumber = 78885423L
                        },
                        new
                        {
                            ID = 2,
                            Category = 3,
                            City = "Jerash",
                            Country = "Jordan",
                            Description = "A historical place that the Romanian civilization build before thousands years.",
                            Name = "Jerash",
                            PhoneNumber = 88782215L
                        },
                        new
                        {
                            ID = 3,
                            Category = 3,
                            City = "Irbid",
                            Country = "Jordan",
                            Description = "A historical place that the Romanian civilization build before thousands years. In the north of Jordan",
                            Name = "Um Qais",
                            PhoneNumber = 788442523L
                        });
                });

            modelBuilder.Entity("GlobeWander.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Activity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TourSpotID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourSpotID");

                    b.ToTable("Trips");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Activity = "walking",
                            Cost = 20.0,
                            Description = "trip start at 8 am and going from Amman to Petra",
<<<<<<<< HEAD:GlobeWander/GlobeWander/Migrations/20230819185135_UpdateDataBase.Designer.cs
                            EndDate = new DateTime(2023, 8, 19, 18, 51, 34, 914, DateTimeKind.Utc).AddTicks(3698),
                            Name = "Petra ride",
                            StartDate = new DateTime(2023, 8, 19, 21, 51, 34, 914, DateTimeKind.Local).AddTicks(3685),
========
                            EndDate = new DateTime(2023, 8, 19, 22, 28, 29, 128, DateTimeKind.Utc).AddTicks(8046),
                            Name = "Petra ride",
                            StartDate = new DateTime(2023, 8, 20, 1, 28, 29, 128, DateTimeKind.Local).AddTicks(8005),
>>>>>>>> 835d49d (test Hotel Room And Room):GlobeWander/GlobeWander/Migrations/20230819222829_dd.Designer.cs
                            Theme = "Discovering",
                            TourSpotID = 1
                        },
                        new
                        {
                            Id = 2,
                            Activity = "visiting",
                            Cost = 30.0,
                            Description = "Amman to Jerash with a trip manager who can speak many languages",
<<<<<<<< HEAD:GlobeWander/GlobeWander/Migrations/20230819185135_UpdateDataBase.Designer.cs
                            EndDate = new DateTime(2023, 8, 19, 18, 51, 34, 914, DateTimeKind.Utc).AddTicks(3701),
                            Name = "Jerash ride",
                            StartDate = new DateTime(2023, 8, 19, 21, 51, 34, 914, DateTimeKind.Local).AddTicks(3701),
========
                            EndDate = new DateTime(2023, 8, 19, 22, 28, 29, 128, DateTimeKind.Utc).AddTicks(8050),
                            Name = "Jerash ride",
                            StartDate = new DateTime(2023, 8, 20, 1, 28, 29, 128, DateTimeKind.Local).AddTicks(8049),
>>>>>>>> 835d49d (test Hotel Room And Room):GlobeWander/GlobeWander/Migrations/20230819222829_dd.Designer.cs
                            Theme = "Discovering",
                            TourSpotID = 2
                        },
                        new
                        {
                            Id = 3,
                            Activity = "climbing",
                            Cost = 40.0,
                            Description = "Amman to Irbid with a trip manager who can speak many languages",
<<<<<<<< HEAD:GlobeWander/GlobeWander/Migrations/20230819185135_UpdateDataBase.Designer.cs
                            EndDate = new DateTime(2023, 8, 19, 18, 51, 34, 914, DateTimeKind.Utc).AddTicks(3704),
                            Name = "Um-Qais ride",
                            StartDate = new DateTime(2023, 8, 19, 21, 51, 34, 914, DateTimeKind.Local).AddTicks(3703),
========
                            EndDate = new DateTime(2023, 8, 19, 22, 28, 29, 128, DateTimeKind.Utc).AddTicks(8054),
                            Name = "Um-Qais ride",
                            StartDate = new DateTime(2023, 8, 20, 1, 28, 29, 128, DateTimeKind.Local).AddTicks(8052),
>>>>>>>> 835d49d (test Hotel Room And Room):GlobeWander/GlobeWander/Migrations/20230819222829_dd.Designer.cs
                            Theme = "Discovering",
                            TourSpotID = 3
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "admin manager",
                            ConcurrencyStamp = "00000000-0000-0000-0000-000000000000",
                            Name = "Admin Manager",
                            NormalizedName = "ADMIN MANAGER"
                        },
                        new
                        {
                            Id = "tour manager",
                            ConcurrencyStamp = "00000000-0000-0000-0000-000000000000",
                            Name = "Tour Manager",
                            NormalizedName = "TOUR MANAGER"
                        },
                        new
                        {
                            Id = "trip manager",
                            ConcurrencyStamp = "00000000-0000-0000-0000-000000000000",
                            Name = "Trip Manager",
                            NormalizedName = "TRIP MANAGER"
                        },
                        new
                        {
                            Id = "hotel manager",
                            ConcurrencyStamp = "00000000-0000-0000-0000-000000000000",
                            Name = "Hotel Manager",
                            NormalizedName = "HOTEL MANAGER"
                        },
                        new
                        {
                            Id = "user",
                            ConcurrencyStamp = "00000000-0000-0000-0000-000000000000",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "anonymoususer",
                            ConcurrencyStamp = "00000000-0000-0000-0000-000000000000",
                            Name = "AnonymousUser",
                            NormalizedName = "ANONYMOUSUSER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GlobeWander.Models.BookingRoom", b =>
                {
                    b.HasOne("GlobeWander.Models.HotelRoom", "HotelRooms")
                        .WithOne("BookingRoom")
                        .HasForeignKey("GlobeWander.Models.BookingRoom", "HotelID", "RoomNumber")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("HotelRooms");
                });

            modelBuilder.Entity("GlobeWander.Models.BookingTrip", b =>
                {
                    b.HasOne("GlobeWander.Models.Trip", "Trip")
                        .WithMany("BookingTrips")
                        .HasForeignKey("TripID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("GlobeWander.Models.Hotel", b =>
                {
                    b.HasOne("GlobeWander.Models.TourSpot", "TourSpot")
                        .WithMany("Hotels")
                        .HasForeignKey("TourSpotID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TourSpot");
                });

            modelBuilder.Entity("GlobeWander.Models.HotelRoom", b =>
                {
                    b.HasOne("GlobeWander.Models.Hotel", "Hotel")
                        .WithMany("HotelRoom")
                        .HasForeignKey("HotelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GlobeWander.Models.Room", "Rooms")
                        .WithMany("HotelRooms")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("GlobeWander.Models.Rate", b =>
                {
                    b.HasOne("GlobeWander.Models.Trip", "Trip")
                        .WithMany("Rates")
                        .HasForeignKey("TripID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("GlobeWander.Models.Trip", b =>
                {
                    b.HasOne("GlobeWander.Models.TourSpot", "TourSpots")
                        .WithMany("Trips")
                        .HasForeignKey("TourSpotID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TourSpots");
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
                    b.HasOne("GlobeWander.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GlobeWander.Models.ApplicationUser", null)
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

                    b.HasOne("GlobeWander.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GlobeWander.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GlobeWander.Models.Hotel", b =>
                {
                    b.Navigation("HotelRoom");
                });

            modelBuilder.Entity("GlobeWander.Models.HotelRoom", b =>
                {
                    b.Navigation("BookingRoom");
                });

            modelBuilder.Entity("GlobeWander.Models.Room", b =>
                {
                    b.Navigation("HotelRooms");
                });

            modelBuilder.Entity("GlobeWander.Models.TourSpot", b =>
                {
                    b.Navigation("Hotels");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("GlobeWander.Models.Trip", b =>
                {
                    b.Navigation("BookingTrips");

                    b.Navigation("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
