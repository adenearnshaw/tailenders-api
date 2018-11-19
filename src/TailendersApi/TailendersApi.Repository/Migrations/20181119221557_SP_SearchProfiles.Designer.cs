﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TailendersApi.Repository;

namespace TailendersApi.Repository.Migrations
{
    [DbContext(typeof(TailendersContext))]
    [Migration("20181119221557_SP_SearchProfiles")]
    partial class SP_SearchProfiles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TailendersApi.Repository.Entities.ConversationEntity", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<DateTime>("LastUpdated");

                    b.HasKey("ID");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("TailendersApi.Repository.Entities.PairingEntity", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConversationID");

                    b.Property<bool>("IsBlocked");

                    b.Property<bool>("IsLiked");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("PairedProfileId");

                    b.Property<string>("ProfileEntityID");

                    b.Property<string>("ProfileId");

                    b.HasKey("ID");

                    b.HasIndex("ConversationID");

                    b.HasIndex("ProfileEntityID");

                    b.ToTable("Pairings");
                });

            modelBuilder.Entity("TailendersApi.Repository.Entities.ProfileEntity", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Bio");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("FavouritePosition");

                    b.Property<double>("Latitude");

                    b.Property<string>("Location");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<int>("SearchForCategory");

                    b.Property<int>("SearchMaxAge");

                    b.Property<int>("SearchMinAge");

                    b.Property<int>("SearchRadius");

                    b.Property<int>("SearchShowInCategory");

                    b.Property<bool>("ShowAge");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("TailendersApi.Repository.Entities.ProfileImageEntity", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("ProfileEntityID");

                    b.Property<string>("ProfileId");

                    b.HasKey("ID");

                    b.HasIndex("ProfileEntityID");

                    b.ToTable("ProfileImageEntity");
                });

            modelBuilder.Entity("TailendersApi.Repository.Entities.PairingEntity", b =>
                {
                    b.HasOne("TailendersApi.Repository.Entities.ConversationEntity", "Conversation")
                        .WithMany()
                        .HasForeignKey("ConversationID");

                    b.HasOne("TailendersApi.Repository.Entities.ProfileEntity")
                        .WithMany("Pairings")
                        .HasForeignKey("ProfileEntityID");
                });

            modelBuilder.Entity("TailendersApi.Repository.Entities.ProfileImageEntity", b =>
                {
                    b.HasOne("TailendersApi.Repository.Entities.ProfileEntity")
                        .WithMany("ProfileImages")
                        .HasForeignKey("ProfileEntityID");
                });
#pragma warning restore 612, 618
        }
    }
}
