using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class Profesore
{
    public int IdProfesor { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdGrupo { get; set; }

    public virtual Grupo? IdGrupoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<MateriasProfesor> MateriasProfesors { get; set; } = new List<MateriasProfesor>();
}
