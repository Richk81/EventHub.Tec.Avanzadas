using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Tec.Avanzadas.Models;

public partial class EventHubDbContext : DbContext
{
    public EventHubDbContext()
    {
    }

    public EventHubDbContext(DbContextOptions<EventHubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistente> Asistentes { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asistent__3214EC07DB34176F");

            entity.HasIndex(e => e.EventoId, "IX_Asistentes_EventoId");

            entity.HasIndex(e => e.Nombre, "IX_Asistentes_Nombre");

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.RegistradoEn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.Evento).WithMany(p => p.Asistentes)
                .HasForeignKey(d => d.EventoId)
                .HasConstraintName("FK_Asistentes_Eventos");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3214EC07414FBC88");

            entity.HasIndex(e => e.EventoId, "IX_Comentarios_EventoId");

            entity.HasIndex(e => e.FechaCreacion, "IX_Comentarios_Fecha");

            entity.Property(e => e.Autor).HasMaxLength(150);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Texto).HasMaxLength(1000);

            entity.HasOne(d => d.Evento).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.EventoId)
                .HasConstraintName("FK_Comentarios_Eventos");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Eventos__3214EC070122343A");

            entity.HasIndex(e => e.Fecha, "IX_Eventos_Fecha");

            entity.HasIndex(e => e.Ubicacion, "IX_Eventos_Ubicacion");

            entity.Property(e => e.CreadoEn).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Ubicacion).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
