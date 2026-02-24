using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MercanciaSegura.RestAPI.Models.Poliza
{
    public class PolizaRequest
    {
        public int? ProductoId { get; set; }
        public int? ContratanteId { get; set; }
        public int? AseguradoraId { get; set; }
        public int? SubRamoId { get; set; }
        public int? MonedaId { get; set; }
        public int? FormaPagoId { get; set; }
        public int? EstatusPolizaId { get; set; }

        [MaxLength(30)]
        public string? NumeroPoliza { get; set; }

        [MaxLength(150)]
        public string? TipoPoliza { get; set; }

        [MaxLength(50)]
        public string? ClaveAgente { get; set; }

        [MaxLength(20)]
        public string? FolioPoliza { get; set; }

        public DateTime? VigenciaDel { get; set; }
        public DateTime? VigenciaHasta { get; set; }

        // Objetos hijos (relaciones)
        public PolizaContenedorRequest? PolizaContenedor { get; set; }
        public List<PolizaMercanciaRequest>? PolizaMercancia { get; set; }
        public List<BienRequest>? Bien { get; set; }
    }
}