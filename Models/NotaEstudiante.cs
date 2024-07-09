using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proyectoscrum.Models;

public partial class NotaEstudiante
{
    [Display(Name = "ID Nota Estudiante")]
    public int IdNotaEst { get; set; }

    [Display(Name = "ID Usuario")]
    public int IdUsuario { get; set; }

    [Display(Name = "ID Materia")]
    public int IdMateria { get; set; }

    [Display(Name = "Rúbrica")]
    public string? Rubica { get; set; }

    [Display(Name = "Puntaje Obtenido")]
    public double? PuntajeObtenido { get; set; }

    [Display(Name = "ID Materia")]
    public virtual Materia IdMateriaNavigation { get; set; } = null!;

    [Display(Name = "ID Usuario")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
