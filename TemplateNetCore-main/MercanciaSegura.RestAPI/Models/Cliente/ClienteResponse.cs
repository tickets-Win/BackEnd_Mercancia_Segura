using System;
using System.Collections.Generic;

namespace MercanciaSegura.RestAPI.Models.Cliente
{
    public class ClienteResponse
    {
        public int ClienteId { get; set; }

        public string Rfc { get; set; }
        public int? RfcGenericoId { get; set; }

        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nacionalidad { get; set; }

        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string NombreCompleto { get; set; }

        public string Calle { get; set; }
        public string NumeroInt { get; set; }
        public string NumeroExt { get; set; }
        public string Pais { get; set; }
        public string Municipio { get; set; }
        public string Poblacion { get; set; }
        public string Colonia { get; set; }
        public string Estado { get; set; }
        public string Cp { get; set; }

        public int? TipoSeguroId { get; set; }
        public int? OrigenClienteId { get; set; }
        public int? TipoCuentaId { get; set; }
        public int? TipoSectorId { get; set; }
        public int? RegimenFiscalId { get; set; }
        public int? TipoPersonaId { get; set; }
        public int? EstatusId { get; set; }

        public decimal? CuotaMinimaInternacional { get; set; }
        public decimal? CuotaMinimaNacional { get; set; }
        public decimal? CuotaAplicableInternacional { get; set; }
        public decimal? CuotaAplicableNacional { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public DateTime? FechaBaja { get; set; }

        public string Clave { get; set; }

        public string Genero { get; set; }

        public List<BeneficiarioPreferenteResponse> BeneficiarioPreferente { get; set; } = new();
        public List<CorreoResponse> Correos { get; set; } = new();
        public List<CuotaResponse> Cuota { get; set; } = new();
        public List<ClienteVendedorResponse> ClienteVendedor { get; set; } = new();
        public ClienteCreditoResponse ClienteCredito { get; set; }


    }
}
