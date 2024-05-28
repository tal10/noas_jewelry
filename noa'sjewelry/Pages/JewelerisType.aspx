<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JewelerisType.aspx.cs" Inherits="JewelerisType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Jeweleris Type</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link rel="stylesheet" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
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

            <h1>Jewelry List</h1>

            <!-- רפיטר להצגת רשימת התכשיטים -->
            <asp:Repeater ID="rptJewelries" runat="server">
                <ItemTemplate>
                    <div class="jewelryType">
                        <!-- הצגת שם התכשיט -->
                        <h2><%# Eval("Name") %></h2>
                        <!-- הצגת מחיר התכשיט -->
                        <p>Price: <%# Eval("Price") %></p>
                        <!-- הצגת תמונת התכשיט -->
                        <img src="../Images/<%# Eval("PictureUrl") %>" width="200px" height="200px" alt='<%# Eval("Name") %>' />
                        <%  
                            // אם משתמש מחובר
                            if ((Session["userName"] != null))
                            {
                        %>
                        <br />
                        <!-- כפתור להוספה לעגלה -->
                        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" CommandArgument='<%# Eval("JewelryId") %>' />
                        <% 
                            }
                        %>
                        <br />
                        &nbsp;
                    </div>
                    <br />
                </ItemTemplate>
            </asp:Repeater>

            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
