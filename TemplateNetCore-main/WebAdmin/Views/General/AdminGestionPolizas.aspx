<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminGestionPolizas.aspx.vb" Inherits="WebAdmin.AdminGestionPolizas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Gestión de Pólizas</h2>
            <asp:Button ID="btnAgregarPoliza" runat="server" CssClass="btn btn-primary btn-add"
                Text="Agregar Póliza" OnClick="btnAgregarPoliza_Click" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarPolizas" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar pólizas..."></asp:TextBox>
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
        <div class="card card-shadow p-4 mb-4">
            <div style="overflow-x: auto; width: 100%;">
                <asp:GridView ID="gvPolizas" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    OnRowCommand="gvPolizas_RowCommand"
                    HeaderStyle-CssClass="table-light"
                    DataKeyNames="PolizaId"
                    AllowPaging="True"
                    PageSize="10"
                    OnPageIndexChanged="gvPolizas_PageIndexChanged">
                    <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="nombreAseguradora" HeaderText="Aseguradora" />
                        <asp:BoundField DataField="NumeroPoliza" HeaderText="N° Póliza" />
                        <asp:BoundField DataField="nombreContratante" HeaderText="Contratante" />
                        <asp:BoundField DataField="VigenciaDel" HeaderText="Vigencia del" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="VigenciaHasta" HeaderText="Vigencia hasta" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="nombreEstatusPoliza" HeaderText="Estatus" />
                        <asp:BoundField DataField="nombreMoneda" HeaderText="Moneda" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("PolizaId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Editar">
          <i class="bi bi-pencil"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("PolizaId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Eliminar"
                                    OnClientClick="return confirm('¿Seguro que deseas eliminar esta póliza?');">
          <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkCorreo" runat="server" CommandName="Correo" CommandArgument='<%# Eval("PolizaId") %>'
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
    <asp:Panel ID="pnlFormularioPolizas" runat="server" CssClass="card p-4 mt-4" Visible="false">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label></h2>

            <div>
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0" ForeColor="White" Text="Cancelar" />
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4" ForeColor="White" Text="Guardar" OnClick="btnGuardar_Click" />
            </div>
        </div>
        <h5 class="border-bottom pb-2">Pólizas</h5>
        <div class="row g-3 mt-2">
            <div class="col-md-4">
                <label class="form-label">Producto</label>
                <asp:DropDownList ID="ddlProducto" CssClass="form-select" runat="server" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Tipo Póliza</label>
                <asp:TextBox ID="txtTipoPoliza" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">N° Póliza</label>
                <asp:TextBox ID="txtNumeroPoliza" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Contratante</label>
                <asp:DropDownList ID="ddlContratante" runat="server" CssClass="form-select">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Aseguradora</label>
                <asp:DropDownList ID="ddlAseguradora" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Sub Ramo</label>
                <asp:DropDownList ID="ddlSubRamo" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Vigencia del</label>
                <asp:TextBox ID="txtVigenciaDel" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Vigencia Hasta </label>
                <asp:TextBox ID="txtVigenciaHasta" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Estatus</label>
                <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Suspendido" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Forma de Pago</label>
                <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Moneda</label>
                <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="form-select">
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label class="form-label">Clave Agente</label>
                <asp:TextBox ID="txtClaveAgente" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label class="form-label">Folio Póliza</label>
                <asp:TextBox ID="txtFolioPoliza" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Panel ID="pnlNombreInternoPoliza" runat="server" Visible="false" CssClass="col-md-8">
                <div class="row g-3">

                    <div class="col-md-6">
                        <label class="form-label">Nombre Interno Póliza</label>
                        <asp:TextBox ID="txtNombreInternoPolizaContenedor" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Trayectos asegurados</label>
                        <asp:TextBox ID="txtTrayectosAsegurados" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                </div>
            </asp:Panel>
            <div class="col-md-4">
                <asp:Panel ID="pnlMediosTransporte" runat="server" Visible="false">
                    <label class="form-label">Medios de transporte</label>
                    <asp:TextBox ID="txtMedioTransporte" runat="server" CssClass="form-control"></asp:TextBox>
                </asp:Panel>
            </div>
        </div>
        <asp:Panel ID="pnlMercancia" runat="server">
            <div class="row mt-3">
                <div class="col-md-12">
                    <div class="card p-3 shadow-sm">
                        <h5>Administración de bienes de la poliza</h5>
                        <ul class="nav nav-tabs mb-4 justify-content-center" id="clienteTabs" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" id="tab-datos" data-bs-toggle="tab"
                                    data-bs-target="#panel-datos" type="button" role="tab">
                                    Generales</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="tab-credito" data-bs-toggle="tab"
                                    data-bs-target="#panel-credito" type="button" role="tab">
                                    Bienes susceptibles y/o específicas</button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="tab-estado" data-bs-toggle="tab"
                                    data-bs-target="#panel-estado" type="button" role="tab">
                                    Maquinaria y/o sobre dimensionado</button>
                            </li>
                        </ul>
                        <div class="tab-content">

                            <div class="tab-pane fade show active" id="panel-datos" role="tabpanel">
                                <div class="row g-3 mt-2">
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <label class="form-label">Nombre interno Póliza</label>
                                            <asp:TextBox ID="txtNombreInternoPoliza1" runat="server" CssClass="form-control w-100"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesAsegurados')">
                                                    <h4 class="mb-0">Bienes Asegurados</h4>
                                                    <i id="icon-bienesAsegurados" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="UpBienesAsegurados1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesAsegurados" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtBienesAsegurados" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregaBienAsegurado" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregaBienAsegurado_Click" />
                                                            </div>

                                                            <asp:GridView ID="GvBienes" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienes_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienes_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Asegurado" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
                                                                        Eliminar
                                                                       </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">

                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesExcluidos')">
                                                    <h4 class="mb-0">Bienes Excluidos</h4>
                                                    <i id="icon-bienesExcluidos" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="UpBienesExcluidos1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesExcluidos" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtBienesExcluidos" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregacollapseBienesExcluidos" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregacollapseBienesExcluidos_Click" />
                                                            </div>
                                                            <asp:GridView ID="GvBienesExcluidos1" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienesExcluidos1_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienesExcluidos1_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Excluido" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
             Eliminar
            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesSujetosConsulta')">
                                                    <h4 class="mb-0">Bienes Sujetos a Consulta</h4>
                                                    <i id="icon-bienesSujetosConsulta" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="UpBienesSujetosConsulta" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesSujetosConsulta" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtbienesSujetosConsulta" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregaBienesSujetosConsulta" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregaBienesSujetosConsulta_Click" />
                                                            </div>
                                                            <asp:GridView ID="GvBienesSujetosConsulta" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienesSujetosConsulta_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienesSujetosConsulta_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Excluido" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
 Eliminar
</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <h5 class="border-bottom pb-2">Limite Máximo Embarque por Medio de Transporte</h5>

                                    <div class="col-md-4">
                                        <label class="form-label">Terrestre y/o Aéreo</label>
                                        <asp:TextBox ID="txtTerrestreAereo1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Marítimo</label>
                                        <asp:TextBox ID="txtMaritimo1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Paquetería y/o Mensajería</label>
                                        <asp:TextBox ID="txtPaqueteria1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <h5 class="border-bottom pb-2">Riesgos Cubiertos</h5>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('viajeCompleto')">
                                                    <h4 class="mb-0">Viaje Completo</h4>
                                                    <i id="icon-viajeCompleto" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="viajeCompleto" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox ID="txtViajeCompleto" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese una cobertura" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button ID="BtnAgregarCoberturaV" runat="server" CssClass="btn btn-primary" Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Cobertura</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('continuacionViaje')">
                                                    <h4 class="mb-0">Continuación del viaje</h4>
                                                    <i id="icon-continuacionViaje" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="continuacionViaje" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox ID="txtcontinuacionViajeC" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese una cobertura" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button ID="btnagregarContinuacionViaje" runat="server" CssClass="btn btn-primary" Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Cobertura</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('coberturasAdicionales')">
                                                    <h4 class="mb-0">Coberturas Adicionales</h4>
                                                    <i id="icon-coberturasAdicionales" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="coberturasAdicionales" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox ID="txtcoberturasAdicionales" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese una cobertura" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button ID="btnAgregarCcoberturasAdicionales" runat="server" CssClass="btn btn-primary" Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Cobertura</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <label class="form-label">Deducibles</label>
                                        <asp:TextBox ID="txtDeducibles1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                    <h5 class="border-bottom pb-2">Bases de Indemnización</h5>
                                    <div class="col-6">
                                        <label class="form-label">Compras</label>
                                        <asp:TextBox ID="txtCompras1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Ventas</label>
                                        <asp:TextBox ID="txtVentas1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Maquila</label>
                                        <asp:TextBox ID="txtMaquila1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Bienes Usados</label>
                                        <asp:TextBox ID="txtBienesUsados1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Embarques entre Filiales</label>
                                        <asp:TextBox ID="txtEmbarquesEntreFiliales1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Otros</label>
                                        <asp:TextBox ID="txtOtrosBasesIndemnizacion1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Cuota General de la Póliza</label>
                                        <asp:TextBox ID="txtCuotaGeneralPoliza1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <div class="card p-3 shadow-sm">
                                                <h6>Cuotas Mercancías Especiales</h6>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Medicamentos</h6>
                                                        <asp:TextBox ID="txtMedicamentos1" runat="server" CssClass="form-control"
                                                            placeholder="%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <h6 class="titulo-cuota">Cobre, Aluminio y Acero</h6>
                                                        <asp:TextBox ID="txtCobreAluminioAcero1" runat="server" CssClass="form-control"
                                                            placeholder="%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Medicamentos controlados</h6>
                                                        <asp:TextBox ID="txtMedicamentosControlados1" runat="server" CssClass="form-control"
                                                            placeholder="%"></asp:TextBox>
                                                    </div>
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">EQ contratistas</h6>
                                                        <asp:TextBox ID="txtEQ1" runat="server" CssClass="form-control"
                                                            placeholder="%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-check mt-2">
                                                    <input class="form-check-input" type="checkbox" id="chkEQAplica" runat="server">
                                                    <label class="form-check-label" for="chkEQAplica">
                                                        No Aplica
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="card p-3 shadow-sm">
                                                <h6>Datos de Prima</h6>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Prima Neta</h6>
                                                        <asp:TextBox ID="txtPrimaNetaMercancia1" runat="server" CssClass="form-control"
                                                            placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <h6 class="titulo-cuota">Derecho de Póliza</h6>
                                                        <asp:TextBox ID="txtDerechoPolizaMercancia1" runat="server" CssClass="form-control"
                                                            placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Otros</h6>
                                                        <asp:TextBox ID="txtOtrosMercancia1" runat="server" CssClass="form-control"
                                                            placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">IVA</h6>
                                                        <asp:TextBox ID="txtIVAMercancia1" runat="server" CssClass="form-control"
                                                            placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col col-md-12">
                                                    <h6 class="titulo-cuota">Prima Total</h6>
                                                    <asp:TextBox ID="txtPrimaTotalMercancia1" runat="server" CssClass="form-control"
                                                        placeholder="$0.00"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="panel-credito" role="tabpanel">
                                <div class="row g-3 mt-2">
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <label class="form-label">Nombre interno Póliza</label>
                                            <asp:TextBox ID="txtNombreInternoPoliza2" runat="server" CssClass="form-control w-100"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesAsegurados2')">
                                                    <h4 class="mb-0">Bienes Asegurados</h4>
                                                    <i id="icon-bienesAsegurados2" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="upBienesAsegurados2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesAsegurados2" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtBienesAsegurados2" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregaBienAsegurado2" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregaBienAsegurado2_Click" />
                                                            </div>

                                                            <asp:GridView ID="GvBienes2" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienes2_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienes2_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Asegurado" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
                    Eliminar
                   </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesExcluidos2')">
                                                    <h4 class="mb-0">Bienes Excluidos</h4>
                                                    <i id="icon-bienesExcluidos2" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="UpBienesExcluidos2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesExcluidos2" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtBienesExcluidos2" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregacollapseBienesExcluidos2" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregacollapseBienesExcluidos2_Click" />
                                                            </div>
                                                            <asp:GridView ID="GvBienesExcluidos2" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienesExcluidos2_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienesExcluidos2_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Excluido" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
 Eliminar
</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesSujetosConsulta2')">
                                                    <h4 class="mb-0">Bienes Sujetos a Consulta</h4>
                                                    <i id="icon-bienesSujetosConsulta2" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="UpbienesSujetosConsulta2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesSujetosConsulta2" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtbienesSujetosConsulta2" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregaBienesSujetosConsulta2" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregaBienesSujetosConsulta2_Click" />
                                                            </div>
                                                            <asp:GridView ID="GvBienesSujetosConsulta2" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienesSujetosConsulta2_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienesSujetosConsulta2_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Excluido" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
 Eliminar
</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <h5 class="border-bottom pb-2">Limite Máximo Embarque por Medio de Transporte</h5>

                                    <div class="col-md-4">
                                        <label class="form-label">Terrestre y/o Aéreo</label>
                                        <asp:TextBox ID="txtTerrestreAereo2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Marítimo</label>
                                        <asp:TextBox ID="txtMaritimo2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Paquetería y/o Mensajería</label>
                                        <asp:TextBox ID="txtPaqueteria2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <h5 class="border-bottom pb-2">Riesgos Cubiertos</h5>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2"
                                                    style="cursor: pointer;"
                                                    onclick="toggleCollapse('viajeCompleto2')">

                                                    <h4 class="mb-0">Viaje Completo </h4>
                                                    <i id="icon-viajeCompleto2" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="viajeCompleto2" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox ID="txtviajeCompleto2" runat="server"
                                                        CssClass="form-control w-100 mb-2"
                                                        Placeholder="Ingrese una cobertura" TextMode="MultiLine" Rows="4">
                                                    </asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button ID="BtnAgregarCoberturaV2"
                                                            runat="server"
                                                            CssClass="btn btn-primary"
                                                            Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Bien Asegurado</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2"
                                                    style="cursor: pointer;"
                                                    onclick="toggleCollapse('continuacionViaje2')">

                                                    <h4 class="mb-0">Continuación del viaje</h4>
                                                    <i id="icon-continuacionViaje2" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="continuacionViaje2" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox
                                                        ID="txtcontinuacionViajeC2"
                                                        runat="server"
                                                        CssClass="form-control w-100 mb-2"
                                                        Placeholder="Ingrese una cobertura"
                                                        TextMode="MultiLine" Rows="4">
                                                    </asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button
                                                            ID="btnagregarContinuacionViaje2"
                                                            runat="server"
                                                            CssClass="btn btn-primary"
                                                            Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Cobertura</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2"
                                                    style="cursor: pointer;"
                                                    onclick="toggleCollapse('coberturasAdicionales2')">

                                                    <h4 class="mb-0">Coberturas Adicionales</h4>
                                                    <i id="icon-coberturasAdicionales2" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="coberturasAdicionales2" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox
                                                        ID="txtcoberturasAdicionales2"
                                                        runat="server"
                                                        CssClass="form-control w-100 mb-2"
                                                        Placeholder="Ingrese una cobertura"
                                                        TextMode="MultiLine" Rows="4">
                                                    </asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button
                                                            ID="btnAgregarCcoberturasAdicionales2"
                                                            runat="server"
                                                            CssClass="btn btn-primary"
                                                            Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Cobertura</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-12">
                                        <label class="form-label">Deducibles</label>
                                        <asp:TextBox ID="txtDeducibles2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>

                                    <h5 class="border-bottom pb-2">Bases de Indemnización</h5>
                                    <div class="col-6">
                                        <label class="form-label">Compras</label>
                                        <asp:TextBox ID="txtCompras2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Ventas</label>
                                        <asp:TextBox ID="txtVentas2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Maquila</label>
                                        <asp:TextBox ID="txtMaquila2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Bienes Usados</label>
                                        <asp:TextBox ID="txtBienesUsados2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Embarques entre Filiales</label>
                                        <asp:TextBox ID="txtEmbarquesEntreFiliales2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Otros</label>
                                        <asp:TextBox ID="txtOtrosBasesIndemnizacion2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Cuota General de la Póliza</label>
                                        <asp:TextBox ID="txtCuotaGeneralPoliza2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <div class="card p-3 shadow-sm">
                                                <h6>Cuotas Mercancías Especiales</h6>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Medicamentos</h6>
                                                        <asp:TextBox ID="txtMedicamentos2" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <h6 class="titulo-cuota">Cobre, Aluminio y Acero</h6>
                                                        <asp:TextBox ID="txtCobreAluminioAcero2" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Medicamentos controlados</h6>
                                                        <asp:TextBox ID="txtMedicamentosControlados2" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                                    </div>
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">EQ contratistas</h6>
                                                        <asp:TextBox ID="txtEQ2" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-check mt-2">
                                                    <input class="form-check-input" type="checkbox" id="chkEQAplica2" runat="server">
                                                    <label class="form-check-label" for="chkEQAplica2">
                                                        No Aplica
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="card p-3 shadow-sm">
                                                <h6>Datos de Prima</h6>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Prima Neta</h6>
                                                        <asp:TextBox ID="txtPrimaNetaMercancia2" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <h6 class="titulo-cuota">Derecho de Póliza</h6>
                                                        <asp:TextBox ID="txtDerechoPolizaMercancia2" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Otros</h6>
                                                        <asp:TextBox ID="txtOtrosMercancia2" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">IVA</h6>
                                                        <asp:TextBox ID="txtIVAMercancia2" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col col-md-12">
                                                    <h6 class="titulo-cuota">Prima Total</h6>
                                                    <asp:TextBox ID="txtPrimaTotalMercancia2" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="panel-estado" role="tabpanel">
                                <div class="row g-3 mt-2">
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <label class="form-label">Nombre interno Póliza</label>
                                            <asp:TextBox ID="txtNombreInternoPoliza3" runat="server" CssClass="form-control w-100"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesAsegurados3')">
                                                    <h4 class="mb-0">Bienes Asegurados</h4>
                                                    <i id="icon-bienesAsegurados3" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="upBienesAsegurados3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesAsegurados3" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtBienesAsegurados3" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregaBienAsegurado3" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregaBienAsegurado3_Click" />
                                                            </div>

                                                            <asp:GridView ID="GvBienes3" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienes3_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienes3_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Asegurado" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
 Eliminar
</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesExcluidos3')">
                                                    <h4 class="mb-0">Bienes Excluidos</h4>
                                                    <i id="icon-bienesExcluidos3" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="UpBienesExluidos3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesExcluidos3" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtBienesExcluidos3" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregacollapseBienesExcluidos3" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregacollapseBienesExcluidos3_Click" />
                                                            </div>
                                                            <asp:GridView ID="GvBienesExcluidos3" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienesExcluidos3_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienesExcluidos3_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Excluido" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
 Eliminar
</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('bienesSujetosConsulta3')">
                                                    <h4 class="mb-0">Bienes Sujetos a Consulta</h4>
                                                    <i id="icon-bienesSujetosConsulta3" class="bi bi-chevron-down"></i>
                                                </div>
                                                <asp:UpdatePanel ID="UpbienesSujetosConsulta3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="bienesSujetosConsulta3" class="collapse-content px-3 pb-3" style="display: none;">
                                                            <asp:TextBox ID="txtbienesSujetosConsulta3" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese un bien" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                            <div class="d-flex justify-content-end mb-3">
                                                                <asp:Button ID="btnAgregaBienesSujetosConsulta3" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregaBienesSujetosConsulta3_Click" />
                                                            </div>
                                                            <asp:GridView ID="GvBienesSujetosConsulta3" runat="server" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-hover align-middle"
                                                                HeaderStyle-CssClass="table-light"
                                                                DataKeyNames="BienId"
                                                                OnRowCommand="GvBienesSujetosConsulta3_RowCommand"
                                                                AllowPaging="True"
                                                                PageSize="10"
                                                                OnPageIndexChanging="GvBienesSujetosConsulta3_PageIndexChanging">
                                                                <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="Nombre" HeaderText="Bien Excluido" />

                                                                    <asp:TemplateField HeaderText="Acciones">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                                CssClass="btn btn-danger btn-sm"
                                                                                OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
 Eliminar
</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <h5 class="border-bottom pb-2">Limite Máximo Embarque por Medio de Transporte</h5>

                                    <div class="col-md-4">
                                        <label class="form-label">Terrestre y/o Aéreo</label>
                                        <asp:TextBox ID="txtTerrestreAereo3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Marítimo</label>
                                        <asp:TextBox ID="txtMaritimo3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Paquetería y/o Mensajería</label>
                                        <asp:TextBox ID="txtPaqueteria3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <h5 class="border-bottom pb-2">Riesgos Cubiertos</h5>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2"
                                                    style="cursor: pointer;"
                                                    onclick="toggleCollapse('viajeCompleto3')">

                                                    <h4 class="mb-0">Viaje Completo</h4>
                                                    <i id="icon-viajeCompleto3" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="viajeCompleto3" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox ID="txtviajeCompleto3" runat="server"
                                                        CssClass="form-control w-100 mb-2"
                                                        Placeholder="Ingrese una cobertura"
                                                        TextMode="MultiLine" Rows="4">
                                                    </asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button ID="BtnAgregarCoberturaV3"
                                                            runat="server"
                                                            CssClass="btn btn-primary"
                                                            Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Bien Asegurado</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2"
                                                    style="cursor: pointer;"
                                                    onclick="toggleCollapse('continuacionViaje3')">

                                                    <h4 class="mb-0">Continuación del viaje</h4>
                                                    <i id="icon-continuacionViaje3" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="continuacionViaje3" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox
                                                        ID="txtcontinuacionViajeC3"
                                                        runat="server"
                                                        CssClass="form-control w-100 mb-2"
                                                        Placeholder="Ingrese una cobertura"
                                                        TextMode="MultiLine" Rows="4">
                                                    </asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button
                                                            ID="btnagregarContinuacionViaje3"
                                                            runat="server"
                                                            CssClass="btn btn-primary"
                                                            Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Cobertura</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3 gx-0">
                                        <div class="col-md-12 px-2">
                                            <div class="card shadow-sm bg-light w-100">
                                                <div class="d-flex justify-content-between align-items-center p-2"
                                                    style="cursor: pointer;"
                                                    onclick="toggleCollapse('coberturasAdicionales3')">

                                                    <h4 class="mb-0">Coberturas Adicionales </h4>
                                                    <i id="icon-coberturasAdicionales3" class="bi bi-chevron-down"></i>
                                                </div>

                                                <div id="coberturasAdicionales3" class="collapse-content px-3 pb-3" style="display: none;">
                                                    <asp:TextBox
                                                        ID="txtcoberturasAdicionales3"
                                                        runat="server"
                                                        CssClass="form-control w-100 mb-2"
                                                        Placeholder="Ingrese una cobertura"
                                                        TextMode="MultiLine" Rows="4">
                                                    </asp:TextBox>

                                                    <div class="d-flex justify-content-end mb-3">
                                                        <asp:Button
                                                            ID="btnAgregarCcoberturasAdicionales3"
                                                            runat="server"
                                                            CssClass="btn btn-primary"
                                                            Text="Agregar" />
                                                    </div>

                                                    <table class="table table-bordered">
                                                        <thead class="table-light">
                                                            <tr>
                                                                <th>Cobertura</th>
                                                                <th>Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>Mercancía General</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Electrónicos</td>
                                                                <td>
                                                                    <button class="btn btn-danger btn-sm">Eliminar</button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <label class="form-label">Deducibles</label>
                                        <asp:TextBox ID="txtDeducibles3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>

                                    <h5 class="border-bottom pb-2">Bases de Indemnización</h5>
                                    <div class="col-6">
                                        <label class="form-label">Compras</label>
                                        <asp:TextBox ID="txtCompras3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Ventas</label>
                                        <asp:TextBox ID="txtVentas3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Maquila</label>
                                        <asp:TextBox ID="txtMaquila3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Bienes Usados</label>
                                        <asp:TextBox ID="txtBienesUsados3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Embarques entre Filiales</label>
                                        <asp:TextBox ID="txtEmbarquesEntreFiliales3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Otros</label>
                                        <asp:TextBox ID="txtOtrosBasesIndemnizacion3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label">Cuota General de la Póliza</label>
                                        <asp:TextBox ID="txtCuotaGeneralPoliza3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <div class="card p-3 shadow-sm">
                                                <h6>Cuotas Mercancías Especiales</h6>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Medicamentos</h6>
                                                        <asp:TextBox ID="txtMedicamentos3" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <h6 class="titulo-cuota">Cobre, Aluminio y Acero</h6>
                                                        <asp:TextBox ID="txtCobreAluminioAcero3" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Medicamentos controlados</h6>
                                                        <asp:TextBox ID="txtMedicamentosControlados3" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                                    </div>
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">EQ contratistas</h6>
                                                        <asp:TextBox ID="txtEQ3" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-check mt-2">
                                                    <input class="form-check-input" type="checkbox" id="chkEQAplica3" runat="server">
                                                    <label class="form-check-label" for="chkEQAplica3">
                                                        No Aplica
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="card p-3 shadow-sm">
                                                <h6>Datos de Prima</h6>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Prima Neta</h6>
                                                        <asp:TextBox ID="txtPrimaNetaMercancia3" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <h6 class="titulo-cuota">Derecho de Póliza</h6>
                                                        <asp:TextBox ID="txtDerechoPolizaMercancia3" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">Otros</h6>
                                                        <asp:TextBox ID="txtOtrosMercancia3" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                    <div class="col col-md-6">
                                                        <h6 class="titulo-cuota">IVA</h6>
                                                        <asp:TextBox ID="txtIVAMercancia3" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col col-md-12">
                                                    <h6 class="titulo-cuota">Prima Total</h6>
                                                    <asp:TextBox ID="txtPrimaTotalMercancia3" runat="server" CssClass="form-control" placeholder="$0.00"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlContenedor" runat="server">
            <div class="row mt-3">
                <div class="col-md-12">
                    <div class="card p-2 shadow-sm">
                        <div class="row mt-3 gx-0">
                            <div class="col-md-12 px-2">
                                <div class="card shadow-sm bg-light w-100">
                                    <div class="d-flex justify-content-between align-items-center p-2"
                                        style="cursor: pointer;"
                                        onclick="toggleCollapse('BienesAseguradosContenedor')">

                                        <h4 class="mb-0">Bienes asegurados</h4>
                                        <i id="icon-BienesAseguradosContenedor" class="bi bi-chevron-down"></i>
                                    </div>
                                    <asp:UpdatePanel ID="UpBienesAseguradosContenedor" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="BienesAseguradosContenedor" class="collapse-content px-3 pb-3" style="display: none;">
                                                <asp:TextBox ID="txtBienesAseguradosContenedor"
                                                    runat="server"
                                                    CssClass="form-control w-100 mb-2"
                                                    Placeholder="Ingrese un bien asegurado"
                                                    TextMode="MultiLine" Rows="4">
                                                </asp:TextBox>

                                                <div class="d-flex justify-content-end mb-3">
                                                    <asp:Button ID="btnAgregarBienAseguradoContenedor"
                                                        runat="server"
                                                        CssClass="btn btn-primary"
                                                        Text="Agregar" OnClick="btnAgregarBienAseguradoContenedor_Click" />
                                                </div>

                                                <asp:GridView ID="GvBienContenedor" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-hover align-middle"
                                                    HeaderStyle-CssClass="table-light"
                                                    DataKeyNames="BienId"
                                                    OnRowCommand="GvBienContenedor_RowCommand"
                                                    AllowPaging="True"
                                                    PageSize="10"
                                                    OnPageIndexChanging="GvBienContenedor_PageIndexChanging">
                                                    <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                    <Columns>
                                                        <asp:BoundField DataField="Nombre" HeaderText="Bien Asegurado" />

                                                        <asp:TemplateField HeaderText="Acciones">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BienId") %>'
                                                                    CssClass="btn btn-danger btn-sm"
                                                                    OnClientClick="return confirm('¿Seguro que deseas eliminar este bien?');">
 Eliminar
</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-3 gx-0">
                            <div class="col-md-12 px-2">
                                <div class="card shadow-sm bg-light w-100">
                                    <div class="d-flex justify-content-between align-items-center p-2" style="cursor: pointer;" onclick="toggleCollapse('Coberturas')">
                                        <h4 class="mb-0">Coberturas</h4>
                                        <i id="icon-Coberturas" class="bi bi-chevron-down"></i>
                                    </div>
                                    <asp:UpdatePanel ID="UpCoberturas" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="Coberturas" class="collapse-content px-3 pb-3" style="display: none;">
                                                <asp:TextBox ID="txtCoberturaContenedor" runat="server" CssClass="form-control w-100 mb-2" Placeholder="Ingrese una Cobertura" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                <div class="d-flex justify-content-end mb-3">
                                                    <asp:Button ID="btnAgregarCobertura" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregarCobertura_Click" />
                                                </div>

                                                <asp:GridView ID="GvCoberturasContenedor" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-hover align-middle"
                                                    HeaderStyle-CssClass="table-light"
                                                    DataKeyNames="CoberturaId"
                                                    OnRowCommand="GvCoberturasContenedor_RowCommand"
                                                    AllowPaging="True"
                                                    PageSize="10"
                                                    OnPageIndexChanging="GvCoberturasContenedor_PageIndexChanging">
                                                    <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />

                                                    <Columns>
                                                        <asp:BoundField DataField="Nombre" HeaderText="Bien Asegurado" />

                                                        <asp:TemplateField HeaderText="Acciones">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("CoberturaId") %>'
                                                                    CssClass="btn btn-danger btn-sm"
                                                                    OnClientClick="return confirm('¿Seguro que deseas eliminar está cobertura?');">
 Eliminar
</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <h6>Montos Póliza</h6>
                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <h6>Prima Neta</h6>
                                            <asp:TextBox ID="txtPrimaNeta" runat="server" CssClass="form-control mb-3"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6>Otros</h6>
                                            <asp:TextBox ID="txtOtrosMontosPoliza" runat="server" CssClass="form-control mb-3"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <h6>Derecho de póliza</h6>
                                            <asp:TextBox ID="txtDerechoPoliza" runat="server" CssClass="form-control mb-3"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6>IVA</h6>
                                            <asp:TextBox ID="txtIVA" runat="server" CssClass="form-control mb-3"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-12">
                                            <h6>Total</h6>
                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control mb-3"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <h6>Deducibles</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6>Daño Material</h6>
                                            <asp:TextBox ID="txtDañoMaterial" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6>Robo</h6>
                                            <asp:TextBox ID="txtRobo" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6>Pérdida Total</h6>
                                            <asp:TextBox ID="txtPerdidaTotal" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col col-md-6">
                                            <h6>Pérdida Parcial</h6>
                                            <asp:TextBox ID="txtPerdidaParcial" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <div class="card p-3 shadow-sm">
                                    <h6>Valor Máximo Asegurado</h6>
                                    <h6>Por contenedor</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <asp:TextBox ID="txtPorContenedor" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <h6>Por medio de transporte</h6>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6>Ferrocarril</h6>
                                            <asp:TextBox ID="txtFerrocarril" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <h6>Terrestre</h6>
                                            <asp:TextBox ID="txtTerrestreMontosPoliza" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6>Cuota Aplicable</h6>
                                            <asp:TextBox ID="txtCuotaAplicable" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col col-md-6">
                                            <h6>Maniobras de rescate por contenedor</h6>
                                            <asp:TextBox ID="txtManiobrasRescateContenedor" runat="server" CssClass="form-control"
                                                placeholder="$0.00"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
    <script>
        function toggleCollapse(id) {
            const div = document.getElementById(id);
            const icon = document.getElementById('icon-' + id);
            if (!div || !icon) return;

            if (div.style.display === 'none' || div.style.display === '') {
                div.style.display = 'block';
                icon.classList.remove('bi-chevron-down');
                icon.classList.add('bi-chevron-up');

                localStorage.setItem('openCollapse', id);
            } else {
                div.style.display = 'none';
                icon.classList.remove('bi-chevron-up');
                icon.classList.add('bi-chevron-down');

                localStorage.removeItem('openCollapse');
            }
        }

        window.addEventListener('load', () => {
            const collapsibles = document.querySelectorAll('.collapse-content');
            const openId = localStorage.getItem('openCollapse');

            collapsibles.forEach(div => {
                const icon = document.getElementById('icon-' + div.id);
                if (!icon) return;

                if (div.id === openId) {
                    div.style.display = 'block';
                    icon.classList.remove('bi-chevron-down');
                    icon.classList.add('bi-chevron-up');
                } else {
                    div.style.display = 'none';
                    icon.classList.remove('bi-chevron-up');
                    icon.classList.add('bi-chevron-down');
                }
            });
        });
    </script>
</asp:Content>
