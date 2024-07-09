using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace proyectoscrum.Models;

public partial class ProtectoScrumMepContext : DbContext
{
    public ProtectoScrumMepContext()
    {
    }

    public ProtectoScrumMepContext(DbContextOptions<ProtectoScrumMepContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bitacora> Bitacoras { get; set; }

    public virtual DbSet<Cantone> Cantones { get; set; }

    public virtual DbSet<CentrosEducativo> CentrosEducativos { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<EstudiantesGrupo> EstudiantesGrupos { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<MateriasProfesor> MateriasProfesors { get; set; }

    public virtual DbSet<Nivele> Niveles { get; set; }

    public virtual DbSet<NotaEstudiante> NotaEstudiantes { get; set; }

    public virtual DbSet<NotasEstudiante> NotasEstudiantes { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TipoCentro> TipoCentros { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => e.IdBitacora).HasName("PK__bitacora__7E4268B06559C124");

            entity.ToTable("bitacora");

            entity.Property(e => e.IdBitacora)
                .ValueGeneratedNever()
                .HasColumnName("id_bitacora");
            entity.Property(e => e.Accion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("accion");
            entity.Property(e => e.Afectado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("afectado");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Bitacoras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_bitacora_Usuarios");
        });

        modelBuilder.Entity<Cantone>(entity =>
        {
            entity.HasKey(e => new { e.IdCanton, e.IdProvincia }).HasName("PK_canton_provincia");

            entity.ToTable("cantones");

            entity.Property(e => e.IdCanton)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_canton");
            entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
            entity.Property(e => e.NombreCanton)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_canton");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Cantones)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cantones_provincias");
        });

        modelBuilder.Entity<CentrosEducativo>(entity =>
        {
            entity.HasKey(e => e.IdCentro).HasName("PK__centros___7197C04FBD40489D");

            entity.ToTable("centros_educativos");

            entity.Property(e => e.IdCentro).HasColumnName("id_centro");
            entity.Property(e => e.IdCanton).HasColumnName("id_canton");
            entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");
            entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
            entity.Property(e => e.IdTipo).HasColumnName("id_tipo");
            entity.Property(e => e.NombreCentro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_centro");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.CentrosEducativos)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK_centros_educativos_provincias");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.CentrosEducativos)
                .HasForeignKey(d => d.IdTipo)
                .HasConstraintName("FK_centros_educativos_tipo_centro");

            entity.HasOne(d => d.Cantone).WithMany(p => p.CentrosEducativos)
                .HasForeignKey(d => new { d.IdCanton, d.IdProvincia })
                .HasConstraintName("FK_centros_educativos_cantones");

            entity.HasOne(d => d.Distrito).WithMany(p => p.CentrosEducativos)
                .HasForeignKey(d => new { d.IdDistrito, d.IdCanton, d.IdProvincia })
                .HasConstraintName("FK_centros_educativos_distritos");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => new { e.IdDistrito, e.IdCanton, e.IdProvincia }).HasName("PK_prov_cant_dist");

            entity.ToTable("distritos");

            entity.Property(e => e.IdDistrito)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_distrito");
            entity.Property(e => e.IdCanton).HasColumnName("id_canton");
            entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
            entity.Property(e => e.NombreDistrito)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_distrito");

            entity.HasOne(d => d.Cantone).WithMany(p => p.Distritos)
                .HasForeignKey(d => new { d.IdCanton, d.IdProvincia })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_distritos_cantones");
        });

        modelBuilder.Entity<EstudiantesGrupo>(entity =>
        {
            entity.HasKey(e => e.IdEstudianteGrupo).HasName("PK__estudian__88D38805578AE5B7");

            entity.ToTable("estudiantes_grupo");

            entity.Property(e => e.IdEstudianteGrupo).HasColumnName("id_estudiante_grupo");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.IdNota).HasColumnName("id_nota");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Notafinal).HasColumnName("notafinal");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.EstudiantesGrupos)
                .HasForeignKey(d => d.IdGrupo)
                .HasConstraintName("FK_estudiantes_grupo_grupos");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.EstudiantesGrupos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_estudiantes_grupo_Usuarios");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PK__grupos__8B68D6880AF23BEA");

            entity.ToTable("grupos");

            entity.Property(e => e.IdGrupo)
                .ValueGeneratedNever()
                .HasColumnName("id_grupo");
            entity.Property(e => e.IdCentro).HasColumnName("id_centro");
            entity.Property(e => e.IdNivel).HasColumnName("id_nivel");
            entity.Property(e => e.NombreGrupo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre_grupo");

            entity.HasOne(d => d.IdCentroNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdCentro)
                .HasConstraintName("FK_grupos_centros_educativos");

            entity.HasOne(d => d.IdNivelNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.IdNivel)
                .HasConstraintName("FK_grupos_niveles");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__materias__7E03FD399E4CD2F7");

            entity.ToTable("materias");

            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.NombreMateria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_materia");
        });

        modelBuilder.Entity<MateriasProfesor>(entity =>
        {
            entity.HasKey(e => e.IdMateriaProfesor);

            entity.ToTable("materias_profesor");

            entity.Property(e => e.IdMateriaProfesor)
                .ValueGeneratedNever()
                .HasColumnName("id_materia_profesor");
            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.MateriasProfesors)
                .HasForeignKey(d => d.IdMateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_materias_profesor_materias");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.MateriasProfesors)
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_materias_profesor_profesores");
        });

        modelBuilder.Entity<Nivele>(entity =>
        {
            entity.HasKey(e => e.IdNivel).HasName("PK__niveles__9CAF1C53B759688E");

            entity.ToTable("niveles");

            entity.Property(e => e.IdNivel).HasColumnName("id_nivel");
            entity.Property(e => e.NombreNivel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_nivel");
        });

        modelBuilder.Entity<NotaEstudiante>(entity =>
        {
            entity.HasKey(e => e.IdNotaEst).HasName("PK__nota_est__465420C473C2330E");

            entity.ToTable("nota_estudiante");

            entity.Property(e => e.IdNotaEst).HasColumnName("id_nota_est");
            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.PuntajeObtenido).HasColumnName("puntaje_obtenido");
            entity.Property(e => e.Rubica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rubica");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.NotaEstudiantes)
                .HasForeignKey(d => d.IdMateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_nota_estudiante_materias");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.NotaEstudiantes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_nota_estudiante_Usuarios");
        });

        modelBuilder.Entity<NotasEstudiante>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Notas_Estudiantes");

            entity.Property(e => e.Nombre)
                .HasMaxLength(152)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreGrupo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre_grupo");
            entity.Property(e => e.NombreMateria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_materia");
            entity.Property(e => e.Nota).HasColumnName("nota");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PK__profesor__159ED6178A0DD50D");

            entity.ToTable("profesores");

            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");
            entity.Property(e => e.IdGrupo).HasColumnName("id_grupo");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.IdGrupo)
                .HasConstraintName("FK_profesores_grupos");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_profesores_Usuarios");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK__provinci__66C18BFD564452F3");

            entity.ToTable("provincias");

            entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre_provincia");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__6ABCB5E0D62643B7");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("id_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<TipoCentro>(entity =>
        {
            entity.HasKey(e => e.IdTipo).HasName("PK__tipo_cen__CF90108939D6F7C1");

            entity.ToTable("tipo_centro");

            entity.Property(e => e.IdTipo)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__4E3E04ADF1FD77EC");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido1");
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido2");
            entity.Property(e => e.Contrasenna)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasenna");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Identificacion).HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_Usuario_Roles");
        });

        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    //LK
    public async Task<List<NotasEstudiante>> GetNotasEstudianteAsync(int usuarioId)
    {
        return await NotasEstudiantes.FromSqlRaw("EXECUTE dbo.GET_NOTA_EST {usuarioId}").ToListAsync();
    }
}
 