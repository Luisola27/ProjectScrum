using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class NotasEstudiante
{
    public string Nombre { get; set; } = null!;

    public string? NombreMateria { get; set; }

    public string? NombreGrupo { get; set; }

    public double? Nota { get; set; }
}
