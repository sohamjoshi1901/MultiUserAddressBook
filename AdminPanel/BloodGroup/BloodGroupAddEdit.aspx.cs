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

public partial class AddressBook_AdminPanel_BloodGroup_BloodGroupAddEdit : System.Web.UI.Page
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
            if (Request.QueryString["BloodGroupID"] == null)
            {
                lblPageHeaderText.Text = "BloodGroup Add";
            }
            else
            {
                lblPageHeaderText.Text = "BloodGroup Edit" + "|BloodGroupID=" + Request.QueryString["BloodGroupID"].ToString();
                FillBloodGroupForm(Convert.ToInt32(Request.QueryString["BloodGroupID"].ToString()));
            }
        }

    }
    #endregion Load Event

    #region Fill Cantrols
    private void FillBloodGroupForm(SqlInt32 BloodGroupID)
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
                    objCmd.CommandText = "PR_BloodGroup_SelectByPK";

                    objCmd.Parameters.Add("@BloodGroupID", SqlDbType.Int).Value = BloodGroupID;
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {
                                if (!objSDR["BloodGroupName"].Equals(DBNull.Value))
                                    txtBloodGroupName.Text = objSDR["BloodGroupName"].ToString().Trim();

                             
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
        SqlString strBloodGroupName = SqlString.Null;
        SqlInt32 UserID = SqlInt32.Null;
        string strError = "";
        #endregion Local Variables

        #region Server Side Validation
        if (txtBloodGroupName.Text == "")
            strError += "Enter Blood Group Name</br>";
        if (strError != "")
        {
            lblMessage.Text = strError;
            return;
        }
        #endregion Server Side Validation

        #region Gather Information
        if (txtBloodGroupName.Text.Trim() != "")
        strBloodGroupName = txtBloodGroupName.Text.Trim();

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
                    if (Request.QueryString["BloodGroupID"] == null)
                        #region Insert
                        objCmd.CommandText = "PR_BloodGroup_InsertByUserID";
                    #endregion Insert
                    else
                    {
                        #region Update

                        objCmd.CommandText = "PR_BloodGroup_UpdateByPKByUserID";
                        objCmd.Parameters.Add("@BloodGroupID", SqlDbType.Int).Value = Request.QueryString["BloodGroupID"];
                        #endregion Update
                    }
                    #region Parameters
                    objCmd.Parameters.Add("@BloodGroupName", SqlDbType.VarChar).Value = strBloodGroupName;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    objCmd.ExecuteNonQuery();
                    #endregion Parameters

                    if (Request.QueryString["BloodGroupID"] == null)
                    {
                        lblSuccess.Text = "Blood Group save successfully.";
                        txtBloodGroupName.Text = "";
                        txtBloodGroupName.Focus();
                    }
                    else
                    {
                        Response.Redirect("~/AdminPanel/BloodGroup/BloodGroupList.aspx");
                        //lblSuccess.Text = "Blood Group edited successfully.";
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
        Response.Redirect("~/AdminPanel/BloodGroup/BloodGroupList.aspx");
    }
    #endregion Button : Cancel
}