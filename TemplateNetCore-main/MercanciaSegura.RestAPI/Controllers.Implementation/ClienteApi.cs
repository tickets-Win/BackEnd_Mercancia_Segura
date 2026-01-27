using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MercanciaSegura.RestAPI.Controllers.Implementation
{
    public class ClienteApiController : Controllers.ClienteApiControllerBase
    {
        private readonly ServiceDbContext _context;

        public ClienteApiController(ServiceDbContext context)
        {
            _context = context;
        }

        // Mapear Cliente → ClienteRequest
        private ClienteRequest MapToRequest(Cliente c)
        {
            return new ClienteRequest
            {
                Rfc = c.Rfc,
                RfcGenericoId = c.RfcGenericoId,
                Telefono = c.Telefono,
                CorreoElectronico = c.CorreoElectronico,
                ApellidoPaterno = c.ApellidoPaterno,
                ApellidoMaterno = c.ApellidoMaterno,
                Nombres = c.Nombres,
                NombreCompleto = c.NombreCompleto,
                Nacionalidad = c.Nacionalidad,
                Calle = c.Calle,
                NumeroInt = c.NumeroInt,
                NumeroExt = c.NumeroExt,
                Pais = c.Pais,
                Municipio = c.Municipio,
                Poblacion = c.Poblacion,
                Colonia = c.Colonia,
                Estado = c.Estado,
                Cp = c.Cp,
                TipoSeguroId = c.TipoSeguroId,
                OrigenClienteId = c.OrigenClienteId,
                TipoCuentaId = c.TipoCuentaId,
                TipoSectorId = c.TipoSectorId,
                RegimenFiscalId = c.RegimenFiscalId,
                TipoPersonaId = c.TipoPersonaId,
                EstatusId = c.EstatusId,
                CuotaMinimaInternacional = c.CuotaMinimaInternacional,
                CuotaMinimaNacional = c.CuotaMinimaNacional,
                CuotaAplicableInternacional = c.CuotaAplicableInternacional,
                CuotaAplicableNacional = c.CuotaAplicableNacional,
                FechaRegistro = c.FechaRegistro
            };
        }

        // Mapear ClienteRequest → Cliente (para Create/Update)
        private void MapToCliente(Cliente cliente, ClienteRequest body)
        {
            cliente.Rfc = body.Rfc;
            cliente.RfcGenericoId = body.RfcGenericoId;
            cliente.Telefono = body.Telefono;
            cliente.CorreoElectronico = body.CorreoElectronico;
            cliente.ApellidoPaterno = body.ApellidoPaterno;
            cliente.ApellidoMaterno = body.ApellidoMaterno;
            cliente.Nombres = body.Nombres;
            cliente.NombreCompleto = body.NombreCompleto;
            cliente.Nacionalidad = body.Nacionalidad;
            cliente.Calle = body.Calle;
            cliente.NumeroInt = body.NumeroInt;
            cliente.NumeroExt = body.NumeroExt;
            cliente.Pais = body.Pais;
            cliente.Municipio = body.Municipio;
            cliente.Poblacion = body.Poblacion;
            cliente.Colonia = body.Colonia;
            cliente.Estado = body.Estado;
            cliente.Cp = body.Cp;
            cliente.TipoSeguroId = body.TipoSeguroId;
            cliente.OrigenClienteId = body.OrigenClienteId;
            cliente.TipoCuentaId = body.TipoCuentaId;
            cliente.TipoSectorId = body.TipoSectorId;
            cliente.RegimenFiscalId = body.RegimenFiscalId;
            cliente.TipoPersonaId = body.TipoPersonaId;
            cliente.EstatusId = body.EstatusId;
            cliente.CuotaMinimaInternacional = body.CuotaMinimaInternacional;
            cliente.CuotaMinimaNacional = body.CuotaMinimaNacional;
            cliente.CuotaAplicableInternacional = body.CuotaAplicableInternacional;
            cliente.CuotaAplicableNacional = body.CuotaAplicableNacional;
        }

        public override async Task<IActionResult> GetClientesAsync(string version)
        {
            var clientes = await _context.Cliente.ToListAsync();
            return Ok(clientes.Select(MapToRequest));
        }

        public override async Task<IActionResult> GetClienteByIdAsync(string version, int idCliente)
        {
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.ClienteId == idCliente);

            if (cliente == null)
                return NotFound();

            return Ok(MapToRequest(cliente));
        }

        public override async Task<IActionResult> CreateClienteAsync(string version, [FromBody] ClienteRequest body)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage)
                    });

                return BadRequest(new { errors });
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var nuevoCliente = new Cliente();
                MapToCliente(nuevoCliente, body);

                _context.Cliente.Add(nuevoCliente);
                await _context.SaveChangesAsync();

                if (body.Beneficiario != null)
                {
                    var beneficiario = new BeneficiarioPreferente
                    {
                        ClienteId = nuevoCliente.ClienteId,
                        Nombre = body.Beneficiario.Nombre,
                        RFC = body.Beneficiario.RFC,
                        Domicilio = body.Beneficiario.Domicilio
                    };

                    _context.BeneficiarioPreferente.Add(beneficiario);
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();


                return Ok(new
                {
                    message = "Cliente y beneficiario creados correctamente",
                    clienteId = nuevoCliente.ClienteId
                });
            }
            catch (Exception ex)
            {
                // 4️⃣ Revertir TODO
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "Ocurrió un error al crear el cliente",
                    error = ex.Message
                });
            }
        }

        public override async Task<IActionResult> UpdateClienteAsync(string version, int idCliente, [FromBody] ClienteRequest body)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.ClienteId == idCliente);

                if (cliente == null)
                    return NotFound();
                // 1️⃣ Actualizar cliente
                MapToCliente(cliente, body);
                await _context.SaveChangesAsync();

                // 2️⃣ Actualizar o crear beneficiario
                if (body.Beneficiario != null)
                {
                    var beneficiario = await _context.BeneficiarioPreferente
                        .FirstOrDefaultAsync(b => b.ClienteId == idCliente);

                    if (beneficiario != null)
                    {
                        // 🔁 UPDATE
                        beneficiario.Nombre = body.Beneficiario.Nombre;
                        beneficiario.RFC = body.Beneficiario.RFC;
                        beneficiario.Domicilio = body.Beneficiario.Domicilio;
                    }
                    else
                    {
                        // ➕ INSERT
                        beneficiario = new BeneficiarioPreferente
                        {
                            ClienteId = idCliente,
                            Nombre = body.Beneficiario.Nombre,
                            RFC = body.Beneficiario.RFC,
                            Domicilio = body.Beneficiario.Domicilio
                        };

                        _context.BeneficiarioPreferente.Add(beneficiario);
                    }

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return Ok(MapToRequest(cliente));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "Error al actualizar cliente y beneficiario",
                    error = ex.Message
                });
            }
        }

        public override async Task<IActionResult> DeleteClienteAsync(string version, int idCliente)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(c => c.ClienteId == idCliente);

                if (cliente == null)
                    return NotFound();

                // 1️⃣ Borrar beneficiario si existe
                var beneficiario = await _context.BeneficiarioPreferente
                    .FirstOrDefaultAsync(b => b.ClienteId == idCliente);

                if (beneficiario != null)
                    _context.BeneficiarioPreferente.Remove(beneficiario);

                // 2️⃣ Borrar cliente
                _context.Cliente.Remove(cliente);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Cliente eliminado correctamente" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "Error al eliminar cliente",
                    error = ex.Message
                });
            }
        }
    }
}