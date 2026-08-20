<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminEndosos.aspx.vb" Inherits="WebAdmin.AdminEndosos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Encabezado, tabla y formulario van en el mismo UpdatePanel: los botones
         que cambian el Visible de un panel tienen que vivir junto a ese panel,
         si no la pantalla se queda en blanco. --%>
    <asp:UpdatePanel ID="UpEndosos" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <asp:Panel ID="pnlAviso" runat="server" Visible="false">
                <asp:Label ID="lblAviso" runat="server"></asp:Label>
            </asp:Panel>

            <%-- ========================= LISTADO ========================= --%>
            <asp:Panel ID="pnlEncabezado" runat="server">
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2>Endosos</h2>
                    <asp:Button ID="btnAgregarEndoso" runat="server" CssClass="btn btn-primary btn-add"
                        Text="Agregar Endoso" OnClick="btnAgregarEndoso_Click" />
                </div>
                <div class="mb-4">
                    <asp:TextBox ID="txtBuscarEndoso" runat="server" CssClass="form-control"
                        placeholder="🔍 Buscar endosos..." AutoPostBack="true"
                        OnTextChanged="txtBuscarEndoso_TextChanged"></asp:TextBox>
                </div>
            </asp:Panel>

            <asp:Panel ID="PnlTabla" runat="server">
                <%-- Mismo esquema que polizas: el contenedor desplaza en
                     horizontal y el grid no parte renglones. --%>
                <div style="overflow-x: auto; width: 100%;">
                    <asp:GridView ID="gvEndosos" runat="server"
                        Style="min-width: 1000px; white-space: nowrap;"
                        AutoGenerateColumns="False"
                        CssClass="table table-bordered"
                        HeaderStyle-CssClass="table-light"
                        OnRowCommand="gvEndosos_RowCommand"
                        AllowPaging="True" PageSize="10"
                        OnPageIndexChanging="gvEndosos_PageIndexChanging">

                        <Columns>
                            <asp:BoundField DataField="Endoso" HeaderText="Endoso" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="Desde" HeaderText="Desde"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Hasta" HeaderText="Hasta"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" />

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar"
                                        CommandArgument='<%# Eval("EndosoId") %>'
                                        CssClass="icon-btn action-icon" ToolTip="Editar">
                                    <i class="bi bi-pencil"></i>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar"
                                        CommandArgument='<%# Eval("EndosoId") %>'
                                        CssClass="icon-btn action-icon" ToolTip="Eliminar"
                                        OnClientClick="return confirm('¿Seguro que deseas eliminar este endoso?');">
                                    <i class="bi bi-trash"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            <div class="text-muted py-3">No se encontraron endosos con ese criterio.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <%-- ======================= FORMULARIO ======================== --%>
            <asp:Panel ID="pnlFormularioEndosos" runat="server" CssClass="card p-4 mt-4" Visible="false">

                <asp:HiddenField ID="hfEndosoId" runat="server" Value="" />

                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2><asp:Label ID="lblMensaje" runat="server"></asp:Label></h2>
                    <div>
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0"
                            ForeColor="White" Text="Cancelar" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4"
                            ForeColor="White" Text="Guardar" OnClick="btnGuardar_Click" />
                    </div>
                </div>

                <h5 class="border-bottom pb-2">Datos del endoso</h5>
                <div class="row g-3 mt-2">
                    <div class="col-md-4">
                        <label class="form-label">Tipo Endoso</label>
                        <asp:DropDownList ID="ddlTipoEndoso" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">N° Endoso</label>
                        <asp:TextBox ID="txtNumeroEndoso" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Fecha de elaboración</label>
                        <asp:TextBox ID="txtFechaElaboracion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>

                    <%-- Cliente y poliza no se guardan: solo acotan hasta el
                         certificado, que es lo unico que el endoso referencia. --%>
                    <div class="col-md-4">
                        <label class="form-label">Cliente</label>
                        <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label"># Póliza</label>
                        <asp:DropDownList ID="ddlPoliza" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlPoliza_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Certificado</label>
                        <asp:DropDownList ID="ddlCertificado" runat="server" CssClass="form-select"></asp:DropDownList>
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
                        <asp:DropDownList ID="ddlBeneficiarioPreferente" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Moneda</label>
                        <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>

                    <%-- Lo que el endoso cambia. El certificado no se modifica:
                         conserva lo que se emitio y esto queda como constancia.
                         Se dejan vacios cuando el tipo de endoso no los toca. --%>
                    <div class="col-md-4">
                        <label class="form-label">Nueva vigencia del</label>
                        <asp:TextBox ID="txtVigenciaDel" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Nueva vigencia hasta</label>
                        <asp:TextBox ID="txtVigenciaHasta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Nueva suma asegurada</label>
                        <asp:TextBox ID="txtSumaAsegurada" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Prima y servicio de aseguramiento</label>
                        <asp:TextBox ID="txtPrima" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtPrima_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">IVA</label>
                        <asp:TextBox ID="txtIVA" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        <small class="text-muted">16% de la prima</small>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Total a pagar</label>
                        <asp:TextBox ID="txtTotalPagar" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        <small class="text-muted">prima + IVA</small>
                    </div>
                    <div class="col-md-8">
                        <label class="form-label">Observaciones</label>
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control textarea"
                            TextMode="MultiLine" Rows="4" placeholder="Notas adicionales del endoso"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        // Buscador incremental: mismo helper que usan los demas modulos.
        document.addEventListener('DOMContentLoaded', function () {
            msBuscadorIncremental('<%= txtBuscarEndoso.ClientID %>', '<%= txtBuscarEndoso.UniqueID %>', 500);
        });
    </script>

</asp:Content>
