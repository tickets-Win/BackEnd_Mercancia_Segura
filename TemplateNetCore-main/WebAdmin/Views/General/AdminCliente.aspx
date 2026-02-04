<%@ Page Title="" Language="vb" AutoEventWireup="false" EnableEventValidation="false" MasterPageFile="~/Default.Master" CodeBehind="AdminCliente.aspx.vb" Inherits="WebAdmin.AdminCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="../../Content/site.css" rel="stylesheet" />
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
        <div class="card card-shadow p-4 mb-4">
            <div style="overflow-x: auto; width: 100%;">
                <asp:GridView ID="gvClientes" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    OnRowCommand="gvClientes_RowCommand"
                    HeaderStyle-CssClass="table-light"
                    DataKeyNames="ClienteId"
                    AllowPaging="True"
                    PageSize="10"
                    OnPageIndexChanging="gvClientes_PageIndexChanging">
                    <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                        <asp:BoundField DataField="EstatusId" HeaderText="Estatus" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" DataFormatString="{0:dd/MM/yyyy}" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("ClienteId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Editar">
                         <i class="bi bi-pencil"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("ClienteId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Eliminar"
                                    OnClientClick="return confirm('¿Seguro que deseas eliminar este vendedor?');">
                         <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkCorreo" runat="server" CommandName="Correo" CommandArgument='<%# Eval("ClienteId") %>'
                                    CssClass="icon-btn" ToolTip="Enviar correo">
                         <i class="bi bi-envelope"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
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
                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4" ForeColor="White" Text="Guardar" OnClick="btnGuardar_Click" />
                    </div>
                </div>

                <!-- DATOS GENERALES -->
                <h5 class="border-bottom pb-2">Datos generales</h5>

                <div class="row g-3 mt-2">

                    <div class="col-md-4">
                        <label class="form-label">Tipo de persona</label>
                        <asp:DropDownList ID="ddlTipoPersona" CssClass="form-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Clave</label>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Estatus</label>
                        <asp:DropDownList ID="ddlEstatus" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:Panel ID="pnlDatosFiscales" runat="server">
                        <div class="row g-3">
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
                    <asp:Panel ID="pnlNombreCompleto" runat="server" CssClass="col-md-4">
                        <label class="form-label">Nombre Completo</label>
                        <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control"></asp:TextBox>
                    </asp:Panel>
                    <div class="col-md-4">
                        <label class="form-label">Regimen Fiscal</label>
                        <asp:DropDownList ID="ddlRegimenFiscal" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">RFC</label>
                        <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">RFC Génerico</label>
                        <asp:DropDownList ID="ddlRFCGenerico" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:Panel ID="pnlRazonSocial" runat="server" CssClass="col-md-4" Visible="false">
                        <label class="form-label">Razón Social</label>
                        <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                    </asp:Panel>
                    <div class="col-md-4">
                        <label class="form-label">Fecha registro</label>
                        <asp:TextBox ID="txtFechaRegistro" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Seguro que Contrata</label>
                        <asp:DropDownList ID="ddlSeguroContrata" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Tipo de Cuenta</label>
                        <asp:DropDownList ID="ddlTipoCuenta" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Origen Cliente</label>
                        <asp:DropDownList ID="ddlOrigenCliente" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Sector</label>
                        <asp:DropDownList ID="ddlSector" CssClass="form-select" runat="server">
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
                        <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Género</label>
                        <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Selecciona género" Value="" />
                            <asp:ListItem>Masculino</asp:ListItem>
                            <asp:ListItem>Femenino</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">País</label>
                        <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" Visible="true">
                            <asp:ListItem Text="Selecciona un país" Value="" />
                            <asp:ListItem Text="México" Value="MX"></asp:ListItem>
                            <asp:ListItem Text="Estados Unidos" Value="US"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Municipio</label>
                        <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Colonia</label>
                        <asp:TextBox ID="txtColonia" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Calle</label>
                        <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control"></asp:TextBox>
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

                    <h5 class="border-bottom pb-2 mt-4"></h5>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <h5>Correos</h5>
                            <div class="card p-3 shadow-sm">
                                <div class="d-flex justify-content-end align-items-center mb-4">
                                    <asp:Button ID="btnAgregarCorreo" runat="server" CssClass="btn btn-primary btn-add" OnClientClick="return false;" data-bs-toggle="modal"
                                        data-bs-target="#modalCorreo"
                                        Text="Agregar" OnClick="btnAgregarCorreo_Click" />
                                </div>
                                <asp:GridView ID="gvCorreos" runat="server"
                                    CssClass="table table-bordered"
                                    AutoGenerateColumns="False"
                                    ShowHeader="True">

                                    <Columns>
                                        <asp:BoundField DataField="TipoCorreoId" HeaderText="Tipo" />
                                        <asp:BoundField DataField="Correo" HeaderText="Correo" />

                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:Button runat="server"
                                                    Text="Eliminar"
                                                    CssClass="btn btn-danger btn-sm"
                                                    CommandName="Eliminar"
                                                    CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal fade" id="modalCorreo" tabindex="-1" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">

                                    <div class="modal-header">
                                        <h5 class="modal-title">Agregar Correo</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                                    </div>

                                    <div class="modal-body">
                                        <div class="mb-3">
                                            <label class="form-label">Tipo</label>
                                            <asp:DropDownList ID="ddlTipoCorreo" runat="server"
                                                CssClass="form-select">
                                                <asp:ListItem Text="Seleccione" Value="" />
                                                <asp:ListItem Text="Facturación" Value="F" />
                                                <asp:ListItem Text="Recepción" Value="R" />
                                                <asp:ListItem Text="Adicional" Value="A" />
                                            </asp:DropDownList>
                                        </div>

                                        <div class="mb-3">
                                            <label class="form-label">Correo</label>
                                            <asp:TextBox ID="TextBox1" runat="server"
                                                CssClass="form-control"
                                                placeholder="correo@ejemplo.com"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary"
                                            data-bs-dismiss="modal">
                                            Cancelar</button>

                                        <asp:Button ID="btnGuardarCorreo" runat="server"
                                            CssClass="btn btn-primary"
                                            Text="Guardar" />
                                    </div>

                                </div>
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
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <h6>Isotanques</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtCuotaIsotanques">Cuota (%)</label>
                                            <asp:TextBox ID="txtCuotaIsotanques" runat="server" CssClass="form-control"
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
                                <div class="card card-shadow p-4 mb-4">
                                    <div style="overflow-x: auto; width: 100%;">
                                        <asp:GridView ID="gvListaVendedores" runat="server"
                                            CssClass="table table-hover align-middle"
                                            AutoGenerateColumns="False"
                                            OnRowCommand="gvListaVendedores_RowCommand"
                                            HeaderStyle-CssClass="table-light"
                                            DataKeyNames="VendedorId"
                                            AllowPaging="True"
                                            PageSize="10"
                                            OnPageIndexChanging="gvListaVendedores_PageIndexChanging">
                                            <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:BoundField DataField="VendedorId" HeaderText="Tipo Vendedor" />
                                                <asp:BoundField DataField="ClienteId" HeaderText="Nombre/Razón Social" />
                                                <asp:BoundField DataField="Comision" HeaderText="Comision" />
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("VendedorId") %>'
                                                            CssClass="icon-btn action-icon" ToolTip="Eliminar"
                                                            OnClientClick="return confirm('¿Seguro que deseas eliminar este vendedor?');">
                 <i class="bi bi-trash"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <div class="card p-3 shadow-sm">
                                <h5>Beneficiario Preferente</h5>
                                <div class="d-flex justify-content-between align-items-center mb-4">
                                    <div>
                                        <button type="button" class="btn btn-primary btn-add" data-bs-toggle="modal" data-bs-target="#modalAgregarBeneficiario">
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
                        <asp:DropDownList ID="ddlNombreVendedor" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlNombreVendedor_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <h6>Comisión</h6>
                        <asp:TextBox ID="txtComision" runat="server" CssClass="form-control" placeholder="Comisión (%)"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarVendedor" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarVendedor_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalAgregarBeneficiario" tabindex="-1" aria-labelledby="modalAgregarBeneficiarioLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalAgregarBeneficiarioLabel">Agregar Beneficiario</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <h6>Nombre</h6>
                        <asp:TextBox ID="txtNombreBeneficiarioP" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <h6>Domicilio</h6>
                        <asp:TextBox ID="txtDomicilioBeneficiario" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <h6>RFC</h6>
                        <asp:TextBox ID="txtRFCBeneficiario" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarBeneficiario" runat="server" CssClass="btn btn-primary" Text="Guardar" />
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
