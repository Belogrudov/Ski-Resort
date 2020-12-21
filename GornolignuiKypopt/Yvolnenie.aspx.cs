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
    public partial class Yvolnenie : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrYvolnenie;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlFill();
                ddlFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsYvolnenie.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsYvolnenie.SelectCommand = qr;
            sdsYvolnenie.DataSourceMode = SqlDataSourceMode.DataReader;
            gvYvolnenie.DataSource = sdsYvolnenie;
            gvYvolnenie.DataBind();
        }
        private void ddlFill()
        {
            sdsSotrydniki.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsSotrydniki.SelectCommand = DBConnection.qrSotrydnikiData;
            sdsSotrydniki.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlSotrydniki.DataSource = sdsSotrydniki;
            ddlSotrydniki.DataTextField = "ФИО";
            ddlSotrydniki.DataValueField = "ID_Sotrydnika";
            ddlSotrydniki.DataBind();
        }
        //Удаление данных из полей
        protected void DeleteDate()
        {
            tbData_Yvolneniya.Text = "";
            tbPrichina_yvolneniya.Text = "";
            ddlSotrydniki.SelectedIndex = -1;
            DBConnection.selectedRow = 0;
        }
        protected void btInsert_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            int orderID;
            DateTime theDate = DateTime.ParseExact(tbData_Yvolneniya.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string date = theDate.ToString("dd.MM.yyyy");
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Yvolnenie_Insert(tbPrichina_yvolneniya.Text, Convert.ToInt32(ddlSotrydniki.SelectedValue.ToString()), date);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT MAX(ID_Fired_Order) FROM [Fired_Order]";
            DBConnection.connection.Open();
            orderID = Convert.ToInt32(command.ExecuteScalar().ToString());
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
            gvFill(QR);
            DeleteDate();
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DateTime theDate = DateTime.ParseExact(tbData_Yvolneniya.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string date = theDate.ToString("dd.MM.yyyy");
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Yvolnenie_Update(DBConnection.selectedRow, tbPrichina_yvolneniya.Text, Convert.ToInt32(ddlSotrydniki.SelectedValue.ToString()), date);
            gvFill(QR);
            DeleteDate();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (DBConnection.selectedRow != 0)
            {
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Yvolnenie_Delete(DBConnection.selectedRow);
                DBConnection.selectedRow = 0;
                gvFill(QR);
                DeleteDate();
            }
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvYvolnenie.Rows)
                {
                    if (row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[11].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [Familiya] like '%" + tbSearch.Text + "%' or [Name] like '%" + tbSearch.Text + "%'" +
                    "or [Otchestvo] like '%" + tbSearch.Text + "%' or [Data_Yvolneniya] like '%" + tbSearch.Text + "%' " +
                    "or [Prichina_yvolneniya] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
            gvFill(QR);
            DeleteDate();
        }

        protected void gvYvolnenie_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = gvYvolnenie.SelectedRow;
            DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text.ToString());
            ddlSotrydniki.SelectedValue = rows.Cells[2].Text.ToString();
            tbData_Yvolneniya.Text = Convert.ToDateTime(rows.Cells[8].Text.ToString()).ToString("yyyy-MM-dd");
            tbPrichina_yvolneniya.Text = rows.Cells[9].Text;
        }
        protected void gvYvolnenie_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Фамилия"):
                    e.SortExpression = "Familiya";
                    break;
                case ("Имя"):
                    e.SortExpression = "Name";
                    break;
                case ("Отчество"):
                    e.SortExpression = "Otchestvo";
                    break;
                case ("Логин"):
                    e.SortExpression = "Login";
                    break;
                case ("Дата увольнения"):
                    e.SortExpression = "Data_Yvolneniya";
                    break;
                case ("Причина увольнения"):
                    e.SortExpression = "Prichina_yvolneniya";
                    break;
            }
            sortGridView(gvYvolnenie, e, out sortDirection, out strField);
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
        }

        protected void gvYvolnenie_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }
}