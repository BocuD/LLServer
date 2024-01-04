﻿// <auto-generated />
using LLServer.Gacha.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LLServer.Gacha.Database.Migrations
{
    [DbContext(typeof(GachaDbContext))]
    partial class GachaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.6.23329.4");

            modelBuilder.Entity("LLServer.Gacha.Database.GachaCard", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<int>("cardType")
                        .HasColumnType("INTEGER");

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

                    b.ToTable("GachaCards");
                });

            modelBuilder.Entity("LLServer.Gacha.Database.GachaCardGroup", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("cardIds")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("GachaCardGroups");
                });

            modelBuilder.Entity("LLServer.Gacha.Database.GachaTable", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("GachaTableMetaData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("cardGroupIds")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("cardIds")
                        .IsRequired()
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
#pragma warning restore 612, 618
        }
    }
}
