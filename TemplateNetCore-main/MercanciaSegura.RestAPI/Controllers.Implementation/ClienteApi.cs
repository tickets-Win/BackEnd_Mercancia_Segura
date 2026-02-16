using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercanciaSegura.DOM.ApplicationDbContext;
using MercanciaSegura.DOM.Modelos;
using MercanciaSegura.DOM.Modelos.Cliente;
using MercanciaSegura.RestAPI.Models;
using MercanciaSegura.RestAPI.Models.Cliente;
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


                BeneficiarioPreferente = c.BeneficiarioPreferente != null
                ? c.BeneficiarioPreferente.Select(x => new BeneficiarioPreferenteResponse
                {
                    BeneficiarioPreferenteId = x.BeneficiarioPreferenteId,
                    Nombre = x.Nombre,
                    Domicilio = x.Domicilio,
                    Rfc = x.RFC,
                }).ToList()
                : new List<BeneficiarioPreferenteResponse>(),


                Correos = c.Correos?.Select(x => new CorreoResponse
                {
                    CorreoId = x.CorreoId,
                    Correo = x.Correo,
                    TipoCorreoId = x.TipoCorreoId
                }).ToList() ?? new List<CorreoResponse>(),

                Cuota = c.Cuota?.Select(x => new CuotaResponse
                {
                    CuotaId = x.CuotaId,
                    TipoCuotaId = x.TipoCuotaId,
                    TipoTarifaId = x.TipoTarifaId,
                    Monto = x.Monto
                }).ToList() ?? new List<CuotaResponse>(),

                ClienteVendedor = c.ClienteVendedor?.Select(x => new ClienteVendedorResponse
                {
                    ClienteVendedorId = x.ClienteVendedorId,
                    VendedorId = x.VendedorId,
                    Comision = x.Comision,
                }).ToList() ?? new List<ClienteVendedorResponse>(),


                ClienteCredito = c.ClienteCredito == null ? null : new ClienteCreditoResponse
                {
                    ClienteCreditoId = c.ClienteCredito.ClienteCreditoId,
                    DiasDeCredito = c.ClienteCredito.DiasDeCredito,
                    MetodoDePago = c.ClienteCredito.MetodoDePago,
                    NumeroCuenta = c.ClienteCredito.NumeroCuenta,
                    LimiteDeCredito = c.ClienteCredito.LimiteDeCredito,
                    DiasDePago = c.ClienteCredito.DiasDePago,
                    DiasDeRevision = c.ClienteCredito.DiasDeRevision,
                    Saldo = c.ClienteCredito.Saldo
                }
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
            .Include(c => c.BeneficiarioPreferente)
            .Include(c => c.Correos)
            .Include(c => c.Cuota)
            .Include(c => c.ClienteVendedor)
            .Include(c => c.ClienteCredito)
            .Where(c => !c.FechaBaja.HasValue)
            .ToListAsync();


            return Ok(clientes.Select(MapToResponse));
        }


        public override async Task<IActionResult> GetClienteByIdAsync(string version, int idCliente)
        {
            var cliente = await _context.Cliente
            .Include(c => c.BeneficiarioPreferente)
            .Include(c => c.Correos)
            .Include(c => c.Cuota)
            .Include(c => c.ClienteVendedor)
            .Include(c => c.ClienteCredito)
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
                if (body.BeneficiarioPreferente != null && body.BeneficiarioPreferente.Any())
                {
                    foreach (var item in body.BeneficiarioPreferente)
                    {
                        var beneficiario = new BeneficiarioPreferente
                        {
                            ClienteId = nuevoCliente.ClienteId,
                            Nombre = item.Nombre,
                            RFC = item.RFC,
                            Domicilio = item.Domicilio
                        };

                        _context.BeneficiarioPreferente.Add(beneficiario);
                    }
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
                if (body.ClienteVendedor != null && body.ClienteVendedor.Any())
                {
                    var vendedorIds = body.ClienteVendedor
                        .Select(v => v.VendedorId)
                        .Distinct()
                        .ToList();

                    if (vendedorIds.Count != body.ClienteVendedor.Count)
                        return BadRequest("No se permiten vendedores duplicados");

                    foreach (var v in body.ClienteVendedor)
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
                if (body.Cuota != null && body.Cuota.Any())
                {
                    foreach (var c in body.Cuota)
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
                // 1️⃣ Buscar Cliente Principal
                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(c => c.ClienteId == idCliente);

                if (cliente == null)
                    return NotFound();

                // Mapeo de datos básicos
                MapToCliente(cliente, body);
                cliente.FechaActualizacion = DateTime.Now;

                // --- INICIO DE REEMPLAZO DE LISTAS ---

                // 2️⃣ Beneficiarios
                var benefActuales = await _context.BeneficiarioPreferente.Where(b => b.ClienteId == idCliente).ToListAsync();
                _context.BeneficiarioPreferente.RemoveRange(benefActuales);

                // 3️⃣ Correos
                var correosActuales = await _context.Correos.Where(c => c.ClienteId == idCliente).ToListAsync();
                _context.Correos.RemoveRange(correosActuales);

                // 4️⃣ Vendedores
                var vendActuales = await _context.ClienteVendedor.Where(cv => cv.ClienteId == idCliente).ToListAsync();
                _context.ClienteVendedor.RemoveRange(vendActuales);

                // 5️⃣ Cuotas
                var cuotasActuales = await _context.Cuota.Where(c => c.ClienteId == idCliente).ToListAsync();
                _context.Cuota.RemoveRange(cuotasActuales);

                // 💾 Guardado intermedio para limpiar los registros viejos antes de insertar
                await _context.SaveChangesAsync();

                // --- INSERCIÓN DE NUEVOS DATOS ---

                if (body.BeneficiarioPreferente != null)
                {
                    foreach (var b in body.BeneficiarioPreferente)
                    {
                        _context.BeneficiarioPreferente.Add(new BeneficiarioPreferente
                        {
                            ClienteId = idCliente,
                            Nombre = b.Nombre,
                            RFC = b.RFC,
                            Domicilio = b.Domicilio
                        });
                    }
                }

                if (body.Correos != null)
                {
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

                if (body.ClienteVendedor != null)
                {
                    // Usamos Distinct por VendedorId para evitar error de llave duplicada
                    foreach (var v in body.ClienteVendedor.GroupBy(x => x.VendedorId).Select(g => g.First()))
                    {
                        _context.ClienteVendedor.Add(new ClienteVendedor
                        {
                            ClienteId = idCliente,
                            VendedorId = v.VendedorId,
                            Comision = v.Comision
                        });
                    }
                }

                if (body.Cuota != null)
                {
                    foreach (var q in body.Cuota)
                    {
                        _context.Cuota.Add(new Cuota
                        {
                            ClienteId = idCliente,
                            TipoCuotaId = q.TipoCuotaId,
                            TipoTarifaId = q.TipoTarifaId,
                            Monto = q.Monto
                        });
                    }
                }

                // 6️⃣ Cliente Crédito (Lógica de Upsert se mantiene igual)
                if (body.ClienteCredito != null)
                {
                    var credito = await _context.ClienteCredito.FirstOrDefaultAsync(cc => cc.ClienteId == idCliente);
                    if (credito != null)
                    {
                        // Update
                        credito.DiasDeCredito = body.ClienteCredito.DiasDeCredito;
                        credito.MetodoDePago = body.ClienteCredito.MetodoDePago;
                        credito.NumeroCuenta = body.ClienteCredito.NumeroCuenta;
                        credito.LimiteDeCredito = body.ClienteCredito.LimiteDeCredito;
                        credito.DiasDePago = body.ClienteCredito.DiasDePago;
                        credito.DiasDeRevision = body.ClienteCredito.DiasDeRevision;
                        credito.Saldo = body.ClienteCredito.Saldo;
                    }
                    else
                    {

                        // Insert
                        _context.ClienteCredito.Add(new ClienteCredito
                        {
                            ClienteId = idCliente,
                            DiasDeCredito = body.ClienteCredito.DiasDeCredito,
                            LimiteDeCredito = body.ClienteCredito.LimiteDeCredito,
                            MetodoDePago = body.ClienteCredito.MetodoDePago,
                            NumeroCuenta = body.ClienteCredito.NumeroCuenta,
                            DiasDePago = body.ClienteCredito.DiasDePago,
                            DiasDeRevision = body.ClienteCredito.DiasDeRevision,
                            Saldo = body.ClienteCredito.Saldo
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(MapToResponse(cliente));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    message = "Error al actualizar",
                    error = ex.InnerException?.Message ?? ex.Message
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