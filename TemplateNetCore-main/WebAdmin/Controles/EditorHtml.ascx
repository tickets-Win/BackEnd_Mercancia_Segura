<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditorHtml.ascx.vb" Inherits="WebAdmin.EditorHtml" %>

<%-- Editor de texto enriquecido, compartido por el envio de correo y la
     captura de plantillas. Los estilos y el script viven en Default.Master
     porque son globales y se registran una sola vez.

     Quien lo usa lo envuelve en un elemento con clase cor-contenedor: el
     script resuelve todo por clases dentro de ese contenedor, nunca por id,
     asi que admite varias instancias en la misma pagina. --%>

                <%-- Barra de herramientas. data-cor-cmd lo lee el script; se usa
                     mousedown y no click para no perder la seleccion del texto. --%>
                <div class="cor-editor-barra mt-3">
                    <a class="cor-herramienta cor-azul" data-cor-cmd="undo" title="Deshacer"><i class="bi bi-arrow-counterclockwise"></i></a>
                    <a class="cor-herramienta cor-azul" data-cor-cmd="redo" title="Rehacer"><i class="bi bi-arrow-clockwise"></i></a>
                    <a class="cor-herramienta cor-rojo" data-cor-cmd="cut" title="Cortar"><i class="bi bi-scissors"></i></a>
                    <a class="cor-herramienta cor-rojo" data-cor-cmd="copy" title="Copiar"><i class="bi bi-clipboard"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="bold" title="Negrita"><i class="bi bi-type-bold"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="italic" title="Cursiva"><i class="bi bi-type-italic"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="underline" title="Subrayado"><i class="bi bi-type-underline"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="strikeThrough" title="Tachado"><i class="bi bi-type-strikethrough"></i></a>
                    <a class="cor-herramienta cor-azul" data-cor-cmd="foreColor" data-cor-pide="color" title="Color de texto"><i class="bi bi-palette-fill"></i></a>
                    <a class="cor-herramienta cor-rojo" data-cor-cmd="hiliteColor" data-cor-pide="color" title="Resaltar"><i class="bi bi-highlighter"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="justifyLeft" title="Alinear a la izquierda"><i class="bi bi-text-left"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="justifyCenter" title="Centrar"><i class="bi bi-text-center"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="justifyRight" title="Alinear a la derecha"><i class="bi bi-text-right"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="justifyFull" title="Justificar"><i class="bi bi-justify"></i></a>
                    <a class="cor-herramienta cor-verde" data-cor-cmd="insertImage" data-cor-pide="imagen" title="Insertar imagen"><i class="bi bi-image"></i></a>
                    <a class="cor-herramienta cor-azul" data-cor-cmd="tabla" data-cor-pide="tabla" title="Insertar tabla"><i class="bi bi-table"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="insertUnorderedList" title="Lista con viñetas"><i class="bi bi-list-ul"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="insertOrderedList" title="Lista numerada"><i class="bi bi-list-ol"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="createLink" data-cor-pide="enlace" title="Insertar enlace"><i class="bi bi-link-45deg"></i></a>
                    <a class="cor-herramienta" data-cor-cmd="insertHTML" data-cor-pide="simbolo" title="Símbolo"><i class="bi bi-omega"></i></a>
                    <a class="cor-herramienta cor-azul" data-cor-cmd="removeFormat" title="Quitar formato"><i class="bi bi-eraser"></i></a>
                </div>

                <div class="cor-editor-barra">
                    <select class="form-select form-select-sm cor-select-mini" data-cor-sel="fontName">
                        <option value="Montserrat">Montserrat</option>
                        <option value="Arial">Arial</option>
                        <option value="Times New Roman">Times New Roman</option>
                        <option value="Courier New">Courier New</option>
                    </select>
                    <select class="form-select form-select-sm cor-select-mini" data-cor-sel="fontSize">
                        <option value="2">12px</option>
                        <option value="3" selected="selected">16px</option>
                        <option value="4">18px</option>
                        <option value="5">24px</option>
                    </select>
                    <select class="form-select form-select-sm cor-select-mini" data-cor-sel="formatBlock">
                        <option value="p">Normal</option>
                        <option value="h1">Título 1</option>
                        <option value="h2">Título 2</option>
                        <option value="blockquote">Cita</option>
                    </select>
                </div>

                <%-- Tres vistas del mismo contenido. El textarea del codigo NO es
                     control de servidor: si posteara el HTML en claro, la
                     validacion de peticiones de ASP.NET tumbaria la pagina. Lo
                     unico que viaja es el hidden, y va en base64. --%>
                <%-- El cuerpo lo pinta el servidor aqui mismo. Antes solo lo
                     inyectaba el script leyendo el hidden, y si esa lectura
                     fallaba el editor aparecia vacio. --%>
                <div class="cor-cuerpo cor-editable cor-disenio" contenteditable="true"><asp:Literal ID="litCuerpo" runat="server" /></div>
                <textarea class="form-control cor-cuerpo cor-html cor-oculto"></textarea>
                <div class="cor-cuerpo cor-preview cor-oculto"></div>

                <input type="hidden" runat="server" id="hfHtml" class="cor-hidden" />

                <div class="cor-pestanas">
                    <a class="cor-pestana cor-pestana-activa" data-cor-vista="disenio">
                        <i class="bi bi-palette-fill me-1" style="color: #e8963c"></i>Diseño
                    </a>
                    <a class="cor-pestana" data-cor-vista="html">
                        <i class="bi bi-code-slash me-1"></i>HTML
                    </a>
                    <a class="cor-pestana" data-cor-vista="preview">
                        <i class="bi bi-eye me-1"></i>Preview
                    </a>
                </div>
