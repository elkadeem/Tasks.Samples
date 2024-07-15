﻿// <auto-generated />
using System;
using CodeFirst.Sample.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodeFirst.Sample.AnimalsMigrationsTPC
{
    [DbContext(typeof(AnimalsDbContextTPC))]
    [Migration("20240715094411_AddAnimals")]
    partial class AddAnimals
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("AnimalSequence");

            modelBuilder.Entity("CodeFirst.Sample.Entities.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [AnimalSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<int?>("Food")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("HumanPet", b =>
                {
                    b.Property<int>("HumansId")
                        .HasColumnType("int");

                    b.Property<int>("PetsId")
                        .HasColumnType("int");

                    b.HasKey("HumansId", "PetsId");

                    b.HasIndex("PetsId");

                    b.ToTable("HumanPet");
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.FarmAnimal", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Animal");

                    b.Property<decimal>("Value")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("FarmAnimal");
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Human", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Animal");

                    b.Property<int?>("FavoriteAnimalId")
                        .HasColumnType("int");

                    b.HasIndex("FavoriteAnimalId");

                    b.ToTable("Humans");
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Pet", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Animal");

                    b.Property<string>("Vet")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Cat", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Pet");

                    b.Property<string>("EducationLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Cat");
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Dog", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Pet");

                    b.Property<string>("FavoriteToy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Dog");
                });

            modelBuilder.Entity("HumanPet", b =>
                {
                    b.HasOne("CodeFirst.Sample.Entities.Human", null)
                        .WithMany()
                        .HasForeignKey("HumansId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("CodeFirst.Sample.Entities.Pet", null)
                        .WithMany()
                        .HasForeignKey("PetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Human", b =>
                {
                    b.HasOne("CodeFirst.Sample.Entities.Animal", "FavoriteAnimal")
                        .WithMany()
                        .HasForeignKey("FavoriteAnimalId");

                    b.Navigation("FavoriteAnimal");
                });
#pragma warning restore 612, 618
        }
    }
}
