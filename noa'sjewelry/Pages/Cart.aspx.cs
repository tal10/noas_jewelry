using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cart : System.Web.UI.Page
{
    public class CartItem
    {
        // מאפיינים של פריט בעגלה
        public string JewelryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // אם משתמש רגיל לא מחובר
        if (Session["userName"] == null)
        {
            // User is not logged in, redirect to login page
            Response.Redirect("Login.aspx");
        }

        lblGreeting.Text = Utils.GetGreeting(Session);

        // האם מדובר בטעינה הראשונה של העמוד
        if (!IsPostBack)
        {
            BindCart(); // טעינת נתוני העגלה
        }
    }

    private void BindCart()
    {
        string userName = Session["userName"].ToString(); // קבלת שם המשתמש מהסשן
        List<CartItem> cart = GetCartItemsFromDatabase(userName);// קבלת פריטי העגלה ממסד הנתונים

        if (cart != null && cart.Count > 0)
        {
            rptCartItems.DataSource = cart;
            rptCartItems.DataBind();// קשירת נתוני העגלה לרכיב התצוגה
        }
        else
        {
            lblMessage.Text = "Your cart is empty"; // הודעה למשתמש אם העגלה ריקה
        }
    }

    private List<CartItem> GetCartItemsFromDatabase(string userName)
    {
        List<CartItem> cartItems = new List<CartItem>(); // יצירת רשימה חדשה של פריטי העגלה
        SqlConnection connection = DatabaseHelper.GetOpenConnection(); // פתיחת חיבור למסד הנתונים
        string query = @"SELECT c.JewelryID, c.Quantity, j.Name, j.Price, j.PictureUrl 
                     FROM CartItems c
                     JOIN Jewelries j ON c.JewelryID = j.JewelryID
                     WHERE c.UserName = @UserName";

        SqlCommand command = new SqlCommand(query, connection); // הכנת פקודת SQL
        command.Parameters.AddWithValue("@UserName", userName);

        SqlDataReader reader = command.ExecuteReader(); // ביצוע השאילתה וקבלת התוצאות


        while (reader.Read())
        {
            // יצירת אובייקט CartItem עבור כל שורה בתוצאה והוספתו לרשימה
            CartItem item = new CartItem
            {
                JewelryId = reader["JewelryID"].ToString(), // מזהה תכשיט
                Name = reader["Name"].ToString(), // שם התכשיט
                Price = Convert.ToDecimal(reader["Price"]), // מחיר התכשיט
                Quantity = Convert.ToInt32(reader["Quantity"]), // כמות הפריטים
                PictureUrl = reader["PictureUrl"].ToString() // כתובת תמונה של התכשיט
            };
            cartItems.Add(item); // הוספת הפריט לרשימת העגלה
        }

        reader.Close();
        DatabaseHelper.CloseConnection(connection); // סגירת החיבור למסד הנתונים
        return cartItems; // החזרת רשימת פריטי העגלה;
    }

    protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "RemoveFromCart")
        {
            string jewelryIdToRemove = e.CommandArgument.ToString();
            string userName = Session["userName"].ToString();

            bool isRemoved = RemoveJewelryFromCart(jewelryIdToRemove, userName);

            if (isRemoved)
            {
                lblMessage.Text = "Jewelry removed from the cart successfully";
                BindCart(); // Rebind the cart after removal
            }
            else
            {
                lblMessage.Text = "Failed to remove jewelry from the cart";
            }
        }
    }

    public bool RemoveJewelryFromCart(string jewelryId, string userName)
    {
        // Initialize a flag to track whether the removal operation was successful
        bool isRemoved = false;

        // פתיחת חיבור מול המסד נתונים
        SqlConnection connection = DatabaseHelper.GetOpenConnection();

        // Check if the jewelry item exists in the cart
        string checkQuery = "SELECT COUNT(*) FROM CartItems WHERE UserName = @UserName AND JewelryID = @JewelryID";
        SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
        checkCommand.Parameters.AddWithValue("@UserName", userName);
        checkCommand.Parameters.AddWithValue("@JewelryID", jewelryId);

        int count = (int)checkCommand.ExecuteScalar();

        if (count > 0)
        {
            // If the jewelry item exists in the cart, delete it
            string deleteQuery = "DELETE FROM CartItems WHERE UserName = @UserName AND JewelryID = @JewelryID";
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
            deleteCommand.Parameters.AddWithValue("@UserName", userName);
            deleteCommand.Parameters.AddWithValue("@JewelryID", jewelryId);

            int rowsAffected = deleteCommand.ExecuteNonQuery();

            // If rows were affected, the deletion was successful
            if (rowsAffected > 0)
            {
                isRemoved = true;
            }
        }

        // Return true if the jewelry item was successfully removed, otherwise false
        return isRemoved;
    }
}