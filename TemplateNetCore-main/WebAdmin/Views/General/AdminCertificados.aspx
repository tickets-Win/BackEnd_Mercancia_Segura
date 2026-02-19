<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminCertificados.aspx.vb" Inherits="WebAdmin.AdminCertificados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlEncabezado" runat="server">      
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
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-02</th>
                    <td>Ana García</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-03</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-04</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-05</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-06</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
                <tr>
                    <th scope="row">COT-07</th>
                    <td>Carlos Lopez</td>
                    <td>Pendiente</td>
                    <td>2023-01-15</td>
                    <td>Mercancia</td>
                    <td>
                        <button class="icon-btn" title="Enviar correo"><i class="bi bi-envelope"></i></button>
                        <button class="icon-btn" title="Eliminar"><i class="bi bi-trash"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>
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
