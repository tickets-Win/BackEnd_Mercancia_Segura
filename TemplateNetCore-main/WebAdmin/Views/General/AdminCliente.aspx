<%@ Page Title="" Language="vb" AutoEventWireup="false" EnableEventValidation="false" MasterPageFile="~/Default.Master" CodeBehind="AdminCliente.aspx.vb" Inherits="WebAdmin.AdminCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
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

        .nav-tabs .nav-link.active {
            color: #000 !important;
            background-color: #f8f9fa !important;
            border-color: #dee2e6 #dee2e6 #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Cliente</h2>
            <asp:Button ID="btnAgregarCliente" runat="server" CssClass="btn btn-primary btn-add"
                Text="Agregar Cliente" OnClick="btnAgregarCliente_Click" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarCliente" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar clientes..."></asp:TextBox>
        </div>
        <div class="d-flex justify-content-left mb-4">
            <label for="ddlTipoEstatusCliente" class="form-label visually-hidden">Filtrar</label>
            <asp:DropDownList ID="ddlTipoEstatusCliente" runat="server" CssClass="form-select form-select-sm filtro-estilo w-auto">
                <asp:ListItem Text="-- Todos --" Value="0" />
                <asp:ListItem Text="Activo" Value="1" />
                <asp:ListItem Text="Suspendido" Value="2" />
                <asp:ListItem Text="Morosos" Value="3" />
                <asp:ListItem Text="Más de 3 meses sin comprar" Value="4" />
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlTabla" runat="server">
        <table class="table table-bordered">
            <thead class="table-light">
                <tr>
                    <th scope="col">Clave</th>
                    <th scope="col">Estatus</th>
                    <th scope="col">Nombre</th>
                    <th scope="col">Télefono</th>
                    <th scope="col">Fecha Registro</th>
                    <th scope="col">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">F-01</th>
                    <td>Activo</td>
                    <td>Carlos López</td>
                    <td>555-1234</td>
                    <td>2023-01-15</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">F-02</th>
                    <td>Suspendido</td>
                    <td>Ana García</td>
                    <td>555-1234</td>
                    <td>2023-01-15</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">F-03</th>
                    <td>Activo</td>
                    <td>Roberto Pérez</td>
                    <td>555-1234</td>
                    <td>2023-01-15</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">F-04</th>
                    <td>Moroso</td>
                    <td>Sofía Martínez</td>
                    <td>555-1234</td>
                    <td>2023-01-15</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">F-05</th>
                    <td>Suspendido</td>
                    <td>Carlos López</td>
                    <td>555-1234</td>
                    <td>2023-01-15</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">F-06</th>
                    <td>Activo</td>
                    <td>Javier Ruiz</td>
                    <td>555-1234</td>
                    <td>2023-01-15</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">F-07</th>
                    <td>Activo</td>
                    <td>Elena Sánchez</td>
                    <td>555-1234</td>
                    <td>2023-01-15</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlTabs" runat="server">
        <ul class="nav nav-tabs mb-4 justify-content-center" id="clienteTabs" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="tab-datos" data-bs-toggle="tab"
                    data-bs-target="#panel-datos" type="button" role="tab">
                    Datos Generales</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="tab-credito" data-bs-toggle="tab"
                    data-bs-target="#panel-credito" type="button" role="tab">
                    Gestionar Crédito</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="tab-estado" data-bs-toggle="tab"
                    data-bs-target="#panel-estado" type="button" role="tab">
                    Estado  de Cuenta</button>
            </li>
        </ul>
    </asp:Panel>
    <div class="tab-content">
        <div class="tab-pane fade show active" id="panel-datos" role="tabpanel">
            <asp:Panel ID="pnlFormularioCliente" runat="server" CssClass="card p-4 mt-4" Visible="false">

                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label></h2>

                    <div>
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0" ForeColor="White" Text="Cancelar" />
                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4" ForeColor="White" Text="Guardar" />
                    </div>
                </div>

                <!-- DATOS GENERALES -->
                <h5 class="border-bottom pb-2">Datos generales</h5>

                <div class="row g-3 mt-2">

                    <div class="col-md-4">
                        <label class="form-label">Tipo de persona</label>
                        <asp:DropDownList ID="ddlTipoPersona" CssClass="form-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged">
                            <asp:ListItem Text="Fisica" Value="F"></asp:ListItem>
                            <asp:ListItem Text="Moral" Value="M"></asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Clave</label>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Estatus</label>
                        <asp:DropDownList ID="ddlEstatus" CssClass="form-select" runat="server">
                            <asp:ListItem>Activo</asp:ListItem>
                            <asp:ListItem>Suspendido</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:Panel ID="pnlDatosFiscales" runat="server">
                        <div class="row g-3 mt-2">
                            <div class="col-md-4">
                                <label class="form-label">Apellido paterno</label>
                                <asp:TextBox ID="txtApellidoP" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label class="form-label">Apellido materno</label>
                                <asp:TextBox ID="txtApellidoM" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-4">
                                <label class="form-label">Nombre</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-md-4">
                        <label class="form-label">Regimen Fiscal</label>
                        <asp:TextBox ID="txtRegimenFiscal" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">RFC</label>
                        <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">RFC Génerico</label>
                        <asp:TextBox ID="txtRFCGenerico" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Nombre o Razón Social</label>
                        <asp:TextBox ID="txtNombreRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Fecha registro</label>
                        <asp:TextBox ID="txtFechaRegistro" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Seguro que Contrata</label>
                        <asp:DropDownList ID="ddlSeguroContrata" CssClass="form-select" runat="server">
                            <asp:ListItem Text="-- Selecciona --" Value=""></asp:ListItem>
                            <asp:ListItem>Interno</asp:ListItem>
                            <asp:ListItem>Socio</asp:ListItem>
                            <asp:ListItem>Intermediario</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Tipo de Cuenta</label>
                        <asp:DropDownList ID="ddlTipoCuenta" CssClass="form-select" runat="server">
                            <asp:ListItem Text="-- Selecciona --" Value=""></asp:ListItem>
                            <asp:ListItem>Interno</asp:ListItem>
                            <asp:ListItem>Socio</asp:ListItem>
                            <asp:ListItem>Intermediario</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Origen Cliente</label>
                        <asp:DropDownList ID="ddlOrigenCliente" CssClass="form-select" runat="server">
                            <asp:ListItem Text="-- Selecciona --" Value=""></asp:ListItem>
                            <asp:ListItem>Interno</asp:ListItem>
                            <asp:ListItem>Socio</asp:ListItem>
                            <asp:ListItem>Intermediario</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Sector</label>
                        <asp:DropDownList ID="ddlSector" CssClass="form-select" runat="server">
                            <asp:ListItem Text="-- Selecciona --" Value=""></asp:ListItem>
                            <asp:ListItem>Interno</asp:ListItem>
                            <asp:ListItem>Socio</asp:ListItem>
                            <asp:ListItem>Intermediario</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>

                <!-- DATOS DE CONTACTO -->
                <h5 class="border-bottom pb-2 mt-4">Datos de contacto</h5>

                <div class="row g-3">

                    <div class="col-md-4">
                        <label class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Correo</label>
                        <asp:TextBox ID="txtCorreo" TextMode="Email" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Nacionalidad</label>
                        <asp:TextBox ID="txtNacionalidad" TextMode="Email" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Género</label>
                        <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-select">
                            <asp:ListItem>Masculino</asp:ListItem>
                            <asp:ListItem>Femenino</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">País</label>
                        <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-select">
                            <asp:ListItem>Masculino</asp:ListItem>
                            <asp:ListItem>Femenino</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                            <asp:ListItem>Masculino</asp:ListItem>
                            <asp:ListItem>Femenino</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Municipio</label>
                        <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="form-select">
                            <asp:ListItem>Masculino</asp:ListItem>
                            <asp:ListItem>Femenino</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Colonia</label>
                        <asp:TextBox ID="txtColonia" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Calle</label>
                        <asp:TextBox ID="txtDomicilio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">C.P.</label>
                        <asp:TextBox ID="txtCP" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Número Int.</label>
                        <asp:TextBox ID="txtNumeroInterior" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Número Ext.</label>
                        <asp:TextBox ID="txtNumeroExterior" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Población</label>
                        <asp:DropDownList ID="ddlPoblacion" runat="server" CssClass="form-select">
                            <asp:ListItem>Masculino</asp:ListItem>
                            <asp:ListItem>Femenino</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <h5 class="border-bottom pb-2 mt-4">Correos Adicionales</h5>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <h5>Correos</h5>
                            <div class="card p-3 shadow-sm">
                                <h6>Correos para recepción de facturas</h6>
                                <asp:TextBox ID="txtCorreoFacturas" runat="server" CssClass="form-control mb-3"
                                    placeholder="Ingresar Correos adicionales, separados por comas"></asp:TextBox>
                                <h6>Correos para recepción cp</h6>
                                <asp:TextBox ID="txtCorreosRecepcion" runat="server" CssClass="form-control mb-3"
                                    placeholder="Ingresar Correos adicionales, separados por comas"></asp:TextBox>
                                <h6>Correos adicionales</h6>
                                <asp:TextBox ID="txtCorreosAdicionales" runat="server" CssClass="form-control mb-3"
                                    placeholder="Ingresar Correos adicionales, separados por comas"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <h5>Cuotas</h5>
                            <div class="card p-3 shadow-sm">
                                <h6>Cuota Aplicable</h6>
                                <div class="row mb-3">
                                    <div class="col col-md-6">
                                        <asp:TextBox ID="txtCuotaNacional" runat="server" CssClass="form-control"
                                            placeholder="Nacional (%)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtCuotaInternacional" runat="server" CssClass="form-control"
                                            placeholder="Internacional (%)"></asp:TextBox>
                                    </div>
                                </div>
                                <h6>Cuota Mínima</h6>
                                <div class="row mb-3">
                                    <div class="col col-md-6">
                                        <asp:TextBox ID="txtMinimoNacional" runat="server" CssClass="form-control"
                                            placeholder="Nacional $0.00"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtMinimoInternacional" runat="server" CssClass="form-control"
                                            placeholder="Internacional $0.00"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <h5>Cuota Aplicable Contenedor</h5>
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
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <div class="card p-3 shadow-sm">
                                <h5>Listado de vendedores</h5>
                                <div class="d-flex justify-content-between align-items-center mb-4">
                                    <div>
                                        <button type="button" class="btn btn-primary btn-add" data-bs-toggle="modal" data-bs-target="#modalAgregarVendedor">
                                            Agregar Vendedor
                                        </button>
                                    </div>
                                </div>
                                <table class="table table-bordered">
                                    <thead class="table-light">
                                        <tr>
                                            <th>Tipo Vendedor</th>
                                            <th>Nombre / Razón Social</th>
                                            <th>Comisión</th>
                                            <th>Acciones</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <div class="card p-3 shadow-sm">
                                <h5>Beneficiario Preferente</h5>
                                <div class="d-flex justify-content-between align-items-center mb-4">
                                    <div>
                                        <button type="button" class="btn btn-primary btn-add" data-bs-toggle="modal" data-bs-target="#modalAgregarVendedor">
                                            Agregar Beneficiario
                                        </button>
                                    </div>
                                </div>
                                <table class="table table-bordered">
                                    <thead class="table-light">
                                        <tr>
                                            <th>Nombre Beneficiario / Razón Social</th>
                                            <th>Domicilio</th>
                                            <th>RFC</th>
                                            <th>Acciones</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="tab-pane fade" id="panel-credito" role="tabpanel">
            <asp:Panel ID="pnlGestionarCredito" runat="server" CssClass="card p-4 mt-4" Visible="True">
                <h5 class="border-bottom pb-2">Gestionar Crédito</h5>
                <div class="col-md-12 mb-3">
                    <asp:CheckBox ID="chkHabilitarCampos" runat="server" />
                    <label class="form-check-label">
                        Gestionar Crédito
                    </label>
                </div>
                <div class="row g-3 mt-2">
                    <div class="col-md-4">
                        <label class="form-label">Días de Crédito</label>
                        <asp:TextBox ID="txtDiasCredito" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Método de Pago</label>
                        <asp:TextBox ID="txtMetodoPago" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Número de Cuenta</label>
                        <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Limite de Crédito</label>
                        <asp:TextBox ID="txtLimiteCredito" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Días de Pago</label>
                        <asp:TextBox ID="txtDiasPago" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Días de Revisión</label>
                        <asp:TextBox ID="txtDiasRevision" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Saldo</label>
                        <asp:TextBox ID="txtSaldo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="tab-pane fade" id="panel-estado" role="tabpanel">
            <asp:Panel ID="pnlEstadoCuenta" runat="server" CssClass="card p-4 mt-4" Visible="true">
                <h5 class="border-bottom pb-2">Detalle de Estado de Cuenta</h5>
                <div class="d-flex justify-content-left mb-4 gap-2">
                    <label for="ddlFiltroMes" class="form-label visually-hidden">Filtrar</label>
                    <asp:DropDownList ID="ddlFiltroMes" runat="server" CssClass="form-select form-select-sm filtro-estilo w-auto">
                    </asp:DropDownList>

                    <label for="ddlFiltroAño" class="form-label visually-hidden ms-2">Filtrar</label>
                    <asp:DropDownList ID="ddlFiltroAño" runat="server" CssClass="form-select form-select-sm filtro-estilo w-auto">
                    </asp:DropDownList>
                </div>
                <table class="table table-bordered">
                    <thead class="table-light">
                        <tr>
                            <th>Nombre</th>
                            <th>Mes</th>
                            <th>Fecha de Emisión</th>
                            <th>Saldo</th>
                        </tr>
                    </thead>
                </table>
                <div class="d-flex justify-content-end align-items-center mb-4">
                    <div>
                        <asp:Button ID="btnDescargar" runat="server" CssClass="btn btn-add" BackColor="#adb5bd" ForeColor="Black" Text="Descargar" />
                        <asp:Button ID="btnImprimir" runat="server" CssClass="btn btn-primary btn-add" BackColor="#1294D4" ForeColor="White" Text="Imprimir" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

    <div class="modal fade" id="modalAgregarVendedor" tabindex="-1" aria-labelledby="modalAgregarVendedorLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalAgregarVendedorLabel">Agregar Vendedor</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <h6>Selecciona un vendedor</h6>
                        <asp:DropDownList ID="ddlNombreVendedor" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Edgar Pérez" Value="1" />
                            <asp:ListItem Text="Marco Salas" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <h6>Comisión</h6>
                        <asp:TextBox ID="txtComision" runat="server" CssClass="form-control" placeholder="Comisión (%)"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarVendedor" runat="server" CssClass="btn btn-primary" Text="Guardar" />
                </div>
            </div>
        </div>
    </div>

    <script>
        const ddlMes = document.getElementById('<%= ddlFiltroMes.ClientID %>');
        const meses = ["-- Mes --", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
        meses.forEach((mes, index) => {
            const option = document.createElement("option");
            option.text = mes;
            option.value = index;
            ddlMes.add(option);
        });

        const ddlAño = document.getElementById('<%= ddlFiltroAño.ClientID %>');
        const añoActual = new Date().getFullYear();
        const rango = 10;
        ddlAño.add(new Option("-- Año --", "0"));
        for (let año = añoActual - rango; año <= añoActual + rango; año++) {
            ddlAño.add(new Option(año, año));
        }
    </script>
</asp:Content>
