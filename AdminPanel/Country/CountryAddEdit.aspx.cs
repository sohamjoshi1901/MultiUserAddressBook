using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

public partial class AddressBook_AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
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
            #region Post Back
            if (Request.QueryString["CountryID"] == null)
            {
                #region Add
                lblPageHeaderText.Text = "Country Add";
                #endregion Add
            }
            else
            {
                #region edit
                lblPageHeaderText.Text = "Country Edit" + "|CountryID=" + Request.QueryString["CountryID"].ToString();
                FillCountryForm(Convert.ToInt32(Request.QueryString["CountryID"].ToString()));
                #endregion edit
            }
            #endregion Post Back
        }
    }
    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        SqlString strCountryName = SqlString.Null;
        SqlString strCountryCode = SqlString.Null;
        SqlInt32 UserID=SqlInt32.Null;
        string strError = "";
        #endregion Local Variables

        #region Server Side Validation
        if (txtCountryName.Text.Trim() == "")
        {
            strError += "-Enter Country Name </br>";
        }
        if (txtCountryCode.Text.Trim() == "")
        {
            strError += "-Enter Country Code </br>";
        }
        if (strError.Trim() != "")
        {
            lblMessage.Text = strError;
            return;
        }
        #endregion Server Side Validation

        #region Gather Information
        if (txtCountryName.Text.Trim()!="")
        strCountryName = txtCountryName.Text.Trim();

        if(txtCountryCode.Text.Trim()!="")
        strCountryCode = txtCountryCode.Text.Trim();

        if(Session["UserID"]!=null)
            UserID=Convert.ToInt32(Session["UserID"]);
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
                    if (Request.QueryString["CountryID"] == null)
                        #region Insert
                        objCmd.CommandText = "PR_Country_InsertByUserID";
                    #endregion Insert
                    else
                    {
                        #region Update
                        objCmd.CommandText = "PR_Country_UpdateByPKByUserID";
                        objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = Request.QueryString["CountryID"].ToString().Trim();
                        #endregion Update
                    }

                    #region Parameter
                    objCmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = strCountryName;
                    objCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = strCountryCode;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Parameter
                    objCmd.ExecuteNonQuery();

                    if (Request.QueryString["CountryID"] == null)
                    {
                        lblSuccess.Text = "Country save successfully.";
                        txtCountryCode.Text = "";
                        txtCountryName.Text = "";
                        txtCountryName.Focus();
                    }
                    else
                    {
                        Response.Redirect("~/AdminPanel/Country/CountryList.aspx");
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

    #region Fill Cantrols
    private void FillCountryForm(SqlInt32 CountryID)
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
                    objCmd.CommandText = "PR_Country_SelectByPK";

                    objCmd.Parameters.Add("CountryID", SqlDbType.Int).Value = CountryID;
                    #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            while(objSDR.Read())
                            {
                                #region read Data
                                if (!objSDR["CountryName"].Equals(DBNull.Value))
                                    txtCountryName.Text = objSDR["CountryName"].ToString().Trim();

                                if (!objSDR["CountryCode"].Equals(DBNull.Value))
                                    txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                                #endregion read Data
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

    #region Button : Cancel
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Country/CountryList.aspx");
    }
    #endregion Button : Cancel
}