using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.DOM.Modelos.Poliza;
using MercanciaSegura.DOM.Modelos.Vendedor;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.DOM.ApplicationDbContext;

public class ServiceDbContext : DbContext
{

    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
    }

    public DbSet<TipoVendedor> TipoVendedor { get; set; }

    public DbSet<Vendedor> Vendedor { get; set; }

    //------------------------------------------------------

    public DbSet<TipoSeguro> TipoSeguro { get; set; }

    public DbSet<OrigenCliente> OrigenCliente { get; set; }

    public DbSet<TipoCuenta> TipoCuenta { get; set; }

    public DbSet<TipoSector> TipoSector { get; set; }

    public DbSet<RegimenFiscal> RegimenFiscal { get; set; }

    public DbSet<BeneficiarioPreferente> BeneficiarioPreferente { get; set; }

    public DbSet<Cliente> Cliente { get; set; }

    public DbSet<RfcGenerico> RfcGenerico { get; set; }

    public DbSet<TipoEstatus> TipoEstatus { get; set; }

    public DbSet<TipoCorreo> TipoCorreo { get; set; }

    public DbSet<TipoCuota> TipoCuota { get; set; }

    public DbSet<TipoTarifa> TipoTarifa { get; set; }

    public DbSet<Correos> Correos { get; set; }

    public DbSet<Cuota> Cuota { get; set; }

    public DbSet<ClienteCredito> ClienteCredito { get; set; }

    public DbSet<ClienteBeneficiario> ClienteBeneficiario { get; set; }

    //----------------------------------------------------------

    public DbSet<Aseguradora> Aseguradora { get; set; }
    public DbSet<Cobertura> Cobertura { get; set; }
    public DbSet<Contratante> Contratante { get; set; }
    public DbSet<FormaPago> FormaPago { get; set; }
    public DbSet<Moneda> Moneda { get; set; }
    public DbSet<Poliza> Poliza { get; set; }
    public DbSet<PolizaContenedor> PolizaContenedor { get; set; }
    public DbSet<PolizaMercancia> PolizaMercancia { get; set; }
    public DbSet<Producto> Producto { get; set; }
    public DbSet<RiesgoCubierto> RiesgoCubierto { get; set; }
    public DbSet<TipoRiesgo> TipoRiesgo { get; set; }
    public DbSet<SubRamo> SubRamo { get; set; }
    public DbSet<EstatusPoliza> EstatusPoliza { get; set; }
    public DbSet<Bien> Bien { get; set; }
    public DbSet<TipoBien> TipoBien { get; set; }
    public DbSet<AdministracionBien> AdministracionBien { get; set; }

    //-------------------------------------------------------------
    public DbSet<TipoPersona> TipoPersona { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<ClienteVendedor> ClienteVendedor { get; set; }

}