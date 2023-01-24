﻿// <auto-generated />
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

                    b.Property<string>("Speler1Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Speler2Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Spels");
                });
#pragma warning restore 612, 618
        }
    }
}
