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
        private ClienteResponse MapToResponse(Cliente c)
        {
            return new ClienteResponse
            {
                ClienteId = c.ClienteId,
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
                FechaRegistro = c.FechaRegistro,
                FechaBaja = c.FechaBaja,
                Clave = c.Clave,
                FechaActualizacion = c.FechaActualizacion,
                Genero = c.Genero,
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
            cliente.Clave = body.Clave;
            cliente.Genero = body.Genero;
        }

        public override async Task<IActionResult> GetClientesAsync(string version)
        {
            var clientes = await _context.Cliente
                .Where(c => !c.FechaBaja.HasValue)
                .ToListAsync();

            return Ok(clientes.Select(MapToResponse));
        }


        public override async Task<IActionResult> GetClienteByIdAsync(string version, int idCliente)
        {
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c =>
                    c.ClienteId == idCliente &&
                    !c.FechaBaja.HasValue);

            if (cliente == null)
                return NotFound();

            return Ok(MapToResponse(cliente));
        }



        public override async Task<IActionResult> CreateClienteAsync(string version,[FromBody] ClienteRequest body)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage)
                    });

                return BadRequest(new { errors });
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Crear Cliente
                var nuevoCliente = new Cliente();
                MapToCliente(nuevoCliente, body);

                _context.Cliente.Add(nuevoCliente);
                await _context.SaveChangesAsync(); // necesario para obtener ClienteId

                // 2️⃣ Beneficiario Preferente
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
                }

                // 3️⃣ Correos (LISTA)
                if (body.Correos != null && body.Correos.Any())
                {
                    foreach (var c in body.Correos)
                    {
                        _context.Correos.Add(new Correos
                        {
                            ClienteId = nuevoCliente.ClienteId,
                            Correo = c.Correo,
                            TipoCorreoId = c.TipoCorreoId
                        });
                    }
                }


                // 4️⃣ Cliente - Vendedores (LISTA)
                if (body.Vendedores != null && body.Vendedores.Any())
                {
                    var vendedorIds = body.Vendedores
                        .Select(v => v.VendedorId)
                        .Distinct()
                        .ToList();

                    if (vendedorIds.Count != body.Vendedores.Count)
                        return BadRequest("No se permiten vendedores duplicados");

                    foreach (var v in body.Vendedores)
                    {
                        // Validar que el vendedor exista
                        var existeVendedor = await _context.Vendedor
                            .AnyAsync(x => x.VendedorId == v.VendedorId);

                        if (!existeVendedor)
                            return BadRequest($"El vendedor {v.VendedorId} no existe");

                        _context.ClienteVendedor.Add(new ClienteVendedor
                        {
                            ClienteId = nuevoCliente.ClienteId,
                            VendedorId = v.VendedorId,
                            Comision = v.Comision
                        });
                    }
                }


                // 5️⃣ Cuotas (LISTA)
                if (body.Cuotas != null && body.Cuotas.Any())
                {
                    foreach (var c in body.Cuotas)
                    {
                        _context.Cuota.Add(new Cuota
                        {
                            ClienteId = nuevoCliente.ClienteId,
                            TipoCuotaId = c.TipoCuotaId,
                            TipoTarifaId = c.TipoTarifaId,
                            Monto = c.Monto
                        });
                    }
                }

                // 6️⃣ Cliente Crédito (CREATE)
                if (body.ClienteCredito != null)
                {
                    _context.ClienteCredito.Add(new ClienteCredito
                    {
                        ClienteId = nuevoCliente.ClienteId,
                        DiasDeCredito = body.ClienteCredito.DiasDeCredito,
                        MetodoDePago = body.ClienteCredito.MetodoDePago,
                        NumeroCuenta = body.ClienteCredito.NumeroCuenta,
                        LimiteDeCredito = body.ClienteCredito.LimiteDeCredito,
                        DiasDePago = body.ClienteCredito.DiasDePago,
                        DiasDeRevision = body.ClienteCredito.DiasDeRevision,
                        Saldo = body.ClienteCredito.Saldo
                    });
                }



                // 6️⃣ Guardar TODO
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(MapToResponse(nuevoCliente));
            }
            catch (Exception ex)
            {
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
                // 1️⃣ Cliente
                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(c => c.ClienteId == idCliente);

                if (cliente == null)
                    return NotFound();

                MapToCliente(cliente, body);

                cliente.FechaActualizacion = DateTime.Now;


                // 2️⃣ Beneficiario
                if (body.Beneficiario != null)
                {
                    var beneficiario = await _context.BeneficiarioPreferente
                        .FirstOrDefaultAsync(b => b.ClienteId == idCliente);

                    if (beneficiario != null)
                    {
                        beneficiario.Nombre = body.Beneficiario.Nombre;
                        beneficiario.RFC = body.Beneficiario.RFC;
                        beneficiario.Domicilio = body.Beneficiario.Domicilio;
                    }
                    else
                    {
                        _context.BeneficiarioPreferente.Add(new BeneficiarioPreferente
                        {
                            ClienteId = idCliente,
                            Nombre = body.Beneficiario.Nombre,
                            RFC = body.Beneficiario.RFC,
                            Domicilio = body.Beneficiario.Domicilio
                        });
                    }
                }

                // 3️⃣ Correos (REEMPLAZAR)
                if (body.Correos != null)
                {
                    var actuales = await _context.Correos
                        .Where(c => c.ClienteId == idCliente)
                        .ToListAsync();

                    _context.Correos.RemoveRange(actuales);

                    foreach (var c in body.Correos)
                    {
                        _context.Correos.Add(new Correos
                        {
                            ClienteId = idCliente,
                            Correo = c.Correo,
                            TipoCorreoId = c.TipoCorreoId
                        });
                    }
                }


                // 4️⃣ Cliente - Vendedores (REEMPLAZAR)
                if (body.Vendedores != null)
                {
                    // Eliminar relaciones actuales
                    var actuales = await _context.ClienteVendedor
                        .Where(cv => cv.ClienteId == idCliente)
                        .ToListAsync();

                    _context.ClienteVendedor.RemoveRange(actuales);

                    // Insertar nuevas
                    foreach (var v in body.Vendedores)
                    {
                        _context.ClienteVendedor.Add(new ClienteVendedor
                        {
                            ClienteId = idCliente,
                            VendedorId = v.VendedorId,
                            Comision = v.Comision
                        });
                    }
                }


                // 5️⃣ Cuotas (LISTA)
                if (body.Cuotas != null)
                {
                    var cuotasActuales = await _context.Cuota
                        .Where(c => c.ClienteId == idCliente)
                        .ToListAsync();

                    _context.Cuota.RemoveRange(cuotasActuales);

                    foreach (var c in body.Cuotas)
                    {
                        _context.Cuota.Add(new Cuota
                        {
                            ClienteId = idCliente,
                            TipoCuotaId = c.TipoCuotaId,
                            TipoTarifaId = c.TipoTarifaId,
                            Monto = c.Monto
                        });
                    }
                }


                // Cliente Crédito (UPDATE / UPSERT)
                if (body.ClienteCredito != null)
                {
                    var clienteCredito = await _context.ClienteCredito
                        .FirstOrDefaultAsync(cc => cc.ClienteId == idCliente);

                    if (clienteCredito != null)
                    {
                        // 🔁 UPDATE
                        clienteCredito.DiasDeCredito = body.ClienteCredito.DiasDeCredito;
                        clienteCredito.MetodoDePago = body.ClienteCredito.MetodoDePago;
                        clienteCredito.NumeroCuenta = body.ClienteCredito.NumeroCuenta;
                        clienteCredito.LimiteDeCredito = body.ClienteCredito.LimiteDeCredito;
                        clienteCredito.DiasDePago = body.ClienteCredito.DiasDePago;
                        clienteCredito.DiasDeRevision = body.ClienteCredito.DiasDeRevision;
                        clienteCredito.Saldo = body.ClienteCredito.Saldo;
                    }
                    else
                    {
                        // ➕ INSERT (solo si no existe)
                        _context.ClienteCredito.Add(new ClienteCredito
                        {
                            ClienteId = idCliente,
                            DiasDeCredito = body.ClienteCredito.DiasDeCredito,
                            MetodoDePago = body.ClienteCredito.MetodoDePago,
                            NumeroCuenta = body.ClienteCredito.NumeroCuenta,
                            LimiteDeCredito = body.ClienteCredito.LimiteDeCredito,
                            DiasDePago = body.ClienteCredito.DiasDePago,
                            DiasDeRevision = body.ClienteCredito.DiasDeRevision,
                            Saldo = body.ClienteCredito.Saldo
                        });
                    }
                }



                // 6️⃣ Guardar TODO
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(MapToResponse(cliente));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    message = "Error al actualizar cliente",
                    error = ex.Message
                });
            }
        }


        public override async Task<IActionResult> DeleteClienteAsync(string version, int idCliente)
        {
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.ClienteId == idCliente);

            if (cliente == null)
                return NotFound();

            if (cliente.FechaBaja.HasValue)
                return BadRequest(new { message = "El cliente se ha eliminado correctamente" });

            cliente.FechaBaja = DateTime.Now;
            cliente.EstatusId = 2;

            _context.Entry(cliente).Property(c => c.FechaBaja).IsModified = true;

            await _context.SaveChangesAsync();

            return Ok(new { message = "El cliente ya fue eliminado" });
        }

    }
}