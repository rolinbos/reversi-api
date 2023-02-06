﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReversiApi.Data;

#nullable disable

namespace ReversiApi.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.13");

            modelBuilder.Entity("ReversiApi.Models.Spel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AandeBeurt")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BordAsString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Speler1Read")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Speler1Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Speler2Read")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Speler2Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Spels");
                });

            modelBuilder.Entity("ReversiApi.Models.SpelGegevens", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("datum")
                        .HasColumnType("TEXT");

                    b.Property<string>("spelToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("spelerToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("waarde")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("SpelGegevens");
                });
#pragma warning restore 612, 618
        }
    }
}
