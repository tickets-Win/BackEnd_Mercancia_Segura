using System;

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

            public string? TipoPoliza { get; set; }
            public string? NumeroPoliza { get; set; }

            public DateTime? VigenciaDel { get; set; }
            public DateTime? VigenciaHasta { get; set; }

            public string? OtrosPoliza { get; set; }

            public decimal? PrimaNeta { get; set; }
            public decimal? DerechoPoliza { get; set; }
            public decimal? Otros { get; set; }
            public decimal? IVA { get; set; }
            public decimal? PrimaTotal { get; set; }
            public PolizaContenedorRequest? PolizaContenedor { get; set; }
            public PolizaMercanciaRequest? PolizaMercancia { get; set; }

    }
}
