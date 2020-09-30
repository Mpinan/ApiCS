using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Recipe.Models
{
    public partial class recipesContext : DbContext
    {
        public recipesContext()
        {
        }

        public recipesContext(DbContextOptions<recipesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<RecipeTable> RecipeTables { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\mssqllocaldb;Initial Catalog = recipes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PK__Ingredie__BEAEB25AF59F733E");

                entity.Property(e => e.IngredientId).ValueGeneratedOnAdd();

                entity.Property(e => e.IngredientAmount)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IngredientName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.RecipeTable)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.RecipeTableId)
                    .HasConstraintName("FK__Ingredien__Recip__286302EC");
            });

            modelBuilder.Entity<RecipeTable>(entity =>
            {
                entity.HasKey(e => e.RecipeTableId)
                    .HasName("PK__Recipes__FDD988B02F32FFD7");

                entity.Property(e => e.RecipeTableId).ValueGeneratedOnAdd();

                entity.Property(e => e.RecipeDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RecipeName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RecipeTables)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Recipes__UserId__25869641");
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.HasKey(e => e.StepId)
                    .HasName("PK__Steps__2434335752018F14");

                entity.Property(e => e.StepId).ValueGeneratedOnAdd();

                entity.Property(e => e.StepDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.RecipeTable)
                    .WithMany(p => p.Steps)
                    .HasForeignKey(d => d.RecipeTableId)
                    .HasConstraintName("FK__Steps__RecipeId__2B3F6F97");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4CB50B52E6");

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
