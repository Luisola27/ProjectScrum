using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proyectoscrum.Models;

public partial class Materia
{
    [Display(Name = "ID Materia")]
    public int IdMateria { get; set; }

    [Display(Name = "Nombre Materia")]
    public string? NombreMateria { get; set; }

    [Display(Name = "Materias Profesores")]
    public virtual ICollection<MateriasProfesor> MateriasProfesors { get; set; } = new List<MateriasProfesor>();

    [Display(Name = "Nota Estudiantes")]
    public virtual ICollection<NotaEstudiante> NotaEstudiantes { get; set; } = new List<NotaEstudiante>();
}
