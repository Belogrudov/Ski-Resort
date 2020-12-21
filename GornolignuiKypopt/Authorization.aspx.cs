using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GornolignuiKypopt
{
    public partial class Authorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            if (connection.Authorization(tbLogin.Text, tbPassword.Text) == 0)
            {
                lblError.Visible = true;
            }
            else
            {
                switch (connection.Role(DBConnection.userID))
                {
                    case ("1"):
                        Response.Redirect("Main.aspx");
                        break;
                    case ("2"):
                        Response.Redirect("Sotrydniki.aspx");
                        break;
                    case ("3"):
                        Response.Redirect("Sotrydniki.aspx");
                        break;
                    case ("4"):
                        Response.Redirect("Arenda.aspx");
                        break;
                    case ("5"):
                        Response.Redirect("Tovari.aspx");
                        break;
                }

            }
        }
    }
}