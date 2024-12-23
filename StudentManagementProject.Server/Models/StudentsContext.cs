using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RankingApp.Server.Models;

public partial class StudentsContext : DbContext
{
    public StudentsContext()
    {
    }

    public StudentsContext(DbContextOptions<StudentsContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=Students;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        //modelBuilder.Entity<Sale>(entity =>
        //{
        //    entity.HasKey(e => e.SaleId).HasName("PK__sales__E1EB00B23AE3917E");

        //    entity.ToTable("sales");

        //    entity.Property(e => e.SaleId)
        //        .ValueGeneratedNever()
        //        .HasColumnName("sale_id");
        //    entity.Property(e => e.Amount)
        //        .HasColumnType("decimal(10, 2)")
        //        .HasColumnName("amount");
        //    entity.Property(e => e.SaleDate).HasColumnName("sale_date");
        //});

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC078BB10685");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
