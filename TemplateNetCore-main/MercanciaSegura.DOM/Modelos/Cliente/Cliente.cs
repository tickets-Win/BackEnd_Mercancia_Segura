using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MercanciaSegura.DOM.Modelos.Cliente
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("Cliente_ID")]
        public int ClienteId { get; set; }

        [Column("Tipo_Seguro_ID")]
        public int? TipoSeguroId { get; set; }

        [Column("Origen_Cliente_ID")]
        public int? OrigenClienteId { get; set; }

        [Column("Tipo_Cuenta_ID")]
        public int? TipoCuentaId { get; set; }

        [Column("Tipo_Sector_ID")]
        public int? TipoSectorId { get; set; }

        [Column("Regimen_Fiscal_ID")]
        public int? RegimenFiscalId { get; set; }

        [Column("Tipo_Persona_ID")]
        public int? TipoPersonaId { get; set; }

        [Column("RFC")]
        [MaxLength(13)]
        public string? Rfc { get; set; }

        [Column("RfcGenerico_ID")]
        public int? RfcGenericoId { get; set; }

        [Column("Telefono")]
        [MaxLength(13)]
        public string? Telefono { get; set; }

        [Column("Correo_Electronico")]
        [MaxLength(200)]
        public string? CorreoElectronico { get; set; }

        [Column("Nacionalidad")]
        [MaxLength(30)]
        public string? Nacionalidad { get; set; }

        [Column("Calle")]
        [MaxLength(120)]
        public string? Calle { get; set; }

        [Column("Numero_Int")]
        [MaxLength(10)]
        public string? NumeroInt { get; set; }

        [Column("Numero_Ext")]
        [MaxLength(10)]
        public string? NumeroExt { get; set; }

        [Column("Pais")]
        [MaxLength(50)]
        public string? Pais { get; set; }

        [Column("Municipio")]
        [MaxLength(80)]
        public string? Municipio { get; set; }

        [Column("Poblacion")]
        [MaxLength(80)]
        public string? Poblacion { get; set; }

        [Column("Colonia")]
        [MaxLength(60)]
        public string? Colonia { get; set; }

        [Column("Estado")]
        [MaxLength(20)]
        public string? Estado { get; set; }

        [Column("CP")]
        [StringLength(5)]
        public string? Cp { get; set; }

        [Column("Apellido_Paterno")]
        [MaxLength(60)]
        public string? ApellidoPaterno { get; set; }

        [Column("Apellido_Materno")]
        [MaxLength(60)]
        public string? ApellidoMaterno { get; set; }

        [Column("Nombres")]
        [MaxLength(60)]
        public string? Nombres { get; set; }

        [Column("Nombre_Completo")]
        [MaxLength(120)]
        public string? NombreCompleto { get; set; }

        [Column("Cuota_Minima_Internacional", TypeName = "decimal(10,2)")]
        public decimal? CuotaMinimaInternacional { get; set; }

        [Column("Cuota_Minima_Nacional", TypeName = "decimal(10,2)")]
        public decimal? CuotaMinimaNacional { get; set; }

        [Column("Cuota_Aplicable_Internacional", TypeName = "decimal(10,2)")]
        public decimal? CuotaAplicableInternacional { get; set; }

        [Column("Cuota_Aplicable_Nacional", TypeName = "decimal(10,2)")]
        public decimal? CuotaAplicableNacional { get; set; }

        [Column("Fecha_Actualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        [Column("Fecha_Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Column("Tipo_Estatus_ID")]
        public int? EstatusId { get; set; }

        [Column("Fecha_Baja")]
        public DateTime? FechaBaja { get; set; }

        [Column("Clave")]
        [MaxLength(10)]
        public string? Clave { get; set; }

        [Column("Genero")]
        [StringLength(1)]
        public string? Genero { get; set; }

        //Llaves foraneas

        [ForeignKey(nameof(TipoSeguroId))]
        public TipoSeguro? TipoSeguro { get; set; }

        [ForeignKey(nameof(OrigenClienteId))]
        public OrigenCliente? OrigenCliente { get; set; }

        [ForeignKey(nameof(TipoCuentaId))]
        public TipoCuenta? TipoCuenta { get; set; }

        [ForeignKey(nameof(TipoSectorId))]
        public TipoSector? TipoSector { get; set; }

        [ForeignKey(nameof(RegimenFiscalId))]
        public RegimenFiscal? RegimenFiscal { get; set; }

        [ForeignKey(nameof(EstatusId))]
        public TipoEstatus? TipoEstatus { get; set; }

        [ForeignKey(nameof(TipoPersonaId))]
        public TipoPersona? TipoPersona { get; set; }

        [ForeignKey(nameof(RfcGenericoId))]
        public RfcGenerico? RfcGenerico { get; set; }

        public ICollection<ClienteBeneficiario> ClienteBeneficiario { get; set; } = new List<ClienteBeneficiario>();

        public ClienteCredito? ClienteCredito { get; set; }

        public ICollection<Correos> Correos { get; set; } = new List<Correos>();

        public ICollection<Cuota> Cuota { get; set; } = new List<Cuota>();

        public ICollection<ClienteVendedor> ClienteVendedor { get; set; } = new List<ClienteVendedor>();



    }
}
