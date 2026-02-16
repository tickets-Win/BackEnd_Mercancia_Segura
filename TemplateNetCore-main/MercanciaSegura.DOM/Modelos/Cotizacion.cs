using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercanciaSegura.DOM.Modelos
{
    [Table("Cotizacion")]
    public class Cotizacion
    {
        [Key]
        [Column("Cotizacion_ID")]
        public int CotizacionId { get; set; }

        [Column("Fecha_cotizacion")]
        public DateTime FechaCotizacion { get; set; }

        [Column("Cliente_ID")]
        public int ClienteId { get; set; }

        [Column("Poliza_ID")]
        public int PolizaId { get; set; }

        [Column("Moneda_ID")]
        public int MonedaId { get; set; }

        [Column("Cotizacion_Cliente")]
        [StringLength(100)]
        public string CotizacionCliente { get; set; }

        [Column("Transito")]
        [StringLength(50)]
        public string Transito { get; set; }

        [Column("Vigencia_Del")]
        public DateTime VigenciaDel { get; set; }

        [Column("Vigencia_Hasta")]
        public DateTime VigenciaHasta { get; set; }

        [Column("Clasificacion")]
        public string Clasificacion { get; set; }

        [Column("Sub_Clasificacion")]
        public string SubClasificacion { get; set; }

        [Column("Descripcion_Mercancia")]
        public string DescripcionMercancia { get; set; }

        [Column("Observaciones")]
        public string Observaciones { get; set; }

        [Column("Origen")]
        public string Origen { get; set; }

        [Column("Destino")]
        public string Destino { get; set; }

        [Column("Tipo_Empaque")]
        public string TipoEmpaque { get; set; }

        [Column("Medio_De_Transporte")]
        public string MedioDeTransporte { get; set; }

        [Column("Medio_De_Conduccion")]
        public string MedioDeConduccion { get; set; }

        [Column("Motivo_Cancelacion")]
        [StringLength(200)]
        public string MotivoCancelacion { get; set; }

        [Column("Cobertura_Id")]
        public string CoberturaId { get; set; }

        [Column("Suma_Asegurada")]
        public decimal SumaAsegurada { get; set; }

        [Column("Bienes_Asegurados")]
        public string BienesAsegurados { get; set; }

        [Column("Tamano_y_Tipo_Contenedor")]
        public string TamanoYTipoContenedor { get; set; }

        [Column("Numero_contenedor")]
        public string NumeroContenedor { get; set; }

        [Column("Opcion_Cuota")]
        public decimal OpcionCuota { get; set; }

        [Column("Medidas_de_seguridad_adicionales")]
        public string MedidasDeSeguridadAdicionales { get; set; }

        [Column("Deducibles")]
        public decimal Deducibles { get; set; }

        [Column("Cuota_aplicable")]
        public decimal CuotaAplicable { get; set; }

        [Column("Cuota_minima")]
        public decimal CuotaMinima { get; set; }

        [Column("Moneda_para_cotizar")]
        public string MonedaParaCotizar { get; set; }

        [Column("Tipo_Cambio_Cotizar")]
        public decimal TipoCambioCotizar { get; set; }

        [Column("Prima_servicio_de_aseguramiento")]
        public decimal PrimaServicioDeAseguramiento { get; set; }

        [Column("Gastos_De_Expedicion")]
        public decimal GastosDeExpedicion { get; set; }

        [Column("Subtotal")]
        public decimal Subtotal { get; set; }

        [Column("IVA")]
        public decimal IVA { get; set; }

        [Column("Total_Seguro_Mercancia")]
        public decimal TotalSeguroMercancia { get; set; }

        [Column("Total_Seguro_Contenedor")]
        public decimal TotalSeguroContenedor { get; set; }

        [Column("Total_a_Pagar")]
        public decimal TotalAPagar { get; set; }

        [Column("Cuota_Secos")]
        public decimal CuotaSecos { get; set; }

        [Column("Tarifa_Secos")]
        public string TarifaSecos { get; set; }

        [Column("Cuota_Refrigerados")]
        public decimal CuotaRefrigerados { get; set; }

        [Column("Tarifa_Refrigerados")]
        public string TarifaRefrigerados { get; set; }

        [Column("Cuota_Isotanques")]
        public decimal CuotaIsotanques { get; set; }

        [Column("Tarifa_Isotanques")]
        public string TarifaIsotanques { get; set; }

        [Column("Total_Tarifa")]
        public decimal TotalTarifa { get; set; }
    }
}
