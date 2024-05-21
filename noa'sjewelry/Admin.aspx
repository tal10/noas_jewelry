<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Page</title>
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
                else
                {
            %>
            <a href="UserProfile.aspx">My Details</a>
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

        <div align="center">
            <h1>Admin Page</h1>
            <asp:GridView ID="GridViewUsers" runat="server" AutoGenerateColumns="false" OnRowDeleting="GridViewUsers_RowDeleting" OnRowCommand="GridViewUsers_RowCommand" BorderWidth="2">
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="Username" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                    <asp:BoundField DataField="BirthDate" HeaderText="Birth Date" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("UserName") %>'>Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
