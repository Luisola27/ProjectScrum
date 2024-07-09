using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proyectoscrum.Models;

public partial class Bitacora
{
    [Display(Name = "ID Bitácora")]
    public int IdBitacora { get; set; }

    [Display(Name = "Acción")]
    public string? Accion { get; set; }

    [Display(Name = "Afectado")]
    public string? Afectado { get; set; }

    [Display(Name = "ID Usuario")]
    public int? IdUsuario { get; set; }

    [Display(Name = "Fecha y hora")]
    public DateTime? FechaHora { get; set; }

    [Display(Name = "ID Usuario")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
