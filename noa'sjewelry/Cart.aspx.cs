using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["userName"] == null) && (Session["isAdmin"] == null))
        {
            // User is not logged in, redirect to login page
            Response.Redirect("Login.aspx");
        }

        lblGreeting.Text = Utils.GetGreeting(Session);

        if (!IsPostBack)
        {
            BindCart();
        }
    }

    private void BindCart()
    {
        List<CartItem> cart = Session["Cart"] as List<CartItem>;
        if (cart != null)
        {
            rptCartItems.DataSource = cart;
            rptCartItems.DataBind();
        }
        else
        {
            lblMessage.Text = "Your cart is empty";
        }
    }
}