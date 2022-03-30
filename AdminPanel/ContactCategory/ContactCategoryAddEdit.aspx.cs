using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

public partial class AddressBook_AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Check For Valid User

        if (Session["UserID"] == null)
            Response.Redirect("~/Login.aspx");

        #endregion Check For Valid User

        #region Post Back
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["ContactCategoryID"] == null)
            {
                lblPageHeaderText.Text = "Contact category Add";
            }
            else
            {
                lblPageHeaderText.Text = "Contact category Edit";
                FillContactCategory(Convert.ToInt32(Request.QueryString["ContactCategoryID"].ToString().Trim()));
            }
        }
        #endregion Post Back
    }
    #endregion Load Event

    #region Fill Cantrols
    private void FillContactCategory(SqlInt32 ContactCategoryID)
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
                    objCmd.CommandText = "PR_ContactCategory_SelectByPK";

                    objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                    #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {
                                #region Read the value and set the controls
                                if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                                    txtCCName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                                #endregion Read the value and set the controls
                            }
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
    #endregion Fill Cantrols

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        SqlString strCCName = SqlString.Null;
        SqlInt32 UserID = SqlInt32.Null;
        string strError = "";
        #endregion Local Variables

        #region Server Side Validation
        if (txtCCName.Text == "")
            strError += "Enter Contact Category Name</br>";
        if (strError != "")
        {
            lblMessage.Text = strError;
            return;
        }
        #endregion Server Side Validation

        #region Gather Information
        strCCName = txtCCName.Text.Trim();
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
                    #endregion Set Connection & Command Object

                    if (Request.QueryString["ContactCategoryID"] == null)
                        #region Insert
                        objCmd.CommandText = "PR_ContactCategory_InsertByUserID";
                        #endregion Insert

                    else
                    {
                        #region Update
                        objCmd.CommandText = "PR_ContactCategory_UpdateByPKByUserID";
                        objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = Request.QueryString["ContactCategoryID"];
                        #endregion Update
                    }

                    #region Parameters
                    objCmd.Parameters.Add("@ContactCategoryName", SqlDbType.VarChar).Value = strCCName;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Parameters

                    objCmd.ExecuteNonQuery();

                    if (Request.QueryString["ContactCategoryID"] == null)
                    {
                        lblSuccess.Text = "Contact save successfully.";
                        txtCCName.Text = "";
                        txtCCName.Focus();

                    }
                    else
                    {
                        Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx");
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
    #endregion Button : Save

    #region Button : Cancel
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx");
    }
    #endregion Button : Cancel
}