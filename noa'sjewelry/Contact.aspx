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
            <h1>Contact</h1>
            phone number:0533325137
            <br />
            instegram:noas_jewely
            <br />
            facbook:noas_jewely1
            <br />
            Mail:noasjewely@gmail.com
        </div>
    </form>
</body>
</html>
