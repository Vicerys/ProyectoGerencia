using Gerencia.Core.Repositorios.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        private readonly string _connectionString;
        public AppDbContext()
        {
            _connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=ProyectoFinal2; Trusted_Connection=True; TrustServerCertificate=True;MultipleActiveResultSets=true";
        }

        public DbSet<ProcesoEntidad> Procesos => Set<ProcesoEntidad>();
        public DbSet<EmpleadoEntidad> Empleados => Set<EmpleadoEntidad>();
        public DbSet<ProyectoEntidad> Proyectos => Set<ProyectoEntidad>();
        public DbSet<EmpleadoProyectoEntidad> EmpleadoProyectos => Set<EmpleadoProyectoEntidad>();
        public DbSet<EmpleadoProyectoDetalleEntidad> EmpleadoProyectoDetalles => Set<EmpleadoProyectoDetalleEntidad>();
        public DbSet<TipoPuesto> TipoPuestos => Set<TipoPuesto>();
        public DbSet<Estado> Estados => Set<Estado>();
        public DbSet<Disponibilidad> Disponibilidades => Set<Disponibilidad>();
        public DbSet<Importancia> Importancias => Set<Importancia>();
        public DbSet<TareaEntidad> Tareas => Set<TareaEntidad>();
        public DbSet<EquipoEntidad> Equipos => Set<EquipoEntidad>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var charToString = new ValueConverter<char, string>(
                v => v.ToString(),
                v => string.IsNullOrEmpty(v) ? ' ' : v[0]);

            modelBuilder.Entity<EmpleadoEntidad>(entity =>
            {
                entity.ToTable("empleado");


                entity.HasKey(e => e.EmpleadoId);
                entity.Property(e => e.EmpleadoId)
                      .ValueGeneratedOnAdd(); // IDENTITY(1,1)

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false) // varchar(50)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.PuestoId)
                      .IsRequired()
                      .HasColumnType("Int");
                /*
                // Configuración del discriminador (TPH)
                entity.HasDiscriminator<int>("PuestoId")
                      .HasValue<Gerente>(1)
                      .HasValue<Analista>(2)
                      .HasValue<Tecnico>(3);*/

                /*entity.Property(e => e.Edad)
                      .HasColumnType("int")
                      .IsRequired();*/

                entity.Property(e => e.Ubicacion)
                      .HasColumnType("varchar(100)")
                      .IsRequired();

                entity.Property(e => e.Foto)
                      .HasColumnType("varbinary(max)");

                entity.Property(e => e.Usuario)
                      .HasColumnType("varchar(100)")
                      .IsRequired();

                entity.Property(e => e.Contrasena)
                      .HasColumnType("varchar(200)")
                      .IsRequired();

                entity.Property(e => e.FechaNacimiento)
                      .HasColumnType("date") // explícito
                      .IsRequired();

            });

            //Proyecto
            modelBuilder.Entity<ProyectoEntidad>(e =>
            {
                e.ToTable("Proyecto");
                e.HasKey(p => p.ProyectoId);
                e.Property(p => p.ProyectoId).ValueGeneratedOnAdd();
                e.Property(p => p.Nombre).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnType("varchar(100)");
                e.Property(p => p.FechaLimite).HasColumnType("datetime");

            });

            modelBuilder.Entity<EmpleadoProyectoEntidad>(e =>
            {
                e.ToTable("EmpleadoProyecto");
                e.HasOne(p => p.Empleado).WithMany().HasForeignKey(p => p.EmpleadoId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmpleadoProyectoDetalleEntidad>(e =>
            {
                e.ToTable("EmpleadoProyectoDetalle");
                e.HasKey(d => new { d.EmpleadoProyectoId, d.Linea });
                e.HasOne(d => d.EmpleadoProyecto).WithMany(p => p.Detalles).HasForeignKey(d => d.EmpleadoProyectoId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne<ProyectoEntidad>().WithMany().HasForeignKey(d => d.ProyectoID).OnDelete(DeleteBehavior.Restrict);
            });

            // Proceso
            modelBuilder.Entity<ProcesoEntidad>(e =>
            {
                e.ToTable("Proceso");
                e.HasKey(p => p.ProcesoId);
                e.Property(p => p.ProcesoId).ValueGeneratedOnAdd();
                e.Property(p => p.Nombre).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnType("varchar(100)");
                e.Property(p => p.Entregables).HasMaxLength(100).IsUnicode(false).HasColumnType("varchar(100)");
                e.Property(p => p.FechaTerminoEstimada).HasColumnType("datetime");
                e.Property(p => p.FechaTerminoReal).HasColumnType("datetime");
                e.Property(e => e.ProyectoId)
                     .IsRequired()
                     .HasColumnType("Int");
                e.Property(e => e.ImportanciaId)
                     .IsRequired()
                     .HasColumnType("Int");
                e.Property(e => e.EstadoId)
                     .IsRequired()
                     .HasColumnType("Int");
            });

            //Tarea
            modelBuilder.Entity<TareaEntidad>(e =>
            {
                e.ToTable("Tarea");
                e.HasKey(t => t.TareaId);
                e.Property(t => t.TareaId).ValueGeneratedOnAdd();
                e.Property(t => t.Nombre).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnType("varchar(100)");
                e.Property(t => t.Descripcion).HasMaxLength(500).IsUnicode(false).HasColumnType("varchar(500)");
                e.Property(e => e.ProcesoId)
                     .IsRequired()
                     .HasColumnType("Int");
                e.Property(e => e.EstadoId)
                     .IsRequired()
                     .HasColumnType("Int");
            });

            //Equipo
            modelBuilder.Entity<EquipoEntidad>(e =>
            {
                e.ToTable("Equipo");
                e.HasKey(t => t.EquipoId);
                e.Property(t => t.EquipoId).ValueGeneratedOnAdd();
                e.Property(t => t.Nombre).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnType("varchar(100)");
                e.Property(t => t.Ubicacion).HasMaxLength(500).IsUnicode(false).HasColumnType("varchar(500)");
                e.Property(e => e.Foto).HasMaxLength(500).IsUnicode(false).HasColumnType("varchar(500)");
                e.Property(e => e.Especificaciones).HasMaxLength(500).IsUnicode(false).HasColumnType("varchar(500)");
                e.Property(e => e.DisponibilidadId)
                     .IsRequired()
                     .HasColumnType("Int");
                e.Property(e => e.Mantenimiento).HasColumnType("Int");

            });

            // Catálogos renombrados
            modelBuilder.Entity<TipoPuesto>(e =>
            {
                e.ToTable("Puesto");
                e.HasKey(x => x.PuestoId);
                e.Property(x => x.Nombre).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnType("varchar(50)");
                e.HasData(
                    new TipoPuesto { PuestoId = 1, Nombre = "Gerente" },
                    new TipoPuesto { PuestoId = 2, Nombre = "Análista" },
                    new TipoPuesto { PuestoId = 3, Nombre = "Técnico" }
                );
            });

            modelBuilder.Entity<Estado>(e =>
            {
                e.ToTable("Estado");
                e.HasKey(x => x.EstadoId);
                e.Property(x => x.Nombre).IsRequired().HasMaxLength(20).IsUnicode(false).HasColumnType("varchar(20)");
                e.HasData(
                    new Estado { EstadoId = 1, Nombre = "Incompleto" },
                    new Estado { EstadoId = 2, Nombre = "En curso" },
                    new Estado { EstadoId = 3, Nombre = "Completado" }
                );
            });

            modelBuilder.Entity<Disponibilidad>(e =>
            {
                e.ToTable("Disponibilidad");
                e.HasKey(x => x.DisponibilidadId);
                e.Property(x => x.Nombre).IsRequired().HasMaxLength(20).IsUnicode(false).HasColumnType("varchar(20)");
                e.HasData(
                    new Disponibilidad { DisponibilidadId = 1, Nombre = "Disponible" },
                    new Disponibilidad { DisponibilidadId = 2, Nombre = "Ocupado" },
                    new Disponibilidad { DisponibilidadId = 3, Nombre = "Mantenimiento" }
                );
            });

            modelBuilder.Entity<Importancia>(e =>
            {
                e.ToTable("Importancia");
                e.HasKey(x => x.ImportanciaId);
                e.Property(x => x.Nombre).IsRequired().HasMaxLength(20).IsUnicode(false).HasColumnType("varchar(20)");
                e.HasData(
                    new Importancia { ImportanciaId = 1, Nombre = "Baja" },
                    new Importancia { ImportanciaId = 2, Nombre = "Media" },
                    new Importancia { ImportanciaId = 3, Nombre = "Alta" },
                    new Importancia { ImportanciaId = 4, Nombre = "Urgente" }
                );
            });

        }

    }
}