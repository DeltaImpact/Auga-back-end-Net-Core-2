﻿// <auto-generated />
using System;
using Auga.DAO.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auga.DAO.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Auga.DAO.Entities.ChatConnectedUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConnectionId");

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime?>("Modified");

                    b.Property<long?>("UpdatedBy");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ChatConnectedUsers");
                });

            modelBuilder.Entity("Auga.DAO.Entities.GameWaitingUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<long>("ItemId");

                    b.Property<DateTime?>("Modified");

                    b.Property<long?>("UpdatedBy");

                    b.Property<long>("UserId");

                    b.Property<long>("Username");

                    b.HasKey("Id");

                    b.ToTable("GameWaitingUsers");
                });

            modelBuilder.Entity("Auga.DAO.Entities.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BuyerId");

                    b.Property<double>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.Property<long>("NumberOfParticipants");

                    b.Property<long?>("SellerId");

                    b.Property<long?>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Auga.DAO.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<string>("Email");

                    b.Property<DateTime?>("Modified");

                    b.Property<long?>("UpdatedBy");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Auga.DAO.Entities.Item", b =>
                {
                    b.HasOne("Auga.DAO.Entities.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("Auga.DAO.Entities.User", "Seller")
                        .WithMany("Items")
                        .HasForeignKey("SellerId");
                });
#pragma warning restore 612, 618
        }
    }
}
