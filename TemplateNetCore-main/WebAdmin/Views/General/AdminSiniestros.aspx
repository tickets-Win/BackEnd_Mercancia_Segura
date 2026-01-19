<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminSiniestros.aspx.vb" Inherits="WebAdmin.AdminSiniestros" %>

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
    </style>
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
                <label class="form-label">Folio</label>
                <asp:TextBox ID="txtFolio" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
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
                <label class="form-label">Póliza Maestra</label>
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
                <label class="form-label">Tipo Siniestro</label>
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
                <label class="form-label">Lugar de Siniestro</label>
                <asp:TextBox ID="txtLugarSiniestro" runat="server" CssClass="form-control"></asp:TextBox>
            </div>           
            <div class="col-md-4">
                <label class="form-label">Modo de Reclamo</label>
                <asp:TextBox ID="txtModoReclamo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>           
            <div class="col-md-4">
                <label class="form-label">Monto de indemnizacion</label>
                <asp:TextBox ID="txtMontoIndemnizacion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>           
        </div>
    </asp:Panel>
</asp:Content>
