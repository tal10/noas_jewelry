<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Jeweleris.aspx.cs" Inherits="Jeweleris" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Jeweleris</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link rel="stylesheet" href="Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
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
                // if no one is connected
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

        <div align="center">
            <h1>Jewelries</h1>

            <table border="1">
                <tr>
                    <td><a href="JewelerisType.aspx?type=1">
                        <img src="../Images/rings.png" width="300" height="100">
                    </a></td>
                    <td><a href="JewelerisType.aspx?type=2">
                        <img src="../Images/necklace.png" width="300" height="100"></a></td>
                </tr>
                <tr>
                    <td><a href="JewelerisType.aspx?type=3">
                        <img src="../Images/bracelets.png" width="300" height="100"></a></td>
                    <td><a href="JewelerisType.aspx?type=4">
                        <img src="../Images/earrings.png" width="300" height="100">
                    </a></td>

                </tr>
            </table>
        </div>
    </form>
</body>
</html>
