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

public partial class MultiUserAddressBook_AdminPanel_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        #region Local Variables
        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        string strError = "";
        #endregion Local Variables

        #region Server Side Validation
        if (txtUserName.Text == "")
            strError += "Enter User Name</br>";

        if (txtPassword.Text == "")
            strError += "Enter Password";

        if (strError != "")
        {
            lblMessage.Text = strError;
            return;
        }

        #endregion Server Side Validation

        #region Read From Value
        if (txtUserName.Text.Trim() != "")
            strUserName = txtUserName.Text.Trim();

        if (txtPassword.Text.Trim() != "")
            strPassword = txtPassword.Text.Trim();

        #endregion Read From Value


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
                    objCmd.CommandText = "PR_User_SelectByUserNamePassword";

                    objCmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = strUserName;
                    objCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = strPassword;

                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                           while(objSDR.Read())
                           {
                               if (!objSDR["UserID"].Equals(DBNull.Value))
                                   Session["UserID"] = objSDR["UserID"].ToString().Trim();

                               if (!objSDR["DisplayName"].Equals(DBNull.Value))
                                   Session["DisplayName"] = objSDR["DisplayName"].ToString().Trim();

                               Response.Redirect("~/Home.aspx");
                           }
                        }
                        else
                        {
                            lblMessage.Text = "Invalid User";
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
}

