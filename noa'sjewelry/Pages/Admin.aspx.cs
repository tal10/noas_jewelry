using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // אם המנהל לא מחובר, נעביר את המשתמש לדף הבית
        if (Session["isAdmin"] == null || !(bool)Session["isAdmin"])
        {
            Response.Redirect("Login.aspx");
        }

        lblGreeting.Text = Utils.GetGreeting(Session);

        // האם מדובר בטעינה הראשונה של העמוד
        if (!IsPostBack)
        {
            // טעינת פרטי המשתמשים
            LoadUserDetails();
        }
    }

    public void LoadUserDetails()
    {
        // שאילתת SQL לשליפת פרטי המשתמשים מהטבלה Users
        string query = "SELECT UserName, FirstName, LastName, Email, Password,PhoneNumber, BirthDate FROM Users";

        SqlConnection connection = DatabaseHelper.GetOpenConnection(); // פתיחת חיבור למסד נתונים
        SqlCommand command = new SqlCommand(query, connection); // יצירת אובייקט פקודה עם השאילתה והחיבור
        SqlDataAdapter adapter = new SqlDataAdapter(command); // יצירת אובייקט מתאם נתונים
        DataTable dtUsers = new DataTable(); // יצירת אובייקט טבלת נתונים
        adapter.Fill(dtUsers); // מילוי טבלת הנתונים עם תוצאות השאילתה

        if (dtUsers.Rows.Count > 0)
        {
            GridViewUsers.DataSource = dtUsers;
            GridViewUsers.DataBind();
        }

        DatabaseHelper.CloseConnection(connection); // סגירת חיבור למסד נתונים
    }

    protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string usernameToDelete = e.CommandArgument.ToString(); // קבלת שם המשתמש למחיקה
            DeleteUser(usernameToDelete); // קריאה לפונקציה למחיקת המשתמש
            LoadUserDetails(); // טעינת פרטי המשתמשים מחדש 
        }
    }

    protected void GridViewUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // משאירה את הפונקציה ריקה
    }

    protected void DeleteUser(string username)
    {
        // שאילתת SQL מחיקת משתמש לפי שם המשתמש
        string query = "DELETE FROM Users WHERE UserName = @UserName";

        SqlConnection connection = DatabaseHelper.GetOpenConnection(); // פתיחת חיבור למסד נתונים
        SqlCommand command = new SqlCommand(query, connection); // יצירת אובייקט פקודה עם השאילתה והחיבור
        command.Parameters.AddWithValue("@UserName", username); // הגדרת פרמטר שם המשתמש בשאילתה
        command.ExecuteNonQuery(); // ביצוע הפקודה
        DatabaseHelper.CloseConnection(connection); // סגירת חיבור למסד נתונים
    }
}