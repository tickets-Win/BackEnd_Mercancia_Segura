<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminCobranza.aspx.vb" Inherits="WebAdmin.AdminCobranza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />

    <style>
        /* Estilos propios de Cobranza. Viven en la pagina, como en Reportes, para
           no afectar al resto de las pantallas. */
        .cob-titulo {
            color: #17406d;
            font-weight: 700;
        }

        .cob-subtitulo {
            color: #5b7fa6;
            font-size: .8125rem;
        }

        /* Tarjetas de indicadores */
        .cob-kpi {
            background: #fff;
            border: 1px solid #e3e8ef;
            border-radius: .5rem;
            padding: .9rem 1.1rem;
            height: 100%;
        }

        .cob-kpi-nombre {
            font-size: .8125rem;
            color: #475569;
            margin-bottom: .35rem;
        }

        .cob-kpi-dato {
            font-size: 1.5rem;
            font-weight: 700;
            line-height: 1.1;
        }

        .cob-total {
            color: #17406d;
        }

        .cob-cobrado {
            color: #16a34a;
        }

        .cob-pendiente {
            color: #d97706;
        }

        .cob-vencido {
            color: #dc2626;
        }

        /* Buscador */
        .cob-buscador {
            background: #eef2f7;
            border: 1px solid #e3e8ef;
        }

            .cob-buscador:focus {
                background: #fff;
            }

        /* Tabla */
        .cob-panel {
            background: #fff;
            border: 1px solid #e3e8ef;
            border-radius: .5rem;
        }

        .cob-tabla {
            font-size: .8125rem;
            margin-bottom: 0;
        }

            .cob-tabla thead th {
                color: #475569;
                font-weight: 600;
                font-size: .75rem;
                border-bottom: 1px solid #e3e8ef;
                white-space: nowrap;
                background: #fff;
            }

            .cob-tabla td {
                vertical-align: middle;
                border-bottom: 1px solid #f1f5f9;
            }

        .cob-enlace {
            color: #2563eb;
            text-decoration: none;
        }

            .cob-enlace:hover {
                text-decoration: underline;
            }

        .cob-estado-pendiente {
            color: #1f2937;
            font-weight: 600;
        }

        .cob-estado-pagado {
            color: #1f2937;
            font-weight: 600;
        }

        .cob-estado-vencida {
            color: #1f2937;
            font-weight: 600;
        }

        .cob-accion {
            color: #334155;
            font-size: 1rem;
            padding: 0 .3rem;
            text-decoration: none !important;
        }

            .cob-accion:hover {
                color: #17406d;
            }

        /* La tabla nunca debe empujar el ancho de la pagina */
        .cob-scroll {
            overflow-x: auto;
        }

        /* ---------------------------- Recibos ---------------------------- */
        .cob-volver {
            border: 1px solid #d7dee8;
            background: #fff;
            border-radius: .375rem;
            padding: .25rem .6rem;
            color: #17406d;
            text-decoration: none !important;
            line-height: 1.6;
        }

            .cob-volver:hover {
                background: #f1f5f9;
            }

        .cob-factura {
            font-size: .8125rem;
            color: #5b7fa6;
        }

        .cob-factura-clave {
            font-size: 1.05rem;
            font-weight: 700;
            color: #17406d;
        }

        .cob-comisiones {
            background: #5b8aa6;
            border-color: #5b8aa6;
            color: #fff;
            font-size: .8125rem;
        }

            .cob-comisiones:hover {
                background: #4d7a94;
                color: #fff;
            }

        /* ------------------------ Detalle del recibo ---------------------- */
        .rec-clave {
            color: #22c55e;
            font-weight: 700;
        }

        .rec-seccion {
            font-weight: 700;
            color: #1f2937;
            font-size: 1rem;
        }

        /* Cada dato es una etiqueta chica arriba y el valor abajo */
        .rec-etiqueta {
            font-size: .75rem;
            color: #5b8aa6;
            margin-bottom: .1rem;
        }

        .rec-valor {
            font-size: .875rem;
            color: #1f2937;
        }

        .rec-grupo {
            border-bottom: 1px solid #eef2f7;
            padding-bottom: .75rem;
            margin-bottom: .75rem;
        }

        .rec-pagar {
            background: #2563eb;
            border-color: #2563eb;
            color: #fff;
        }

            .rec-pagar:hover {
                background: #1d4ed8;
                color: #fff;
            }

        .rec-descargar {
            background: #16a34a;
            border-color: #16a34a;
            color: #fff;
        }

            .rec-descargar:hover {
                background: #15803d;
                color: #fff;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%-- Listado y recibos van en el mismo UpdatePanel: los botones que cambian el
     Visible de un panel tienen que vivir junto a ese panel, si no la pantalla
     se queda en blanco. --%>
<asp:UpdatePanel ID="UpCobranza" runat="server" UpdateMode="Always">
    <ContentTemplate>

<asp:Panel ID="pnlListado" runat="server">

    <%-- ============================ ENCABEZADO ============================ --%>
    <div class="mb-4">
        <h2 class="cob-titulo mb-1">Cobranza</h2>
        <p class="cob-subtitulo mb-0">Gestiona el cobro de las facturas emitidas por certificado</p>
    </div>

    <%-- ========================== INDICADORES ============================= --%>
    <div class="row g-3 mb-4">
        <div class="col-md-3 col-sm-6">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Total facturado</div>
                <div class="cob-kpi-dato cob-total">$107,800</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Cobrado</div>
                <div class="cob-kpi-dato cob-cobrado">$24,300</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Pendiente</div>
                <div class="cob-kpi-dato cob-pendiente">$60,000</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Vencido</div>
                <div class="cob-kpi-dato cob-vencido">$5,000</div>
            </div>
        </div>
    </div>

    <%-- ====================== HISTORIAL DE FACTURAS ======================= --%>
    <h4 class="cob-titulo mb-3">Historial de facturas</h4>

    <div class="mb-3">
        <div class="input-group">
            <span class="input-group-text cob-buscador border-end-0">
                <i class="bi bi-search text-muted"></i>
            </span>
            <asp:TextBox ID="txtBuscarCobro" runat="server" CssClass="form-control cob-buscador border-start-0"
                placeholder="Buscar  por cliente o certificado"></asp:TextBox>
        </div>
    </div>

    <asp:Panel ID="PnlTabla" runat="server" CssClass="cob-panel p-3">
        <div class="cob-scroll">
            <table class="table cob-tabla">
                <thead>
                    <tr>
                        <th scope="col">Factura</th>
                        <th scope="col">Certificado</th>
                        <th scope="col">Cotizacion</th>
                        <th scope="col">Cliente</th>
                        <th scope="col">Recibos</th>
                        <th scope="col">Fecha vencimiento</th>
                        <th scope="col">Monto</th>
                        <th scope="col">Estado</th>
                        <th scope="col" class="cob-enlace">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Facturas" CommandArgument="VNT-01" OnCommand="lnkFacturas_Command" Text="Vnt-01" /></td>
                        <td>CERT-01</td>
                        <td>cot-01</td>
                        <td>Ana García</td>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Recibos" CommandArgument="FAC-2025-001" OnCommand="lnkRecibos_Command" Text="Rec-01" /></td>
                        <td>30-12-2025</td>
                        <td>$50,000</td>
                        <td><span class="cob-estado-pendiente">Pendiente</span></td>
                        <td class="text-nowrap">
                            <a href="javascript:void(0)" class="cob-accion" title="Registrar pago"><i class="bi bi-cash-stack"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Ver detalle"><i class="bi bi-eye"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Eliminar"><i class="bi bi-trash"></i></a>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Facturas" CommandArgument="VNT-02" OnCommand="lnkFacturas_Command" Text="Vnt-02" /></td>
                        <td>CERT-02</td>
                        <td>cot-02</td>
                        <td>Juan Pérez</td>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Recibos" CommandArgument="FAC-2025-002" OnCommand="lnkRecibos_Command" Text="Rec-02" /></td>
                        <td>30-09-2025</td>
                        <td>$10,000</td>
                        <td><span class="cob-estado-pendiente">Pendiente</span></td>
                        <td class="text-nowrap">
                            <a href="javascript:void(0)" class="cob-accion" title="Registrar pago"><i class="bi bi-cash-stack"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Ver detalle"><i class="bi bi-eye"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Eliminar"><i class="bi bi-trash"></i></a>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Facturas" CommandArgument="VNT-03" OnCommand="lnkFacturas_Command" Text="Vnt-03" /></td>
                        <td>CERT-03</td>
                        <td>cot-03</td>
                        <td>Sofía Martínez</td>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Recibos" CommandArgument="FAC-2025-003" OnCommand="lnkRecibos_Command" Text="Rec-03" /></td>
                        <td>30-09-2026</td>
                        <td>$22,300</td>
                        <td><span class="cob-estado-pagado">Pagado</span></td>
                        <td class="text-nowrap">
                            <a href="javascript:void(0)" class="cob-accion" title="Registrar pago"><i class="bi bi-cash-stack"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Ver detalle"><i class="bi bi-eye"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Eliminar"><i class="bi bi-trash"></i></a>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Facturas" CommandArgument="VNT-04" OnCommand="lnkFacturas_Command" Text="Vnt-04" /></td>
                        <td>CERT-04</td>
                        <td>cot-04</td>
                        <td>Diego Sánchez</td>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Recibos" CommandArgument="FAC-2025-004" OnCommand="lnkRecibos_Command" Text="Rec-04" /></td>
                        <td>12-01-2026</td>
                        <td>$2,000</td>
                        <td><span class="cob-estado-pagado">Pagado</span></td>
                        <td class="text-nowrap">
                            <a href="javascript:void(0)" class="cob-accion" title="Registrar pago"><i class="bi bi-cash-stack"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Ver detalle"><i class="bi bi-eye"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Eliminar"><i class="bi bi-trash"></i></a>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Facturas" CommandArgument="VNT-05" OnCommand="lnkFacturas_Command" Text="Vnt-05" /></td>
                        <td>CERT-05</td>
                        <td>cot-05</td>
                        <td>Isabel Torres</td>
                        <td><asp:LinkButton runat="server" CssClass="cob-enlace" CommandName="Recibos" CommandArgument="FAC-2025-005" OnCommand="lnkRecibos_Command" Text="Rec-05" /></td>
                        <td>20-06-2026</td>
                        <td>$5,000</td>
                        <td><span class="cob-estado-vencida">Vencida</span></td>
                        <td class="text-nowrap">
                            <a href="javascript:void(0)" class="cob-accion" title="Registrar pago"><i class="bi bi-cash-stack"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Ver detalle"><i class="bi bi-eye"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Eliminar"><i class="bi bi-trash"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>

</asp:Panel>

<%-- ============================== RECIBOS ==============================
     Se abre al dar clic en el enlace de Recibos de cualquier factura. --%>
<asp:Panel ID="pnlRecibos" runat="server" Visible="false">

    <div class="d-flex align-items-center gap-3 mb-1">
        <asp:LinkButton ID="lnkVolver" runat="server" CssClass="cob-volver"
            ToolTip="Regresar a cobranza" OnClick="lnkVolver_Click">
            <i class="bi bi-arrow-left"></i>
        </asp:LinkButton>
        <h2 class="cob-titulo mb-0">Recibos</h2>
    </div>
    <p class="cob-subtitulo mb-4">Gestiona y analiza tus recibos por cada Certificado</p>

    <div class="mb-4">
        <div class="input-group">
            <span class="input-group-text cob-buscador border-end-0">
                <i class="bi bi-search text-muted"></i>
            </span>
            <asp:TextBox ID="txtBuscarRecibo" runat="server" CssClass="form-control cob-buscador border-start-0"
                placeholder="Buscar  ventas por cliente, vendedor o póliza"></asp:TextBox>
        </div>
    </div>

    <h4 class="cob-titulo mb-3">Recibos</h4>

    <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mb-3">
        <div>
            <span class="cob-factura">Recibos de la factura:</span>
            <span class="cob-factura-clave ms-1">
                <asp:Literal ID="litFactura" runat="server" /></span>
        </div>
        <asp:LinkButton ID="lnkComisiones" runat="server" CssClass="btn btn-sm cob-comisiones"
            OnClick="lnkComisiones_Click">
            Comisiones
        </asp:LinkButton>
    </div>

    <asp:Panel ID="pnlTablaRecibos" runat="server" CssClass="cob-panel p-3">
        <div class="cob-scroll">
            <table class="table cob-tabla">
                <thead>
                    <tr>
                        <th scope="col">Número de Recibo</th>
                        <th scope="col">Fecha de Emisión</th>
                        <th scope="col">Vencimiento</th>
                        <th scope="col">Monto</th>
                        <th scope="col">Estado</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><asp:Literal ID="litRecibo1" runat="server" /></td>
                        <td>30/07/2025</td>
                        <td>30/08/2025</td>
                        <td>$500.00</td>
                        <td>Vencido</td>
                        <td class="text-nowrap">
                            <asp:LinkButton ID="lnkVerRecibo1" runat="server" CssClass="cob-accion"
                                ToolTip="Ver detalle" OnCommand="lnkVerRecibo_Command" CommandName="Detalle"
                                CommandArgument="1"><i class="bi bi-eye"></i></asp:LinkButton>
                            <a href="javascript:void(0)" class="cob-accion" title="Enviar por correo"><i class="bi bi-envelope-fill"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Descargar"><i class="bi bi-cloud-arrow-down-fill"></i></a>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Literal ID="litRecibo2" runat="server" /></td>
                        <td>30/08/2025</td>
                        <td>30/09/2025</td>
                        <td>$500.00</td>
                        <td>Pendiente</td>
                        <td class="text-nowrap">
                            <asp:LinkButton ID="lnkVerRecibo2" runat="server" CssClass="cob-accion"
                                ToolTip="Ver detalle" OnCommand="lnkVerRecibo_Command" CommandName="Detalle"
                                CommandArgument="2"><i class="bi bi-eye"></i></asp:LinkButton>
                            <a href="javascript:void(0)" class="cob-accion" title="Enviar por correo"><i class="bi bi-envelope-fill"></i></a>
                            <a href="javascript:void(0)" class="cob-accion" title="Descargar"><i class="bi bi-cloud-arrow-down-fill"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>

</asp:Panel>

<%-- ============================ COMISIONES =============================
     Se abre con el boton Comisiones de la vista de recibos. --%>
<asp:Panel ID="pnlComisiones" runat="server" Visible="false">

    <div class="d-flex justify-content-between align-items-start flex-wrap gap-2 mb-4">
        <div>
            <h2 class="cob-titulo mb-1">Comisiones</h2>
            <p class="cob-subtitulo mb-0">Gestiona y analiza tus comisiones</p>
        </div>
        <div class="d-flex gap-2">
            <asp:LinkButton ID="lnkDescargarDesglose" runat="server" CssClass="btn btn-sm btn-light border">
                Descargar desglose
            </asp:LinkButton>
            <%-- Este boton hace de regreso; por eso esta vista no lleva flecha. --%>
            <asp:LinkButton ID="lnkVolverARecibos" runat="server" CssClass="btn btn-sm btn-primary"
                OnClick="lnkVolverARecibos_Click">
                Volver a recibos
            </asp:LinkButton>
        </div>
    </div>

    <div class="row g-3 mb-4">
        <div class="col-md-4">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Devengada</div>
                <div class="cob-kpi-dato cob-cobrado">$0.00</div>
                <div class="cob-subtitulo mt-1">Ya se puede pagar al agente</div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Por devengar</div>
                <div class="cob-kpi-dato cob-pendiente">$1,250.00</div>
                <div class="cob-subtitulo mt-1">Depende de recibos por cobrar</div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Comisión total</div>
                <div class="cob-kpi-dato cob-cobrado">$1,250.00</div>
                <div class="cob-subtitulo mt-1">Si se cobra la factura completa</div>
            </div>
        </div>
    </div>

    <h4 class="cob-titulo mb-3">Listado de comisiones</h4>

    <div class="cob-factura mb-3">
        Comisiones de:
        <span class="cob-factura-clave ms-1"><asp:Literal ID="litFacturaComisiones" runat="server" /></span>
    </div>

    <asp:Panel ID="pnlTablaComisiones" runat="server" CssClass="cob-panel p-3">
        <div class="cob-scroll">
            <table class="table cob-tabla">
                <thead>
                    <tr>
                        <th scope="col">Recibo</th>
                        <th scope="col">Estado</th>
                        <th scope="col">Monto</th>
                        <th scope="col">Neta</th>
                        <th scope="col">Bono</th>
                        <th scope="col">Bonificación</th>
                        <th scope="col">Gastos</th>
                        <th scope="col">Total</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><asp:Literal ID="litComisionRecibo" runat="server" /></td>
                        <td>Vencido</td>
                        <td>$5,000.00</td>
                        <td>$500.00</td>
                        <td>$100.00</td>
                        <td>$100.00</td>
                        <td>-$25.00</td>
                        <td>$675.00</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>

</asp:Panel>

<%-- ============================= FACTURAS ==============================
     Se abre al dar clic en el numero de venta del listado de cobranza. --%>
<asp:Panel ID="pnlFacturas" runat="server" Visible="false">

    <div class="d-flex align-items-center gap-3 mb-1">
        <asp:LinkButton ID="lnkVolverDeFacturas" runat="server" CssClass="cob-volver"
            ToolTip="Regresar a cobranza" OnClick="lnkVolverDeFacturas_Click">
            <i class="bi bi-arrow-left"></i>
        </asp:LinkButton>
        <h2 class="cob-titulo mb-0">Facturas</h2>
    </div>
    <p class="cob-subtitulo mb-4">Gestiona y analiza tus facturas por venta</p>

    <div class="mb-4">
        <div class="input-group">
            <span class="input-group-text cob-buscador border-end-0">
                <i class="bi bi-search text-muted"></i>
            </span>
            <asp:TextBox ID="txtBuscarFactura" runat="server" CssClass="form-control cob-buscador border-start-0"
                placeholder="Buscar  factura por cliente, cotizacion o venta"></asp:TextBox>
        </div>
    </div>

    <h4 class="cob-titulo mb-3">Listado de facturas</h4>

    <div class="cob-factura mb-3">
        Facturas de la venta:
        <span class="cob-factura-clave ms-1"><asp:Literal ID="litVenta" runat="server" /></span>
    </div>

    <asp:Panel ID="pnlTablaFacturas" runat="server" CssClass="cob-panel p-3">
        <div class="cob-scroll">
            <table class="table cob-tabla">
                <thead>
                    <tr>
                        <th scope="col">Número de factura</th>
                        <th scope="col">Fecha de Emisión</th>
                        <th scope="col">Cliente</th>
                        <th scope="col">Fecha de Vencimiento</th>
                        <th scope="col">Monto</th>
                        <th scope="col">Estatus</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><asp:Literal ID="litFacturaNumero" runat="server" /></td>
                        <td>15/05/2024</td>
                        <td>Ana García</td>
                        <td>15/01/2025</td>
                        <td>$250.00</td>
                        <td>Pagado</td>
                        <td class="text-nowrap">
                            <asp:LinkButton ID="lnkVerFactura" runat="server" CssClass="cob-accion" ToolTip="Ver detalle" OnClick="lnkVerFactura_Click"><i class="bi bi-eye"></i></asp:LinkButton>
                            <a href="javascript:void(0)" class="cob-accion" title="Descargar"><i class="bi bi-cloud-arrow-down-fill"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>

</asp:Panel>


<%-- ======================= DETALLE DE LA FACTURA =======================
     Se abre con el ojito de la tabla de facturas. --%>
<asp:Panel ID="pnlDetalleFactura" runat="server" Visible="false">

    <h2 class="cob-titulo mb-1">
        Factura <span class="rec-clave"><asp:Literal ID="litDetFactura" runat="server" /></span>
    </h2>
    <p class="cob-subtitulo mb-3">Ana García &middot; Vencida &middot; 0 de 2 recibos cobrados</p>

    <div class="d-flex flex-wrap gap-2 mb-4">
        <asp:LinkButton ID="lnkDetDescargar" runat="server" CssClass="btn btn-sm btn-light border">Descargar factura</asp:LinkButton>
        <asp:LinkButton ID="lnkDetEnviar" runat="server" CssClass="btn btn-sm btn-light border">Enviar por correo</asp:LinkButton>
        <asp:LinkButton ID="lnkDetVolver" runat="server" CssClass="btn btn-sm btn-light border"
            OnClick="lnkDetVolver_Click">Volver a cobranza</asp:LinkButton>
    </div>

    <div class="row g-3 mb-4">
        <div class="col-md-3 col-sm-6">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Monto de la factura</div>
                <div class="cob-kpi-dato cob-total">$10,000.00</div>
                <div class="cob-subtitulo mt-1">2 recibos de $5,000.00</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Cobrado</div>
                <div class="cob-kpi-dato cob-cobrado">$0.00</div>
                <div class="cob-subtitulo mt-1">0% de la factura</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Pendiente</div>
                <div class="cob-kpi-dato cob-pendiente">$5,000.00</div>
                <div class="cob-subtitulo mt-1">Aún dentro de fecha</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="cob-kpi">
                <div class="cob-kpi-nombre">Vencido</div>
                <div class="cob-kpi-dato cob-vencido">$5,000.00</div>
                <div class="cob-subtitulo mt-1">Pasó la fecha límite</div>
            </div>
        </div>
    </div>

    <div class="rec-seccion mb-3">Datos de la factura</div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-3">
            <div class="rec-etiqueta">Número de factura</div>
            <div class="rec-valor"><asp:Literal ID="litDetNumero" runat="server" /></div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Fecha de emisión</div>
            <div class="rec-valor">30/08/2025</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Fecha de vencimiento</div>
            <div class="rec-valor">30/12/2025</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Monto total</div>
            <div class="rec-valor">$10,000.00</div>
        </div>
    </div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-3">
            <div class="rec-etiqueta">Frecuencia de pago</div>
            <div class="rec-valor">Mensual</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Forma de cobro</div>
            <div class="rec-valor">Tarjeta de crédito</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Moneda</div>
            <div class="rec-valor">MXN</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Número de recibos</div>
            <div class="rec-valor">2</div>
        </div>
    </div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-3">
            <div class="rec-etiqueta">Estado</div>
            <div class="rec-valor cob-vencido">Vencida</div>
        </div>
    </div>

    <div class="rec-seccion mt-4 mb-3">Origen de la factura</div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-3">
            <div class="rec-etiqueta">Venta</div>
            <div class="rec-valor"><asp:Literal ID="litDetVenta" runat="server" /></div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Cotización</div>
            <div class="rec-valor">COT-02</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Certificado</div>
            <div class="rec-valor">CRT-02</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Póliza</div>
            <div class="rec-valor">AX123456</div>
        </div>
    </div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-3">
            <div class="rec-etiqueta">Concepto</div>
            <div class="rec-valor">Mercancia de don Julio</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Bien asegurado</div>
            <div class="rec-valor">50 refrigeradores</div>
        </div>
    </div>

    <div class="rec-seccion mt-4 mb-3">Participantes</div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-3">
            <div class="rec-etiqueta">Cliente</div>
            <div class="rec-valor">Ana García</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Contratante</div>
            <div class="rec-valor">María Rodríguez</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Agente</div>
            <div class="rec-valor">Carlos Pérez</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">Vendedor</div>
            <div class="rec-valor">Luis Ramírez</div>
        </div>
    </div>

    <div class="rec-etiqueta mt-4">Comisión total de la factura</div>
    <div class="rec-valor mb-3">$1,250.00</div>

    <asp:Panel ID="pnlTablaRecibosFactura" runat="server" CssClass="cob-panel p-3">
        <div class="cob-scroll">
            <table class="table cob-tabla">
                <thead>
                    <tr>
                        <th scope="col">Número de Recibo</th>
                        <th scope="col">Vencimiento</th>
                        <th scope="col">Monto</th>
                        <th scope="col">Estado</th>
                        <th scope="col">Fecha de pago</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><asp:Literal ID="litDetRecibo1" runat="server" /></td>
                        <td>30/08/2025</td>
                        <td>$5,000.00</td>
                        <td class="cob-vencido">Vencido</td>
                        <td class="text-muted">---------</td>
                    </tr>
                    <tr>
                        <td><asp:Literal ID="litDetRecibo2" runat="server" /></td>
                        <td>30/09/2025</td>
                        <td>$5,000.00</td>
                        <td class="cob-pendiente">Pendiente</td>
                        <td class="text-muted">---------</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </asp:Panel>

</asp:Panel>
<%-- ========================= DETALLE DEL RECIBO =========================
     Se abre con el ojito de la tabla de recibos. --%>
<asp:Panel ID="pnlDetalleRecibo" runat="server" Visible="false">

    <div class="d-flex align-items-center gap-3 mb-3">
        <asp:LinkButton ID="lnkVolverRecibos" runat="server" CssClass="cob-volver"
            ToolTip="Regresar a recibos" OnClick="lnkVolverRecibos_Click">
            <i class="bi bi-arrow-left"></i>
        </asp:LinkButton>
        <h2 class="cob-titulo mb-0">
            Recibo <span class="rec-clave"><asp:Literal ID="litReciboClave" runat="server" /></span>
        </h2>
    </div>

    <div class="d-flex gap-2 mb-4">
        <asp:LinkButton ID="lnkPagar" runat="server" CssClass="btn btn-sm rec-pagar">Pagar</asp:LinkButton>
        <asp:LinkButton ID="lnkDescargarDetalle" runat="server" CssClass="btn btn-sm rec-descargar">
            Descargar detalle <i class="bi bi-chevron-down ms-1"></i>
        </asp:LinkButton>
        <asp:LinkButton ID="lnkEnviarCorreoRecibo" runat="server" CssClass="btn btn-sm btn-light border">
            Enviar por correo
        </asp:LinkButton>
    </div>

    <div class="rec-seccion mb-3">Detalles del recibo</div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-4">
            <div class="rec-etiqueta">Póliza:</div>
            <div class="rec-valor">AX123456</div>
        </div>
        <div class="col-md-4">
            <div class="rec-etiqueta">Certificado:</div>
            <div class="rec-valor">CRT-02</div>
        </div>
        <div class="col-md-4">
            <div class="rec-etiqueta">Numero recibo:</div>
            <div class="rec-valor"><asp:Literal ID="litReciboCorto" runat="server" /></div>
        </div>
    </div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-4">
            <div class="rec-etiqueta">Frecuencia de pago</div>
            <div class="rec-valor">Mensual</div>
        </div>
        <div class="col-md-4">
            <div class="rec-etiqueta">Registro</div>
            <div class="rec-valor">Mensual</div>
        </div>
        <div class="col-md-4">
            <div class="rec-etiqueta">Tipo de cambio</div>
            <div class="rec-valor">Pago de Prima</div>
        </div>
    </div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-4">
            <div class="rec-etiqueta">Tipo de Cobranza</div>
            <div class="rec-valor">Tarjeta de Crédito</div>
        </div>
        <div class="col-md-4">
            <div class="rec-etiqueta">Concepto</div>
            <div class="rec-valor">Mercancia de don julio</div>
        </div>
        <div class="col-md-4">
            <div class="rec-etiqueta">Contratante</div>
            <div class="rec-valor">María Rodríguez</div>
        </div>
    </div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-4">
            <div class="rec-etiqueta">Asegurado</div>
            <div class="rec-valor">50 Refrigeradores</div>
        </div>
        <div class="col-md-4">
            <div class="rec-etiqueta">Dias vencidos</div>
            <div class="rec-valor">31</div>
        </div>
        <div class="col-md-4">
            <div class="rec-etiqueta">Agente</div>
            <div class="rec-valor">Carlos Pérez</div>
        </div>
    </div>

    <div class="row g-3 rec-grupo">
        <div class="col-md-4">
            <div class="rec-etiqueta">Monto</div>
            <div class="rec-valor">$ 500.00</div>
        </div>
    </div>

    <div class="rec-seccion mt-4 mb-3">Comisiones</div>

    <div class="row g-3">
        <div class="col-md-3">
            <div class="rec-etiqueta">NETA:</div>
            <div class="rec-valor">$ 000.00</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">BONO</div>
            <div class="rec-valor">$ 000.00</div>
        </div>
    </div>
    <div class="row g-3 mt-1">
        <div class="col-md-3">
            <div class="rec-etiqueta">RECARGO:</div>
            <div class="rec-valor">$ 000.00</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">BONIFICACION</div>
            <div class="rec-valor">$ 000.00</div>
        </div>
    </div>
    <div class="row g-3 mt-1">
        <div class="col-md-3">
            <div class="rec-etiqueta">GASTOS:</div>
            <div class="rec-valor">$ 000.00</div>
        </div>
        <div class="col-md-3">
            <div class="rec-etiqueta">TOTAL</div>
            <div class="rec-valor">$ 000.00</div>
        </div>
    </div>

</asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
