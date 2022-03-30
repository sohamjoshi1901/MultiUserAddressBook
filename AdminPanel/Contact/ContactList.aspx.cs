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

public partial class AddressBook_AdminPanel_Contact_Contact : System.Web.UI.Page
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
            FillGridViewContact();
        }

    }
    #endregion Load Event

    #region FillGridView
    private void FillGridViewContact()
    {
        
        #region Local Variables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        #endregion Local Variables
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
                    objCmd.CommandText = "PR_Contact_SelectAllByUserID";
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
                            gvContact.DataSource = objSDR;
                            gvContact.DataBind();
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
        Response.Redirect("~/AdminPanel/Contact/ContactAddEdit.aspx");
    }
    #endregion Button : Add

    #region gvContact : RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument != null)
            {
                DeleteContact(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                
            }


        }
        else if (e.CommandName == "EditRecord")
        {

        }
    }
    #endregion gvContact : RowCommand

    #region Delete Record
    private void DeleteContact(SqlInt32 ContactID)
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
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_Contact_DeleteByPKByUserID";

                    objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Set Connection & Command Object

                    objCmd.ExecuteNonQuery();
                    lblSuccess.Text = "Data Deleted succsessfully";
                    FillGridViewContact();

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
