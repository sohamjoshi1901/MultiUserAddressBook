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

public partial class AddressBook_AdminPanel_ContactCategory_ContactCategoryList : System.Web.UI.Page
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
            FillGridViewContactCategory();
        }

    }
    #endregion Load Event

    #region FillGridView
    private void FillGridViewContactCategory()
    {
        #region Load Event
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        #endregion Load Event
        using (SqlConnection objConn = new SqlConnection(ConnectionString))
        {
            try
            {
                #region Set Connection & Command Object
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_ContactCategory_SelectAllByUserID";
                    #endregion Set Connection & Command Object
                    if (Session["UserID"] != null)
                    {
                        objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"];
                    }
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows == true)
                        {
                            #region read Data
                            gvContactCategory.DataSource = objSDR;
                            gvContactCategory.DataBind();
                            #endregion read Data
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
        Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryAddEdit.aspx");
    }
    #endregion Button : Add

    #region gvContactCategory : RowCommand
    protected void gvContactCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument != null)
            {
                DeleteContactCategory(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                FillGridViewContactCategory();
            }   
        }
    }
    #endregion gvContactCategory : RowCommand

    #region Delete Record
    private void DeleteContactCategory(SqlInt32 ContactCategoryID)
    {
        #region Local Variables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        SqlInt32 UserID = SqlInt32.Null;
        #endregion Local Variables

        #region Gather Information
        if (Session["UserID"] != null)
            UserID = Convert.ToInt32(Session["UserID"]);
        #endregion Gather Information
        using (SqlConnection objConn = new SqlConnection(ConnectionString))
        {
            try
            {
                #region Set Connection & Command Object
                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_ContactCategory_DeleteByPKByUserID";

                    objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Set Connection & Command Object

                    objCmd.ExecuteNonQuery();
                    lblSuccess.Text = "-Data Deleted succsessfully";
                    FillGridViewContactCategory();
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