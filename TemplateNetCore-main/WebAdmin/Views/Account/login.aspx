<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="WebAdmin.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <style>
        body {
            background-color: #f5f7fb;
        }

        .login-card {
            max-width: 400px;
            margin: 100px auto;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
            background-color: #fff;
        }

        .btn-custom {
            background-color: #18CE91;
            border-color: #18CE91;
            color: #fff;
        }

            .btn-custom:hover {
                background-color: #15b981;
                border-color: #15b981;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-card">
            <div class="text-center mb-3">
                <img src="../../Resources/Image/logo_MS.png" alt="Logo" width="120" />
            </div>
            <h3 class="text-center mb-4">Iniciar Sesión</h3>
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Correo</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtPassword" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-custom w-100" OnClick="btnLogin_Click" />
           
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
