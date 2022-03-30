using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Configuration;

public partial class AddressBook_AdminPanel_State_StateList : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Check For Valid User

        if (Session["UserID"] == null)
            Response.Redirect("~/Login.aspx");

        #endregion Check For Valid User
        if (!Page.IsPostBack)
        {
            FillGridViewState();
        }

    }
    #endregion Load Event

    #region FillGridView
    private void FillGridViewState()
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        using (SqlConnection objConn = new SqlConnection(ConnectionString))
        {
            try
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_State_SelectAllByUserID";
                    if (Session["UserID"] != null)
                    {
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"];
                    }
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows == true)
                        {
                            gvState.DataSource = objSDR;
                            gvState.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
            }

        }
    }
    #endregion FillGridView

    #region Button : Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/State/StateAddEdit.aspx");
    }
    #endregion Button : Add

    #region gvState : RowCommand
    protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument != null)
            {
                DeleteState(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
               
            }


        }
        else if (e.CommandName == "EditRecord")
        {

        }
    }
    #endregion gvState : RowCommand

    #region Delete Record
    private void DeleteState(SqlInt32 StateID)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;

        SqlInt32 UserID = SqlInt32.Null;

        if (Session["UserID"] != null)
            UserID = Convert.ToInt32(Session["UserID"]);
        using (SqlConnection objConn = new SqlConnection(ConnectionString))
        {
            try
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_State_DeleteByPKByUserID";

                    objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    objCmd.ExecuteNonQuery();
                    lblSuccess.Text = "Data Deleted succsessfully";
                    FillGridViewState();

                }
            }
            catch (SqlException sqlex)
            {
                lblMessage.Text = sqlex.Message;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
            }
        }
    }
    #endregion Delete Record
}