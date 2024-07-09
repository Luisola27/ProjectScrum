using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class MateriasProfesor
{
    public int IdMateriaProfesor { get; set; }

    public int IdProfesor { get; set; }

    public int IdMateria { get; set; }

    public virtual Materia IdMateriaNavigation { get; set; } = null!;

    public virtual Profesore IdProfesorNavigation { get; set; } = null!;
}
