﻿// <auto-generated />
using AccountServer.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AccountServer.DB.AccountDb", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AccountPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("PlayerId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("AccountServer.DB.ItemDb", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.HasIndex("PlayerId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("AccountServer.DB.PlayerDb", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PlayerStatId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.HasIndex("PlayerStatId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("AccountServer.DB.PlayerStatDb", b =>
                {
                    b.Property<int>("PlayerStatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<float>("AtkSpeed")
                        .HasColumnType("float");

                    b.Property<float>("CritDamage")
                        .HasColumnType("float");

                    b.Property<int>("CritRate")
                        .HasColumnType("int");

                    b.Property<float>("DodgeTime")
                        .HasColumnType("float");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MaxHp")
                        .HasColumnType("int");

                    b.Property<float>("MoveSpeed")
                        .HasColumnType("float");

                    b.Property<float>("RunMultiplier")
                        .HasColumnType("float");

                    b.HasKey("PlayerStatId");

                    b.HasIndex("PlayerStatId")
                        .IsUnique();

                    b.ToTable("Player_Stat");
                });

            modelBuilder.Entity("AccountServer.DB.AccountDb", b =>
                {
                    b.HasOne("AccountServer.DB.PlayerDb", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("AccountServer.DB.ItemDb", b =>
                {
                    b.HasOne("AccountServer.DB.PlayerDb", "Player")
                        .WithMany("Items")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("AccountServer.DB.PlayerDb", b =>
                {
                    b.HasOne("AccountServer.DB.PlayerStatDb", "PlayerStat")
                        .WithMany()
                        .HasForeignKey("PlayerStatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerStat");
                });

            modelBuilder.Entity("AccountServer.DB.PlayerDb", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
