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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                        <td><a href="javascript:void(0)" class="cob-enlace">Vnt-01</a></td>
                        <td>CERT-01</td>
                        <td>cot-01</td>
                        <td>Ana García</td>
                        <td><a href="javascript:void(0)" class="cob-enlace">Rec-01</a></td>
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
                        <td><a href="javascript:void(0)" class="cob-enlace">Vnt-02</a></td>
                        <td>CERT-02</td>
                        <td>cot-02</td>
                        <td>Juan Pérez</td>
                        <td><a href="javascript:void(0)" class="cob-enlace">Rec-02</a></td>
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
                        <td><a href="javascript:void(0)" class="cob-enlace">Vnt-03</a></td>
                        <td>CERT-03</td>
                        <td>cot-03</td>
                        <td>Sofía Martínez</td>
                        <td><a href="javascript:void(0)" class="cob-enlace">Rec-03</a></td>
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
                        <td><a href="javascript:void(0)" class="cob-enlace">Vnt-04</a></td>
                        <td>CERT-04</td>
                        <td>cot-04</td>
                        <td>Diego Sánchez</td>
                        <td><a href="javascript:void(0)" class="cob-enlace">Rec-04</a></td>
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
                        <td><a href="javascript:void(0)" class="cob-enlace">Vnt-05</a></td>
                        <td>CERT-05</td>
                        <td>cot-05</td>
                        <td>Isabel Torres</td>
                        <td><a href="javascript:void(0)" class="cob-enlace">Rec-05</a></td>
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

</asp:Content>
