<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cart</title>
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
            <h1>My cart</h1>

            <asp:Repeater ID="rptCartItems" runat="server">
                <ItemTemplate>
                    <div>
                        <h2><%# Eval("Name") %></h2> <!-- שם התכשיט -->
                        <br />
                        <img src="Images/<%# Eval("PictureUrl") %>" width="200px" height="200px" alt='<%# Eval("Name") %>' /><!-- תמונת התכשיט -->
                        <h2><%# Eval("Quantity") %></h2> <!-- כמות התכשיטים -->
                        <p>Price: <%# Eval("Price", "{0:C}") %></p> <!-- מחיר התכשיט -->
                    </div>
                    <br />
                </ItemTemplate>
            </asp:Repeater>

            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label> <!-- תווית להודעות למשתמש על הקנייה-->
        </div>
    </form>
</body>
</html>
