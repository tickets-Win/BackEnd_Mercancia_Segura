using MercanciaSegura.DOM.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.DOM.ApplicationDbContext;

public class ServiceDbContext : DbContext
{
    
    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
    }

    //public DbSet<Cliente> Cliente { get; set; }   

    public DbSet<TipoVendedor> TipoVendedor { get; set; }

    public DbSet<Vendedor> Vendedor { get; set; }

    public DbSet<TipoSeguro> TipoSeguro { get; set; }

    public DbSet<OrigenCliente> OrigenCliente { get; set; }

    public DbSet<TipoCuenta> TipoCuenta { get; set; }

    public DbSet<TipoSector> TipoSector { get; set; }

    public DbSet<RegimenFiscal> RegimenFiscal { get; set; }

    public DbSet<BeneficiarioPreferente> BeneficiarioPreferente { get; set; }

    public DbSet<Cliente> Cliente { get; set; }

    public DbSet<Usuario> Usuario { get; set; }


}