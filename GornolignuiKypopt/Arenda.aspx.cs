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
    public partial class Arenda : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrArenda;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlClientFill();
                ddlProductFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsArenda.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsArenda.SelectCommand = qr;
            sdsArenda.DataSourceMode = SqlDataSourceMode.DataReader;
            gvArenda.DataSource = sdsArenda;
            gvArenda.DataBind();
        }

        private void ddlClientFill()
        {
            sdsKlienti.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsKlienti.SelectCommand = DBConnection.qrKlienti;
            sdsKlienti.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlKlienti.DataSource = sdsKlienti;
            ddlKlienti.DataTextField = "ФИО";
            ddlKlienti.DataValueField = "ID_Klienta";
            ddlKlienti.DataBind();
        }
        private void ddlProductFill()
        {
            sdsTovari.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTovari.SelectCommand = DBConnection.qrTovari;
            sdsTovari.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlTovari.DataSource = sdsTovari;
            ddlTovari.DataTextField = "Название товара";
            ddlTovari.DataValueField = "ID_Tovara";
            ddlTovari.DataBind();
        }

        protected void gvArenda_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Название товара"):
                    e.SortExpression = "Nazvanie";
                    break;
                case ("Количество"):
                    e.SortExpression = "Kolichestvo";
                    break;
                case ("Клиент"):
                    e.SortExpression = "Клиент";
                    break;
                case ("Цена"):
                    e.SortExpression = "Cymma";
                    break;
                case ("Дата продажи"):
                    e.SortExpression = "Date";
                    break;
                case ("Сотрудник"):
                    e.SortExpression = "Familiya";
                    break;
            }
            sortGridView(gvArenda, e, out sortDirection, out strField);
            string strDirection = sortDirection
                == SortDirection.Ascending ? "ASC" : "DESC";
            gvFill(QR + " order by " + e.SortExpression + " " + strDirection);
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
        }
        protected void gvArenda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[5].Visible = false;
        }

        private void DeleteData()
        {
            tbKolichestvo.Text = "";
            tbDate.Text = "";
            tbCymma.Text = "";
            ddlKlienti.SelectedIndex = -1;
            ddlTovari.SelectedIndex = -1;
            DBConnection.selectedRow = 0;
        }
        protected void btInsert_Click(object sender, EventArgs e)
        {
            int ArendaID;
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            if (DBConnection.userID == 0)
            {
                Response.Redirect("Authorization.aspx");
            }
            else
            {
                decimal Cost;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT (Cena) FROM [Tovari] where [ID_Tovara] = " + Convert.ToInt32(ddlTovari.SelectedValue) + "";
                DBConnection.connection.Open();
                Cost = Convert.ToDecimal(command.ExecuteScalar().ToString());
                command.ExecuteNonQuery();
                DBConnection.connection.Close();
                int Sum = Convert.ToInt32(tbKolichestvo.Text) * Convert.ToInt32(Cost);
                string time = DateTime.Now.ToString("h:mm");
                DateTime theDate = DateTime.ParseExact(tbDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string date = theDate.ToString("dd.MM.yyyy");
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Arenda_Insert(Convert.ToInt32(ddlTovari.SelectedValue), Convert.ToInt32(tbKolichestvo.Text), Convert.ToInt32(ddlKlienti.SelectedValue), Convert.ToDecimal(Sum));
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT MAX(ID_Order) FROM [Order]";
                DBConnection.connection.Open();
                ArendaID = Convert.ToInt32(command.ExecuteScalar().ToString());
                command.ExecuteNonQuery();
                DBConnection.connection.Close();
                gvFill(QR);
            }
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            string time = DateTime.Now.ToString("h:mm");
            DateTime theDate = DateTime.ParseExact(tbDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string date = theDate.ToString("dd.MM.yyyy");
            dBProcedures.Arenda_Update(DBConnection.selectedRow, Convert.ToInt32(ddlTovari.SelectedValue), Convert.ToInt32(tbKolichestvo.Text), Convert.ToInt32(ddlKlienti.SelectedValue));
            command.CommandType = System.Data.CommandType.Text;
            DBConnection.connection.Open();
            gvFill(QR);
            DeleteData();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            if (DBConnection.selectedRow != 0)
            {
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Arenda_Delete(DBConnection.selectedRow);
                DeleteData();
            }
            gvFill(QR);
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvArenda.Rows)
                {
                    if (row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[12].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [Nazvanie] like '%" + tbSearch.Text + "%' or [Kolichestvo] like '%" + tbSearch.Text + "%'" +
                    "or CONCAT([Familiya], + ' ' + [Name], + ' ' + [Otchestvo]) like '%" + tbSearch.Text + "%' or [Cymma] like '%" + tbSearch.Text + "%' or [Date] like '%" + tbSearch.Text + "%' " +
                    "or [Familiya] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }

        protected void gvArenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = gvArenda.SelectedRow;
            try
            {
                DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text.ToString());
                ddlTovari.SelectedValue = rows.Cells[2].Text.ToString();
                ddlKlienti.SelectedValue = rows.Cells[5].Text.ToString();
                tbDate.Text = Convert.ToDateTime(rows.Cells[8].Text.ToString()).ToString("yyyy-MM-dd");
                tbKolichestvo.Text = rows.Cells[4].Text.ToString();
                tbCymma.Text = rows.Cells[7].Text;
            }
            catch
            {
                DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text.ToString());
                ddlTovari.SelectedValue = rows.Cells[2].Text.ToString();
                ddlKlienti.SelectedValue = rows.Cells[5].Text.ToString();
                tbKolichestvo.Text = rows.Cells[4].Text.ToString();
                tbCymma.Text = rows.Cells[7].Text;
            }
        }
    }
}