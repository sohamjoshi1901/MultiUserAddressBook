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

public partial class AddressBook_AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    #region Load Event
    
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Check For Valid User

        if (Session["UserID"] == null)
            Response.Redirect("~/Login.aspx");

        #endregion Check For Valid User

        #region Postback
        if (!Page.IsPostBack)
        {
            #region add Mode
            FillDropDownListCountry();
            FillDropDownListStateNew();
            if (Request.QueryString["CityID"] == null)
            {
                lblHeaderText.Text = "City add";


            }
            #endregion add Mode
            else
            {
                #region edit Mode
                lblHeaderText.Text = "City Edit";
                FillCityForm(Convert.ToInt32(Request.QueryString["CityID"].ToString().Trim()));
                #endregion edit Mode
            }
        }
        #endregion Postback

    }
    #endregion Load Event

    #region fill Control
    
    private void FillCityForm(SqlInt32 CityID)
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
                    objCmd.CommandText = "PR_City_SelectByPK";

                    objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                    #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            #region read Data
                            while (objSDR.Read())
                            {
                                if (!objSDR["CityName"].Equals(DBNull.Value))
                                    txtCityName.Text = objSDR["CityName"].ToString().Trim();

                                if (!objSDR["Pincode"].Equals(DBNull.Value))
                                    txtPinCode.Text = objSDR["Pincode"].ToString().Trim();

                                if (!objSDR["STDCode"].Equals(DBNull.Value))
                                    txtSTDCode.Text = objSDR["STDCode"].ToString().Trim();

                                if (!objSDR["StateID"].Equals(DBNull.Value))
                                    ddlState.SelectedValue = objSDR["StateID"].ToString().Trim();

                                if (!objSDR["CountryID"].Equals(DBNull.Value))
                                    ddlCountry.SelectedValue = objSDR["CountryID"].ToString().Trim();
                            }
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
    #endregion fill Control

    #region fill DropDownListCountry
    protected void FillDropDownListCountry()
    {
        SqlInt32 UserID = SqlInt32.Null;
        if (Session["UserID"] != null)
            UserID = Convert.ToInt32(Session["UserID"]);

        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountry, UserID);
    }
    #endregion fill DropDownList

    #region fill DropDownListStateNew
    protected void FillDropDownListStateNew()
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
                    objCmd.CommandText = "PR_State_SelectForDropDownListByUserID";
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            #region Read the value and set the controls
                            ddlState.DataValueField = "StateID";
                            ddlState.DataTextField = "StateName";

                            ddlState.DataSource = objSDR;
                            ddlState.DataBind();

                            ddlState.Items.Insert(0, new ListItem("Select Country", "-1"));
                            #endregion Read the value and set the controls
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

    #endregion fill DropDownListNew

    #region fill DropDownListState

    protected void FillDropDownListState()
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
                    objCmd.CommandText = "PR_State_SelectForDropDownListNew";
                    objCmd.Parameters.AddWithValue("@CountryID", ddlCountry.SelectedItem.Value.ToString());
                    #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            #region Read the value and set the controls
                            ddlState.DataValueField = "StateID";
                            ddlState.DataTextField = "StateName";

                            ddlState.DataSource = objSDR;
                            ddlState.DataBind();

                            ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
                            #endregion Read the value and set the controls
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
    #endregion fill DropDownListState

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {

        #region Local Variables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        string strError = "";

        SqlInt32 CountryID = SqlInt32.Null;
        SqlInt32 StateID = SqlInt32.Null;
        SqlString CityName = SqlString.Null;
        SqlString Pincode = SqlString.Null;
        SqlString STDCode = SqlString.Null;
        SqlInt32 UserID = SqlInt32.Null;
        #endregion Local Variables

        #region Server Side Validation
        if (ddlCountry.SelectedIndex == 0)
            strError += "-Select Country</br>";
        if (ddlState.SelectedIndex == 0)
            strError += "-Select State</br>";

        if (txtCityName.Text.Trim() == "")
            strError += "-Enter City Name";
        if (strError != "")
        {
            lblMessage.Text = strError;
            return;
        }
        #endregion Server Side Validation

        #region Gather Information
        if (ddlCountry.SelectedIndex > 0)
            CountryID = Convert.ToInt32(ddlCountry.SelectedItem.Value);
        if (ddlState.SelectedIndex > 0)
            StateID = Convert.ToInt32(ddlState.SelectedItem.Value);
        if (txtCityName.Text.Trim() != "")
            CityName = txtCityName.Text.Trim();
        if (txtPinCode.Text != "")
            Pincode = txtPinCode.Text.Trim();
        if (txtSTDCode.Text != "")
            STDCode = txtSTDCode.Text.Trim();
        if (Session["UserID"] != null)
            UserID = Convert.ToInt32(Session["UserID"]);
        #endregion Gather Information

        //lblMessage.Text = StateID.ToString();


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
                    if (Request.QueryString["CityID"] == null)
                        #region Insert
                        objCmd.CommandText = "PR_City_InsertByUserID";
                        #endregion Insert
                    else
                    {
                        #region Update
                        objCmd.CommandText = "PR_City_UpdateByPKByUserID";
                        objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = Request.QueryString["CityID"].ToString().Trim();
                        #endregion Update
                    }

                    #region Parameters
                    objCmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = CityName;
                    objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                    objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;

                    objCmd.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = Pincode;
                    objCmd.Parameters.Add("@STDCode", SqlDbType.VarChar).Value = STDCode;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Parameters
                    objCmd.ExecuteNonQuery();

                    if (Request.QueryString["CityID"] == null)
                    {
                        lblSuccess.Text = "data insert successfully.";
                        ddlState.SelectedIndex = 0;
                        txtCityName.Text = "";
                        ddlCountry.Focus();
                    }
                    else
                    {
                        Response.Redirect("~/AdminPanel/City/CityList.aspx");
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

    #region Country_SelectedIndexChanged
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState.Items.Clear();
        FillDropDownListState();
    }
    #endregion Country_SelectedIndexChanged

    #region Button : Cancel
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/City/CityList.aspx");
    }
    #endregion Button : Cancel
}

