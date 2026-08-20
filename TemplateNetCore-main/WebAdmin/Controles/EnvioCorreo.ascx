<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EnvioCorreo.ascx.vb" Inherits="WebAdmin.EnvioCorreoControl" %>
<%@ Register TagPrefix="uc" TagName="EditorHtml" Src="~/Controles/EditorHtml.ascx" %>

<%-- Pantalla de envio de correo, compartida por cotizaciones, clientes,
     vendedores, beneficiarios y polizas. Todo lo que necesita (estilos, script y
     controles) viaja dentro del control: la pagina que lo usa solo lo coloca
     dentro de su UpdatePanel y llama a Abrir(). --%>

<%-- Los estilos y el script del editor viven en Default.Master: los
     comparte con la captura de plantillas. --%>

<div class="cor-contenedor">

    <asp:Panel ID="pnlAviso" runat="server" Visible="false">
        <asp:Label ID="lblAviso" runat="server"></asp:Label>
    </asp:Panel>

    <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mb-4">
        <div class="d-flex align-items-center gap-3">
            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="cor-volver"
                ToolTip="Regresar" OnClick="lnkCancelar_Click">
                <i class="bi bi-arrow-left"></i>
            </asp:LinkButton>
            <h2 class="cor-titulo mb-0">Envío de correo</h2>
        </div>
        <div class="d-flex gap-2">
            <asp:LinkButton ID="lnkCancelar" runat="server" CssClass="btn btn-light border"
                OnClick="lnkCancelar_Click">Cancelar</asp:LinkButton>
            <asp:LinkButton ID="lnkEnviar" runat="server" CssClass="btn btn-primary"
                OnClick="lnkEnviar_Click">Enviar</asp:LinkButton>
        </div>
    </div>

    <div class="row g-4">

        <%-- ------------------- CUERPO DEL CORREO ------------------- --%>
        <div class="col-lg-8">
            <div class="cor-panel p-4">

                <div class="row g-3 align-items-center">
                    <label class="col-sm-2 col-form-label cor-label">Plantilla:</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlPlantilla" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlPlantilla_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <label class="col-sm-2 col-form-label cor-label">De:</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>

                    <label class="col-sm-2 col-form-label cor-label">Para:</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtPara" runat="server" CssClass="form-control"
                            placeholder="correo@ejemplo.com"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 col-form-label cor-label">CC:</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtCC" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 col-form-label cor-label">CCO:</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtCCO" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <label class="col-sm-2 col-form-label cor-label">Asunto:</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="d-flex align-items-center gap-4 mt-3 cor-opciones">
                    <div class="form-check mb-0">
                        <input class="form-check-input" type="checkbox" id="chkConfirmacion" runat="server" />
                        <label class="form-check-label" for="chkConfirmacion">Solicitar confirmación de lectura</label>
                    </div>
                    <div class="form-check mb-0">
                        <input class="form-check-input" type="checkbox" id="chkFirma" runat="server" />
                        <label class="form-check-label" for="chkFirma">Agregar firma</label>
                    </div>
                </div>

                <%-- Los adjuntos se suben por su cuenta contra el handler, no por
                     postback: un FileUpload de WebForms obliga a recargar toda la
                     pagina y eso interrumpia la captura del correo. --%>
                <div class="d-flex justify-content-end align-items-center gap-2 mt-3">
                    <input type="file" class="form-control form-control-sm cor-archivo cor-file" multiple />
                    <a class="btn btn-sm btn-light border text-nowrap cor-adjuntar">
                        <i class="bi bi-paperclip me-1"></i>Adjuntar archivo
                    </a>
                </div>

                <input type="hidden" runat="server" id="hfLlaveAdjuntos" class="cor-llave" />
                <div class="cor-adjuntos mt-2 cor-lista"></div>

                <%-- Editor compartido con la pantalla de Plantillas. --%>
                <uc:EditorHtml ID="edCuerpo" runat="server" />

            </div>
        </div>

        <%-- ----------------- CAMPOS Y BIBLIOTECA ------------------- --%>
        <div class="col-lg-4">

            <input type="hidden" runat="server" id="hfCampos" class="cor-json-campos" value="[]" />
            <input type="hidden" runat="server" id="hfBiblioteca" class="cor-json-biblioteca" value="[]" />

            <div class="cor-panel mb-4">
                <div class="cor-panel-titulo">Incluir campos:</div>
                <asp:Repeater ID="rptCampos" runat="server">
                    <ItemTemplate>
                        <a class="cor-enlace" data-cor-insertar="campos" data-cor-indice='<%# Container.ItemIndex %>'><%# Container.DataItem %></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="cor-panel">
                <div class="cor-panel-titulo">Biblioteca de textos</div>
                <asp:Repeater ID="rptBiblioteca" runat="server">
                    <ItemTemplate>
                        <a class="cor-enlace" data-cor-insertar="biblioteca" data-cor-indice='<%# Container.ItemIndex %>'><%# Eval("Key") %></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </div>
    </div>
</div>

