<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My details</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link rel="stylesheet" href="Styles/StyleSheet.css" />
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
                    else
                    {
                %>
                <a href="UserProfile.aspx">My Details</a>
                <a href="Cart.aspx">My Cart</a>
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
            <h2>User Profile</h2>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblUserName" runat="server" Text="Username:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblBirthDate" runat="server" Text="Birth Date:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtBirthDate" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPassword" runat="server" Text="New Password:"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
