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

    <!-- FORMULARIO DE REGISTRO / EDICIÓN DE VENDEDOR -->
    <asp:Panel ID="pnlFormularioVendedor" runat="server" CssClass="card p-4 mt-4" Visible="false">

        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </h2>

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
                <asp:DropDownList ID="ddlTipoPersona" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged" >
                    <asp:ListItem Text="Fisica" Value="F"></asp:ListItem>
                    <asp:ListItem Text="Moral" Value="M"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-4">
                <label class="form-label">Estatus</label>
                <asp:DropDownList ID="ddlEstatus" CssClass="form-select" runat="server">
                    <asp:ListItem>Activo</asp:ListItem>
                    <asp:ListItem>Suspendido</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-4">
                <label class="form-label">Tipo Vendedor</label>
                <asp:DropDownList ID="ddlTipoVendedor" CssClass="form-select" runat="server">
                    <asp:ListItem>Interno</asp:ListItem>
                    <asp:ListItem>Socio</asp:ListItem>
                    <asp:ListItem>Intermediario</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Panel ID="pnlDatosFisica" runat="server">
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
                <label class="form-label">Clave</label>
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">RFC</label>
                <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">Fecha registro</label>
                <asp:TextBox ID="txtFechaRegistro" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
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
                    <asp:ListItem>Masculino</asp:ListItem>
                    <asp:ListItem>Femenino</asp:ListItem>
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

</asp:Content>
