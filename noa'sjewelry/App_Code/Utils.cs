﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Summary description for Utils
/// </summary>
public static class Utils
{
    public static string GenerateMenu(HttpSessionState session)
    {
        string menuHTML = "<div class=\"topnav\">";

        menuHTML += "<a class=\"active\" href=\"Default.aspx\"><div class=\"fas fa-home\"></div></a>";

        if (session["userName"] == null && session["isAdmin"] == null)
        {
            menuHTML += "<a href=\"Login.aspx\">Login</a>";
            menuHTML += "<a href=\"Register.aspx\">Register</a>";
        }
        else
        {
            menuHTML += "<a href=\"UserProfile.aspx\">My Details</a>";
        }

        menuHTML += "<a href=\"Jeweleris.aspx\">Jeweleris</a>";

        if (session["isAdmin"] != null && (bool)session["isAdmin"] == true)
        {
            menuHTML += "<a href=\"Admin.aspx\">Admin Page</a>";
        }

        menuHTML += "<a href=\"Contact.aspx\">Contact</a>";

        if (session["userName"] != null || (session["isAdmin"] != null && (bool)session["isAdmin"] == true))
        {
            menuHTML += "<a href=\"Logout.aspx\">Logout</a>";
        }

        menuHTML += "</div>";

        return menuHTML;
    }

    public static string GetGreeting(HttpSessionState session)
    {
        string greeting = null;
        if (session["isAdmin"] != null && (bool)session["isAdmin"] == true)
        {
            greeting = "Welcome, Admin!";
        }
        else if (session["userName"] != null)
        {
            string user;
            if (session["userFullName"] != null)
                user = session["userFullName"].ToString();
            else
                user = session["userName"].ToString();
            greeting = $"Welcom, {user}!";
        }
        return greeting;
    }
}