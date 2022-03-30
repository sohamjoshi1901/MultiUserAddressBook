using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddressBook_AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
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
            
            FillDropDownListCountry();
            FillDropDownListState(null);
            FillDropDownListCity(null);
            FillDropDownListBoodGroup();
            FillDropDownListContactCategory();
            if (Request.QueryString["ContactID"] == null)
            {
                lblPageHeaderText.Text = "Contact Add";
            }
            else
            {
                lblPageHeaderText.Text = "Contact Edit";
                
                
                FillContactForm(Convert.ToInt32(Request.QueryString["ContactID"].ToString().Trim()));

            }
        }
    }
    #endregion Load Event

    #region Fill Cantrols
    private void FillContactForm(SqlInt32 ContactID)
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
                    objCmd.CommandText = "PR_Contact_SelectByPK";

                    objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            while (objSDR.Read())
                            {
                                if (!objSDR["PersonName"].Equals(DBNull.Value))
                                    txtPeresonName.Text = objSDR["PersonName"].ToString().Trim();

                                if (!objSDR["Address"].Equals(DBNull.Value))
                                    txtAddress.Text = objSDR["Address"].ToString().Trim();

                                if (!objSDR["Email"].Equals(DBNull.Value))
                                    txtEmail.Text = objSDR["Address"].ToString().Trim();

                                if (!objSDR["MobileNo"].Equals(DBNull.Value))
                                    txtMobileNo.Text = objSDR["MobileNo"].ToString().Trim();

                                if (!objSDR["PhoneNo"].Equals(DBNull.Value))
                                    txtPhoneNo.Text = objSDR["PhoneNo"].ToString().Trim();

                                if (!objSDR["Pincode"].Equals(DBNull.Value))
                                    txtPincode.Text = objSDR["Pincode"].ToString().Trim();

                                if (!objSDR["Gender"].Equals(DBNull.Value))
                                    txtGender.Text = objSDR["Gender"].ToString().Trim();

                                if (!objSDR["Profession"].Equals(DBNull.Value))
                                    txtProfession.Text = objSDR["Profession"].ToString().Trim();

                                if (!objSDR["BirthDate"].Equals(DBNull.Value))
                                    txtBirthDate.Text = objSDR["BirthDate"].ToString().Trim();



                                if (!objSDR["CountryID"].Equals(DBNull.Value))
                                    ddlCountry.SelectedValue = objSDR["CountryID"].ToString().Trim();

                                FillDropDownListState(Convert.ToInt32(objSDR["CountryID"]));

                                if (!objSDR["StateID"].Equals(DBNull.Value))
                                    ddlState.SelectedValue = objSDR["StateID"].ToString().Trim();
                                FillDropDownListCity(Convert.ToInt32(objSDR["StateID"]));

                                if (!objSDR["CityID"].Equals(DBNull.Value))
                                    ddlCity.SelectedValue = objSDR["CityID"].ToString().Trim();

                                if (!objSDR["BloodGroupID"].Equals(DBNull.Value))
                                    ddlBloodGroup.SelectedValue = objSDR["BloodGroupID"].ToString().Trim();

                                if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                                    ddlContactCategory.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();

                                if (objSDR["Photo"].Equals(DBNull.Value) != true)
                                {
                                    imgOldPhoto.ImageUrl = objSDR["Photo"].ToString().Trim();
                                }
                                //if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                                //    fuPhoto.sele = objSDR["Photo"].ToString().Trim();
                                objSDR.Close();
                                #region FillContactCategory
                                DataTable dt = new DataTable();

                                SqlCommand objCmdContactCategory = objConn.CreateCommand();
                                objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                                objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_CheckboxList";

                                if (Session["UserID"] != null)
                                    objCmdContactCategory.Parameters.AddWithValue("@UserID", Session["UserID"]);

                                objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID);
                                SqlDataReader objSDRContactCategory = objCmdContactCategory.ExecuteReader();

                                if (objSDRContactCategory.HasRows)
                                {
                                    while (objSDRContactCategory.Read())
                                    {
                                        if (!objSDRContactCategory["ContactCategoryID"].Equals(DBNull.Value))
                                        {
                                            foreach (ListItem liContactCategoryID in ddlContactCategory.Items)
                                            {
                                                if (liContactCategoryID.Value == objSDRContactCategory["ContactCategoryID"].ToString().Trim())
                                                {
                                                    liContactCategoryID.Selected = true;
                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion FillContactCategory
                                break;

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
        SqlString strPersonName = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strContactPhotoPath = SqlString.Null;
        SqlString strPinCode = SqlString.Null;
        SqlString strMobileNo = SqlString.Null;
        SqlString strPhoneNo = SqlString.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strGender = SqlString.Null;
        SqlString strProfession = SqlString.Null;
        SqlInt32 CountryID = SqlInt32.Null;
        SqlInt32 StateID = SqlInt32.Null;
        SqlInt32 CityID = SqlInt32.Null;
        SqlInt32 BloodGroupID = SqlInt32.Null;
        SqlInt32 ContactCategoryID = SqlInt32.Null;
        SqlString BirthDate = SqlString.Null;
        SqlInt32 UserID = SqlInt32.Null;
        string strError = "";
        #endregion Local Variables

        //#region FileUpload

        ////if (fuPhoto.HasFiles)
        ////{
        //    string strFileLocationToSave = "~/UploadedData/";
        //    string strPhysicalPath = "";
        //    string logicalPath = strFileLocationToSave + fuPhoto.FileName;


        //    lblMessage.Text = "path to upload<br/>";
        //    lblMessage.Text += "Logical path:" + strFileLocationToSave;

        //    strPhysicalPath = Server.MapPath(strFileLocationToSave);
        //    strPhysicalPath += fuPhoto.FileName;

        //    if (File.Exists(strPhysicalPath))
        //    {
        //        File.Delete(strPhysicalPath);
        //    }

        //        fuPhoto.SaveAs(strPhysicalPath);
        ////    }
        ////else
        ////{
        ////   strError+= "uploade file<br>";
        ////}

            

        //#endregion FileUpload
        using (SqlConnection objConn = new SqlConnection(ConnectionString))
        {

            #region Server Side Validation
            if (txtPeresonName.Text.Trim() == "")
                strError += "-Enter Person Name</br>";
            if (ddlCountry.SelectedIndex == 0)
                strError += "-Select Country</br>";
            if (ddlState.SelectedIndex == 0)
                strError += "-Select State</br>";
            if (ddlCity.SelectedIndex == 0)
                strError += "-Select City</br>";
            if (ddlBloodGroup.SelectedIndex == 0)
                strError += "-Select Blood Group</br>";
           
            if (ddlContactCategory.SelectedIndex == 0)
                strError += "-Select Contact Category</br>";
            if (txtMobileNo.Text.Trim() == "")
                strError += "-Enter Mobile Number</br>";
            if (!fuPhoto.HasFiles)
                strError += "-Upload Picture";
            

            if (strError != "")
            {
                lblMessage.Text = strError;
                return;
            }
            #endregion Server Side Validation

            String ContactPhotoPath = "";
            if (fuPhoto.HasFile)
            {
                ContactPhotoPath = "~/UploadedData/" + fuPhoto.FileName.ToString().Trim();
                fuPhoto.SaveAs(Server.MapPath(ContactPhotoPath));
            }
            #region Gather Information
            strPersonName = txtPeresonName.Text.Trim();
            strAddress = txtAddress.Text.Trim();
            strEmail = txtEmail.Text.Trim();
            strMobileNo = txtMobileNo.Text.Trim();
            strPhoneNo = txtPhoneNo.Text.Trim();
            strPinCode = txtPincode.Text.Trim();
            strGender = txtGender.Text.Trim();
            strProfession = txtProfession.Text.Trim();
            CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            StateID = Convert.ToInt32(ddlState.SelectedValue);
            CityID = Convert.ToInt32(ddlCity.SelectedValue);
            BloodGroupID = Convert.ToInt32(ddlBloodGroup.SelectedValue);
            ContactCategoryID = Convert.ToInt32(ddlContactCategory.SelectedValue);
            BirthDate = txtBirthDate.Text.Trim();
            if (ContactPhotoPath != "")
            {
                strContactPhotoPath = ContactPhotoPath;
            }
            if (Session["UserID"] != null)
                UserID = Convert.ToInt32(Session["UserID"]);
           
            #endregion Gather Information
            try
            {
                #region Set Connection & Command Object
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    #region Parameters
                    objCmd.Parameters.Add("@PersonName", SqlDbType.VarChar).Value = strPersonName;
                    objCmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = strAddress;
                    objCmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = strMobileNo;
                    objCmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = strPhoneNo;
                    objCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = strEmail;
                    objCmd.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = strPinCode;
                    objCmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = strGender;
                    objCmd.Parameters.Add("@Profession", SqlDbType.VarChar).Value = strProfession;
                    objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                    objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                    objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                    objCmd.Parameters.Add("@BloodGroupID", SqlDbType.Int).Value = BloodGroupID;
                    objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                    objCmd.Parameters.Add("@BirthDate", SqlDbType.VarChar).Value = BirthDate;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    objCmd.Parameters.AddWithValue("@Photo", strContactPhotoPath);

                    #endregion Parameters
                    #endregion Set Connection & Command Object
                    if (Request.QueryString["ContactID"] == null)
                    #region Insert
                    {
                        objCmd.CommandText = "PR_Contact_InsertByUserID";
                       
                        
                        objCmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                        objCmd.ExecuteNonQuery();
                        SqlInt32 ContactID = 0;
                        ContactID = Convert.ToInt32(objCmd.Parameters["@ContactID"].Value);

                       



                        foreach (ListItem liContactCategoryID in ddlContactCategory.Items)
                        {
                            if (liContactCategoryID.Selected)
                            {
                                SqlCommand objCmdContactCategory = objConn.CreateCommand();
                                objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                                objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_Insert";
                                objCmdContactCategory.Parameters.AddWithValue("ContactID", ContactID.ToString());
                                objCmdContactCategory.Parameters.AddWithValue("ContactCategoryID", liContactCategoryID.Value.ToString());
                                objCmdContactCategory.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                                objCmdContactCategory.ExecuteNonQuery();

                            }
                        }

                         lblSuccess.Text = "data insert successfully with ContactID = "+ ContactID.ToString();

                    }
                    #endregion Insert

                    else
                    {
                        #region Update
                        objCmd.CommandText = "PR_Contact_UpdateByPKByUserID";
                        //objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = Request.QueryString["ContactID"].ToString().Trim();
                        #region Gather Information
                        SqlCommand objCmdContactCategoryDelete = objConn.CreateCommand();
                        objCmdContactCategoryDelete.CommandType = CommandType.StoredProcedure;
                        //if (Session["UserId"] != null)
                        //    objCmdContactCategoryDelete.Parameters.AddWithValue("@UserID", Session["UserId"]);
                        objCmdContactCategoryDelete.Parameters.AddWithValue("ContactID", Request.QueryString["ContactID"].ToString().Trim());
                        #endregion Gather Information

                        objCmdContactCategoryDelete.CommandText = "PR_ContactWiseContactCategory_DeleteByUserID";
                        objCmd.Parameters.AddWithValue("ContactID", Request.QueryString["ContactID"].ToString().Trim());
                        objCmdContactCategoryDelete.ExecuteNonQuery();
                        #endregion Update


                        
                        foreach (ListItem liContactCategoryID in ddlContactCategory.Items)
                        {
                            if (liContactCategoryID.Selected)
                            {
                                SqlCommand objCmdContactCategory = objConn.CreateCommand();
                                objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                                objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_Insert";
                                objCmdContactCategory.Parameters.AddWithValue("ContactID", Request.QueryString["ContactID"].ToString().Trim());
                                objCmdContactCategory.Parameters.AddWithValue("ContactCategoryID", liContactCategoryID.Value.ToString());
                                objCmdContactCategory.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                                objCmdContactCategory.ExecuteNonQuery();

                            }
                        }

                        objCmd.ExecuteNonQuery();
                    }

                    if (Request.QueryString["ContactID"] == null)
                    {
                       
                        #region clear Field
                        ddlCountry.SelectedIndex = 0;
                        ddlState.SelectedIndex = 0;
                        ddlCity.SelectedIndex = 0;
                        ddlBloodGroup.SelectedIndex = 0;
                        ddlContactCategory.SelectedIndex = 0;
                        txtPeresonName.Text = "";
                        txtAddress.Text = "";
                        txtEmail.Text = "";
                        txtMobileNo.Text = "";
                        txtPhoneNo.Text = "";
                        txtPincode.Text = "";
                        txtGender.Text = "";
                        txtProfession.Text = "";
                        txtBirthDate.Text = "";
                        txtPeresonName.Focus();
                        #endregion clear Field
                    }
                    else
                    {
                        Response.Redirect("~/AdminPanel/Contact/ContactList.aspx");
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

    #region fill DropDownListCountry
    void FillDropDownListCountry()
    {
        SqlInt32 UserID = SqlInt32.Null;
             if (Session["UserID"] != null)
                  UserID = Convert.ToInt32(Session["UserID"]);

        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountry,UserID);
       
    }
    #endregion fill DropDownListCountry

    #region fill DropDownListBloodGroup
    void FillDropDownListBoodGroup()
    {
        SqlInt32 UserID = SqlInt32.Null;
        if (Session["UserID"] != null)
            UserID = Convert.ToInt32(Session["UserID"]);

        CommonDropDownFillMethods.FillDropDownListBloodGroup(ddlBloodGroup, UserID);
        
    }
    #endregion fill DropDownListBloodGroup

    #region fill DropDownListContactcategory
    void FillDropDownListContactCategory()
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
                    objCmd.CommandText = "PR_ContactCategory_SelectForDropDownListByUserID";
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            #region read Data
                            ddlContactCategory.DataValueField = "ContactCategoryID";
                            ddlContactCategory.DataTextField = "ContactCategoryName";

                            ddlContactCategory.DataSource = objSDR;
                            ddlContactCategory.DataBind();

                            ddlContactCategory.Items.Insert(0, new ListItem("Select Contact Category", "-1"));
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
    #endregion fill DropDownListContactCategory

    #region fill DropDownListState
    void FillDropDownListState(Int32? CountryID)
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

                   // if (Request.QueryString["ContactID"] == null)
                    
                        objCmd.CommandText = "PR_State_SelectForDropDownListNewByUserID";

                        if (CountryID != null)
                        {
                            objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = ddlCountry.SelectedItem.Value;
                        }
                        ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
                    
                    //else
                    //{
                        
                    //    objCmd.CommandText = "PR_State_SelectForDropDownListByNewUserID";
                    //    objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                        

                    //}
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Set Connection & Command Object


                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            #region read Data
                            ddlState.DataValueField = "StateID";
                            ddlState.DataTextField = "StateName";

                            ddlState.DataSource = objSDR;
                            ddlState.DataBind();

                            ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
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
        lblMessage.Text =Convert.ToString( CountryID);
    }
    #endregion fill DropDownListState

    #region fill DropDownListCity
    void FillDropDownListCity(Int32? StateID)
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
                    //if (Request.QueryString["ContactID"] == null)
                    
                        objCmd.CommandText = "PR_City_SelectForDropDownListNewByUserID";
                        objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = ddlState.SelectedItem.Value;
                        //if (StateID != null)
                        //{
                        //    objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = ddlState.SelectedItem.Value;
                        //}
                        ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
                    
                    //else
                    //{
                    //    objCmd.CommandText = "PR_City_SelectForDropDownListByUserID";
                    //}
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            #region read Data
                            ddlCity.DataValueField = "CityID";
                            ddlCity.DataTextField = "CityName";

                            ddlCity.DataSource = objSDR;
                            ddlCity.DataBind();

                           ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
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
    #endregion fill DropDownList

    #region country_SelectedIndexChanged
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState.Items.Clear();
        ddlCity.Items.Clear();
        FillDropDownListState(Convert.ToInt32( ddlCountry.SelectedValue));
        FillDropDownListCity(Convert.ToInt32(ddlState.SelectedValue));
    }
    #endregion country_SelectedIndexChanged

    #region State_SelectedIndexChanged
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCity.Items.Clear();
        FillDropDownListCity(Convert.ToInt32(ddlState.SelectedValue));
    }
    #endregion State_SelectedIndexChanged

    #region Button : Cancel
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Contact/ContactList.aspx");
    }
    #endregion Button : Cancel


}
