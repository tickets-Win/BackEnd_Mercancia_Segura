using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercanciaSegura.DOM.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.Funcionalidad.Services
{
    public async Task<ClienteResponse> CrearClienteAsync(CreateClienteRequest request)
    {
        var cliente = new Cliente
        {
            TipoPersona = request.TipoPersona,
            Rfc = request.Rfc,
            CorreoElectronico = request.CorreoElectronico,
            Telefono = request.Telefono,
            ApellidoPaterno = request.ApellidoPaterno,
            ApellidoMaterno = request.ApellidoMaterno,
            Nombres = request.Nombres,
            NombreCompleto = request.NombreCompleto,
            FechaRegistro = DateTime.UtcNow
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return new ClienteResponse
        {
            ClienteId = cliente.ClienteId,
            TipoPersona = cliente.TipoPersona!,
            Rfc = cliente.Rfc!,
            NombreCompleto = cliente.NombreCompleto ?? "",
            CorreoElectronico = cliente.CorreoElectronico!,
            Telefono = cliente.Telefono!,
            FechaRegistro = cliente.FechaRegistro
        };
    }

}
