<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EnvioCorreo.ascx.vb" Inherits="WebAdmin.EnvioCorreoControl" %>

<%-- Pantalla de envio de correo, compartida por cotizaciones, clientes,
     vendedores, beneficiarios y polizas. Todo lo que necesita (estilos, script y
     controles) viaja dentro del control: la pagina que lo usa solo lo coloca
     dentro de su UpdatePanel y llama a Abrir(). --%>

<style>
    .cor-titulo {
        color: #17406d;
        font-weight: 700;
    }

    .cor-volver {
        border: 1px solid #d7dee8;
        background: #fff;
        border-radius: .375rem;
        padding: .25rem .6rem;
        color: #17406d;
        text-decoration: none !important;
        line-height: 1.6;
    }

        .cor-volver:hover {
            background: #f1f5f9;
        }

    .cor-panel {
        background: #fff;
        border: 1px solid #e3e8ef;
        border-radius: .5rem;
    }

    .cor-panel-titulo {
        font-size: .8125rem;
        font-weight: 600;
        color: #1f2937;
        padding: .9rem .9rem .6rem;
    }

    .cor-label {
        font-size: .8125rem;
        color: #374151;
        text-align: right;
    }

    .cor-opciones {
        font-size: .75rem;
        color: #4b5563;
    }

    .cor-editor-barra {
        border: 1px solid #e3e8ef;
        border-bottom: none;
        background: #fff;
        padding: .35rem .5rem;
        display: flex;
        flex-wrap: wrap;
        gap: .15rem;
        align-items: center;
    }

    .cor-herramienta {
        border: none;
        background: transparent;
        border-radius: .25rem;
        padding: .1rem .3rem;
        font-size: .75rem;
        color: #475569;
        line-height: 1.5;
        text-decoration: none !important;
    }

        .cor-herramienta:hover {
            background: #eef2f7;
        }

    .cor-azul {
        color: #2563eb;
    }

    .cor-rojo {
        color: #dc2626;
    }

    .cor-verde {
        color: #16a34a;
    }

    .cor-select-mini {
        font-size: .6875rem;
        padding: .1rem 1.2rem .1rem .35rem;
        height: auto;
        width: auto;
        display: inline-block;
    }

    .cor-cuerpo {
        border: 1px solid #e3e8ef;
        border-radius: 0;
        min-height: 240px;
        resize: vertical;
        font-size: .8125rem;
    }

    .cor-editable {
        overflow-y: auto;
        padding: .6rem .75rem;
        background: #fff;
    }

        .cor-editable:focus {
            outline: none;
            border-color: #3b82f6;
        }

    .cor-oculto {
        display: none !important;
    }

    .cor-pestanas {
        border: 1px solid #e3e8ef;
        border-top: none;
        display: flex;
    }

    .cor-pestana {
        font-size: .75rem;
        color: #374151;
        padding: .45rem .9rem;
        border-right: 1px solid #e3e8ef;
        text-decoration: none !important;
        cursor: pointer;
    }

        .cor-pestana:hover {
            background: #f8fafc;
        }

    .cor-pestana-activa {
        background: #f8fafc;
        font-weight: 600;
    }

    .cor-enlace {
        display: block;
        padding: .55rem .9rem;
        font-size: .8125rem;
        color: #2563eb;
        border-top: 1px solid #eef2f7;
        text-decoration: none !important;
        cursor: pointer;
    }

        .cor-enlace:hover {
            background: #f8fafc;
        }

    .cor-archivo {
        max-width: 320px;
    }

    .cor-adjuntos {
        display: flex;
        flex-wrap: wrap;
        gap: .4rem;
    }

    .cor-adjunto {
        background: #f1f5f9;
        border: 1px solid #e3e8ef;
        border-radius: 1rem;
        padding: .15rem .6rem;
        font-size: .75rem;
        color: #374151;
    }

    .cor-quitar {
        color: #dc2626;
        font-weight: 700;
        margin-left: .35rem;
        text-decoration: none !important;
    }
</style>

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

                <div class="d-flex justify-content-end align-items-center gap-2 mt-3">
                    <asp:FileUpload ID="fuAdjunto" runat="server" AllowMultiple="true"
                        CssClass="form-control form-control-sm cor-archivo" />
                    <asp:LinkButton ID="lnkAdjuntar" runat="server" CssClass="btn btn-sm btn-light border text-nowrap"
                        OnClick="lnkAdjuntar_Click">
                        <i class="bi bi-paperclip me-1"></i>Adjuntar archivo
                    </asp:LinkButton>
                </div>

                <asp:Repeater ID="rptAdjuntos" runat="server" OnItemCommand="rptAdjuntos_ItemCommand">
                    <HeaderTemplate>
                        <div class="cor-adjuntos mt-2">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class="cor-adjunto">
                            <i class="bi bi-paperclip me-1"></i><%# Eval("Nombre") %>
                            <span class="text-muted ms-1">(<%# Eval("Tamanio") %>)</span>
                            <asp:LinkButton runat="server" CommandName="Quitar"
                                CommandArgument='<%# Container.ItemIndex %>'
                                CssClass="cor-quitar" ToolTip="Quitar">&times;</asp:LinkButton>
                        </span>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>

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

<script>
    // Se registra una sola vez aunque el control se vuelva a pintar por un
    // postback parcial. Todo se resuelve por clases dentro de .cor-contenedor,
    // asi que no depende de ids y admite varias instancias.
    (function () {
        if (window.msCorreoRegistrado) { msCorreoPreparar(); return; }
        window.msCorreoRegistrado = true;

        function caja(el) { return el ? el.closest('.cor-contenedor') : null; }
        function uno(c, sel) { return c ? c.querySelector(sel) : null; }

        function codificar(s) {
            try { return btoa(unescape(encodeURIComponent(s || ''))); } catch (e) { return ''; }
        }
        function decodificar(s) {
            try { return decodeURIComponent(escape(atob(s || ''))); } catch (e) { return ''; }
        }

        function leerHtml(c) { var h = uno(c, '.cor-hidden'); return h ? decodificar(h.value) : ''; }
        function escribirHtml(c, html) { var h = uno(c, '.cor-hidden'); if (h) { h.value = codificar(html); } }

        function vistaActual(c) {
            var h = uno(c, '.cor-hidden');
            return (h && h.getAttribute('data-vista')) || 'disenio';
        }

        function sincronizar(c) {
            if (!c) { return; }
            if (vistaActual(c) === 'html') {
                var src = uno(c, '.cor-html');
                if (src) { escribirHtml(c, src.value); }
            } else {
                var ed = uno(c, '.cor-disenio');
                if (ed) { escribirHtml(c, ed.innerHTML); }
            }
        }
        window.msCorreoSincronizar = function () {
            document.querySelectorAll('.cor-contenedor').forEach(sincronizar);
        };

        function cambiarVista(c, vista) {
            sincronizar(c);

            var html = leerHtml(c);
            var ed = uno(c, '.cor-disenio'), src = uno(c, '.cor-html'), pv = uno(c, '.cor-preview');

            [ed, src, pv].forEach(function (x) { if (x) { x.classList.add('cor-oculto'); } });
            c.querySelectorAll('.cor-pestana').forEach(function (t) { t.classList.remove('cor-pestana-activa'); });

            if (vista === 'html') { src.value = html; src.classList.remove('cor-oculto'); }
            else if (vista === 'preview') { pv.innerHTML = html; pv.classList.remove('cor-oculto'); }
            else { ed.innerHTML = html; ed.classList.remove('cor-oculto'); }

            var tab = c.querySelector('.cor-pestana[data-cor-vista="' + vista + '"]');
            if (tab) { tab.classList.add('cor-pestana-activa'); }

            var h = uno(c, '.cor-hidden');
            if (h) { h.setAttribute('data-vista', vista); }
        }

        function ejecutar(c, comando, valor) {
            var ed = uno(c, '.cor-disenio');
            if (ed) { ed.focus(); }
            try { document.execCommand(comando, false, valor === undefined ? null : valor); } catch (e) { }
            sincronizar(c);
        }

        // mousedown: con click el boton toma el foco y se pierde la seleccion.
        document.addEventListener('mousedown', function (ev) {
            var boton = ev.target.closest('[data-cor-cmd]');
            if (!boton) { return; }

            ev.preventDefault();

            var c = caja(boton);
            if (!c) { return; }

            var comando = boton.getAttribute('data-cor-cmd');
            var pide = boton.getAttribute('data-cor-pide');

            if (!pide) { ejecutar(c, comando); return; }

            if (pide === 'color') {
                var color = prompt('Color (nombre o #RRGGBB):', '#000000');
                if (color) { ejecutar(c, comando, color); }
            } else if (pide === 'enlace') {
                var url = prompt('Dirección del enlace:', 'https://');
                if (url) { ejecutar(c, comando, url); }
            } else if (pide === 'imagen') {
                var img = prompt('Dirección de la imagen:', 'https://');
                if (img) { ejecutar(c, comando, img); }
            } else if (pide === 'simbolo') {
                var s = prompt('Símbolo a insertar:', '©');
                if (s) { ejecutar(c, 'insertHTML', s); }
            } else if (pide === 'tabla') {
                var filas = parseInt(prompt('Número de filas:', '2'), 10);
                var cols = parseInt(prompt('Número de columnas:', '2'), 10);
                if (!filas || !cols || filas < 1 || cols < 1) { return; }

                var t = '<table border="1" cellpadding="4" cellspacing="0" style="border-collapse:collapse">';
                for (var f = 0; f < filas; f++) {
                    t += '<tr>';
                    for (var k = 0; k < cols; k++) { t += '<td>&nbsp;</td>'; }
                    t += '</tr>';
                }
                t += '</table><br />';
                ejecutar(c, 'insertHTML', t);
            }
        });

        document.addEventListener('change', function (ev) {
            var sel = ev.target.closest('[data-cor-sel]');
            if (!sel) { return; }
            ejecutar(caja(sel), sel.getAttribute('data-cor-sel'), sel.value);
        });

        document.addEventListener('click', function (ev) {
            var tab = ev.target.closest('.cor-pestana[data-cor-vista]');
            if (tab) {
                ev.preventDefault();
                cambiarVista(caja(tab), tab.getAttribute('data-cor-vista'));
                return;
            }

            var ins = ev.target.closest('[data-cor-insertar]');
            if (!ins) { return; }

            ev.preventDefault();

            var c = caja(ins);
            if (!c) { return; }

            var origen = ins.getAttribute('data-cor-insertar');
            var hf = uno(c, origen === 'campos' ? '.cor-json-campos' : '.cor-json-biblioteca');
            if (!hf) { return; }

            var lista;
            try { lista = JSON.parse(hf.value || '[]'); } catch (e) { return; }

            var i = parseInt(ins.getAttribute('data-cor-indice'), 10);
            if (isNaN(i) || i < 0 || i >= lista.length) { return; }

            var texto = lista[i];

            if (vistaActual(c) === 'html') {
                var src = uno(c, '.cor-html');
                if (!src) { return; }

                var ini = src.selectionStart, fin = src.selectionEnd;
                if (ini === null || ini === undefined) { src.value += texto; }
                else {
                    src.value = src.value.substring(0, ini) + texto + src.value.substring(fin);
                    src.selectionStart = src.selectionEnd = ini + texto.length;
                }
                src.focus();
                sincronizar(c);
            } else {
                var html = texto
                    .replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;')
                    .replace(/\r?\n/g, '<br />');
                ejecutar(c, 'insertHTML', html);
            }
        });

        // Antes de cualquier postback, lo que se ve es lo que se manda.
        if (typeof Sys !== 'undefined' && Sys.WebForms) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(function () { window.msCorreoSincronizar(); });
            prm.add_endRequest(function () { msCorreoPreparar(); });
        }

        var f = document.forms[0];
        if (f) { f.addEventListener('submit', function () { window.msCorreoSincronizar(); }); }

        msCorreoPreparar();
    })();

    // Vuelca el hidden al editor. Corre en cada render del control.
    function msCorreoPreparar() {
        document.querySelectorAll('.cor-contenedor').forEach(function (c) {
            var ed = c.querySelector('.cor-disenio'), h = c.querySelector('.cor-hidden');
            if (!ed || !h) { return; }

            var html = '';
            try { html = decodeURIComponent(escape(atob(h.value || ''))); } catch (e) { html = ''; }

            if (html) {
                // El hidden manda: trae lo ultimo que se edito o lo que puso el
                // servidor al cambiar de plantilla.
                ed.innerHTML = html;
            } else {
                // Sin hidden util, se respeta lo que el servidor ya pinto en el
                // area editable y se sube al hidden para que viaje al postear.
                try { h.value = btoa(unescape(encodeURIComponent(ed.innerHTML))); } catch (e) { }
            }

            ed.oninput = function () { window.msCorreoSincronizar(); };

            var src = c.querySelector('.cor-html');
            if (src) { src.oninput = function () { window.msCorreoSincronizar(); }; }
        });
    }
</script>
