<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="Inicio.aspx.vb" Inherits="WebAdmin.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <link href="../../Content/site.css" rel="stylesheet" />
    <style>
        .text-custom-green {
            color: #93C248;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mb-4">
        <div class="col">
            <h1 class="display-4"><strong>Bienvenido a <span class="text-custom-green">MERCANCIA SEGURA</span></strong></h1>
            <p class="lead"><strong>plataforma para gestionar vendedores, clientes, polizas, cotizaciones y más.</strong></p>
        </div>
    </div>

    <div class="row">

        <!-- Tarjeta de Clientes -->
        <div class="col-md-4 mb-4">
            <div class="card bg-light shadow-sm h-100">
                <div class="card-body">
                    <i class="bi bi-person-vcard fs-1"></i>
                    <h5 class="card-title text-custom-green mb-3">Número de clientes</h5>
                    <p class="card-text"><strong><asp:Label ID="lblClientes" runat="server" Text="—"></asp:Label></strong></p>
                </div>
            </div>
        </div>

        <!-- Tarjeta de Cotizaciones -->
        <div class="col-md-4 mb-4">
            <div class="card bg-light shadow-sm h-100">
                <div class="card-body">
                    <i class="bi bi-file-earmark-ruled fs-1"></i>
                    <h5 class="card-title text-custom-green mb-3">Cotizaciones pendientes</h5>
                    <p class="card-text"><strong><asp:Label ID="lblCotizacionesPendientes" runat="server" Text="—"></asp:Label></strong></p>
                    <small class="text-muted">sin confirmar</small>
                </div>
            </div>
        </div>
        <!-- Tarjeta de Pagos -->
        <div class="col-md-4 mb-4">
            <div class="card bg-light shadow-sm h-100">
                <div class="card-body">
                    <i class="bi bi-cash-coin me-2 fs-1"></i>
                    <h5 class="card-title text-custom-green mb-3">Recibos Vencidos</h5>
                    <p class="card-text"><strong><asp:Label ID="lblRecibosVencidos" runat="server" Text="—"></asp:Label></strong></p>
                    <asp:Panel ID="pnlRecibosPendiente" runat="server" Visible="false">
                        <small class="text-muted">pendiente del módulo de cobranza</small>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="container-fluid" style="max-width: 400px; margin-left: 0; padding: 20px;">
            <h4 class="mb-3" style="color: #000; font-weight: bold;">Tipo de cambio</h4>

            <div class="mb-3 d-flex align-items-center">
                <label for="usd" class="form-label me-2" style="width: 90px;">USD D.O.F:</label>
                <input type="text" id="usd" class="form-control" placeholder="$ 0.00">
            </div>

            <div class="mb-3 d-flex align-items-center">
                <label for="ventanilla" class="form-label me-2" style="width: 90px;">USD Bancario:</label>
                <input type="text" id="ventanilla" class="form-control" placeholder="$ 0.00">
            </div>

            <button type="button" class="btn" style="background-color: #93C248; color: white; font-weight: bold;">Actualizar</button>
        </div>
    </div>
</asp:Content>
