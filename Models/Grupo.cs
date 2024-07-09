using System;
using System.Collections.Generic;

namespace proyectoscrum.Models;

public partial class Grupo
{
    public int IdGrupo { get; set; }

    public int? IdNivel { get; set; }

    public string? NombreGrupo { get; set; }

    public int? IdCentro { get; set; }

    public virtual ICollection<EstudiantesGrupo> EstudiantesGrupos { get; set; } = new List<EstudiantesGrupo>();

    public virtual CentrosEducativo? IdCentroNavigation { get; set; }

    public virtual Nivele? IdNivelNavigation { get; set; }

    public virtual ICollection<Profesore> Profesores { get; set; } = new List<Profesore>();
}
