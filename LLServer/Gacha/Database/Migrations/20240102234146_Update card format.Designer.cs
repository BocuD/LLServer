﻿// <auto-generated />
using LLServer.Gacha.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    [DbContext(typeof(GachaDbContext))]
    [Migration("20240102234146_Update card format")]
    partial class Updatecardformat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.6.23329.4");

            modelBuilder.Entity("LLServer.Gacha.Database.GachaCard", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GachaTableid")
                        .HasColumnType("TEXT");

                    b.Property<string>("characterIds")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("rarityIds")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("GachaTableid");

                    b.ToTable("GachaCards");
                });

            modelBuilder.Entity("LLServer.Gacha.Database.GachaTable", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("characterIds")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("isValid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("maxRarity")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("GachaTables");
                });

            modelBuilder.Entity("LLServer.Gacha.Database.GachaCard", b =>
                {
                    b.HasOne("LLServer.Gacha.Database.GachaTable", null)
                        .WithMany("gachaCards")
                        .HasForeignKey("GachaTableid");
                });

            modelBuilder.Entity("LLServer.Gacha.Database.GachaTable", b =>
                {
                    b.Navigation("gachaCards");
                });
#pragma warning restore 612, 618
        }
    }
}
