<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link rel="stylesheet" href="Styles/StyleSheet.css" />




    <script src="https://code.jquery.com/jquery-3.6.0.min.js" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script>
        $(function () {
            $(".datepicker").datepicker({
                dateFormat: 'dd/mm/yy', // Customize date format as needed
                changeYear: true, // Optionally allow changing year
                yearRange: "-100:+0", // Optionally set the range of selectable years
                onSelect: function () {
                    $(this).blur(); // Hide the calendar after selection
                }
            });
        });
    </script>
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

            <h1>Registration</h1>
            <table border="1">
                <tr>
                    <td>
                        <label for="txtUsername">Username:</label></td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <label for="txtPassword">Password:</label></td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <label for="txtConfirmPassword">Confirm Password:</label></td>
                    <td>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <label for="ddlGender">Gender:</label></td>
                    <td>
                        <asp:RadioButtonList ID="rbGender" runat="server">
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="txtFirstName">First Name:</label></td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <label for="txtLastName">Last Name:</label></td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <label for="txtEmail">Email:</label></td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <label for="txtPhoneNumber">Phone Number:</label></td>
                    <td>
                        <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <label for="txtBirthdate">Birthdate:</label></td>
                    <td>
                        <asp:Label ID="lblBirthdate" runat="server" Text="Select Birthdate:"></asp:Label>
                        <asp:TextBox ID="txtBirthdate" runat="server" CssClass="datepicker"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" /></td>
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
