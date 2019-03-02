﻿// <auto-generated />

using System;
using Auga.DAO.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auga.DAO.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181119191058_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackSide2.DAO.Entities.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("Img");

                    b.Property<bool>("IsPrivate");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.Property<long?>("PersonId");

                    b.Property<long?>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("BackSide2.DAO.Entities.BoardPin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BoardId");

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime?>("Modified");

                    b.Property<long?>("PinId");

                    b.Property<long?>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("PinId");

                    b.ToTable("BoardPin");
                });

            modelBuilder.Entity("BackSide2.DAO.Entities.ChatMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<string>("MessageContent");

                    b.Property<DateTime?>("Modified");

                    b.Property<long?>("ReceivedById");

                    b.Property<long?>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("ReceivedById");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("BackSide2.DAO.Entities.Seller", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool?>("Gender");

                    b.Property<long?>("Language");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<string>("Surname");

                    b.Property<long?>("UpdatedBy");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("BackSide2.DAO.Entities.Pin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long?>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("Img");

                    b.Property<string>("Link");

                    b.Property<DateTime?>("Modified");

                    b.Property<string>("Name");

                    b.Property<long?>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Pins");
                });

            modelBuilder.Entity("BackSide2.DAO.Entities.Item", b =>
                {
                    b.HasOne("BackSide2.DAO.Entities.Seller", "Seller")
                        .WithMany("Boards")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("BackSide2.DAO.Entities.BoardPin", b =>
                {
                    b.HasOne("BackSide2.DAO.Entities.Item", "Item")
                        .WithMany("BoardPins")
                        .HasForeignKey("BoardId");

                    b.HasOne("BackSide2.DAO.Entities.Pin", "Pin")
                        .WithMany("BoardPins")
                        .HasForeignKey("PinId");
                });

            modelBuilder.Entity("BackSide2.DAO.Entities.ChatMessage", b =>
                {
                    b.HasOne("BackSide2.DAO.Entities.Seller", "ReceivedBy")
                        .WithMany()
                        .HasForeignKey("ReceivedById");
                });
#pragma warning restore 612, 618
        }
    }
}
