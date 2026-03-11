<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminEndosos.aspx.vb" Inherits="WebAdmin.AdminEndosos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Endosos</h2>
            <asp:Button ID="btnAgregarEndoso" runat="server" CssClass="btn btn-primary btn-add" Text="Agregar Endoso" OnClick="btnAgregarEndoso_Click" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarEndoso" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar endosos..."></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlTabla" runat="server">
        <table class="table table-bordered">
            <thead class="table-light">
                <tr>
                    <th scope="col">Endoso</th>
                    <th scope="col">Tipo</th>
                    <th scope="col">Desde</th>
                    <th scope="col">Hasta</th>
                    <th scope="col">Concepto</th>
                    <th scope="col">Acciones</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">ENDO-01</th>
                    <td>A</td>
                    <td>2023-01-15</td>
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
                    <th scope="row">ENDO-02</th>
                    <td>B</td>
                    <td>2023-01-15</td>
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
                    <th scope="row">ENDO-03</th>
                    <td>C</td>
                    <td>2023-01-15</td>
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
                    <th scope="row">ENDO-04</th>
                    <td>A</td>
                    <td>2023-01-15</td>
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
    <asp:Panel ID="pnlFormularioEndosos" runat="server" CssClass="card p-4 mt-4" Visible="false">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label></h2>
            <div>
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0" ForeColor="White" Text="Cancelar" />
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4" ForeColor="White" Text="Guardar" />
            </div>
        </div>
        <h5 class="border-bottom pb-2">Endosos</h5>
        <div class="row g-3 mt-2">
                <div class="col-md-4">
                    <label class="form-label">Tipo Endoso</label>
                    <asp:DropDownList ID="ddlEndoso" CssClass="form-select" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Endoso1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Endoso2" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label class="form-label">N° Endoso</label>
                    <asp:TextBox ID="txtNumeroEndoso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Certificado</label>
                    <asp:DropDownList ID="ddlCertificado" CssClass="form-select" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Certificado1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Certificado2" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label class="form-label"># Póliza</label>
                    <asp:DropDownList ID="ddlPoliza" CssClass="form-select" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Póliza1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Póliza2" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Nombre Interno Póliza</label>
                    <asp:DropDownList ID="ddlNombreInternoPoliza" CssClass="form-select" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Póliza1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Póliza2" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Fecha de elaboración</label>
                    <asp:TextBox ID="txtFechaElaboracion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Agente</label>
                    <asp:TextBox ID="txtAgente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">RFC</label>
                    <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Oficina</label>
                    <asp:TextBox ID="txtOficina" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Beneficiario Preferente</label>
                    <asp:DropDownList ID="ddlBeneficiarioPreferente" CssClass="form-select" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Beneficiario1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Beneficiario2" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Moneda</label>
                    <asp:DropDownList ID="ddlMoneda" CssClass="form-select" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Moneda1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Moneda2" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Prima</label>
                    <asp:TextBox ID="txtPrima" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Servicio de Aseguramiento</label>
                    <asp:TextBox ID="txtServicioAseguramiento" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">IVA</label>
                    <asp:TextBox ID="txtIVA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">Total a pagar</label>
                    <asp:TextBox ID="txtTotalPagar" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-12">
                    <label class="form-label">Observaciones</label>
                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control textarea" TextMode="MultiLine" Rows="4" Placeholder="Notas adicionales del endoso"></asp:TextBox>
                </div>
        </div>
    </asp:Panel>
</asp:Content>
