using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GainsModel.TempModels;

public partial class GainsContext : DbContext
{
    private string _connectionString;
    public GainsContext()
    {
    }
    
    public GainsContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public GainsContext(DbContextOptions<GainsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Exerciseset> Exercisesets { get; set; }

    public virtual DbSet<Musclegroup> Musclegroups { get; set; }

    public virtual DbSet<Weightunitlookup> Weightunitlookups { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(_connectionString);
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Exerciseid).HasName("exercise_pkey");

            entity.ToTable("exercise", "lifting");

            entity.Property(e => e.Exerciseid).HasColumnName("exerciseid");
            entity.Property(e => e.Dateadded)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateadded");
            entity.Property(e => e.Dateupdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateupdated");
            entity.Property(e => e.Exercisename).HasColumnName("exercisename");
            entity.Property(e => e.Musclegroupid).HasColumnName("musclegroupid");
            entity.Property(e => e.Notes).HasColumnName("notes");

            entity.HasOne(d => d.Musclegroup).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.Musclegroupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exercise_musclegroupid_fkey");
        });

        modelBuilder.Entity<Exerciseset>(entity =>
        {
            entity.HasKey(e => e.Exercisesetid).HasName("exerciseset_pkey");

            entity.ToTable("exerciseset", "lifting");

            entity.Property(e => e.Exercisesetid).HasColumnName("exercisesetid");
            entity.Property(e => e.Dateadded)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateadded");
            entity.Property(e => e.Dateupdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateupdated");
            entity.Property(e => e.Estimatedrpe).HasColumnName("estimatedrpe");
            entity.Property(e => e.Exerciseid).HasColumnName("exerciseid");
            entity.Property(e => e.Repetitions).HasColumnName("repetitions");
            entity.Property(e => e.Weight).HasColumnName("weight");
            entity.Property(e => e.Weightunitlookupid).HasColumnName("weightunitlookupid");
            entity.Property(e => e.Workoutid).HasColumnName("workoutid");

            entity.HasOne(d => d.Exercise).WithMany(p => p.Exercisesets)
                .HasForeignKey(d => d.Exerciseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exerciseset_exerciseid_fkey");

            entity.HasOne(d => d.Weightunitlookup).WithMany(p => p.Exercisesets)
                .HasForeignKey(d => d.Weightunitlookupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exerciseset_weightunitlookupid_fkey");

            entity.HasOne(d => d.Workout).WithMany(p => p.Exercisesets)
                .HasForeignKey(d => d.Workoutid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("exerciseset_workoutid_fkey");
        });

        modelBuilder.Entity<Musclegroup>(entity =>
        {
            entity.HasKey(e => e.Musclegroupid).HasName("musclegroup_pkey");

            entity.ToTable("musclegroup", "lifting");

            entity.Property(e => e.Musclegroupid).HasColumnName("musclegroupid");
            entity.Property(e => e.Musclegroupname).HasColumnName("musclegroupname");
        });

        modelBuilder.Entity<Weightunitlookup>(entity =>
        {
            entity.HasKey(e => e.Weightunitlookupid).HasName("weightunitlookup_pkey");

            entity.ToTable("weightunitlookup", "lifting");

            entity.Property(e => e.Weightunitlookupid).HasColumnName("weightunitlookupid");
            entity.Property(e => e.Weightunitlabel).HasColumnName("weightunitlabel");
            entity.Property(e => e.Weightunitname).HasColumnName("weightunitname");
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.Workoutid).HasName("workout_pkey");

            entity.ToTable("workout", "lifting");

            entity.Property(e => e.Workoutid).HasColumnName("workoutid");
            entity.Property(e => e.Dateadded)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateadded");
            entity.Property(e => e.Datestarted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datestarted");
            entity.Property(e => e.Dateupdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateupdated");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
