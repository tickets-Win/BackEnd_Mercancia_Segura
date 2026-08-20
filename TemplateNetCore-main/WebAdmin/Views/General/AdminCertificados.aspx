<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminCertificados.aspx.vb" Inherits="WebAdmin.AdminCertificados" %>
<%@ Register TagPrefix="uc" TagName="EnvioCorreo" Src="~/Controles/EnvioCorreo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Encabezado, tabla y detalle van en el mismo UpdatePanel: los botones que
         cambian el Visible de un panel tienen que vivir junto a ese panel, si no
         la pantalla se queda en blanco. --%>
    <asp:UpdatePanel ID="UpCertificados" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <asp:Panel ID="pnlAviso" runat="server" Visible="false">
                <asp:Label ID="lblAviso" runat="server"></asp:Label>
            </asp:Panel>

            <%-- ========================= LISTADO ========================= --%>
            <asp:Panel ID="pnlListado" runat="server">

                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2>Certificados</h2>
                </div>

                <div class="mb-4">
                    <asp:TextBox ID="txtBuscarCertificado" runat="server" CssClass="form-control"
                        placeholder="🔍 Buscar certificados..." AutoPostBack="true"
                        OnTextChanged="txtBuscarCertificado_TextChanged"></asp:TextBox>
                </div>

                <div class="d-flex justify-content-left mb-4">
                    <label for="ddlPeriodo" class="form-label visually-hidden">Filtrar</label>
                    <asp:DropDownList ID="ddlPeriodo" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged"
                        CssClass="form-select form-select-sm filtro-estilo w-auto">
                        <asp:ListItem Text="-- Todos --" Value="0" />
                        <asp:ListItem Text="Hoy" Value="1" />
                        <asp:ListItem Text="Mes actual" Value="2" />
                        <asp:ListItem Text="Mes anterior" Value="3" />
                    </asp:DropDownList>
                </div>

                <asp:Panel ID="PnlTabla" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvCertificados" runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-bordered"
                            HeaderStyle-CssClass="table-light"
                            OnRowCommand="gvCertificados_RowCommand"
                            AllowPaging="True"
                            PageSize="10"
                            OnPageIndexChanging="gvCertificados_PageIndexChanging">

                            <Columns>
                                <asp:BoundField DataField="Clave" HeaderText="Clave" />
                                <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" />
                                <asp:BoundField DataField="NombreEstatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="FechaCotizacion" HeaderText="Fecha Cotización"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="TipoCotizacion" HeaderText="Tipo" />

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDetalle" runat="server" CommandName="Detalle"
                                            CommandArgument='<%# Eval("CertificadoId") %>'
                                            CssClass="icon-btn action-icon" ToolTip="Ver detalle">
                                        <i class="bi bi-eye"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkCorreo" runat="server" CommandName="Correo"
                                            CommandArgument='<%# Eval("CertificadoId") %>'
                                            CssClass="icon-btn action-icon" ToolTip="Enviar correo">
                                        <i class="bi bi-envelope"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar"
                                            CommandArgument='<%# Eval("CertificadoId") %>'
                                            CssClass="icon-btn action-icon" ToolTip="Eliminar"
                                            OnClientClick="return confirm('¿Seguro que deseas eliminar este certificado?');">
                                        <i class="bi bi-trash"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EmptyDataTemplate>
                                <div class="text-muted py-3">No se encontraron certificados con ese criterio.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </asp:Panel>

            <%-- ========================= DETALLE ========================= --%>
            <asp:Panel ID="pnlDetalle" runat="server" CssClass="card p-4 mt-4" Visible="false">

                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h2>Detalle del certificado</h2>
                    <asp:LinkButton ID="lnkVolver" runat="server" CssClass="btn btn-light border"
                        OnClick="lnkVolver_Click">
                        <i class="bi bi-arrow-left me-1"></i>Regresar
                    </asp:LinkButton>
                </div>

                <h5 class="border-bottom pb-2">Datos del certificado</h5>
                <div class="row g-3 mt-2">
                    <div class="col-md-4">
                        <label class="form-label">Clave</label>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Estatus</label>
                        <asp:TextBox ID="txtEstatus" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Fecha de registro</label>
                        <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-8">
                        <label class="form-label">Asegurado</label>
                        <asp:TextBox ID="txtAsegurado" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Suma asegurada</label>
                        <asp:TextBox ID="txtSumaAsegurada" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Vigencia del</label>
                        <asp:TextBox ID="txtVigenciaDel" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Vigencia hasta</label>
                        <asp:TextBox ID="txtVigenciaHasta" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>

                <%-- ================= DATOS DE LA COTIZACION ================= --%>
                <h5 class="border-bottom pb-2 mt-4">Datos de la cotización</h5>
                <div class="row g-3 mt-2">
                    <div class="col-md-4">
                        <label class="form-label">N° de cotización</label>
                        <asp:TextBox ID="txtCotizacionId" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Tipo de cotización</label>
                        <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Fecha de cotización</label>
                        <asp:TextBox ID="txtFechaCotizacion" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-8">
                        <label class="form-label">Nombre interno póliza</label>
                        <asp:TextBox ID="txtNumeroPoliza" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Moneda</label>
                        <asp:TextBox ID="txtMoneda" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Cliente</label>
                        <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Vigencia del</label>
                        <asp:TextBox ID="txtCotVigenciaDel" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Vigencia hasta</label>
                        <asp:TextBox ID="txtCotVigenciaHasta" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>

                <%-- ============ DETALLE DE MERCANCIA (solo ese tipo) ========= --%>
                <asp:Panel ID="pnlDetalleMercancia" runat="server" Visible="false">
                    <h5 class="border-bottom pb-2 mt-4">Detalle de mercancía</h5>
                    <div class="row g-3 mt-2">
                        <div class="col-md-4">
                            <label class="form-label">Tránsito</label>
                            <asp:TextBox ID="txtTransito" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Clasificación</label>
                            <asp:TextBox ID="txtClasificacion" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Subclasificación</label>
                            <asp:TextBox ID="txtSubclasificacion" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <label class="form-label">Descripción de la mercancía</label>
                            <asp:TextBox ID="txtDescripcionMercancia" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Tipo de empaque</label>
                            <asp:TextBox ID="txtTipoEmpaque" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Origen</label>
                            <asp:TextBox ID="txtOrigen" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Destino</label>
                            <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Medios de conducción</label>
                            <asp:TextBox ID="txtMediosConduccion" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label">Medio de transporte</label>
                            <asp:TextBox ID="txtMedioTransporte" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <label class="form-label">Observaciones</label>
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>

                    <h5 class="border-bottom pb-2 mt-4">Medidas de seguridad</h5>
                    <div class="row g-3 mt-2">
                        <div class="col-md-6">
                            <label class="form-label">Medidas de seguridad adicionales</label>
                            <asp:TextBox ID="txtMedidasSeguridad" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Deducibles</label>
                            <asp:TextBox ID="txtDeducibles" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>

                    <h5 class="border-bottom pb-2 mt-4">Cuotas</h5>
                    <div class="row g-3 mt-2">
                        <div class="col-md-3">
                            <label class="form-label">Cuota aplicable</label>
                            <asp:TextBox ID="txtCuotaAplicable" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label">Cuota mínima</label>
                            <asp:TextBox ID="txtCuotaMinima" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label">Tipo de cambio</label>
                            <asp:TextBox ID="txtTipoCambio" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label">Suma asegurada</label>
                            <asp:TextBox ID="txtCotSumaAsegurada" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>

                <%-- =========== DETALLE DE CONTENEDOR (solo ese tipo) ========= --%>
                <asp:Panel ID="pnlDetalleContenedor" runat="server" Visible="false">
                    <h5 class="border-bottom pb-2 mt-4">Detalle de contenedores</h5>
                    <div class="table-responsive mt-2">
                        <asp:GridView ID="gvContenedores" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered" HeaderStyle-CssClass="table-light">
                            <Columns>
                                <asp:BoundField DataField="NumeroContenedor" HeaderText="N° contenedor" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="Tamanio" HeaderText="Tamaño" />
                                <asp:BoundField DataField="Referencia" HeaderText="Referencia" />
                                <asp:BoundField DataField="Cuota" HeaderText="Cuota" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="LR" HeaderText="LR" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="TC" HeaderText="TC" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="PrimaUnitariaUSD" HeaderText="Prima USD" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="PrimaUnitariaMXN" HeaderText="Prima MXN" DataFormatString="{0:N2}" />
                                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N2}" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-muted py-3">Esta cotización no tiene contenedores capturados.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <%-- ==================== BIENES Y COBERTURAS ================== --%>
                <div class="row g-4 mt-1">
                    <div class="col-md-6">
                        <h5 class="border-bottom pb-2">Bienes asegurados</h5>
                        <asp:GridView ID="gvBienes" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered mt-2" HeaderStyle-CssClass="table-light">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Bien" />
                                <asp:BoundField DataField="NombreTipoBien" HeaderText="Tipo de bien" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-muted py-3">Sin bienes asegurados.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="col-md-6">
                        <h5 class="border-bottom pb-2">Coberturas</h5>
                        <asp:GridView ID="gvCoberturas" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered mt-2" HeaderStyle-CssClass="table-light">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Cobertura" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-muted py-3">Sin coberturas.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

                <%-- ======================= IMPORTES ========================== --%>
                <h5 class="border-bottom pb-2 mt-4">Prima y servicios</h5>
                <div class="row g-3 mt-2">
                    <div class="col-md-4">
                        <label class="form-label">Prima y servicio de aseguramiento</label>
                        <asp:TextBox ID="txtPrima" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Gastos de expedición</label>
                        <asp:TextBox ID="txtGastosExpedicion" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Subtotal</label>
                        <asp:TextBox ID="txtSubtotal" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">IVA</label>
                        <asp:TextBox ID="txtIVA" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Total a pagar</label>
                        <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control bg-light" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <%-- Envio de correo: mismo control que cotizaciones, clientes,
                 vendedores, beneficiarios y polizas. Va dentro de este
                 UpdatePanel para mostrarse sin recargar la pagina. --%>
            <uc:EnvioCorreo ID="ucCorreo" runat="server" Visible="false"
                OnCancelado="ucCorreo_Cancelado" OnEnviado="ucCorreo_Enviado" />

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        // Buscador incremental: mismo helper que usan pólizas, clientes,
        // vendedores, beneficiarios y cotizaciones.
        document.addEventListener('DOMContentLoaded', function () {
            msBuscadorIncremental('<%= txtBuscarCertificado.ClientID %>', '<%= txtBuscarCertificado.UniqueID %>', 500);
        });
    </script>

</asp:Content>
