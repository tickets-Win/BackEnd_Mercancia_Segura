namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class BeneficiarioPreferenteRequest
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public string Domicilio { get; set; }
        public string RFC { get; set; }
        public string Clave { get; set; }

        public int? TipoPersonaId { get; set; }
        public int? RfcGenericoId { get; set; }

        public string Calle { get; set; }
        public string NumeroInt { get; set; }
        public string NumeroExt { get; set; }
        public string Poblacion { get; set; }
        public string Colonia { get; set; }
        public string Cp { get; set; }
        public string Pais { get; set; }
        public string Nacionalidad { get; set; }
    }
}