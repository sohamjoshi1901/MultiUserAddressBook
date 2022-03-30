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

public partial class AddressBook_AdminPanel_State_StateAddEdit : System.Web.UI.Page
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
            FillDropDownList();
            if (Request.QueryString["StateID"] == null)
            {
                #region Add
                lblPageHeaderText.Text = "State add";
                #endregion Add
            }
            else
            {
                #region edit
                lblPageHeaderText.Text = "State Edit";
                FillStateForm(Convert.ToInt32(Request.QueryString["StateID"].ToString().Trim()));
                #endregion edit
            }
        }
    }
    #endregion Load Event

    #region FillDropDownList
    protected void FillDropDownList()
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
                    objCmd.CommandText = "PR_Country_SelectForDropDownListByUserID";

                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            #region read Data
                            ddlCountry.DataValueField = "CountryID";
                            ddlCountry.DataTextField = "CountryName";
                            

                            ddlCountry.DataSource = objSDR;
                            ddlCountry.DataBind();

                            ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));
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
                {
                    objConn.Close();
                }
            }
        }
    }

    #endregion FillDropDownList

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region LocalVarriables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        string strError = "";

        SqlInt32 CountryID = SqlInt32.Null;
        SqlString StateName = SqlString.Null;
        SqlInt32 UserID = SqlInt32.Null;

        #endregion LocalVariables

        #region ServerSideValidation
        if (ddlCountry.SelectedIndex == 0)
            strError += "-Select Country</br>";
        if (txtStateName.Text.Trim() == "")
            strError += "-Enter state Name";
        if (strError != "")
        {
            lblMessage.Text = strError;
            return;
        }
        #endregion ServerSideValidation

        #region Gather Data
        if (ddlCountry.SelectedIndex > 0)
            CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        if (txtStateName.Text.Trim() != "")
            StateName = txtStateName.Text.Trim();
          if (Session["UserID"] != null)
            UserID = Convert.ToInt32(Session["UserID"]);

        #endregion Gather Data

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
                    if (Request.QueryString["StateID"] == null)
                    {
                        #region Insert
                        objCmd.CommandText = "PR_State_InsertByUserID";
                        #endregion Insert
                    }

                    else
                    {
                        #region Update
                        objCmd.CommandText = "PR_State_UpdateByPKByUserID";
                        objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = Request.QueryString["StateID"].ToString().Trim();
                        #endregion Update
                    }
                    #region Parameters
                    objCmd.Parameters.Add("@StateName", SqlDbType.VarChar).Value = StateName;
                    objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                     objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                     #endregion Parameters
                     objCmd.ExecuteNonQuery();
                    if (Request.QueryString["StateID"] == null)
                    {
                        lblSuccess.Text = "data insert successfully.";
                        ddlCountry.SelectedIndex = 0;
                        txtStateName.Text = "";
                        ddlCountry.Focus();
                    }
                    else
                    {
                        Response.Redirect("~/AdminPanel/State/StateList.aspx");
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
                {
                    objConn.Close();
                }
            }
        }
    }
    #endregion Button : Save

    #region Fill Cantrols
    private void FillStateForm(SqlInt32 StateID)
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
                     
                     objCmd.CommandText = "PR_State_SelectByPK";

                     objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                     #endregion Set Connection & Command Object
                     using (SqlDataReader objSDR = objCmd.ExecuteReader())
                     {
                         if (objSDR.HasRows)
                         {
                             while (objSDR.Read())
                             {
                                 #region Read the value and set the controls
                                 if (!objSDR["StateName"].Equals(DBNull.Value))
                                     txtStateName.Text = objSDR["StateName"].ToString().Trim();

                                 if (!objSDR["CountryID"].Equals(DBNull.Value))
                                     ddlCountry.SelectedValue = objSDR["CountryID"].ToString().Trim();
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

    #region Button : Cancel
    protected void btncancel_Click(object sender, EventArgs e)
     {
         Response.Redirect("~/AdminPanel/State/StateList.aspx");
     }
    #endregion Button : Cancel
}