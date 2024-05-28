<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contact</title>
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
            <h1>Contact</h1>
            <p>
                <strong>Phone Number:</strong> 0533325137
           
            </p>
            <p>
                <strong>Instagram:</strong> <a href="https://instagram.com/noas_jewely" target="_blank">@noas_jewely</a>
            </p>
            <p>
                <strong>Facebook:</strong> <a href="https://facebook.com/noas_jewely1" target="_blank">noas_jewely1</a>
            </p>
            <p>
                <strong>Email:</strong> <a href="mailto:noasjewely@gmail.com">noasjewely@gmail.com</a>
            </p>
        </div>
    </form>
</body>
</html>
