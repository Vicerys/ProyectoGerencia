using Gerencia.Core.Repositorios.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Gerencia.Repositorios.SQL.Contexts
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

            modelBuilder.Entity<ProcesoEntidad>(entity =>
            {
                entity.ToTable("proceso");

                entity.HasKey(e => e.ProcesoId);
                entity.Property(e => e.ProcesoId)
                      .ValueGeneratedOnAdd(); // IDENTITY(1,1)

                entity.Property(e => e.ProyectoId)
                      .IsRequired()
                      .HasColumnType("Int");

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false) // varchar(50)
                      .HasColumnType("varchar(50)");

                entity.HasOne(p => p.Importancia)
                    .WithMany()
                    .HasForeignKey(p => p.ImportanciaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Entregables)
                      .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.FechaTerminoEstimada)
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.FechaTerminoReal)
                      .HasColumnType("date");

                entity.HasOne(p => p.Estado)
                    .WithMany()
                    .HasForeignKey(p => p.EstadoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Finalizado)
                      .IsRequired()
                      .HasColumnType("tinyint");

            });


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

                entity.Property(e => e.Edad)
                      .HasColumnType("int")
                      .IsRequired();

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