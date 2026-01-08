using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.DOM.ApplicationDbContext;
using Template.DOM.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Template.Funcionalidad.Services
{
    public class AuthService
    {
        private readonly ServiceDbContext _context;

        public AuthService(ServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> LoginAsync(string usuario, string password)
        {
            // Busca el usuario activo con usuario + password
            return await _context.Usuario
                .FirstOrDefaultAsync(u =>
                    u.UsuarioNombre == usuario &&
                    u.Password == password &&
                    u.Estatus);
        }
    }
}
