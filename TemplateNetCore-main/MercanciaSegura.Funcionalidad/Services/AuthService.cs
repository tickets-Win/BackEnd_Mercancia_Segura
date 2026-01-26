using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.Funcionalidad.Services
{
    public class AuthService
    {
        private readonly ServiceDbContext _context;

        public AuthService(ServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> LoginAsync(string correo, string password)
        {
            // Busca el usuario activo con usuario + password
            return await _context.Usuario
                .FirstOrDefaultAsync(u =>
                    u.Correo == correo &&
                    u.Password == password &&
                    u.Estatus);
        }
    }
}
