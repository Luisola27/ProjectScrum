using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proyectoscrum.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdRol { get; set; }

    [Display(Name = "Identificación")]
    public int? Identificacion { get; set; }

    [Display(Name = "Nombre")]
    public string? Nombre { get; set; }

    [Display(Name = "Apellido1")]
    public string? Apellido1 { get; set; }

    [Display(Name = "Apellido2")]
    public string? Apellido2 { get; set; }

    [Display(Name = "Contraseña")]
    public string? Contrasenna { get; set; }

    [Display(Name = "Correo")]
    public string? Correo { get; set; }

    [Display(Name = "Telefono")]
    public string? Telefono { get; set; }

    [Display(Name = "Fecha de Nacimiento")]
    public DateOnly? FechaNacimiento { get; set; }

    public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();

    public virtual ICollection<EstudiantesGrupo> EstudiantesGrupos { get; set; } = new List<EstudiantesGrupo>();

    [Display(Name = "Rol ( 1 Administrador. 2 Profesor. 3 Estudiante.")]
    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<NotaEstudiante> NotaEstudiantes { get; set; } = new List<NotaEstudiante>();

    public virtual ICollection<Profesore> Profesores { get; set; } = new List<Profesore>();
}
