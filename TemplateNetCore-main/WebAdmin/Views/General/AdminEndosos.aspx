<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="AdminEndosos.aspx.vb" Inherits="WebAdmin.AdminEndosos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../../Content/site.css" rel="stylesheet" />   
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
