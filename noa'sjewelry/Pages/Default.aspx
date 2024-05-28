<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link rel="stylesheet" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="topnav">
                <a class="active" href="Default.aspx">
                    <div class="fas fa-home"></div>
                </a>

                <%
                    // if no one is connected
                    if ((Session["userName"] == null) && (Session["isAdmin"] == null))
                    {
                %>
                <a href="Login.aspx">Login</a>
                <a href="Register.aspx">Register</a>
                <%
                    }
           
                %>
                <a href="Jeweleris.aspx">Jeweleris</a>
                <%
                    if (Session["isAdmin"] != null && (bool)Session["isAdmin"] == true)
                    {
                %>
                <a href="Admin.aspx">Admin Page</a>
                <%
                    }
                    else if (Session["userName"] != null)
                    {
                %>
                <a href="UserProfile.aspx">My Details</a>
                 <a href="Cart.aspx">My Cart</a>
                <%
                    }
                %>
                <a href="Contact.aspx">Contact</a>
                <%
                    // if user or admin is connected
                    if ((Session["userName"] != null) ||
                        (Session["isAdmin"] != null && (bool)Session["isAdmin"] == true))
                    {
                %>
                <a href="Logout.aspx">Logout</a>
                <%
                    }
                %>
            </div>
            <br />
            <div class="greeting">
                <asp:Label ID="lblGreeting" runat="server" Text=""></asp:Label>
            </div>

            <div class="center-content">
                <h2 style="color: white">Welcome to noa's jewlery </h2>
            </div>
        </div>
    </form>
</body>
</html>
