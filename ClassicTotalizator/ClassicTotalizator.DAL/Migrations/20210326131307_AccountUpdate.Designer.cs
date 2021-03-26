﻿// <auto-generated />
using System;
using ClassicTotalizator.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ClassicTotalizator.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210326131307_AccountUpdate")]
    partial class AccountUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("AccountCreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AccountType")
                        .HasColumnType("text");

                    b.Property<string>("AvatarLink")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DOB")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Bet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Account_Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("BetTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Choice")
                        .HasColumnType("text");

                    b.Property<Guid>("Event_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Account_Id");

                    b.HasIndex("Event_Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.BetPool", b =>
                {
                    b.Property<Guid>("Event_Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Event_Id");

                    b.ToTable("BetPools");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsEnded")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Margin")
                        .HasColumnType("numeric");

                    b.Property<Guid>("Participant_Id1")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Participant_Id2")
                        .HasColumnType("uuid");

                    b.Property<string[]>("PossibleResults")
                        .HasColumnType("text[]");

                    b.Property<string>("Result")
                        .HasColumnType("text");

                    b.Property<int>("Sport_Id")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Participant_Id1");

                    b.HasIndex("Participant_Id2");

                    b.HasIndex("Sport_Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Account_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Account_Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Parameter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Participant_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Participant_Id");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Participant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("Participant_Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Participant_Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Account_Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Account_Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Account_Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.HasKey("Account_Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Bet", b =>
                {
                    b.HasOne("ClassicTotalizator.DAL.Entities.Account", "Account")
                        .WithMany("BetsHistory")
                        .HasForeignKey("Account_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassicTotalizator.DAL.Entities.BetPool", "BetPool")
                        .WithMany("Bets")
                        .HasForeignKey("Event_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("BetPool");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.BetPool", b =>
                {
                    b.HasOne("ClassicTotalizator.DAL.Entities.Event", "Event")
                        .WithOne("BetPool")
                        .HasForeignKey("ClassicTotalizator.DAL.Entities.BetPool", "Event_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Event", b =>
                {
                    b.HasOne("ClassicTotalizator.DAL.Entities.Participant", "Participant1")
                        .WithMany()
                        .HasForeignKey("Participant_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassicTotalizator.DAL.Entities.Participant", "Participant2")
                        .WithMany()
                        .HasForeignKey("Participant_Id2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassicTotalizator.DAL.Entities.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("Sport_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant1");

                    b.Navigation("Participant2");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Message", b =>
                {
                    b.HasOne("ClassicTotalizator.DAL.Entities.Account", "Account")
                        .WithMany("Messages")
                        .HasForeignKey("Account_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Parameter", b =>
                {
                    b.HasOne("ClassicTotalizator.DAL.Entities.Participant", "Participant")
                        .WithMany("Parameters")
                        .HasForeignKey("Participant_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Player", b =>
                {
                    b.HasOne("ClassicTotalizator.DAL.Entities.Participant", "Participant")
                        .WithMany("Players")
                        .HasForeignKey("Participant_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Transaction", b =>
                {
                    b.HasOne("ClassicTotalizator.DAL.Entities.Account", "Account")
                        .WithMany("TransactionsHistory")
                        .HasForeignKey("Account_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Wallet", b =>
                {
                    b.HasOne("ClassicTotalizator.DAL.Entities.Account", "Account")
                        .WithOne("Wallet")
                        .HasForeignKey("ClassicTotalizator.DAL.Entities.Wallet", "Account_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Account", b =>
                {
                    b.Navigation("BetsHistory");

                    b.Navigation("Messages");

                    b.Navigation("TransactionsHistory");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.BetPool", b =>
                {
                    b.Navigation("Bets");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Event", b =>
                {
                    b.Navigation("BetPool");
                });

            modelBuilder.Entity("ClassicTotalizator.DAL.Entities.Participant", b =>
                {
                    b.Navigation("Parameters");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
