﻿// <auto-generated />
using System;
using BelongeaBoulangerie.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BelongeaBoulangerie.DataContext.Migrations
{
    [DbContext(typeof(BoulangerieContext))]
    partial class BoulangerieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Bread", b =>
                {
                    b.Property<int>("BreadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BreadId"));

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BreadId");

                    b.HasIndex("CountryID");

                    b.ToTable("Breads");
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("CountryContinent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CulinaryHistory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<double?>("Grams")
                        .HasColumnType("float");

                    b.Property<string>("IngredientString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Quantity")
                        .HasColumnType("float");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Instruction", b =>
                {
                    b.Property<int>("InstructionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructionId"));

                    b.Property<string>("InstructionString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstructionId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<int>("BakeTime")
                        .HasColumnType("int");

                    b.Property<int>("BreadId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId");

                    b.HasIndex("BreadId")
                        .IsUnique();

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("EmailAddress");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasAlternateKey("Email");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BreadUser", b =>
                {
                    b.Property<int>("BreadsBreadId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("BreadsBreadId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("BreadUser");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.Property<int>("IngredientsIngredientId")
                        .HasColumnType("int");

                    b.Property<int>("RecipesRecipeId")
                        .HasColumnType("int");

                    b.HasKey("IngredientsIngredientId", "RecipesRecipeId");

                    b.HasIndex("RecipesRecipeId");

                    b.ToTable("IngredientRecipe");
                });

            modelBuilder.Entity("InstructionRecipe", b =>
                {
                    b.Property<int>("InstructionsInstructionId")
                        .HasColumnType("int");

                    b.Property<int>("RecipesRecipeId")
                        .HasColumnType("int");

                    b.HasKey("InstructionsInstructionId", "RecipesRecipeId");

                    b.HasIndex("RecipesRecipeId");

                    b.ToTable("InstructionRecipe");
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Bread", b =>
                {
                    b.HasOne("BelongeaBoulangerie.DataContext.Models.Country", "Country")
                        .WithMany("Breads")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Recipe", b =>
                {
                    b.HasOne("BelongeaBoulangerie.DataContext.Models.Bread", "Bread")
                        .WithOne("Recipe")
                        .HasForeignKey("BelongeaBoulangerie.DataContext.Models.Recipe", "BreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bread");
                });

            modelBuilder.Entity("BreadUser", b =>
                {
                    b.HasOne("BelongeaBoulangerie.DataContext.Models.Bread", null)
                        .WithMany()
                        .HasForeignKey("BreadsBreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BelongeaBoulangerie.DataContext.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.HasOne("BelongeaBoulangerie.DataContext.Models.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsIngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BelongeaBoulangerie.DataContext.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesRecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InstructionRecipe", b =>
                {
                    b.HasOne("BelongeaBoulangerie.DataContext.Models.Instruction", null)
                        .WithMany()
                        .HasForeignKey("InstructionsInstructionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BelongeaBoulangerie.DataContext.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesRecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Bread", b =>
                {
                    b.Navigation("Recipe")
                        .IsRequired();
                });

            modelBuilder.Entity("BelongeaBoulangerie.DataContext.Models.Country", b =>
                {
                    b.Navigation("Breads");
                });
#pragma warning restore 612, 618
        }
    }
}
