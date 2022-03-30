using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Local Varriable
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        SqlString strDisplayName = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strMobileNo = SqlString.Null;
        string strError = "";
        #endregion Local Varriable


        #region server side validation
        if (txtUserName.Text.Trim() == "")
            strError += "Enter Person Name.</br>";

        if (txtPassword.Text.Trim() == "")
            strError += "Enter Password.</br>";

        if (txtDisplayName.Text.Trim() == "")
            strError += "Enter Display Name.</br>";

        if (strError != "")
        {
            lblMessage.Text = strError;
            return;
        }



        #endregion server side validation

        #region read data

        if (txtUserName.Text.Trim() != "")
            strUserName = txtUserName.Text.Trim();

        if (txtPassword.Text.Trim() != "")
            strPassword = txtPassword.Text.Trim();

        if (txtDisplayName.Text.Trim() != "")
            strDisplayName = txtDisplayName.Text.Trim();

        if (txtAddress.Text.Trim() != "")
            strAddress = txtAddress.Text.Trim();

        if (txtMobileNo.Text.Trim() != "")
            strMobileNo = txtMobileNo.Text.Trim();
        #endregion read data

        using (SqlConnection objConn = new SqlConnection(ConnectionString))
        {
            try
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();
                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_User_Insert";

                    objCmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = strUserName;
                    objCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = strPassword;
                    objCmd.Parameters.Add("@DisplayName", SqlDbType.VarChar).Value = strDisplayName;
                    objCmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = strAddress;
                    objCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = strMobileNo;

                    objCmd.ExecuteNonQuery();
                    lblMessage.Text = "data insert successfully.";
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    txtAddress.Text = "";
                    txtMobileNo.Text="";
                    txtDisplayName.Text = "";
                    txtUserName.Focus();
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
    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddressBook/Login.aspx");
    }
}