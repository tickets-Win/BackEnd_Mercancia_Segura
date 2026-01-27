<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminCotizaciones.aspx.vb" Inherits="WebAdmin.AdminCotizaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">

    <style>
        .icon-btn {
            background: none;
            border: none;
            padding: 6px;
            font-size: 20px;
            color: #000;
            cursor: pointer;
        }

            .icon-btn:hover {
                color: #555;
            }

        .filtro-estilo {
            background-color: #f0f4f8;
            border: 1px solid #cbd5e1;
            border-radius: 0.375rem;
            padding-right: 1.5rem;
            cursor: pointer;
            font-size: 0.875rem;
            font-weight: 500;
            color: #334155;
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            background-image: url('data:image/svg+xml;utf8,<svg fill="%23334555" height="20" viewBox="0 0 20 20" width="20" xmlns="http://www.w3.org/2000/svg"><path d="M5.516 7.548a.625.625 0 0 1 .884 0L10 11.154l3.6-3.606a.625.625 0 0 1 .884.884l-4.134 4.134a.625.625 0 0 1-.884 0L5.516 8.432a.625.625 0 0 1 0-.884z"/></svg>');
            background-repeat: no-repeat;
            background-position: right 0.75rem center;
            background-size: 1rem;
        }

            .filtro-estilo:focus {
                outline: none;
                border-color: #3b82f6;
                box-shadow: 0 0 0 3px rgba(59,130,246,0.3);
            }

        .titulo-cuota {
            min-height: 38px;
            display: flex;
            align-items: center;
        }

        .textarea {
            resize: none;
            background-color: #f8f9fa;
            min-height: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Cotizaciones</h2>
            <asp:Button ID="btnAgregarCotizacion" runat="server" CssClass="btn btn-primary btn-add" Text="Agregar Cotización" OnClick="btnAgregarCotizacion_Click" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarCotizacion" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar cotizaciones..."></asp:TextBox>
        </div>
        <div class="d-flex justify-content-left mb-4">
            <label for="ddlTipoPolizas" class="form-label visually-hidden">Filtrar</label>
            <asp:DropDownList ID="ddlTipoPolizas" runat="server" CssClass="form-select form-select-sm filtro-estilo w-auto">
                <asp:ListItem Text="-- Todos --" Value="0" />
                <asp:ListItem Text="Hoy" Value="1" />
                <asp:ListItem Text="Mes actual" Value="2" />
                <asp:ListItem Text="Mes anterior" Value="3" />
                <asp:ListItem Text="Canceladas" Value="4" />
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlTabla" runat="server">
        <table class="table table-bordered">
            <thead class="table-light">
                <tr>
                    <th scope="col">Clave</th>
                    <th scope="col">Cliente</th>
                    <th scope="col">Estatus</th>
                    <th scope="col">Fecha Cotización</th>
                    <th scope="col">Tipo</th>
                    <th scope="col">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">COT-01</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Aceptar"><i class="bi bi-check-circle"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-02</th>
                    <td>Ana García</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Aceptar"><i class="bi bi-check-circle"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-03</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Aceptar"><i class="bi bi-check-circle"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-04</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Aceptar"><i class="bi bi-check-circle"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-05</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Aceptar"><i class="bi bi-check-circle"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-06</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Aceptar"><i class="bi bi-check-circle"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-07</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Aceptar"><i class="bi bi-check-circle"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlFormularioCotizaciones" runat="server" CssClass="card p-4 mt-4" Visible="false">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label></h2>

            <div>
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0" ForeColor="White" Text="Cancelar" />
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4" ForeColor="White" Text="Guardar" />
            </div>
        </div>
        <h5 class="border-bottom pb-2">Datos de la cotización</h5>
        <div class="row g-3 mt-2">
            <div class="col-md-4">
                <label class="form-label">Tipo de Cotización</label>
                <asp:DropDownList ID="ddlTipoCotizacion" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCotizacion_SelectedIndexChanged">
                    <asp:ListItem Text="Mercancia" Value="Mercancia"></asp:ListItem>
                    <asp:ListItem Text="Contenedor" Value="Contenedor"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Fecha Cotización</label>
                <asp:TextBox ID="txtFechaCotizacion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Cliente</label>
                <asp:DropDownList ID="ddlCliente" CssClass="form-select" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Edgar Pérez Garrido" Value="Mercancia"></asp:ListItem>
                    <asp:ListItem Text="Marco Salas" Value="Contenedor"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Nombre Interno Póliza</label>
                <asp:DropDownList ID="ddlNombreInternoPoliza" CssClass="form-select" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Nombre Interno 1" Value="Mercancia"></asp:ListItem>
                    <asp:ListItem Text="Nombre Interno 2" Value="Contenedor"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Beneficiario Preferente</label>
                <asp:DropDownList ID="ddlBeneficiarioPreferente" runat="server" CssClass="form-select">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Vigencia del</label>
                <asp:TextBox ID="txtVigenciaDel" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Vigencia Hasta </label>
                <asp:TextBox ID="txtVigenciaHasta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Moneda</label>
                <asp:TextBox ID="txtMoneda" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Suma Asegurada</label>
                <asp:TextBox ID="txtSumaAsegurada" runat="server" CssClass="form-control"></asp:TextBox>
            </div>


            <asp:Panel ID="pnlContenedor" runat="server" Visible="false">
                <div class="row g-3">
                    <div class="col-md-4">
                        <label class="form-label">Tamaño/Tipo de contenedor</label>
                        <asp:TextBox ID="txtTamanoTipoContenedor" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label"># Contenedores</label>
                        <asp:TextBox ID="txtContenedores" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Opción de cuota</label>
                        <asp:TextBox ID="txtOpcionCuota" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Tárifa</label>
                        <asp:TextBox ID="txtTarifa" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlMercanciaFormulario" runat="server">
                <div class="row g-3">
                    <div class="col-md-4">
                        <label class="form-label">Cotización Cliente #</label>
                        <asp:TextBox ID="txtSubRamo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Tránsito</label>
                        <asp:DropDownList ID="ddlTransito" runat="server" CssClass="form-select">
                            <asp:ListItem>Internacional</asp:ListItem>
                            <asp:ListItem>Nacional</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Clasificación</label>
                        <asp:DropDownList ID="ddlClasificacion" runat="server" CssClass="form-select">
                            <asp:ListItem>Alimentos y Bebidas</asp:ListItem>
                            <asp:ListItem>Alimentos y Bebidas</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Subclasificación</label>
                        <asp:DropDownList ID="ddlSubclasificación" runat="server" CssClass="form-select">
                            <asp:ListItem>Alimentos y Bebidas</asp:ListItem>
                            <asp:ListItem>Alimentos y Bebidas</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Descripción de mercancía</label>
                        <asp:TextBox ID="txtDescripcionMercancia" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Tipo de Empaque</label>
                        <asp:TextBox ID="txtTipoEmpaque" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Origen</label>
                        <asp:TextBox ID="txtOrigen" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Destino</label>
                        <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Medios de conducción</label>
                        <asp:TextBox ID="txtMediosConduccion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Medios de transporte</label>
                        <asp:TextBox ID="txtMedioTransporte" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-8">
                        <label class="form-label">Observaciones</label>
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control textarea" TextMode="MultiLine" Rows="4" Placeholder="Notas adicionales de la cotización"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMercancia" runat="server">
        <div class="row mt-3">
            <div class="col-md-12">
                <div class="card p-3 shadow-sm">
                    <h5>Coberturas de la cotización</h5>
                    <div class="row g-3 mt-2">

                        <div class="row mt-3 gx-0">
                            <div class="col-md-12 px-2">
                                <div class="card shadow-sm bg-light w-100">
                                    <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('coberturas')">
                                        <h4 class="mb-0">Seleccione una cobertura</h4>
                                        <i id="icon-coberturas" class="bi bi-chevron-down"></i>
                                    </div>

                                    <div id="coberturas" class="collapse-content px-3 pb-3" style="display: none;">
                                        <asp:DropDownList ID="ddlCoberturas" runat="server" CssClass="form-select mb-3">
                                            <asp:ListItem>Cobertura  1</asp:ListItem>
                                            <asp:ListItem>Cobertura 2</asp:ListItem>
                                        </asp:DropDownList>

                                        <div class="d-flex justify-content-end mb-3">
                                            <asp:Button ID="btnAgregarCobertura" runat="server" CssClass="btn btn-primary" Text="Agregar" />
                                        </div>

                                        <table class="table table-bordered">
                                            <thead class="table-light">
                                                <tr>
                                                    <th>Cobertura</th>
                                                    <th>Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Mercancía General</td>
                                                    <td>
                                                        <button class="btn btn-warning btn-sm me-2">Editar</button>
                                                        <button class="btn btn-danger btn-sm">Eliminar</button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Electrónicos</td>
                                                    <td>
                                                        <button class="btn btn-warning btn-sm me-2">Editar</button>
                                                        <button class="btn btn-danger btn-sm">Eliminar</button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlBienesAsegurados" runat="server" Visible="false">
                        <h5 class="mt-4">Bienes asegurados</h5>
                        <div class="row g-3 mt-2">

                            <div class="row mt-3 gx-0">
                                <div class="col-md-12 px-2">
                                    <div class="card shadow-sm bg-light w-100">
                                        <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesasegurados')">
                                            <h4 class="mb-0">Seleccione un bien asegurados</h4>
                                            <i id="icon-bienesasegurados" class="bi bi-chevron-down"></i>
                                        </div>

                                        <div id="bienesasegurados" class="collapse-content px-3 pb-3" style="display: none;">
                                            <asp:DropDownList ID="ddlbienesasegurados" runat="server" CssClass="form-select mb-3">
                                                <asp:ListItem>bienes asegurados  1</asp:ListItem>
                                                <asp:ListItem>bienes  asegurados 2</asp:ListItem>
                                            </asp:DropDownList>

                                            <div class="d-flex justify-content-end mb-3">
                                                <asp:Button ID="btnbienesasegurados" runat="server" CssClass="btn btn-primary" Text="Agregar" />
                                            </div>

                                            <table class="table table-bordered">
                                                <thead class="table-light">
                                                    <tr>
                                                        <th>Bienes Asegurados</th>
                                                        <th>Acciones</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>Mercancía General</td>
                                                        <td>
                                                            <button class="btn btn-warning btn-sm me-2">Editar</button>
                                                            <button class="btn btn-danger btn-sm">Eliminar</button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Electrónicos</td>
                                                        <td>
                                                            <button class="btn btn-warning btn-sm me-2">Editar</button>
                                                            <button class="btn btn-danger btn-sm">Eliminar</button>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlCuotaAplicableMercancia" runat="server">
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Medidas de seguridad adicionales</h6>
                                            <asp:TextBox ID="txtMedidasSeguridad" runat="server" CssClass="form-control"
                                                placeholder=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6 class="titulo-cuota">Deducibles</h6>
                                            <asp:TextBox ID="txtDeducibles" runat="server" CssClass="form-control"
                                                placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Cuota Aplicable</h6>
                                            <div class="d-flex justify-content-between small mb-1" style="padding: 0; margin: 0;">

                                                <label class="form-check d-flex align-items-center gap-1 m-0" style="width: 48%;">
                                                    <input type="checkbox" id="chkCuotaAplicableN" runat="server" class="form-check-input" />
                                                    <span>Nacional</span>
                                                </label>

                                                <label class="form-check d-flex align-items-center gap-1 m-0 justify-content-end" style="width: 48%;">
                                                    <input type="checkbox" id="chkCuotaAplicableI" runat="server" class="form-check-input" />
                                                    <span>Internacional</span>
                                                </label>
                                            </div>

                                            <asp:TextBox ID="txtCuotaAplicable" runat="server" CssClass="form-control"
                                                placeholder="%"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Cuota Mínima</h6>
                                            <div class="d-flex justify-content-between small mb-1" style="padding: 0; margin: 0;">
                                                <label class="form-check d-flex align-items-center gap-1 m-0" style="width: 48%;">
                                                    <input type="checkbox" id="chkCuotaMinimaN" runat="server" class="form-check-input" />
                                                    <span>Nacional</span>
                                                </label>

                                                <label class="form-check d-flex align-items-center gap-1 m-0 justify-content-end" style="width: 48%;">
                                                    <input type="checkbox" id="chkCuotaMinimaI" runat="server" class="form-check-input" />
                                                    <span>Internacional</span>
                                                </label>
                                            </div>
                                            <asp:TextBox ID="txtCuotaMinima" runat="server" CssClass="form-control"
                                                placeholder=""></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Tipo de cambio para cotizar</h6>
                                            <asp:TextBox ID="txtTipoCambio" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Moneda para Cotizar</h6>
                                            <asp:DropDownList ID="ddlMonedaCotizar" runat="server" CssClass="form-select">
                                                <asp:ListItem>Nacional</asp:ListItem>
                                                <asp:ListItem>Dólares</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Prima y servicios de seguramiento</h6>
                                            <asp:TextBox ID="txtPrimaYSeguramiento" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6 class="titulo-cuota">Gastos de expedición</h6>
                                            <asp:TextBox ID="txtGastosExpedicion" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Subtotal</h6>
                                            <asp:TextBox ID="txtSubtotal" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">IVA</h6>
                                            <asp:TextBox ID="txtIVA" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Total seguro mercancía</h6>
                                            <asp:TextBox ID="txtTotalSeguroMercancia" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Total seguro contenedor</h6>
                                            <asp:TextBox ID="txtTotalSeguroContenedor" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col col-md-6">
                                        <h6 class="titulo-cuota">Total a Pagar</h6>
                                        <asp:TextBox ID="txtTotalPagar" runat="server" CssClass="form-control"
                                            placeholder="$0.00"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlCuotaAplicableContenedor" runat="server" Visible="false">
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <h5 class="mt-1">Cuota Aplicable Contenedor</h5>
                                <div class="card p-3 shadow-sm">
                                    <h6>Contenedores Secos</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtCuotaSecos">Cuota (%)</label>
                                            <asp:TextBox ID="txtCuotaSecos" runat="server" CssClass="form-control"
                                                placeholder="Cuota (%)"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <label class="form-label" for="ddlTipoTarifaSecos">Tipo Tarifa</label>
                                            <asp:DropDownList ID="ddlTipoTarifaSecos" runat="server" CssClass="form-select">
                                                <asp:ListItem>ejemplo</asp:ListItem>
                                                <asp:ListItem>ejemplo1</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <h6>Contenedores Refrigerados</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtCuotaRefrigerados">Cuota (%)</label>
                                            <asp:TextBox ID="txtCuotaRefrigerados" runat="server" CssClass="form-control"
                                                placeholder="Cuota (%)"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <label class="form-label" for="ddlTipoRefrigerados">Tipo Tarifa</label>
                                            <asp:DropDownList ID="ddlTipoRefrigerados" runat="server" CssClass="form-select">
                                                <asp:ListItem>ejemplo</asp:ListItem>
                                                <asp:ListItem>ejemplo 1</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <h6>Isotanques</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtCuota2">Cuota (%)</label>
                                            <asp:TextBox ID="txtCuota2" runat="server" CssClass="form-control"
                                                placeholder="Cuota (%)"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <label class="form-label" for="ddlTipoIsotaques">Tipo Tarifa</label>
                                            <asp:DropDownList ID="ddlTipoIsotaques" runat="server" CssClass="form-select">
                                                <asp:ListItem>ejemplo</asp:ListItem>
                                                <asp:ListItem>ejemplo1</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtTotalTarifa">Total Tarifa</label>
                                            <asp:TextBox ID="txtTotalTarifa" runat="server" CssClass="form-control"
                                                placeholder="$ 0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtGastosExpedicionContenedor">Gastos de expedición</label>
                                            <asp:TextBox ID="txtGastosExpedicionContenedor" runat="server" CssClass="form-control"
                                                placeholder="$ 0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtIvaContenedor">IVA</label>
                                            <asp:TextBox ID="txtIvaContenedor" runat="server" CssClass="form-control"
                                                placeholder="$ 0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtTotalContenedor">Total</label>
                                            <asp:TextBox ID="txtTotalContenedor" runat="server" CssClass="form-control"
                                                placeholder="$ 0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script>
        function toggleCollapse(id) {
            const content = document.getElementById(id);
            const icon = document.getElementById('icon-' + id);

            if (content.style.display === 'none') {
                content.style.display = 'block';
                icon.classList.remove('bi-chevron-down');
                icon.classList.add('bi-chevron-up');
            } else {
                content.style.display = 'none';
                icon.classList.remove('bi-chevron-up');
                icon.classList.add('bi-chevron-down');
            }
        }
    </script>
</asp:Content>
