using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GornolignuiKypopt
{
    public partial class Sotrydniki : System.Web.UI.Page
    {
        private static string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrSotrydniki;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlRoleFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsSotrydniki.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsSotrydniki.SelectCommand = qr;
            sdsSotrydniki.DataSourceMode = SqlDataSourceMode.DataReader;
            gvSotrydniki.DataSource = sdsSotrydniki;
            gvSotrydniki.DataBind();
        }
        private void ddlRoleFill()
        {
            sdsRole.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsRole.SelectCommand = DBConnection.qrRole;
            sdsRole.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlRole.DataSource = sdsRole;
            ddlRole.DataTextField = "Nazvanie_roli";
            ddlRole.DataValueField = "ID_Role";
            ddlRole.DataBind();
        }
        protected void gvSotrydniki_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID_Sotrydnika"):
                    e.SortExpression = "ID_Sotrydnika";
                    break;
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
            }
            sortGridView(gvSotrydniki, e, out sortDirection, out strField);
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

        protected void gvSotrydniki_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }
        //Удаление данных из полей
        protected void DeleteDate()
        {
            tbOtchestvo.Text = "";
            tbName.Text = "";
            tbFamiliya.Text = "";
            ddlRole.SelectedIndex = -1;
            DBConnection.selectedRow = 0;
            tbLogin.Text = "";
            tbPassword.Text = "";
        }
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            if (connection.UnqLogin(tbLogin.Text) != 0)
            {
                lblError.Visible = true;
            }
            else
            {
                lblError.Visible = false;
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Sotrydniki_Insert(tbFamiliya.Text, tbName.Text, tbOtchestvo.Text, tbLogin.Text, tbPassword.Text, Convert.ToInt32(ddlRole.SelectedValue.ToString()));
                gvFill(QR);
                DeleteDate();
            }
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DBProcedures dBProcedures = new DBProcedures();
            dBProcedures.Sotrydniki_Update(DBConnection.selectedRow, tbFamiliya.Text, tbName.Text, tbOtchestvo.Text, tbLogin.Text, DBConnection.contractID, tbPassword.Text, false,
                Convert.ToInt32(ddlRole.SelectedValue.ToString()));
            gvFill(QR);
            DeleteDate();
            DBConnection.contractID = 0;
            DBConnection.selectedRow = 0;
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (DBConnection.selectedRow != 0)
            {
                DBProcedures dBProcedures = new DBProcedures();
                dBProcedures.Sotrydniki_Delete(DBConnection.selectedRow);
                DBConnection.selectedRow = 0;
                gvFill(QR);
                DeleteDate();
            }
        }

        protected void btSerch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvSotrydniki.Rows)
                {
                    if (row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[12].Text.Equals(tbSearch.Text) ||
                        row.Cells[13].Text.Equals(tbSearch.Text) ||
                        row.Cells[14].Text.Equals(tbSearch.Text) ||
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
                string newQR = QR + "and [Familiya] like '%" + tbSearch.Text + "%' or [Name] like '%" + tbSearch.Text + "%'" + "or [Otchestvo] like '%" + tbSearch.Text + "%' or [Login] like '%" + tbSearch.Text + "%'"; 
                gvFill(newQR);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbSearch.Text = "";
        }

        protected void gvSotrydniki_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = gvSotrydniki.SelectedRow;
            DBConnection.selectedRow = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbFamiliya.Text = rows.Cells[2].Text;
            tbName.Text = rows.Cells[3].Text;
            tbOtchestvo.Text = rows.Cells[4].Text;
            tbLogin.Text = rows.Cells[5].Text;
            ddlRole.SelectedIndex = Convert.ToInt32(rows.Cells[15].Text.ToString()) - 1;
            tbPassword.Text = rows.Cells[6].Text;
            DBConnection.contractID = Convert.ToInt32(rows.Cells[11].Text.ToString());
        }
    }
}