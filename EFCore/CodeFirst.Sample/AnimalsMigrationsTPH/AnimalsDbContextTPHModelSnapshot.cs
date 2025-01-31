﻿// <auto-generated />
using System;
using CodeFirst.Sample.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodeFirst.Sample.AnimalsMigrationsTPH
{
    [DbContext(typeof(AnimalsDbContextTPH))]
    partial class AnimalsDbContextTPHModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CodeFirst.Sample.Entities.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int?>("Food")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Animals");

                    b.HasDiscriminator().HasValue("Animal");

                    b.UseTphMappingStrategy();
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

                    b.HasDiscriminator().HasValue("FarmAnimal");
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Human", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Animal");

                    b.Property<int?>("FavoriteAnimalId")
                        .HasColumnType("int");

                    b.HasIndex("FavoriteAnimalId");

                    b.HasDiscriminator().HasValue("Human");
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Pet", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Animal");

                    b.Property<string>("Vet")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Pet");
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Cat", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Pet");

                    b.Property<string>("EducationLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Cat");
                });

            modelBuilder.Entity("CodeFirst.Sample.Entities.Dog", b =>
                {
                    b.HasBaseType("CodeFirst.Sample.Entities.Pet");

                    b.Property<string>("FavoriteToy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Dog");
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
