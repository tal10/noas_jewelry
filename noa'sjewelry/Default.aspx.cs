using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["isAdmin"] != null && (bool)Session["isAdmin"] == true)
        {
            lblGreeting.Text = "Welcome Admin!";
        }
        // Check if user is logged in and session variables exist
        else if (Session["userName"] != null)
        {
            string userName = Session["userName"].ToString();

            // Display the greeting message
            lblGreeting.Text = $"Hello, {userName}!";
        }
    }
}