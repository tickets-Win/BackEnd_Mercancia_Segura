namespace MercanciaSegura.RestAPI.Models
{
    /// <summary>
    /// Tipo de cambio de una moneda. Solo se actualizan estos dos valores; el
    /// nombre de la moneda no se toca desde aquí.
    /// </summary>
    public class TipoCambioRequest
    {
        /// <summary>Publicado en el Diario Oficial de la Federación.</summary>
        public decimal? TipoCambio { get; set; }

        /// <summary>El bancario, o de ventanilla.</summary>
        public decimal? TipoCambioVentanilla { get; set; }
    }
}
