<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminSiniestros.aspx.vb" Inherits="WebAdmin.AdminSiniestros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Encabezado, tabla y formulario van en el mismo UpdatePanel: los botones
         que cambian el Visible de un panel tienen que vivir junto a ese panel, si
         no la pantalla se queda en blanco. --%>
    <asp:UpdatePanel ID="UpSiniestros" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <asp:Panel ID="pnlAviso" runat="server" Visible="false">
                <asp:Label ID="lblAviso" runat="server"></asp:Label>
            </asp:Panel>

            <%-- ========================= LISTADO ========================= --%>
            <asp:Panel ID="pnlEncabezado" runat="server">
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2>Siniestros</h2>
                    <asp:Button ID="btnAgregarSiniestro" runat="server" CssClass="btn btn-primary btn-add"
                        Text="Agregar Siniestro" OnClick="btnAgregarSiniestro_Click" />
                </div>
                <div class="mb-4">
                    <asp:TextBox ID="txtBuscarSiniestro" runat="server" CssClass="form-control"
                        placeholder="🔍 Buscar siniestros..." AutoPostBack="true"
                        OnTextChanged="txtBuscarSiniestro_TextChanged"></asp:TextBox>
                </div>
            </asp:Panel>

            <asp:Panel ID="PnlTabla" runat="server">
                <%-- Mismo esquema que polizas: el contenedor desplaza en
                     horizontal y el grid no parte renglones, para que los iconos
                     de Acciones no se bajen a otra linea. --%>
                <div style="overflow-x: auto; width: 100%;">
                    <asp:GridView ID="gvSiniestros" runat="server"
                        Style="min-width: 1000px; white-space: nowrap;"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered"
                        HeaderStyle-CssClass="table-light"
                        OnRowCommand="gvSiniestros_RowCommand"
                        AllowPaging="True" PageSize="10"
                        OnPageIndexChanging="gvSiniestros_PageIndexChanging">

                        <Columns>
                            <asp:BoundField DataField="NumeroReporte" HeaderText="N° Reporte" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                            <asp:BoundField DataField="FechaSiniestro" HeaderText="Fecha siniestro"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="NumeroSiniestro" HeaderText="N° Siniestro" />
                            <asp:BoundField DataField="TipoSiniestro" HeaderText="Tipo siniestro" />
                            <asp:BoundField DataField="MontoReclamo" HeaderText="Monto de reclamo"
                                DataFormatString="{0:C2}" />

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar"
                                        CommandArgument='<%# Eval("SiniestroId") %>'
                                        CssClass="icon-btn action-icon" ToolTip="Editar">
                                    <i class="bi bi-pencil"></i>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar"
                                        CommandArgument='<%# Eval("SiniestroId") %>'
                                        CssClass="icon-btn action-icon" ToolTip="Eliminar"
                                        OnClientClick="return confirm('¿Seguro que deseas eliminar este siniestro?');">
                                    <i class="bi bi-trash"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            <div class="text-muted py-3">No se encontraron siniestros con ese criterio.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <%-- ======================= FORMULARIO ======================== --%>
            <asp:Panel ID="pnlFormularioSiniestros" runat="server" CssClass="card p-4 mt-4" Visible="false">

                <asp:HiddenField ID="hfSiniestroId" runat="server" Value="" />

                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2><asp:Label ID="lblMensaje" runat="server"></asp:Label></h2>
                    <div>
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0"
                            ForeColor="White" Text="Cancelar" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4"
                            ForeColor="White" Text="Guardar" OnClick="btnGuardar_Click" />
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
                        <label class="form-label">Fecha de cierre</label>
                        <asp:TextBox ID="txtFechaCierre" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>

                    <%-- Cliente y poliza no se guardan: solo sirven para ir
                         acotando hasta el certificado, que es lo unico que el
                         siniestro referencia. --%>
                    <div class="col-md-4">
                        <label class="form-label">Cliente</label>
                        <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Póliza maestra</label>
                        <asp:DropDownList ID="ddlPolizaMaestra" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlPolizaMaestra_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">N° de Certificado</label>
                        <asp:DropDownList ID="ddlNumeroCertificado" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlNumeroCertificado_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Tipo siniestro</label>
                        <asp:DropDownList ID="ddlTipoSiniestro" runat="server" CssClass="form-select"></asp:DropDownList>
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
                        <asp:TextBox ID="txtMontoReclamo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Monto de indemnización</label>
                        <asp:TextBox ID="txtMontoIndemnizacion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Suma asegurada</label>
                        <asp:TextBox ID="txtSumaAsegurada" runat="server" CssClass="form-control bg-light"
                            ReadOnly="True"></asp:TextBox>
                        <small class="text-muted">viene del certificado</small>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        // Buscador incremental: mismo helper que usan los demas modulos.
        document.addEventListener('DOMContentLoaded', function () {
            msBuscadorIncremental('<%= txtBuscarSiniestro.ClientID %>', '<%= txtBuscarSiniestro.UniqueID %>', 500);
        });

        // Los importes se muestran como $1,234.56 al salir del campo. El servidor
        // limpia el simbolo y las comas antes de guardar, asi que el formato es
        // solo de presentacion.
        function msImporteSiniestro(id) {

            var campo = document.getElementById(id);
            if (!campo) { return; }

            campo.addEventListener('blur', function () {

                var limpio = (campo.value || '').replace(/[^0-9.\-]/g, '');

                if (limpio === '' || isNaN(parseFloat(limpio))) { campo.value = ''; return; }

                campo.value = '$' + parseFloat(limpio).toLocaleString('en-US', {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                });
            });
        }

        function msEngancharImportesSiniestro() {
            msImporteSiniestro('<%= txtMontoReclamo.ClientID %>');
            msImporteSiniestro('<%= txtMontoIndemnizacion.ClientID %>');
        }

        document.addEventListener('DOMContentLoaded', msEngancharImportesSiniestro);

        // El formulario vive en un UpdatePanel: al repintarse hay que volver a
        // enganchar los campos.
        if (typeof Sys !== 'undefined' && Sys.WebForms) {
            Sys.WebForms.PageRequestManager.getInstance()
                .add_endRequest(msEngancharImportesSiniestro);
        }
    </script>

</asp:Content>
