using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class EstudiantesGrupo
{
    public int? IdUsuario { get; set; }

    public int? IdGrupo { get; set; }

    public int? IdNota { get; set; }

    public int? Notafinal { get; set; }

    public int IdEstudianteGrupo { get; set; }

    public virtual Grupo? IdGrupoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
