using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GornolignuiKypopt
{
    public partial class Kategorii : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrKategorii;
            if (!IsPostBack)
            {
                gvFill(QR);
            }
        }
        private void gvFill(string qr)
        {
            sdsKategorii.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsKategorii.SelectCommand = qr;
            sdsKategorii.DataSourceMode = SqlDataSourceMode.DataReader;
            gvKategorii.DataSource = sdsKategorii;
            gvKategorii.DataBind();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Kategorii_Delete(DBConnection.selectedRow);
            tbName.Text = "";
            gvFill(QR);
        }

        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Kategorii_Insert(tbName.Text);
            tbName.Text = "";
            gvFill(QR);
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Kategorii_Update(DBConnection.selectedRow, tbName.Text);
            tbName.Text = "";
            gvFill(QR);
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvKategorii.Rows)
                {
                    if (row.Cells[2].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#197d34");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#732AAC");
                }
                btCancel.Visible = true;
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }
        protected void gvKategorii_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID_Tovara"):
                    e.SortExpression = "ID_Tovara";
                    break;
                case ("Тип товара"):
                    e.SortExpression = "Nazvanie";
                    break;
            }
            sortGridView(gvKategorii, e, out sortDirection, out strField);
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
        protected void gvKategorii_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow rows = gvKategorii.SelectedRow;
            DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text);
            tbName.Text = rows.Cells[2].Text;
        }

        protected void gvKategorii_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
}