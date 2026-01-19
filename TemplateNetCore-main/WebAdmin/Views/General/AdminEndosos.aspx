<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminEndosos.aspx.vb" Inherits="WebAdmin.AdminEndosos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <style>
        .icon-btn {
            background: none;
            border: none;
            padding: 6px;
            font-size: 20px;
            color: #000;
            cursor: pointer;
        }

            .icon-btn:hover {
                color: #555;
            }

        .filtro-estilo {
            background-color: #f0f4f8;
            border: 1px solid #cbd5e1;
            border-radius: 0.375rem;
            padding-right: 1.5rem;
            cursor: pointer;
            font-size: 0.875rem;
            font-weight: 500;
            color: #334155;
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            background-image: url('data:image/svg+xml;utf8,<svg fill="%23334555" height="20" viewBox="0 0 20 20" width="20" xmlns="http://www.w3.org/2000/svg"><path d="M5.516 7.548a.625.625 0 0 1 .884 0L10 11.154l3.6-3.606a.625.625 0 0 1 .884.884l-4.134 4.134a.625.625 0 0 1-.884 0L5.516 8.432a.625.625 0 0 1 0-.884z"/></svg>');
            background-repeat: no-repeat;
            background-position: right 0.75rem center;
            background-size: 1rem;
        }

            .filtro-estilo:focus {
                outline: none;
                border-color: #3b82f6;
                box-shadow: 0 0 0 3px rgba(59,130,246,0.3);
            }

        .titulo-cuota {
            min-height: 38px;
            display: flex;
            align-items: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlEncabezado" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Endosos</h2>
            <asp:Button ID="btnAgregarEndoso" runat="server" CssClass="btn btn-primary btn-add" Text="Agregar Endoso" />
        </div>
        <div class="mb-4">
            <asp:TextBox ID="txtBuscarEndoso" runat="server" CssClass="form-control"
                placeholder="🔍 Buscar endosos..."></asp:TextBox>
        </div>
    </asp:Panel>
</asp:Content>
