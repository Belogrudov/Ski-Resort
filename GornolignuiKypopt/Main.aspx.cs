using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GornolignuiKypopt
{
    public partial class Main : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrTovari;
            if (!IsPostBack)
            {
                if (DBConnection.userID == 0)
                {
                    btEnter.Visible = true;
                }
                else
                {


                    try
                    {
                        btEnter.Visible = false;
                        string Name;
                        SqlCommand command = new SqlCommand("", DBConnection.connection);
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "select [ФИО] from [Users] where [ID]= " + DBConnection.userID + " and [Роль] = '1'";
                        DBConnection.connection.Open();
                        Name = command.ExecuteScalar().ToString();
                        command.ExecuteNonQuery();
                        DBConnection.connection.Close();
                        lblUserName.Text = Name;
                    }
                    catch
                    {
                        DBConnection.connection.Close();
                    }
                }
                gvFill(QR);
            }
        }
        private void gvFill(string qr)
        {
            sdsTovari.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTovari.SelectCommand = qr;
            sdsTovari.DataSourceMode = SqlDataSourceMode.DataReader;
            gvTovari.DataSource = sdsTovari;
            gvTovari.DataBind();
        }
        protected void gvTovari_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID_Tovara"):
                    e.SortExpression = "ID_Tovara";
                    break;
                case ("Название товара"):
                    e.SortExpression = "Nazvanie";
                    break;
                case ("Категория"):
                    e.SortExpression = "Hazvanie_kategorii";
                    break;
                case ("Цена"):
                    e.SortExpression = "Cena";
                    break;
            }
            sortGridView(gvTovari, e, out sortDirection, out strField);
            string strDirection = sortDirection
                == SortDirection.Ascending ? "ASC" : "DESC";
            gvFill(QR + " arenda by " + e.SortExpression + " " + strDirection);
        }
        private void sortGridView(GridView gridView,
         GridViewSortEventArgs e,
         out SortDirection sortDirection,
         out string strSortField)
        {
            strSortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null &&
                gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (strSortField ==
                    gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"]
                        == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }
            }
            gridView.Attributes["CurrentSortField"] = strSortField;
            gridView.Attributes["CurrentSortDirection"] =
                (sortDirection == SortDirection.Ascending ? "ASC"
                : "DESC");
        }
        protected void gvTovari_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[5].Visible = false;
        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            Response.Redirect("Authorization.aspx");
        }
        protected void btClick(object sender, EventArgs e)
        {
            Response.Redirect("Sotrydniki.aspx");
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvTovari.Rows)
                {
                    if (row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#197d34");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#732AAC");
                }
                btCancel.Visible = true;
            }
        }

        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "where [Nazvanie] like '%" + tbSearch.Text + "%' or [Price] like '%" + tbSearch.Text + "%' or [Hazvanie_kategorii] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }

        protected void gvTovari_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Cymma;
                GridViewRow rows = gvTovari.SelectedRow;
                DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text);
                Cymma = Convert.ToDecimal(rows.Cells[4].Text);
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Arenda_Insert(DBConnection.selectedRow, 1, DBConnection.userID, Cymma);
            }
            catch
            {

            }
            finally
            {
                gvFill(QR);
                DBConnection.selectedRow = 0;
            }
        }
    }
}