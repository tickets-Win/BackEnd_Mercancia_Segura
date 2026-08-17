<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminPlantillas.aspx.vb" Inherits="WebAdmin.AdminPlantillas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />

    <style>
        /* Estilos propios de Plantillas, igual que en Reportes: viven en la
           pagina para no tocar site.css ni afectar a las demas pantallas. */
        .plt-titulo {
            color: #17406d;
            font-weight: 700;
        }

        .plt-buscador {
            background: #eef2f7;
            border: 1px solid #e3e8ef;
        }

            .plt-buscador:focus {
                background: #fff;
            }

        .plt-seccion {
            font-size: .8125rem;
            font-weight: 700;
            color: #1f2937;
        }

        .plt-categorias-titulo {
            font-size: .8125rem;
            color: #6b7280;
        }

        /* Lista de categorias */
        .plt-categoria {
            display: block;
            width: 100%;
            text-align: left;
            padding: .5rem .9rem;
            margin-bottom: .35rem;
            border-radius: .375rem;
            font-size: .8125rem;
            color: #374151;
            text-decoration: none !important;
            background: transparent;
            border: none;
        }

            .plt-categoria:hover {
                background: #f1f5f9;
                color: #17406d;
            }

        .plt-categoria-activa {
            background: #17406d !important;
            color: #fff !important;
            font-weight: 600;
        }

        /* Tarjeta de plantilla */
        .plt-tarjeta {
            background: #fff;
            border: 1px solid #e3e8ef;
            border-radius: .5rem;
            box-shadow: 0 1px 3px rgba(15,23,42,.06);
            height: 100%;
            display: flex;
            flex-direction: column;
        }

        .plt-tarjeta-encabezado {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: .5rem;
            padding: .75rem .9rem;
        }

        .plt-remitente {
            font-size: .875rem;
            font-weight: 600;
            color: #111827;
        }

        .plt-acciones {
            display: flex;
            gap: .25rem;
        }

        .plt-icono {
            border: 1px solid #e3e8ef;
            background: #f8fafc;
            border-radius: .3rem;
            padding: .15rem .35rem;
            font-size: .75rem;
            line-height: 1.4;
            text-decoration: none !important;
        }

            .plt-icono:hover {
                background: #eef2f7;
            }

        .plt-icono-editar {
            color: #e8963c;
        }

        .plt-icono-copiar {
            color: #2563eb;
        }

        .plt-icono-borrar {
            color: #2563eb;
        }

        .plt-vista {
            padding: 0 .9rem .9rem;
            font-size: .75rem;
            color: #6b7280;
            line-height: 1.5;
            flex: 1;
        }

        .plt-tarjeta-pie {
            border-top: 1px solid #eef2f7;
            padding: .6rem .9rem .9rem;
        }

        .plt-adjuntos {
            font-size: .6875rem;
            color: #9ca3af;
        }

        .plt-nombre {
            font-size: .875rem;
            font-weight: 600;
            color: #111827;
            margin-top: .35rem;
        }

        .plt-vacio {
            font-size: .8125rem;
            color: #9ca3af;
        }

        /* ---------------------- Formulario de plantilla ---------------------- */
        .plt-volver {
            border: 1px solid #d7dee8;
            background: #fff;
            border-radius: .375rem;
            padding: .25rem .6rem;
            color: #17406d;
            text-decoration: none !important;
            line-height: 1.6;
        }

            .plt-volver:hover {
                background: #f1f5f9;
            }

        .plt-panel {
            background: #fff;
            border: 1px solid #e3e8ef;
            border-radius: .5rem;
        }

        .plt-label {
            font-size: .8125rem;
            color: #374151;
            margin-bottom: .35rem;
        }

        /* Barra de herramientas del editor. Es decorativa por ahora. */
        .plt-editor-barra {
            border: 1px solid #e3e8ef;
            border-bottom: none;
            background: #fff;
            padding: .35rem .5rem;
            display: flex;
            flex-wrap: wrap;
            gap: .15rem;
            align-items: center;
        }

        .plt-herramienta {
            border: none;
            background: transparent;
            border-radius: .25rem;
            padding: .1rem .3rem;
            font-size: .75rem;
            color: #475569;
            line-height: 1.5;
            text-decoration: none !important;
        }

            .plt-herramienta:hover {
                background: #eef2f7;
            }

        .plt-herramienta-azul {
            color: #2563eb;
        }

        .plt-herramienta-rojo {
            color: #dc2626;
        }

        .plt-herramienta-verde {
            color: #16a34a;
        }

        .plt-select-mini {
            font-size: .6875rem;
            padding: .1rem 1.2rem .1rem .35rem;
            height: auto;
            width: auto;
            display: inline-block;
        }

        .plt-cuerpo {
            border: 1px solid #e3e8ef;
            border-radius: 0;
            min-height: 260px;
            resize: vertical;
            font-size: .8125rem;
        }

        /* Pestanas Diseno / HTML del pie del editor */
        .plt-pestanas {
            border: 1px solid #e3e8ef;
            border-top: none;
            display: flex;
        }

        .plt-pestana {
            font-size: .75rem;
            color: #374151;
            padding: .45rem .9rem;
            border-right: 1px solid #e3e8ef;
            text-decoration: none !important;
        }

            .plt-pestana:hover {
                background: #f8fafc;
            }

        .plt-pestana-activa {
            background: #f8fafc;
            font-weight: 600;
        }

        /* Panel de campos a insertar */
        .plt-campos-titulo {
            font-size: .8125rem;
            font-weight: 600;
            color: #1f2937;
            padding: .9rem .9rem .6rem;
        }

        .plt-campo {
            display: block;
            padding: .55rem .9rem;
            font-size: .8125rem;
            color: #2563eb;
            border-top: 1px solid #eef2f7;
            text-decoration: none !important;
        }

            .plt-campo:hover {
                background: #f8fafc;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Todo vive dentro del mismo UpdatePanel a proposito. Los botones que
         cambian la propiedad Visible de un panel tienen que estar en el mismo
         UpdatePanel que ese panel; si no, la pantalla se queda en blanco. --%>
    <asp:UpdatePanel ID="UpPlantillas" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <asp:Panel ID="pnlAviso" runat="server" Visible="false">
                <asp:Label ID="lblAviso" runat="server"></asp:Label>
            </asp:Panel>

            <asp:HiddenField ID="hfPlantillaId" runat="server" Value="" />

            <%-- ======================== LISTADO ======================== --%>
            <asp:Panel ID="pnlListado" runat="server">

                <div class="mb-4">
                    <h2 class="plt-titulo mb-1">Plantillas</h2>
                    <p class="text-muted small mb-0">Gestiona y personaliza tus plantillas de correo</p>
                </div>

                <div class="row g-3 align-items-center mb-4">
                    <div class="col-md-8">
                        <div class="input-group">
                            <span class="input-group-text plt-buscador border-end-0">
                                <i class="bi bi-search text-muted"></i>
                            </span>
                            <asp:TextBox ID="txtBuscarPlantilla" runat="server" CssClass="form-control plt-buscador border-start-0"
                                placeholder="Buscar  plantilla"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 text-md-end">
                        <asp:LinkButton ID="lnkNuevaPlantilla" runat="server" CssClass="btn btn-primary"
                            OnClick="lnkNuevaPlantilla_Click">
                            Nueva plantilla
                        </asp:LinkButton>
                    </div>
                </div>

                <div class="row g-4">

                    <%-- ------------------- CATEGORIAS ------------------- --%>
                    <div class="col-md-3 col-lg-2">
                        <div class="plt-seccion mb-3">Email</div>
                        <div class="plt-categorias-titulo mb-2">Categorías</div>

                        <asp:Repeater ID="rptCategorias" runat="server" OnItemCommand="rptCategorias_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CommandName="Seleccionar"
                                    CommandArgument='<%# Eval("CategoriaPlantillaId") %>'
                                    CssClass='<%# ClaseCategoria(Eval("CategoriaPlantillaId")) %>'
                                    Text='<%# Eval("Nombre") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <%-- ------------------- PLANTILLAS ------------------- --%>
                    <div class="col-md-9 col-lg-10">
                        <asp:Repeater ID="rptPlantillas" runat="server" OnItemCommand="rptPlantillas_ItemCommand">
                            <HeaderTemplate>
                                <div class="row g-3">
                            </HeaderTemplate>

                            <ItemTemplate>
                                <div class="col-xl-3 col-lg-4 col-md-6">
                                    <div class="plt-tarjeta">

                                        <div class="plt-tarjeta-encabezado">
                                            <span class="plt-remitente"><%# Eval("Remitente") %></span>
                                            <span class="plt-acciones">
                                                <asp:LinkButton runat="server" CommandName="Editar"
                                                    CommandArgument='<%# Eval("PlantillaCorreoId") %>'
                                                    CssClass="plt-icono plt-icono-editar" ToolTip="Editar">
                                                    <i class="bi bi-pencil-fill"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" CommandName="Duplicar"
                                                    CommandArgument='<%# Eval("PlantillaCorreoId") %>'
                                                    CssClass="plt-icono plt-icono-copiar" ToolTip="Duplicar">
                                                    <i class="bi bi-files"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" CommandName="Eliminar"
                                                    CommandArgument='<%# Eval("PlantillaCorreoId") %>'
                                                    CssClass="plt-icono plt-icono-borrar" ToolTip="Eliminar"
                                                    OnClientClick="return confirm('¿Seguro que deseas eliminar esta plantilla?');">
                                                    <i class="bi bi-trash"></i>
                                                </asp:LinkButton>
                                            </span>
                                        </div>

                                        <div class="plt-vista"><%# Eval("Vista") %></div>

                                        <div class="plt-tarjeta-pie">
                                            <div class="plt-adjuntos"><%# Eval("Adjuntos") %> Archivos adjuntos</div>
                                            <div class="plt-nombre"><%# Eval("Nombre") %></div>
                                        </div>

                                    </div>
                                </div>
                            </ItemTemplate>

                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>

                        <asp:Label ID="lblSinPlantillas" runat="server" CssClass="plt-vacio" Visible="false"
                            Text="No hay plantillas en esta categoría."></asp:Label>
                    </div>

                </div>
            </asp:Panel>

            <%-- ====================== FORMULARIO ======================= --%>
            <asp:Panel ID="pnlFormulario" runat="server" Visible="false">

                <div class="d-flex justify-content-between align-items-center flex-wrap gap-2 mb-4">
                    <div class="d-flex align-items-center gap-3">
                        <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="plt-volver"
                            ToolTip="Regresar" OnClick="lnkCancelar_Click">
                            <i class="bi bi-arrow-left"></i>
                        </asp:LinkButton>
                        <h2 class="plt-titulo mb-0">
                            <asp:Label ID="lblTituloFormulario" runat="server" Text="Nueva Plantilla"></asp:Label>
                        </h2>
                    </div>

                    <div class="d-flex gap-2">
                        <asp:LinkButton ID="lnkCancelar" runat="server" CssClass="btn btn-light border"
                            OnClick="lnkCancelar_Click">Cancelar</asp:LinkButton>
                        <asp:LinkButton ID="lnkGuardar" runat="server" CssClass="btn btn-primary"
                            OnClick="lnkGuardar_Click">Guardar</asp:LinkButton>
                    </div>
                </div>

                <div class="row g-4">

                    <%-- ---------------- DATOS Y EDITOR ------------------ --%>
                    <div class="col-lg-8">
                        <div class="plt-panel p-3">

                            <div class="row g-3">
                                <div class="col-md-7">
                                    <div class="plt-label">Nombre de plantilla</div>
                                    <asp:TextBox ID="txtNombrePlantilla" runat="server" CssClass="form-control"
                                        placeholder="Ej: Recordatorio de pago"></asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <div class="plt-label">Categoría</div>
                                    <asp:DropDownList ID="ddlCategoriaPlantilla" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="mt-3">
                                <div class="plt-label">Asunto</div>
                                <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control"
                                    placeholder="Asunto del correo"></asp:TextBox>
                            </div>

                            <div class="text-end mt-3">
                                <a href="javascript:void(0)" class="btn btn-sm btn-light border">
                                    <i class="bi bi-paperclip me-1"></i>Adjuntar archivo
                                </a>
                            </div>

                            <%-- Barra de herramientas: por ahora es decorativa. --%>
                            <div class="plt-editor-barra mt-3">
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-azul" title="Deshacer"><i class="bi bi-arrow-counterclockwise"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-azul" title="Rehacer"><i class="bi bi-arrow-clockwise"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-rojo" title="Cortar"><i class="bi bi-scissors"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-rojo" title="Copiar"><i class="bi bi-clipboard"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Negrita"><i class="bi bi-type-bold"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Cursiva"><i class="bi bi-type-italic"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Subrayado"><i class="bi bi-type-underline"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Tachado"><i class="bi bi-type-strikethrough"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-azul" title="Color de texto"><i class="bi bi-palette-fill"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-rojo" title="Resaltar"><i class="bi bi-highlighter"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Alinear a la izquierda"><i class="bi bi-text-left"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Centrar"><i class="bi bi-text-center"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Alinear a la derecha"><i class="bi bi-text-right"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Justificar"><i class="bi bi-justify"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-verde" title="Insertar imagen"><i class="bi bi-image"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-azul" title="Insertar tabla"><i class="bi bi-table"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Lista con viñetas"><i class="bi bi-list-ul"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Lista numerada"><i class="bi bi-list-ol"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Insertar enlace"><i class="bi bi-link-45deg"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta" title="Símbolo"><i class="bi bi-omega"></i></a>
                                <a href="javascript:void(0)" class="plt-herramienta plt-herramienta-azul" title="Buscar"><i class="bi bi-search"></i></a>
                            </div>

                            <div class="plt-editor-barra">
                                <asp:DropDownList ID="ddlFuente" runat="server" CssClass="form-select form-select-sm plt-select-mini">
                                    <asp:ListItem Text="Montserrat" Value="Montserrat" />
                                    <asp:ListItem Text="Arial" Value="Arial" />
                                    <asp:ListItem Text="Times New Roman" Value="Times New Roman" />
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlTamanioFuente" runat="server" CssClass="form-select form-select-sm plt-select-mini">
                                    <asp:ListItem Text="12px" Value="12" />
                                    <asp:ListItem Text="14px" Value="14" />
                                    <asp:ListItem Text="16px" Value="16" Selected="True" />
                                    <asp:ListItem Text="18px" Value="18" />
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlEstiloTexto" runat="server" CssClass="form-select form-select-sm plt-select-mini">
                                    <asp:ListItem Text="Normal" Value="Normal" />
                                    <asp:ListItem Text="Título 1" Value="H1" />
                                    <asp:ListItem Text="Título 2" Value="H2" />
                                </asp:DropDownList>
                            </div>

                            <asp:TextBox ID="txtCuerpoCorreo" runat="server" TextMode="MultiLine"
                                CssClass="form-control plt-cuerpo"
                                placeholder="Escribe el cuerpo del correo aquí..."></asp:TextBox>

                            <div class="plt-pestanas">
                                <a href="javascript:void(0)" class="plt-pestana plt-pestana-activa">
                                    <i class="bi bi-palette-fill me-1" style="color: #e8963c"></i>Diseño
                                </a>
                                <a href="javascript:void(0)" class="plt-pestana">
                                    <i class="bi bi-code-slash me-1"></i>HTML
                                </a>
                            </div>

                        </div>
                    </div>

                    <%-- ------------------ CAMPOS A INSERTAR -------------- --%>
                    <div class="col-lg-4">
                        <div class="plt-panel">
                            <div class="plt-campos-titulo">Incluir campos:</div>

                            <asp:Repeater ID="rptCampos" runat="server">
                                <ItemTemplate>
                                    <a href="javascript:void(0)" class="plt-campo"><%# Container.DataItem %></a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
