using System;

    namespace MercanciaSegura.RestAPI.Models.Poliza
    {
        public class PolizaResponse
        {
            public int PolizaId { get; set; }

            public int? ProductoId { get; set; }
            public string Producto { get; set; }

            public int? ContratanteId { get; set; }
            public string Contratante { get; set; }

            public int? AseguradoraId { get; set; }
            public string Aseguradora { get; set; }

            public int? SubRamoId { get; set; }
            public string SubRamo { get; set; }

            public int? MonedaId { get; set; }
            public string Moneda { get; set; }

            public int? FormaPagoId { get; set; }
            public string FormaPago { get; set; }

            public bool? Estatus { get; set; }
            public string TipoPoliza { get; set; }
            public string NumeroPoliza { get; set; }

            public DateTime? VigenciaDel { get; set; }
            public DateTime? VigenciaHasta { get; set; }

            public string OtrosPoliza { get; set; }

            public decimal? PrimaNeta { get; set; }
            public decimal? DerechoPoliza { get; set; }
            public decimal? Otros { get; set; }
            public decimal? IVA { get; set; }
            public decimal? PrimaTotal { get; set; }

            public DateTime FechaRegistro { get; set; }
            public DateTime? FechaActualizacion { get; set; }
        }
    }
