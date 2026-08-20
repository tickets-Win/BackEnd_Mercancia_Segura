<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminReportes.aspx.vb" Inherits="WebAdmin.AdminReportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />

    <style>
        /* Estilos propios de Reportes. Van aqui y no en site.css para no afectar
           al resto de las pantallas: .card-body global centra todo su contenido
           y las tarjetas de este tablero son alineadas a la izquierda. */
        .rep-titulo {
            color: #17406d;
            font-weight: 700;
        }

        .rep-panel {
            background: #fff;
            border: 1px solid #e3e8ef;
            border-radius: .5rem;
        }

        .rep-panel-titulo {
            font-size: .8125rem;
            font-weight: 600;
            color: #475569;
        }

        .rep-label {
            font-size: .75rem;
            color: #64748b;
            margin-bottom: .25rem;
        }

        /* Tarjetas de indicadores */
        .rep-kpi {
            background: #fff;
            border: 1px solid #e3e8ef;
            border-radius: .5rem;
            padding: 1rem 1.25rem;
            height: 100%;
        }

        .rep-kpi-nombre {
            font-size: .8125rem;
            color: #475569;
            margin-bottom: .35rem;
        }

        .rep-kpi-dato {
            font-size: 2rem;
            font-weight: 700;
            color: #17406d;
            line-height: 1.1;
        }

        .rep-kpi-pie {
            font-size: .75rem;
            color: #94a3b8;
        }

        /* Graficas: SVG y CSS puros, sin libreria externa. Al conectar los datos
           reales solo cambian las alturas de las barras y los puntos del trazo. */
        .rep-grafica {
            height: 130px;
        }

        .rep-barras {
            display: flex;
            align-items: flex-end;
            justify-content: space-between;
            gap: 4px;
            height: 100%;
        }

            .rep-barras span {
                flex: 1;
                background: #2f9e5c;
                border-radius: 2px 2px 0 0;
            }

        .rep-eje {
            font-size: .625rem;
            color: #94a3b8;
        }

        .rep-leyenda {
            font-size: .6875rem;
            color: #475569;
        }

            .rep-leyenda i {
                font-size: .5rem;
                vertical-align: middle;
            }

        /* Tabla de resultados */
        .rep-tabla {
            font-size: .8125rem;
        }

            .rep-tabla thead th {
                background: #f8fafc;
                color: #475569;
                font-weight: 600;
                font-size: .75rem;
                border-bottom: 1px solid #e3e8ef;
                white-space: nowrap;
            }

            .rep-tabla td {
                vertical-align: middle;
                border-bottom: 1px solid #f1f5f9;
            }

        .rep-folio {
            color: #17406d;
            font-weight: 600;
        }

        .rep-enlace {
            color: #2563eb;
            text-decoration: none;
        }

            .rep-enlace:hover {
                text-decoration: underline;
            }

        .rep-badge {
            font-size: .6875rem;
            font-weight: 600;
            padding: .2rem .55rem;
            border-radius: 1rem;
        }

        .rep-badge-activo {
            background: #dcfce7;
            color: #166534;
        }

        .rep-badge-vencido {
            background: #fef3c7;
            color: #92400e;
        }

        .rep-badge-cancelado {
            background: #fee2e2;
            color: #991b1b;
        }

        .rep-pie {
            font-size: .75rem;
            color: #94a3b8;
        }

        /* La tabla nunca debe empujar el ancho de la pagina */
        .rep-scroll {
            overflow-x: auto;
        }

        /* ------------------------- Impresion -------------------------
           Al imprimir sobra todo lo que no es el reporte: el menu lateral,
           los filtros y los propios botones. */
        @media print {
            nav, .navbar, .sidebar, aside, header, footer,
            .rep-no-imprimir {
                display: none !important;
            }

            body, .container, .container-fluid, main {
                margin: 0 !important;
                padding: 0 !important;
                width: 100% !important;
                max-width: 100% !important;
            }

            .rep-panel, .rep-kpi {
                border: 1px solid #999 !important;
                break-inside: avoid;
            }

            .rep-scroll {
                overflow: visible !important;
            }

            .rep-tabla {
                font-size: 10pt;
            }

                .rep-tabla thead {
                    display: table-header-group;
                }

                .rep-tabla tr {
                    break-inside: avoid;
                }

            a[href]:after {
                content: "";
            }
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- ============================ ENCABEZADO ============================ --%>
    <div class="mb-4">
        <h2 class="rep-titulo mb-1">Reportes</h2>
        <p class="text-muted small mb-0">Consulta y análisis de información operativa</p>
    </div>

    <asp:Panel ID="pnlAviso" runat="server" Visible="false">
        <asp:Label ID="lblAviso" runat="server"></asp:Label>
    </asp:Panel>

    <%-- ======================= FILTROS DE BUSQUEDA ======================== --%>
    <asp:Panel ID="pnlFiltros" runat="server" CssClass="rep-panel p-3 mb-4 rep-no-imprimir">
        <div class="rep-panel-titulo mb-3">Filtros de búsqueda</div>

        <div class="row g-3">
            <div class="col-md-4">
                <div class="rep-label">Fecha inicio</div>
                <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date" CssClass="form-control form-control-sm"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <div class="rep-label">Fecha fin</div>
                <asp:TextBox ID="txtFechaFin" runat="server" TextMode="Date" CssClass="form-control form-control-sm"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <div class="rep-label">Cliente</div>
                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList>
            </div>

            <div class="col-md-4">
                <div class="rep-label">Aseguradora</div>
                <asp:DropDownList ID="ddlAseguradora" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <div class="rep-label">Tipo de reporte</div>
                <asp:DropDownList ID="ddlTipoReporte" runat="server" CssClass="form-select form-select-sm">
                    <asp:ListItem Text="Todos los tipos" Value="" />
                    <asp:ListItem Text="Mercancía" Value="Mercancía" />
                    <asp:ListItem Text="Contenedor" Value="Contenedor" />
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <div class="rep-label">Estatus</div>
                <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList>
            </div>
        </div>

        <div class="d-flex justify-content-end gap-2 mt-3">
            <asp:LinkButton ID="lnkLimpiar" runat="server" CssClass="btn btn-sm btn-light border"
                OnClick="lnkLimpiar_Click">
                <i class="bi bi-x-lg me-1"></i>Limpiar
            </asp:LinkButton>
            <asp:LinkButton ID="lnkBuscar" runat="server" CssClass="btn btn-sm btn-primary"
                OnClick="lnkBuscar_Click">
                <i class="bi bi-search me-1"></i>Buscar
            </asp:LinkButton>
        </div>
    </asp:Panel>

    <%-- ========================== INDICADORES ============================= --%>
    <div class="row g-3 mb-4">
        <div class="col-md-3 col-sm-6">
            <div class="rep-kpi">
                <div class="rep-kpi-nombre">Total de certificados</div>
                <div class="rep-kpi-dato"><asp:Literal ID="litTotalCertificados" runat="server" Text="—" /></div>
                <div class="rep-kpi-pie">activos e históricos</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="rep-kpi">
                <div class="rep-kpi-nombre">Total de pólizas</div>
                <div class="rep-kpi-dato"><asp:Literal ID="litTotalPolizas" runat="server" Text="—" /></div>
                <div class="rep-kpi-pie">maestras y vigentes</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="rep-kpi">
                <div class="rep-kpi-nombre">Total de siniestros</div>
                <div class="rep-kpi-dato"><asp:Literal ID="litTotalSiniestros" runat="server" Text="—" /></div>
                <div class="rep-kpi-pie">registrados en periodo</div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6">
            <div class="rep-kpi">
                <div class="rep-kpi-nombre">Suma asegurada total</div>
                <div class="rep-kpi-dato"><asp:Literal ID="litSumaAsegurada" runat="server" Text="—" /></div>
                <div class="rep-kpi-pie">acumulado vigente</div>
            </div>
        </div>
    </div>

    <%-- =========================== GRAFICAS ===============================
         Las tres se arman en el code-behind con los mismos datos ya filtrados;
         siguen siendo SVG y CSS puros, sin libreria externa. --%>
    <div class="row g-3 mb-4">

        <div class="col-lg-4 col-md-6">
            <div class="rep-panel p-3 h-100">
                <div class="rep-panel-titulo mb-3">Certificados emitidos por mes</div>
                <div class="rep-grafica">
                    <div class="rep-barras">
                        <asp:Literal ID="litBarras" runat="server" />
                    </div>
                </div>
                <div class="rep-eje d-flex justify-content-between mt-2">
                    <asp:Literal ID="litEjeBarras" runat="server" />
                </div>
            </div>
        </div>

        <div class="col-lg-4 col-md-6">
            <div class="rep-panel p-3 h-100">
                <div class="rep-panel-titulo mb-3">Siniestros registrados por mes</div>
                <div class="rep-grafica">
                    <asp:Literal ID="litLinea" runat="server" />
                </div>
                <div class="rep-eje d-flex justify-content-between mt-2">
                    <asp:Literal ID="litEjeLinea" runat="server" />
                </div>
            </div>
        </div>

        <div class="col-lg-4 col-md-12">
            <div class="rep-panel p-3 h-100">
                <div class="rep-panel-titulo mb-3">Estado de certificados</div>
                <div class="d-flex align-items-center justify-content-center rep-grafica">
                    <asp:Literal ID="litDona" runat="server" />
                </div>
                <div class="rep-leyenda d-flex justify-content-center gap-3 mt-2">
                    <asp:Literal ID="litLeyenda" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <%-- ======================= TABLA DE RESULTADOS ======================== --%>
    <asp:Panel ID="pnlResultados" runat="server" CssClass="rep-panel p-3">

        <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mb-3">
            <div>
                <span class="rep-panel-titulo">Tabla de resultados</span>
                <span class="rep-pie ms-2"><asp:Literal ID="litEncontrados" runat="server" /></span>
            </div>
            <div class="d-flex gap-2 rep-no-imprimir">
                <asp:LinkButton ID="lnkExportarPdf" runat="server" CssClass="btn btn-sm btn-light border"
                    ToolTip="Abre el diálogo de impresión; elige 'Guardar como PDF' para obtener el archivo"
                    OnClick="lnkImprimir_Click">
                    <i class="bi bi-file-earmark-pdf me-1"></i>Exportar PDF
                </asp:LinkButton>
                <asp:LinkButton ID="lnkExportarExcel" runat="server" CssClass="btn btn-sm btn-light border"
                    ToolTip="Descarga los resultados filtrados en CSV, que Excel abre directo"
                    OnClick="lnkExportarExcel_Click">
                    <i class="bi bi-file-earmark-excel me-1"></i>Exportar Excel
                </asp:LinkButton>
                <asp:LinkButton ID="lnkImprimir" runat="server" CssClass="btn btn-sm btn-light border"
                    ToolTip="Imprime el reporte completo; desde el diálogo puedes guardarlo como PDF"
                    OnClick="lnkImprimir_Click">
                    <i class="bi bi-printer me-1"></i>Imprimir reporte
                </asp:LinkButton>
            </div>
        </div>

        <div class="rep-scroll">
            <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="False"
                CssClass="table rep-tabla mb-0"
                AllowPaging="True" PageSize="8"
                OnPageIndexChanging="gvResultados_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="Folio" HeaderText="Folio" />
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="Poliza" HeaderText="Póliza" />
                    <asp:BoundField DataField="Certificado" HeaderText="Certificado" />
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo de mercancía" />
                    <asp:TemplateField HeaderText="Estatus">
                        <ItemTemplate>
                            <span class='<%# ClaseEstatus(Eval("Estatus")) %>'><%# Eval("Estatus") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FechaEmision" HeaderText="Fecha de emisión" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="SumaAsegurada" HeaderText="Suma asegurada" DataFormatString="{0:C0}" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="text-muted py-3">No hay resultados con esos filtros.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mt-3">
            <span class="rep-pie"><asp:Literal ID="litMostrando" runat="server" /></span>
        </div>
    </asp:Panel>

</asp:Content>
