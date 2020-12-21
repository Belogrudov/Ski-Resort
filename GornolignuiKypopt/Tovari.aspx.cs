using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GornolignuiKypopt
{
    public partial class Tovari : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrTovari;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlFill();
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
        private void ddlFill()
        {
            sdsKategorii.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsKategorii.SelectCommand = DBConnection.qrKategorii;
            sdsKategorii.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlKategorii.DataSource = sdsKategorii;
            ddlKategorii.DataTextField = "Тип товара";
            ddlKategorii.DataValueField = "ID_Kategorii";
            ddlKategorii.DataBind();
        }
        //Удаление данных из полей
        protected void DeleteDate()
        {
            tbNazvanie.Text = "";
            tbCena.Text = "";
            tbKolichestvo.Text = "";
            ddlKategorii.SelectedIndex = -1;
        }
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Tovari_Insert(tbNazvanie.Text.ToString(), Convert.ToInt32(tbKolichestvo.Text.ToString()),
                Convert.ToDecimal(tbCena.Text.ToString()), Convert.ToInt32(ddlKategorii.SelectedValue));
            gvFill(QR);
            DeleteDate();
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Tovari_Update(DBConnection.selectedRow, tbNazvanie.Text.ToString(), Convert.ToInt32(tbKolichestvo.Text.ToString()),
               Convert.ToDecimal(tbCena.Text.ToString()), Convert.ToInt32(ddlKategorii.SelectedValue));
            gvFill(QR);
            DeleteDate();
            DBConnection.selectedRow = 0;
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Tovari_Delete(DBConnection.selectedRow);
            DBConnection.selectedRow = 0;
            gvFill(QR);
            DeleteDate();
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
                case ("Количество"):
                    e.SortExpression = "Kolichestvo";
                    break;
                case ("Цена"):
                    e.SortExpression = "Cena";
                    break;
                case ("Категория"):
                    e.SortExpression = "Hazvanie_kategorii";
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
            e.Row.Cells[5].Visible = false;
        }
        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvTovari.Rows)
                {
                    if (row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
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
                string newQR = QR + "where [Nazvanie] like '%" + tbSearch.Text + "%' or [Kolichestvo] like '%" + tbSearch.Text + "%'" +
                    "or [Cena] like '%" + tbSearch.Text + "%' or [Hazvanie_kategorii] like '%" + tbSearch.Text + "%'";
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
            GridViewRow rows = gvTovari.SelectedRow;
            tbNazvanie.Text = rows.Cells[2].Text;
            tbKolichestvo.Text = rows.Cells[3].Text;
            tbCena.Text = rows.Cells[4].Text;
            ddlKategorii.SelectedValue = rows.Cells[5].Text;
            DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text);
        }
    }
}