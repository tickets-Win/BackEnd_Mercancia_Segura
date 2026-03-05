<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminBeneficiarios.aspx.vb" Inherits="WebAdmin.AdminBeneficiarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="../../Content/site.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Beneficiario</h2>
            <asp:Button ID="btnAgregarBeneficiarios" runat="server" CssClass="btn btn-primary btn-add"
                Text="Agregar Beneficiario" OnClick="btnAgregarBeneficiarios_Click" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarBeneficiarios" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar beneficiarios..." AutoPostBack="true" OnTextChanged="txtBuscarBeneficiarios_TextChanged"></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlTabla" runat="server">
        <div class="card card-shadow p-4 mb-4">
            <div style="overflow-x: auto; width: 100%;">
                <asp:GridView ID="gvBeneficiariosPreferentes" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    OnRowCommand="gvBeneficiariosPreferentes_RowCommand"
                    HeaderStyle-CssClass="table-light"
                    DataKeyNames="BeneficiarioPreferenteId"
                    AllowPaging="True"
                    PageSize="10"
                    OnPageIndexChanging="gvBeneficiariosPreferentes_PageIndexChanging">
                    <PagerStyle CssClass="gvPager" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="Clave" HeaderText="Clave" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                        <asp:BoundField DataField="RFCAMostrar" HeaderText="RFC" />
                        <asp:BoundField DataField="Pais" HeaderText="País" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("BeneficiarioPreferenteId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Editar">
                                <i class="bi bi-pencil"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("BeneficiarioPreferenteId") %>'
                                    CssClass="icon-btn action-icon" ToolTip="Eliminar"
                                    OnClientClick="return confirm('¿Seguro que deseas eliminar este vendedor?');">
                                <i class="bi bi-trash"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lnkCorreo" runat="server" CommandName="Correo" CommandArgument='<%# Eval("BeneficiarioPreferenteId") %>'
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
    <asp:Panel ID="pnlFormularioBeneficiario" runat="server" CssClass="card p-4 mt-4" Visible="false">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <asp:HiddenField ID="hfBeneficiarioId" runat="server" />
            <h2>
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </h2>

            <div>
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn me-2" BackColor="#97BAA0" ForeColor="White" Text="Cancelar" OnClick="btnCancelar_Click"/>
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn me-2" BackColor="#1294D4" ForeColor="White" Text="Guardar" OnClientClick="return validarCampos();" OnClick="btnGuardar_Click" />
            </div>
        </div>

        <h5 class="border-bottom pb-2">Datos generales</h5>

        <div class="row g-3 mt-2">

            <div class="col-md-4">
                <label class="form-label">Tipo de persona</label>
                <asp:DropDownList ID="ddlTipoPersona" CssClass="form-select required" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="col-md-4">
                <label class="form-label">Clave</label>
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control required clave-15"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">Nacionalidad</label>
                <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Panel ID="pnlDatosFisica" runat="server" Visible="true">
                <div class="row g-3 mt-2">
                    <div class="col-md-4">
                        <label class="form-label">Apellido paterno</label>
                        <asp:TextBox ID="txtApellidoP" runat="server" CssClass="form-control required" oninput="llenarNombreCompleto()"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Apellido materno</label>
                        <asp:TextBox ID="txtApellidoM" runat="server" CssClass="form-control required" oninput="llenarNombreCompleto()"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control required" oninput="llenarNombreCompleto()"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlNombreCompleto" runat="server" CssClass="col-md-4">
                <label class="form-label">Nombre Completo</label>
                <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="pnlRazonSocial" runat="server" CssClass="col-md-4" Visible="false">
                <label class="form-label">Razón / Denominación social</label>
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
            </asp:Panel>

            <div class="col-md-4">
                <label class="form-label">RFC</label>
                <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control rfc-format "></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">RFC Génerico</label>
                <asp:DropDownList ID="ddlRFCGenerico" CssClass="form-select" runat="server">
                </asp:DropDownList>
            </div>

            <div class="col-md-4">
                <label class="form-label">País</label>
                <asp:DropDownList ID="ddlPais" runat="server" CssClass="form-select " Visible="true">
                    <asp:ListItem Text="Selecciona un país" Value="" />
                    <asp:ListItem Text="México" Value="MX"></asp:ListItem>
                    <asp:ListItem Text="Estados Unidos" Value="US"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-4">
                <label class="form-label">Calle</label>
                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control "></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">Número Int.</label>
                <asp:TextBox ID="txtNumeroInt" runat="server" CssClass="form-control "></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">Número Ext.</label>
                <asp:TextBox ID="txtNumeroExt" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">Colonia</label>
                <asp:TextBox ID="txtColonia" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">C.P.</label>
                <asp:TextBox ID="txtCP" runat="server" CssClass="form-control only-numbers"></asp:TextBox>
            </div>

            <div class="col-md-4">
                <label class="form-label">Población</label>
                <asp:TextBox ID="txtPoblacion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>

     <script type="text/javascript">
     function llenarNombreCompleto() {
         var nombre = document.getElementById('<%= txtNombre.ClientID %>').value;
         var apellidoP = document.getElementById('<%= txtApellidoP.ClientID %>').value;
         var apellidoM = document.getElementById('<%= txtApellidoM.ClientID %>').value;

         var nombreCompleto = nombre + ' ' + apellidoP + ' ' + apellidoM;
         document.getElementById('<%= txtNombreCompleto.ClientID %>').value = nombreCompleto.trim();
     }
     </script>
    <script>
        window.onload = function () {
            const rfcInput = document.getElementById('<%= txtRFC.ClientID %>');
     const rfcGenericoSelect = document.getElementById('<%= ddlRFCGenerico.ClientID %>');

            function actualizarEstadoCampos() {
                if (rfcInput.value.trim() !== '') {
                    rfcGenericoSelect.disabled = true;
                } else {
                    rfcGenericoSelect.disabled = false;
                }

                if (rfcGenericoSelect.value !== '0') {
                    rfcInput.disabled = true;
                } else {
                    rfcInput.disabled = false;
                }
            }

            rfcInput.addEventListener('input', actualizarEstadoCampos);
            rfcGenericoSelect.addEventListener('change', actualizarEstadoCampos);

            actualizarEstadoCampos();
        };
    </script>
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
             }, 3000);
         }
     </script>

 <div id="alertPlaceholder" class="position-fixed top-0 start-50 translate-middle-x p-3" style="z-index: 1050;"></div>

   <script>
       function validarCampos() {

           limpiarValidacion();
           let valido = true;

           document.querySelectorAll('.required').forEach(function (campo) {
               if (campo.disabled) return;

               if (!campo.value.trim()) {
                   marcarError(campo, "Este campo es obligatorio");
                   valido = false;
               }
           });

           document.querySelectorAll('.only-numbers').forEach(function (campo) {
               if (campo.value) {
                   if (!/^\d+$/.test(campo.value)) {
                       marcarError(campo, "Solo se permiten números");
                       valido = false;
                   }
                   else if (campo.value.length !== 5) {
                       marcarError(campo, "El código postal debe tener exactamente 5 dígitos");
                       valido = false;
                   }

               }
           });     

           document.querySelectorAll('.rfc-format').forEach(function (campo) {
               if (campo.value) {
                   let rfcRegex = /^([A-ZÑ&]{3,4})\d{6}([A-Z\d]{3})$/;
                   if (!rfcRegex.test(campo.value.toUpperCase())) {
                       marcarError(campo, "RFC no válido");
                       valido = false;
                   }
               }
           });

           document.querySelectorAll('.clave-15').forEach(function (campo) {
               if (campo.value) {
                   let regex = /^[a-zA-Z0-9]{15}$/;

                   if (!regex.test(campo.value)) {
                       marcarError(campo, "La clave debe tener exactamente 15 letras o números");
                       valido = false;
                   }
               }
           });      

           if (!valido) {
               showToast('Corrige los campos marcados', 'danger');
           }

           return valido;
       }

       function marcarError(campo, mensaje) {
           campo.classList.add('is-invalid');

           let feedback = document.createElement("div");
           feedback.className = "invalid-feedback";
           feedback.innerText = mensaje;

           if (!campo.nextElementSibling || !campo.nextElementSibling.classList.contains("invalid-feedback")) {
               campo.parentNode.appendChild(feedback);
           }
       }

       function limpiarValidacion() {
           document.querySelectorAll('.is-invalid').forEach(function (campo) {
               campo.classList.remove('is-invalid');
           });

           document.querySelectorAll('.invalid-feedback').forEach(function (msg) {
               msg.remove();
           });
       }
   </script>
     <script>
         document.addEventListener("DOMContentLoaded", function () {

             document.querySelectorAll("input").forEach(function (input) {

                 input.addEventListener("keydown", function (e) {

                     if (e.key === "Enter") {
                         e.preventDefault();
                         return false;
                     }

                 });

             });

         });
     </script>
</asp:Content>
