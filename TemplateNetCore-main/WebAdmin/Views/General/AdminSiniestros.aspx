<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminSiniestros.aspx.vb" Inherits="WebAdmin.AdminSiniestros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Siniestros</h2>
            <asp:Button ID="btnAgregarSiniestro" runat="server" CssClass="btn btn-primary btn-add" Text="Agregar Siniestro" OnClick="btnAgregarSiniestro_Click" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarSiniestro" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar siniestros..."></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlTabla" runat="server">
        <table class="table table-bordered">
            <thead class="table-light">
                <tr>
                    <th scope="col">Folio</th>
                    <th scope="col">Poliza</th>
                    <th scope="col">Certificado</th>
                    <th scope="col">Vigencia</th>
                    <th scope="col">Estatus</th>
                    <th scope="col">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">ENDO-001</th>
                    <td>POL-2023-12345</td>
                    <td>Cert-01</td>
                    <td>2023-01-15</td>
                    <td>Activo</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">ENDO-001</th>
                    <td>POL-2023-12345</td>
                    <td>Cert-01</td>
                    <td>2023-01-15</td>
                    <td>Activo</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>

                    </td>
                </tr>
                <tr>
                    <th scope="row">ENDO-001</th>
                    <td>POL-2023-12345</td>
                    <td>Cert-01</td>
                    <td>2023-01-15</td>
                    <td>Activo</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>

                    </td>
                </tr>
                <tr>
                    <th scope="row">ENDO-001</th>
                    <td>POL-2023-12345</td>
                    <td>Cert-01</td>
                    <td>2023-01-15</td>
                    <td>Activo</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>

                    </td>
                </tr>
                <tr>
                    <th scope="row">ENDO-001</th>
                    <td>POL-2023-12345</td>
                    <td>Cert-01</td>
                    <td>2023-01-15</td>
                    <td>Activo</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>

                    </td>
                </tr>
                <tr>
                    <th scope="row">ENDO-001</th>
                    <td>POL-2023-12345</td>
                    <td>Cert-01</td>
                    <td>2023-01-15</td>
                    <td>Activo</td>
                    <td>
                        <button class="icon-btn" title="Ver"><i class="bi bi-eye"></i></button>
                        <button class="icon-btn" title="Editar"><i class="bi bi-pencil"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>

                    </td>
                </tr>
                <tr>
                    <th scope="row">ENDO-001</th>
                    <td>POL-2023-12345</td>
                    <td>Cert-01</td>
                    <td>2023-01-15</td>
                    <td>Activo</td>
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
    <asp:Panel ID="pnlFormularioSiniestros" runat="server" CssClass="card p-4 mt-4" Visible="false">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label></h2>

            <div>
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0" ForeColor="White" Text="Cancelar" />
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4" ForeColor="White" Text="Guardar" />
            </div>
        </div>
        <h5 class="border-bottom pb-2">Datos del siniestro</h5>
        <div class="row g-3 mt-2">
            <div class="col-md-4">
                <label class="form-label">N° de reporte</label>
                <asp:TextBox ID="txtFolio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Fecha de apertura</label>
                <asp:TextBox ID="txtFechaApertura" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Fecha de cierre </label>
                <asp:TextBox ID="txtFechaCierre" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Cliente</label>
                <asp:DropDownList ID="ddlCliente" CssClass="form-select" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Edgar Pérez Garrido" Value="Mercancia"></asp:ListItem>
                    <asp:ListItem Text="Marco Salas" Value="Contenedor"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Póliza maestra</label>
                <asp:DropDownList ID="ddlPolizaMaestra" CssClass="form-select" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Póliza Maestra1" Value="PolizaMaestra1"></asp:ListItem>
                    <asp:ListItem Text="Póliza Maestra2" Value="PolizaMaestra2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">N° de Certificado</label>
                <asp:DropDownList ID="ddlNumeroCertificado" CssClass="form-select" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Numero Certificado 1" Value="NumeroCertificado1"></asp:ListItem>
                    <asp:ListItem Text="Numero Certificado 2" Value="NumeroCertificado2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Tipo siniestro</label>
                <asp:DropDownList ID="ddlTipoSiniestro" runat="server" CssClass="form-select">
                    <asp:ListItem>Siniestro 1</asp:ListItem>
                    <asp:ListItem>Siniestro 2</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Mercancía</label>
                <asp:TextBox ID="txtMercancia" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Lugar de siniestro</label>
                <asp:TextBox ID="txtLugarSiniestro" runat="server" CssClass="form-control"></asp:TextBox>
            </div>           
            <div class="col-md-4">
                <label class="form-label">Monto de reclamo</label>
                <asp:TextBox ID="txtModoReclamo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>           
            <div class="col-md-4">
                <label class="form-label">Monto de indemnizacion</label>
                <asp:TextBox ID="txtMontoIndemnizacion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>           
            <div class="col-md-4">
                <label class="form-label">Suma asegurada</label>
                <asp:TextBox ID="txtSumaAsegurada" runat="server" CssClass="form-control"></asp:TextBox>
            </div>           
        </div>
    </asp:Panel>
</asp:Content>
