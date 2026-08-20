<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminCotizaciones.aspx.vb" Inherits="WebAdmin.AdminCotizaciones" %>
<%@ Register TagPrefix="uc" TagName="EnvioCorreo" Src="~/Controles/EnvioCorreo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />

    <style>
        /* Palomita de una cotizacion ya confirmada: .icon-btn deja el cursor en
           mano y aqui ya no hay nada que clickear. */
        .ms-aceptada {
            cursor: default;
            opacity: .65;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpListado" runat="server" UpdateMode="Always">
        <ContentTemplate>
    <asp:Panel ID="pnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Cotizaciones</h2>
            <asp:Button ID="btnAgregarCotizacion" runat="server" CssClass="btn btn-primary btn-add" Text="Agregar Cotización" OnClick="btnAgregarCotizacion_Click" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarCotizacion" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar por cotización..."
                AutoPostBack="True" OnTextChanged="txtBuscarCotizacion_TextChanged"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-left mb-4">
            <label for="ddlTipoPolizas" class="form-label visually-hidden">Filtrar</label>          
            <asp:DropDownList ID="ddlTipoPolizas" runat="server" CssClass="form-select form-select-sm filtro-estilo w-auto"
                AutoPostBack="True" OnSelectedIndexChanged="ddlTipoPolizas_SelectedIndexChanged">
                <asp:ListItem Text="-- Todos --" Value="0" />
                <asp:ListItem Text="Hoy" Value="1" />
                <asp:ListItem Text="Mes actual" Value="2" />
                <asp:ListItem Text="Mes anterior" Value="3" />
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlTabla" runat="server">
        <div class="card card-shadow p-4 mb-4">
            <div style="overflow-x: auto; width: 100%;">
                <asp:GridView ID="gvCotizaciones" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    OnRowCommand="gvCotizaciones_RowCommand"
                    HeaderStyle-CssClass="table-light"
                    DataKeyNames="CotizacionId"
                    AllowPaging="True"
                    PageSize="10"
                    OnPageIndexChanging="gvCotizaciones_PageIndexChanging">
                    <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="nombreCliente" HeaderText="Nombre Cliente" />
                        <asp:BoundField DataField="FechaCotizacion" HeaderText="Fecha Cotizacion" />
                        <asp:BoundField DataField="GastosExpedicion" HeaderText="Gastos Expedicion" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAceptar" runat="server" CommandName="Aceptar" CommandArgument='<%# Eval("CotizacionId") %>'
                                    CssClass='<%# IconoAceptar(Eval("CotizacionId")) %>' ToolTip='<%# TituloAceptar(Eval("CotizacionId")) %>'
                                    Enabled='<%# PuedeAceptar(Eval("CotizacionId")) %>'
                                    OnClientClick='<%# ConfirmacionAceptar(Eval("CotizacionId")) %>'>
                                <i class="bi bi-check-lg"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("CotizacionId") %>'
                                    CssClass='<%# IconoBloqueable(Eval("CotizacionId")) %>'
                                    ToolTip='<%# TituloEditar(Eval("CotizacionId")) %>'
                                    Enabled='<%# PuedeAceptar(Eval("CotizacionId")) %>'
                                    OnClientClick='<%# ConfirmacionEditar(Eval("CotizacionId")) %>'>
                                <i class="bi bi-pencil"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("CotizacionId") %>'
                                    CssClass='<%# IconoBloqueable(Eval("CotizacionId")) %>'
                                    ToolTip='<%# TituloEliminar(Eval("CotizacionId")) %>'
                                    Enabled='<%# PuedeAceptar(Eval("CotizacionId")) %>'
                                    OnClientClick='<%# ConfirmacionEliminar(Eval("CotizacionId")) %>'>
                                <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkCorreo" runat="server" CommandName="Correo" CommandArgument='<%# Eval("CotizacionId") %>'
                                    CssClass="icon-btn" ToolTip="Enviar correo">
                                <i class="bi bi-envelope"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <EmptyDataTemplate>
                        <div class="text-muted py-3">No se encontraron cotizaciones con ese criterio.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>  
    <asp:UpdatePanel ID="UpFormularioCotizacion" runat="server" UpdateMode="Always">
        <ContentTemplate>
    <asp:Panel ID="pnlFormularioCotizaciones" runat="server" CssClass="card p-4 mt-4" Visible="false">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <asp:HiddenField ID="hfCotizacionId" runat="server" Value="" />

            <%-- Renglones de la tabla de contenedores, en JSON. Va DENTRO del
                 UpdatePanel a proposito: el bloque <script> esta fuera y no se
                 vuelve a ejecutar en un postback parcial, asi que un <%= %> ahi
                 se quedaria con el valor de la carga inicial. --%>
            <asp:HiddenField ID="hfContenedores" runat="server" Value="[]" />
            <h2>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label></h2>

            <div>
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0" ForeColor="White" Text="Cancelar" OnClick="btnCancelar_Click" />
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4" ForeColor="White" Text="Guardar" OnClick="btnGuardar_Click" />
            </div>
        </div>
        <h5 class="border-bottom pb-2">Datos de la cotización</h5>
        <div class="row g-3 mt-2">
            <div class="col-md-4">
                <label class="form-label">Tipo de Cotización</label>
                <asp:DropDownList ID="ddlTipoCotizacion" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCotizacion_SelectedIndexChanged">
                    <asp:ListItem Text="Mercancia" Value="Mercancia"></asp:ListItem>
                    <asp:ListItem Text="Contenedor" Value="Contenedor"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Nombre Interno Póliza</label>
                <asp:DropDownList ID="ddlNombreInternoPoliza" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNombreInternoPoliza_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Moneda</label>
                <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="form-select">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Fecha Cotización</label>
                <asp:TextBox ID="txtFechaCotizacion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Cliente</label>
                <asp:DropDownList ID="ddlCliente" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Beneficiario Preferente</label>
                <asp:DropDownList ID="ddlBeneficiarioPreferente" runat="server" CssClass="form-select">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Vigencia del</label>
                <asp:TextBox ID="txtVigenciaDel" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Vigencia Hasta </label>
                <asp:TextBox ID="txtVigenciaHasta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Panel ID="pnlSumaAsegurada" runat="server" CssClass="col-md-4">
                <label class="form-label">Suma Asegurada</label>
                <asp:TextBox ID="txtSumaAsegurada" runat="server" CssClass="form-control"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="pnlMercanciaFormulario" runat="server">
                <div class="row g-3">
                    <div class="col-md-4">
                        <label class="form-label">Cotización Cliente #</label>
                        <asp:TextBox ID="txtSubRamo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Tránsito</label>
                        <asp:DropDownList ID="ddlTransito" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Clasificación</label>
                        <asp:DropDownList ID="ddlClasificacion" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Subclasificación</label>
                        <asp:DropDownList ID="ddlSubclasificación" runat="server" CssClass="form-select">
                            <asp:ListItem>Alimentos y Bebidas</asp:ListItem>
                            <asp:ListItem>Alimentos y Bebidas</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Descripción de mercancía</label>
                        <asp:TextBox ID="txtDescripcionMercancia" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Tipo de Empaque</label>
                        <asp:TextBox ID="txtTipoEmpaque" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Origen</label>
                        <asp:TextBox ID="txtOrigen" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Destino</label>
                        <asp:TextBox ID="txtDestino" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Medios de conducción</label>
                        <asp:TextBox ID="txtMediosConduccion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Medios de transporte</label>
                        <asp:TextBox ID="txtMedioTransporte" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-8">
                        <label class="form-label">Observaciones</label>
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control textarea" TextMode="MultiLine" Rows="4" Placeholder="Notas adicionales de la cotización"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMercancia" runat="server">
        <div class="row mt-3">
            <div class="col-md-12">
                <div class="card p-3 shadow-sm">
                    <h5>Coberturas de la cotización</h5>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card shadow-sm bg-light w-100">
                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('coberturas')">
                                    <h4 class="mb-0">Seleccione una cobertura</h4>
                                    <i id="icon-coberturas" class="bi bi-chevron-down"></i>
                                </div>

                                <div id="coberturas" class="collapse-content px-3 pb-3" style="display: none;">
                                    <asp:DropDownList ID="ddlCoberturas" runat="server" CssClass="form-select mb-3">
                                    </asp:DropDownList>

                                    <div class="d-flex justify-content-end mb-3">
                                        <asp:Button ID="btnAgregarCobertura" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregarCobertura_Click" />
                                    </div>

                                    <asp:GridView ID="GvCoberturasCotizacion" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-hover align-middle"
                                        HeaderStyle-CssClass="table-light"
                                        DataKeyNames="CoberturaId"
                                        OnRowCommand="GvCoberturasCotizacion_RowCommand"
                                        AllowPaging="True"
                                        PageSize="10"
                                        OnPageIndexChanging="GvCoberturasCotizacion_PageIndexChanging">
                                        <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                        <Columns>
                                            <asp:BoundField DataField="Nombre" HeaderText="Cobertura" />

                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("CoberturaId") %>'
                                                        CssClass="btn btn-danger btn-sm"
                                                        OnClientClick="return confirm('¿Seguro que deseas eliminar esta cobertura?');">
Eliminar
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                        <EmptyDataTemplate>
                                            <div class="text-muted py-2">Aún no se han agregado coberturas.</div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlBienesAsegurados" runat="server" Visible="false">
                        <h5 class="mt-2 mb-2">Bienes asegurados</h5>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card shadow-sm bg-light w-100 mb-4">
                                    <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesasegurados')">
                                        <h4 class="mb-0">Seleccione un bien asegurados</h4>
                                        <i id="icon-bienesasegurados" class="bi bi-chevron-down"></i>
                                    </div>

                                    <div id="bienesasegurados" class="collapse-content px-3 pb-3" style="display: none;">
                                        <asp:DropDownList ID="ddlbienesasegurados" runat="server" CssClass="form-select mb-3">
                                        </asp:DropDownList>

                                        <div class="d-flex justify-content-end mb-3">
                                            <asp:Button ID="btnbienesasegurados" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnbienesasegurados_Click" />
                                        </div>

                                        <asp:GridView ID="GvBienesCotizacion" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-hover align-middle"
                                            HeaderStyle-CssClass="table-light"
                                            DataKeyNames="BienId"
                                            OnRowCommand="GvBienesCotizacion_RowCommand"
                                            AllowPaging="True"
                                            PageSize="10"
                                            OnPageIndexChanging="GvBienesCotizacion_PageIndexChanging">
                                            <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                            <Columns>
                                                <asp:BoundField DataField="Nombre" HeaderText="Bienes Asegurados" />

                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                            CssClass="btn btn-danger btn-sm"
                                                            OnClientClick="return confirm('¿Seguro que deseas eliminar este bien asegurado?');">
Eliminar
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataTemplate>
                                                <div class="text-muted py-2">Aún no se han agregado bienes asegurados.</div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlMedidasSeguridad" runat="server">
                        <div class="row mb-3">
                            <div class="col col-md-6">
                                <h6 class="titulo-cuota">Medidas de seguridad adicionales</h6>
                                <asp:TextBox ID="txtMedidasSeguridad" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <h6 class="titulo-cuota">Deducibles</h6>
                                <asp:TextBox ID="txtDeducibles" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </div>
                        </div>

                        <%-- Estos dos no se capturan ni se guardan en la cotizacion:
                             son de la poliza y solo van al formato impreso. Se
                             muestran para que se vea con que se va a imprimir. --%>
                        <div class="row mb-3">
                            <div class="col col-md-6">
                                <h6 class="titulo-cuota">Condiciones Especiales</h6>
                                <asp:TextBox ID="txtCondicionesEspeciales" runat="server" CssClass="form-control bg-light"
                                    TextMode="MultiLine" Rows="4" ReadOnly="True"></asp:TextBox>
                                <small class="text-muted">viene de la póliza</small>
                            </div>
                            <div class="col-md-6">
                                <h6 class="titulo-cuota">Exclusiones</h6>
                                <asp:TextBox ID="txtExclusiones" runat="server" CssClass="form-control bg-light"
                                    TextMode="MultiLine" Rows="4" ReadOnly="True"></asp:TextBox>
                                <small class="text-muted">viene de la póliza</small>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlCuotaAplicableMercancia" runat="server">
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Cuota Aplicable</h6>
                                            <div class="d-flex justify-content-between small mb-1" style="padding: 0; margin: 0;">

                                                <label class="form-check d-flex align-items-center gap-1 m-0" style="width: 48%;">
                                                    <input type="checkbox" id="chkCuotaAplicableN" runat="server" class="form-check-input" />
                                                    <span>Nacional</span>
                                                </label>

                                                <label class="form-check d-flex align-items-center gap-1 m-0 justify-content-end" style="width: 48%;">
                                                    <input type="checkbox" id="chkCuotaAplicableI" runat="server" class="form-check-input" />
                                                    <span>Internacional</span>
                                                </label>
                                            </div>

                                            <asp:TextBox ID="txtCuotaAplicable" runat="server" CssClass="form-control"
                                                placeholder="%"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Cuota Mínima</h6>
                                            <div class="d-flex justify-content-between small mb-1" style="padding: 0; margin: 0;">
                                                <label class="form-check d-flex align-items-center gap-1 m-0" style="width: 48%;">
                                                    <input type="checkbox" id="chkCuotaMinimaN" runat="server" class="form-check-input" />
                                                    <span>Nacional</span>
                                                </label>

                                                <label class="form-check d-flex align-items-center gap-1 m-0 justify-content-end" style="width: 48%;">
                                                    <input type="checkbox" id="chkCuotaMinimaI" runat="server" class="form-check-input" />
                                                    <span>Internacional</span>
                                                </label>
                                            </div>
                                            <asp:TextBox ID="txtCuotaMinima" runat="server" CssClass="form-control"
                                                placeholder=""></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Tipo de cambio para cotizar</h6>
                                            <asp:TextBox ID="txtTipoCambio" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Moneda para cotizar</h6>
                                            <asp:DropDownList ID="ddlMonedaCotizar" runat="server" CssClass="form-select">
                                                <asp:ListItem>Nacional</asp:ListItem>
                                                <asp:ListItem>Dólares</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Prima y servicios de seguramiento</h6>
                                            <asp:TextBox ID="txtPrimaYSeguramiento" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6 class="titulo-cuota">Gastos de expedición</h6>
                                            <asp:TextBox ID="txtGastosExpedicion" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Subtotal</h6>
                                            <asp:TextBox ID="txtSubtotal" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">IVA</h6>
                                            <asp:TextBox ID="txtIVA" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col col-md-6">
                                        <h6 class="titulo-cuota">Total a Pagar</h6>
                                        <asp:TextBox ID="txtTotalPagar" runat="server" CssClass="form-control"
                                            placeholder="$0.00"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlCuotaAplicableContenedor" runat="server" Visible="false">
                        <div class="row mb-2">
                            <div class="col col-md-4">
                                <label class="form-label" for="ddlUnidades">Unidades (Contenedores)</label>
                                <%-- Se llena desde el servidor (1 a 20). Antes el JavaScript
                                     agregaba las opciones 3-20, valores que ASP.NET nunca
                                     registro y que al postear disparaban el error
                                     "Invalid postback or callback argument". --%>
                                <asp:DropDownList ID="ddlUnidades" runat="server" CssClass="form-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="mt-2">
                            <h6 class="mt-2">Detalle de contenedores</h6>
                        </div>
                        <div class="row mb-2">
                            <div class="col-12">
                                <div class="table-responsive">
                                    <table class="table table-bordered table-hover align-middle mb-2" id="tblContenedores">
                                        <thead class="table-light">
                                            <tr>
                                                <th>No. Contenedor</th>
                                                <th>Tipo</th>
                                                <th>Tamaño</th>
                                                <th class="text-end">LR (USD)</th>
                                                <th>Referencia</th>
                                                <th class="text-end">Cuota (%)</th>
                                                <th class="text-end">Prima Unit. USD</th>
                                                <th class="text-end col-tc d-none">T.C.</th>
                                                <th class="text-end col-prima-mn d-none">Prima Unit. MXN</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyContenedores">
                                        </tbody>
                                        <tfoot class="table-light fw-bold">
                                            <tr>
                                                <td colspan="3">Totales</td>
                                                <td class="text-end" id="totalLR">$ 0.00</td>
                                                <td></td>
                                                <td></td>
                                                <td class="text-end" id="totalPrimaUSD">$ 0.00</td>
                                                <td class="col-tc d-none"></td>
                                                <td class="text-end col-prima-mn d-none" id="totalPrimaMN">$ 0.00</td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <h5 class="mt-1">Cuota Aplicable Contenedor</h5>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <h6 class="mt-2 mb-2">Contenedores Secos</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtCuotaSecos">Cuota (%)</label>
                                            <%-- Solo lectura: son las tarifas negociadas del cliente. Se
                                                 llenan al elegirlo y no se guardan en la cotizacion; lo que
                                                 se aplica y persiste es la cuota de cada renglon. --%>
                                            <asp:TextBox ID="txtCuotaSecos" runat="server" CssClass="form-control bg-light"
                                                ReadOnly="True" placeholder="Cuota (%)"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <label class="form-label" for="ddlTipoTarifaSecos">Tipo Tarifa</label>
                                            <asp:DropDownList ID="ddlTipoTarifaSecos" runat="server" CssClass="form-select" Enabled="False">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <h6>Contenedores Refrigerados</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtCuotaRefrigerados">Cuota (%)</label>
                                            <asp:TextBox ID="txtCuotaRefrigerados" runat="server" CssClass="form-control bg-light"
                                                ReadOnly="True" placeholder="Cuota (%)"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <label class="form-label" for="ddlTipoRefrigerados">Tipo Tarifa</label>
                                            <asp:DropDownList ID="ddlTipoRefrigerados" runat="server" CssClass="form-select" Enabled="False">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <h6>Isotanques</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <label class="form-label" for="txtCuota2">Cuota (%)</label>
                                            <asp:TextBox ID="txtCuota2" runat="server" CssClass="form-control bg-light"
                                                ReadOnly="True" placeholder="Cuota (%)"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <label class="form-label" for="ddlTipoIsotaques">Tipo Tarifa</label>
                                            <asp:DropDownList ID="ddlTipoIsotaques" runat="server" CssClass="form-select" Enabled="False">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Prima y servicios de seguramiento</h6>
                                            <asp:TextBox ID="txtPrimaYSeguramiento2" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6 class="titulo-cuota">Gastos de expedición</h6>
                                            <asp:TextBox ID="txtGastosExpedicion2" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">Subtotal</h6>
                                            <asp:TextBox ID="txtSubtotal2" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6 class="titulo-cuota">IVA</h6>
                                            <asp:TextBox ID="txtIVA2" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col col-md-6">
                                        <h6 class="titulo-cuota">Total a Pagar</h6>
                                        <asp:TextBox ID="txtTotalPagar2" runat="server" CssClass="form-control"
                                            placeholder="$0.00"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%-- Envio de correo: control compartido con clientes, vendedores,
         beneficiarios y polizas. Va dentro de este UpdatePanel para poder
         mostrarse y ocultarse sin recargar la pagina. --%>
    <uc:EnvioCorreo ID="ucCorreo" runat="server" Visible="false"
        OnCancelado="ucCorreo_Cancelado" OnEnviado="ucCorreo_Enviado" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGuardar" />
            <asp:PostBackTrigger ControlID="btnCancelar" />
        </Triggers>
    </asp:UpdatePanel>

    <input type="hidden" id="hdnSeccionesAbiertas" value="" />

    <div id="alertPlaceholder" class="position-fixed top-0 start-50 translate-middle-x p-3" style="z-index: 1050;"></div>

    <script>
        function showToast(message, type) {
            const wrapper = document.createElement('div');
            wrapper.innerHTML = `
        <div class="toast align-items-center text-bg-${type} border-0 show" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="d-flex">
                <div class="toast-body">${message}</div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>
    `;
            document.getElementById('alertPlaceholder').append(wrapper);
            setTimeout(() => {
                wrapper.querySelector('.toast').classList.remove('show');
                wrapper.remove();
            }, 4000);
        }
    </script>

    <script>
        function msSeccionesAbiertas() {
            const h = document.getElementById('hdnSeccionesAbiertas');
            if (!h || !h.value) { return []; }
            return h.value.split(',');
        }

        function msGuardarSecciones(lista) {
            const h = document.getElementById('hdnSeccionesAbiertas');
            if (h) { h.value = lista.join(','); }
        }

        function msAbrirSeccion(id, abrir) {
            const content = document.getElementById(id);
            const icon = document.getElementById('icon-' + id);
            if (content) { content.style.display = abrir ? 'block' : 'none'; }
            if (icon) {
                icon.classList.remove(abrir ? 'bi-chevron-down' : 'bi-chevron-up');
                icon.classList.add(abrir ? 'bi-chevron-up' : 'bi-chevron-down');
            }
        }

        function toggleCollapse(id) {
            const content = document.getElementById(id);
            if (!content) { return; }

            const abrir = content.style.display === 'none';
            msAbrirSeccion(id, abrir);

            const abiertas = msSeccionesAbiertas();
            const i = abiertas.indexOf(id);

            if (abrir && i === -1) { abiertas.push(id); }
            if (!abrir && i !== -1) { abiertas.splice(i, 1); }

            msGuardarSecciones(abiertas);
        }
        function msRestaurarSecciones() {
            msSeccionesAbiertas().forEach(function (id) { msAbrirSeccion(id, true); });
        }
    </script>
    <script type="text/javascript">
        // Catalogos reales del API, inyectados desde el code-behind como { id, nombre }.
        // Antes eran arreglos de texto fijo y por eso no se podian guardar: la
        // cotizacion necesita el id, no el nombre.
        const tiposContenedor = <%= TiposContenedorJson %>;
        const tamanosContenedor = <%= TamaniosContenedorJson %>;

        // Renglones que el servidor manda repintar: lo capturado antes del postback,
        // o lo guardado cuando se abre una cotizacion para editar. Se lee del campo
        // oculto en el momento de usarlo, no al cargar el script, porque este
        // bloque no se re-ejecuta en los postbacks parciales.
        function msContenedoresDelServidor() {
            const campo = document.getElementById('<%= hfContenedores.ClientID %>');
            if (!campo || !campo.value) { return []; }

            try {
                const datos = JSON.parse(campo.value);
                return Array.isArray(datos) ? datos : [];
            } catch (e) {
                return [];
            }
        }

        function generateContainerRows() {
            const unidades = parseInt(document.getElementById('<%= ddlUnidades.ClientID %>').value) || 1;
            const tbody = document.getElementById('tbodyContenedores');
            tbody.innerHTML = '';

            for (let i = 1; i <= unidades; i++) {
                const row = document.createElement('tr');
                row.innerHTML = `
                <%-- El atributo name es indispensable: el navegador solo envia los
                     campos que lo tienen. Como estos no son controles de servidor,
                     el code-behind los lee de Request.Form usando ese nombre. --%>
                <td>
                    <input type="text" class="form-control form-control-sm"
                           id="txtNumContenedor_${i}" name="txtNumContenedor_${i}"
                           placeholder="Número"
                           maxlength="11" />
                </td>
                <td>
                    <select class="form-select form-select-sm" id="ddlTipoContenedor_${i}" name="ddlTipoContenedor_${i}">
                        <option value="">Seleccionar...</option>
                        ${tiposContenedor.map(t => `<option value="${t.id}">${t.nombre}</option>`).join('')}
                    </select>
                </td>
                <td>
                    <select class="form-select form-select-sm" id="ddlTamanoContenedor_${i}" name="ddlTamanoContenedor_${i}">
                        <option value="">Seleccionar...</option>
                        ${tamanosContenedor.map(t => `<option value="${t.id}">${t.nombre}</option>`).join('')}
                    </select>
                </td>
                <td>
                    <input type="number" class="form-control form-control-sm text-end"
                           id="txtLR_${i}" name="txtLR_${i}"
                           placeholder="0.00"
                           step="0.01"
                           onchange="calcularPrima(${i})" />
                </td>
                <td>
                    <input type="text" class="form-control form-control-sm"
                           id="txtReferencia_${i}" name="txtReferencia_${i}"
                           placeholder="Referencia"
                           maxlength="30" />
                </td>
                <td>
                    <input type="number" class="form-control form-control-sm text-end"
                           id="txtCuota_${i}" name="txtCuota_${i}"
                           placeholder="0"
                           step="0.01"
                           onchange="calcularPrima(${i})" />
                </td>
                <td>
                    <input type="number" class="form-control form-control-sm text-end bg-light"
                           id="txtPrimaUSD_${i}" name="txtPrimaUSD_${i}"
                           placeholder="0.00"
                           readonly />
                </td>
                <td class="col-tc d-none">
                    <input type="number" class="form-control form-control-sm text-end"
                           id="txtTC_${i}" name="txtTC_${i}"
                           placeholder="0.00"
                           step="0.01"
                           onchange="calcularPrimaMXN(${i})" />
                </td>
                <td class="col-prima-mn d-none">
                    <input type="number" class="form-control form-control-sm text-end bg-light"
                           id="txtPrimaMXN_${i}" name="txtPrimaMXN_${i}"
                           placeholder="0.00"
                           readonly />
                </td>
            `;
                tbody.appendChild(row);
            }

            // Los renglones nacen vacios, asi que hay que volver a poner lo que el
            // servidor mando: lo capturado antes del postback, o lo guardado si se
            // esta editando una cotizacion.
            msLlenarContenedoresGuardados();

            // Las celdas de T.C. y Prima MXN nacen con d-none fijo, asi que hay que
            // volver a aplicar el estado segun la moneda.
            msSincronizarColumnasMXN();

            calcularTotales();
        }

        function msPonerValor(id, valor) {
            const el = document.getElementById(id);
            if (!el) { return; }
            el.value = (valor === null || valor === undefined) ? '' : valor;
        }

        function msLlenarContenedoresGuardados() {
            const guardados = msContenedoresDelServidor();
            if (guardados.length === 0) { return; }

            guardados.forEach(function (c, indice) {
                const i = indice + 1;

                msPonerValor('txtNumContenedor_' + i, c.numero);
                msPonerValor('ddlTipoContenedor_' + i, c.tipoId);
                msPonerValor('ddlTamanoContenedor_' + i, c.tamanioId);
                msPonerValor('txtLR_' + i, c.lr);
                msPonerValor('txtReferencia_' + i, c.referencia);
                msPonerValor('txtCuota_' + i, c.cuota);
                msPonerValor('txtTC_' + i, c.tc);
                msPonerValor('txtPrimaUSD_' + i, c.primaUSD);
                msPonerValor('txtPrimaMXN_' + i, c.primaMXN);
            });
        }

        function calcularPrima(rowIndex) {
            const lr = parseFloat(document.getElementById(`txtLR_${rowIndex}`).value) || 0;
            const cuota = parseFloat(document.getElementById(`txtCuota_${rowIndex}`).value) || 0;
            const primaUSD = (lr * cuota) / 100;

            document.getElementById(`txtPrimaUSD_${rowIndex}`).value = primaUSD.toFixed(2);

            calcularPrimaMXN(rowIndex);
            calcularTotales();
        }

        function calcularPrimaMXN(rowIndex) {
            const primaUSD = parseFloat(document.getElementById(`txtPrimaUSD_${rowIndex}`).value) || 0;
            const tc = parseFloat(document.getElementById(`txtTC_${rowIndex}`).value) || 0;
            const primaMXN = primaUSD * tc;

            document.getElementById(`txtPrimaMXN_${rowIndex}`).value = primaMXN.toFixed(2);

            calcularTotales();
        }

        function calcularTotales() {
            const unidades = parseInt(document.getElementById('<%= ddlUnidades.ClientID %>').value) || 1;
            let totalLR = 0;
            let totalPrimaUSD = 0;
            let totalPrimaMXN = 0;

            for (let i = 1; i <= unidades; i++) {
                totalLR += parseFloat(document.getElementById(`txtLR_${i}`).value) || 0;
                totalPrimaUSD += parseFloat(document.getElementById(`txtPrimaUSD_${i}`).value) || 0;
                totalPrimaMXN += parseFloat(document.getElementById(`txtPrimaMXN_${i}`).value) || 0;
            }

            document.getElementById('totalLR').textContent = '$ ' + totalLR.toFixed(2);
            document.getElementById('totalPrimaUSD').textContent = '$ ' + totalPrimaUSD.toFixed(2);
            document.getElementById('totalPrimaMN').textContent = '$ ' + totalPrimaMXN.toFixed(2);
        }
        var msBuscarTimer = null;
        var msBuscarEnfocar = false;

        function msEngancharBuscador() {
            const el = document.getElementById('<%= txtBuscarCotizacion.ClientID %>');
            if (!el || el.dataset.msWired) { return; }
            el.dataset.msWired = '1';

            el.addEventListener('input', function () {
                clearTimeout(msBuscarTimer);
                msBuscarEnfocar = true;

                msBuscarTimer = setTimeout(function () {
                    __doPostBack('<%= txtBuscarCotizacion.UniqueID %>', '');
                }, 400);
            });
        }       
        if (typeof Sys !== 'undefined' && Sys.WebForms) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                if (!msBuscarEnfocar) { return; }

                const el = document.getElementById('<%= txtBuscarCotizacion.ClientID %>');
                if (!el) { return; }

                el.focus();

                const valor = el.value;
                el.value = '';
                el.value = valor;
            });
        }

        const MS_IVA = 0.16;

        const MS_BLOQUES = [
            {
                prima: '<%= txtPrimaYSeguramiento.ClientID %>',
                gastos: '<%= txtGastosExpedicion.ClientID %>',
                subtotal: '<%= txtSubtotal.ClientID %>',
                iva: '<%= txtIVA.ClientID %>',
                total: '<%= txtTotalPagar.ClientID %>'
            },
            {
                prima: '<%= txtPrimaYSeguramiento2.ClientID %>',
                gastos: '<%= txtGastosExpedicion2.ClientID %>',
                subtotal: '<%= txtSubtotal2.ClientID %>',
                iva: '<%= txtIVA2.ClientID %>',
                total: '<%= txtTotalPagar2.ClientID %>'
            }
        ];

        function msNumero(valor) {
            if (!valor) { return 0; }
            const n = parseFloat(String(valor).replace(/[^0-9.\-]/g, ''));
            return isNaN(n) ? 0 : n;
        }

        function msSufijoMoneda() {
            const ddl = document.getElementById('<%= ddlMoneda.ClientID %>');
            return (ddl && ddl.value === '2') ? 'US' : 'MN';
        }

        function msFormatoMoneda(n) {
            return '$' + n.toLocaleString('en-US', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            }) + ' ' + msSufijoMoneda();
        }

        function msCalcularPrima(ids) {
            const prima = document.getElementById(ids.prima);
            const gastos = document.getElementById(ids.gastos);
            const subtotal = document.getElementById(ids.subtotal);
            const iva = document.getElementById(ids.iva);
            const total = document.getElementById(ids.total);

            if (!prima || !gastos || !subtotal || !iva || !total) { return; }

            if (!prima.value && !gastos.value) { return; }

            const sub = msNumero(prima.value) + msNumero(gastos.value);
            const impuesto = sub * MS_IVA;

            subtotal.value = msFormatoMoneda(sub);
            iva.value = msFormatoMoneda(impuesto);
            total.value = msFormatoMoneda(sub + impuesto);
        }

        function msRecalcularTodo() {
            MS_BLOQUES.forEach(function (ids) { msCalcularPrima(ids); });
        }

     
        const MS_CUOTA_APLICABLE = {
            checks: ['<%= chkCuotaAplicableN.ClientID %>', '<%= chkCuotaAplicableI.ClientID %>'],
            campo: '<%= txtCuotaAplicable.ClientID %>'
        };

        const MS_CUOTA_MINIMA = {
            checks: ['<%= chkCuotaMinimaN.ClientID %>', '<%= chkCuotaMinimaI.ClientID %>'],
            campo: '<%= txtCuotaMinima.ClientID %>'
        };

        function msBloqueActivo(grupo) {
            return grupo.checks.some(function (id) {
                const el = document.getElementById(id);
                return el && el.checked;
            });
        }

        function msHabilitarBloque(grupo, habilitar) {
            grupo.checks.forEach(function (id) {
                const el = document.getElementById(id);
                if (!el) { return; }
                el.disabled = !habilitar;
                if (!habilitar) { el.checked = false; }
            });

            const campo = document.getElementById(grupo.campo);
            if (!campo) { return; }

          
            campo.readOnly = !habilitar;
            campo.classList.toggle('bg-light', !habilitar);
            if (!habilitar) { campo.value = ''; }
        }

        function msSincronizarCuotas() {
            const aplicable = msBloqueActivo(MS_CUOTA_APLICABLE);
            const minima = msBloqueActivo(MS_CUOTA_MINIMA);

            msHabilitarBloque(MS_CUOTA_APLICABLE, !minima);
            msHabilitarBloque(MS_CUOTA_MINIMA, !aplicable);
        }

        function msEngancharCuotas() {
            [
                { grupo: MS_CUOTA_APLICABLE, otro: MS_CUOTA_MINIMA },
                { grupo: MS_CUOTA_MINIMA, otro: MS_CUOTA_APLICABLE }
            ].forEach(function (par) {
                par.grupo.checks.forEach(function (id) {
                    const el = document.getElementById(id);
                    if (!el || el.dataset.msWired) { return; }
                    el.dataset.msWired = '1';

                    el.addEventListener('change', function () {
                        if (el.checked) {
                            par.grupo.checks.forEach(function (otroId) {
                                if (otroId === id) { return; }
                                const hermano = document.getElementById(otroId);
                                if (hermano) { hermano.checked = false; }
                            });

                            msHabilitarBloque(par.otro, false);
                        }

                        msSincronizarCuotas();
                    });
                });
            });

            msSincronizarCuotas();
        }

        function msEngancharPrima(ids) {
            const recalcular = function () { msCalcularPrima(ids); };

            [ids.prima, ids.gastos].forEach(function (id) {
                const el = document.getElementById(id);
                if (el && !el.dataset.msWired) {
                    el.dataset.msWired = '1';

                    el.addEventListener('input', recalcular);

                    el.addEventListener('change', function () {
                        if (this.value) { this.value = msFormatoMoneda(msNumero(this.value)); }
                        recalcular();
                    });
                }
            });

          
            [ids.subtotal, ids.iva, ids.total].forEach(function (id) {
                const el = document.getElementById(id);
                if (el && !el.readOnly) {
                    el.readOnly = true;
                    el.classList.add('bg-light');
                }
            });

            recalcular();
        }

       
        function msEngancharEventos() {
            const ddlUnidades = document.getElementById('<%= ddlUnidades.ClientID %>');
            if (ddlUnidades && !ddlUnidades.dataset.msWired) {
                ddlUnidades.dataset.msWired = '1';
                ddlUnidades.addEventListener('change', generateContainerRows);

                generateContainerRows();
            }

            const ddlMoneda = document.getElementById('<%= ddlMoneda.ClientID %>');
            if (ddlMoneda) {
                if (!ddlMoneda.dataset.msWired) {
                    ddlMoneda.dataset.msWired = '1';
                    ddlMoneda.addEventListener('change', function () {
                        msSincronizarColumnasMXN();
                        msRecalcularTodo();
                    });
                }

                // Fuera del guard a proposito: si la moneda ya viene en Nacional
                // (al abrir el formulario, o porque la poliza la fijo) no hay
                // evento change y las columnas se quedarian ocultas.
                msSincronizarColumnasMXN();
            }

            MS_BLOQUES.forEach(function (ids) { msEngancharPrima(ids); });

            msEngancharCuotas();
            msEngancharBuscador();

            msRestaurarSecciones();
        }

        function pageLoad() {
            msEngancharEventos();
        }

        document.addEventListener('DOMContentLoaded', msEngancharEventos);

        // 1 = Nacional en el catalogo de moneda. Las columnas de T.C. y Prima MXN
        // solo aplican cuando se cotiza en pesos.
        function msSincronizarColumnasMXN() {
            const ddlMoneda = document.getElementById('<%= ddlMoneda.ClientID %>');
            toggleMXNColumns(ddlMoneda != null && ddlMoneda.value === '1');
        }

        function toggleMXNColumns(showMXN) {
            const elements = document.querySelectorAll('.col-tc, .col-prima-mn');
            elements.forEach(el => {
                if (showMXN) {
                    el.classList.remove('d-none');
                } else {
                    el.classList.add('d-none');
                }
            });
        }
</script>

    <style>
        #tblContenedores input.form-control-sm,
        #tblContenedores select.form-select-sm {
            min-width: 100px;
        }

        #tblContenedores input[readonly] {
            background-color: #f8f9fa !important;
            cursor: not-allowed;
        }

        .table-responsive {
            overflow-x: auto;
            -webkit-overflow-scrolling: touch;
        }
    </style>
</asp:Content>
