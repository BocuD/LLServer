﻿// <auto-generated />
using System;
using LLServer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LLServer.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230719003900_Screen filter level")]
    partial class Screenfilterlevel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.6.23329.4");

            modelBuilder.Entity("LLServer.Database.Models.Session", b =>
                {
                    b.Property<string>("SessionId")
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SessionId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("LLServer.Database.Models.User", b =>
                {
                    b.Property<ulong>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CardId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Initialized")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.HasAlternateKey("CardId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LLServer.Models.UserData.MemberCardData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CardMemberId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "m_card_member_id");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "count");

                    b.Property<bool>("New")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "new");

                    b.Property<int>("PrintRest")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "print_rest");

                    b.Property<ulong>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("MemberCardData");
                });

            modelBuilder.Entity("LLServer.Models.UserData.MemberData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AchieveRank")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "achieve_rank");

                    b.Property<int>("Camera")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "camera");

                    b.Property<int>("CardMemberId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "m_card_member_id");

                    b.Property<int>("CardMemorialId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "m_card_memorial_id");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "character_id");

                    b.Property<int>("Main")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "main");

                    b.Property<bool>("New")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "new");

                    b.Property<int>("SelectCount")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "select_count");

                    b.Property<int>("Stage")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "stage");

                    b.Property<ulong>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YellPoint")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "yell_point");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("MemberData");
                });

            modelBuilder.Entity("LLServer.Models.UserData.UserData", b =>
                {
                    b.Property<ulong>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Badge")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "badge");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "character_id");

                    b.Property<int>("CreditCountCenter")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "credit_count_center");

                    b.Property<int>("CreditCountSatellite")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "credit_count_satellite");

                    b.Property<int>("Honor")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "honor");

                    b.Property<int>("IdolKind")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "idol_kind");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<int>("Nameplate")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "nameplate");

                    b.Property<int>("NoteSpeedLevel")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "note_speed_level");

                    b.Property<int>("PlayCenter")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "play_center");

                    b.Property<string>("PlayDate")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "play_date");

                    b.Property<int>("PlayLs4")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "play_ls4");

                    b.Property<int>("PlaySatellite")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "play_satellite");

                    b.Property<string>("ProfileCardId1")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "profile_card_id_1");

                    b.Property<string>("ProfileCardId2")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "profile_card_id_2");

                    b.Property<int>("ScreenFilterLevel")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "screen_filter_level");

                    b.Property<int>("SubMonitorType")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "submonitor_type");

                    b.Property<string>("TenpoName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "tenpo_name");

                    b.Property<int>("TotalExp")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "total_exp");

                    b.Property<int>("VolumeBgm")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "volume_bgm");

                    b.Property<int>("VolumeSe")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "volume_se");

                    b.Property<int>("VolumeVoice")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "volume_voice");

                    b.HasKey("UserID");

                    b.ToTable("UserData");
                });

            modelBuilder.Entity("LLServer.Models.UserData.UserDataAqours", b =>
                {
                    b.Property<ulong>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Badge")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "badge");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "character_id");

                    b.Property<int>("Honor")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "honor");

                    b.Property<int>("Nameplate")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "nameplate");

                    b.Property<string>("ProfileCardId1")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "profile_card_id_1");

                    b.Property<string>("ProfileCardId2")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "profile_card_id_2");

                    b.HasKey("UserID");

                    b.ToTable("UserDataAqours");
                });

            modelBuilder.Entity("LLServer.Models.UserData.UserDataSaintSnow", b =>
                {
                    b.Property<ulong>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Badge")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "badge");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "character_id");

                    b.Property<int>("Honor")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "honor");

                    b.Property<int>("Nameplate")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "nameplate");

                    b.Property<string>("ProfileCardId1")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "profile_card_id_1");

                    b.Property<string>("ProfileCardId2")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "profile_card_id_2");

                    b.HasKey("UserID");

                    b.ToTable("UserDataSaintSnow");
                });

            modelBuilder.Entity("LLServer.Database.Models.Session", b =>
                {
                    b.HasOne("LLServer.Database.Models.User", "User")
                        .WithOne("Session")
                        .HasForeignKey("LLServer.Database.Models.Session", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LLServer.Models.UserData.MemberCardData", b =>
                {
                    b.HasOne("LLServer.Database.Models.User", "User")
                        .WithMany("MemberCards")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LLServer.Models.UserData.MemberData", b =>
                {
                    b.HasOne("LLServer.Database.Models.User", "User")
                        .WithMany("Members")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LLServer.Models.UserData.UserData", b =>
                {
                    b.HasOne("LLServer.Database.Models.User", "User")
                        .WithOne("UserData")
                        .HasForeignKey("LLServer.Models.UserData.UserData", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LLServer.Models.UserData.UserDataAqours", b =>
                {
                    b.HasOne("LLServer.Database.Models.User", "User")
                        .WithOne("UserDataAqours")
                        .HasForeignKey("LLServer.Models.UserData.UserDataAqours", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LLServer.Models.UserData.UserDataSaintSnow", b =>
                {
                    b.HasOne("LLServer.Database.Models.User", "User")
                        .WithOne("UserDataSaintSnow")
                        .HasForeignKey("LLServer.Models.UserData.UserDataSaintSnow", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LLServer.Database.Models.User", b =>
                {
                    b.Navigation("MemberCards");

                    b.Navigation("Members");

                    b.Navigation("Session");

                    b.Navigation("UserData")
                        .IsRequired();

                    b.Navigation("UserDataAqours")
                        .IsRequired();

                    b.Navigation("UserDataSaintSnow")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
