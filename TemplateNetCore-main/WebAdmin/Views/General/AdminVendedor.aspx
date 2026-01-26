<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminVendedor.aspx.vb" Inherits="WebAdmin.AdminVendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        .action-icon {
            text-decoration: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Vendedor</h2>
            <asp:Button ID="btnAgregarVendedor" runat="server" CssClass="btn btn-primary btn-add"
                Text="Agregar Vendedor" OnClick="btnAgregarVendedor_Click" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarVendedor" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar vendedores..."></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlTabla" runat="server">
        <div class="card card-shadow p-4 mb-4">
            <div style="overflow-x: auto; width: 100%;">
                <asp:GridView ID="gvVendedores" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    HeaderStyle-CssClass="table-light"
                    DataKeyNames="VendedorId"
                    AllowPaging="True"
                    PageSize="10"
                    OnPageIndexChanging="gvVendedores_PageIndexChanging">
                    <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                        <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" DataFormatString="{0:dd/MM/yyyy}" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkVer" runat="server" CommandName="Ver" CommandArgument='<%# Eval("VendedorId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Ver">
                                <i class="bi bi-eye"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("VendedorId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Editar">
                                <i class="bi bi-pencil"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("VendedorId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Eliminar"
                                    OnClientClick="return confirm('¿Seguro que deseas eliminar este vendedor?');">
                                <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkCorreo" runat="server" CommandName="Correo" CommandArgument='<%# Eval("VendedorId") %>'
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

    <!-- FORMULARIO DE REGISTRO / EDICIÓN DE VENDEDOR -->
    <asp:Panel ID="pnlFormularioVendedor" runat="server" CssClass="card p-4 mt-4" Visible="false">

        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </h2>

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
                <asp:DropDownList ID="ddlTipoPersona" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged">
                    <asp:ListItem Text="Fisica" Value="Fisica"></asp:ListItem>
                    <asp:ListItem Text="Moral" Value="Moral"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-4">
                <label class="form-label">Estatus</label>
                <asp:DropDownList ID="ddlEstatus" CssClass="form-select" runat="server">
                    <asp:ListItem Text="Activo" Value="Activo"></asp:ListItem>
                    <asp:ListItem Text="Suspendido" Value="Suspendido"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-4">
                <label class="form-label">Tipo Vendedor</label>
                <asp:DropDownList ID="ddlTipoVendedor" CssClass="form-select" runat="server">
                </asp:DropDownList>
            </div>
            <asp:Panel ID="pnlDatosFisica" runat="server">
                <div class="row g-3 mt-2">
                    <div class="col-md-4">
                        <label class="form-label">Apellido paterno</label>
                        <asp:TextBox ID="txtApellidoP" runat="server" CssClass="form-control" oninput="llenarNombreCompleto()"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Apellido materno</label>
                        <asp:TextBox ID="txtApellidoM" runat="server" CssClass="form-control" oninput="llenarNombreCompleto()"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" oninput="llenarNombreCompleto()"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNombreCompleto" runat="server" CssClass="col-md-4">
                <label class="form-label">Nombre Completo</label>
                <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control"></asp:TextBox>
            </asp:Panel>
            <div class="col-md-4">
                <label class="form-label">Clave</label>
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">RFC</label>
                <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">Fecha registro</label>
                <asp:TextBox ID="txtFechaRegistro" TextMode="Date" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
            </div>
            <asp:Panel ID="pnlRazonSocial" runat="server" CssClass="col-md-4">
                <label class="form-label">Razón / Denominación social</label>
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
            </asp:Panel>

            <div class="col-md-4">
                <label class="form-label">Observaciones</label>
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control"></asp:TextBox>
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
                <label class="form-label">Género</label>
                <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Estado</label>
                <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Colonia</label>
                <asp:TextBox ID="txtColonia" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">Domicilio</label>
                <asp:TextBox ID="txtDomicilio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">C.P.</label>
                <asp:TextBox ID="txtCP" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

        </div>

        <h5 class="border-bottom pb-2 mt-4">Comisión por venta</h5>

        <div class="col-md-4">
            <label class="form-label">Comisión</label>
            <asp:TextBox ID="txtComisión" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

    </asp:Panel>
    <script type="text/javascript">
        function llenarNombreCompleto() {
            var nombre = document.getElementById('<%= txtNombre.ClientID %>').value;
            var apellidoP = document.getElementById('<%= txtApellidoP.ClientID %>').value;
            var apellidoM = document.getElementById('<%= txtApellidoM.ClientID %>').value;

            var nombreCompleto = nombre + ' ' + apellidoP + ' ' + apellidoM;
            document.getElementById('<%= txtNombreCompleto.ClientID %>').value = nombreCompleto.trim();
        }
    </script>

</asp:Content>
