using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Invalidate the user's session
        Session.Clear();
        Session.Abandon();

        // Redirect the user to the login page
        Response.Redirect("Default.aspx");
    }
}