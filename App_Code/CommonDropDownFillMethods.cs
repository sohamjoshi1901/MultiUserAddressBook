using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonDropDownFillMethods
/// </summary>
public static class CommonDropDownFillMethods
{
    #region FillDropDownListCountry
    public static void FillDropDownListCountry(DropDownList ddl,SqlInt32 UserID)
    {
        #region Local Variables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        
        #endregion Local Variables

        #region Gather Information
        //if (Session["UserID"] != null)
        //    UserID = Convert.ToInt32(Session["UserID"]);
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
                            ddl.DataValueField = "CountryID";
                            ddl.DataTextField = "CountryName";

                            ddl.DataSource = objSDR;
                            ddl.DataBind();

                            ddl.Items.Insert(0, new ListItem("Select Country", "-1"));
                            #endregion read Data

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
            }
        }
    }
    #endregion fill DropDownListCountry

    #region FillDropDownListBloodGroup
    public static void FillDropDownListBloodGroup(DropDownList ddl, SqlInt32 UserID)
    {
        #region Local Variables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;

        #endregion Local Variables

        #region Gather Information
        //if (Session["UserID"] != null)
        //    UserID = Convert.ToInt32(Session["UserID"]);
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
                    objCmd.CommandText = "PR_BloodGroup_SelectForDropDownListByUserID";
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                #endregion Set Connection & Command Object
                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (objSDR.HasRows)
                        {
                            #region read Data
                            ddl.DataValueField = "BloodGroupID";
                            ddl.DataTextField = "BloodGroupName";

                            ddl.DataSource = objSDR;
                            ddl.DataBind();

                            ddl.Items.Insert(0, new ListItem("Select BloodGroup", "-1"));
                            #endregion read Data

                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
            }
        }
    }
    #endregion fill DropDownListBloodGroup

    #region fill DropDownListState
    public static void FillDropDownListState( DropDownList ddl,SqlInt32 UserID,Int32? CountryID)
    {

        #region Local Variables
        string ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
        

        #endregion Local Variables

        #region Gather Information
        //if (Session["UserID"] != null)
        //    UserID = Convert.ToInt32(Session["UserID"]);

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
                        objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = ddl.SelectedItem.Value;
                    }
                    ddl.Items.Insert(0, new ListItem("Select State", "-1"));

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
                            ddl.DataValueField = "StateID";
                            ddl.DataTextField = "StateName";

                            ddl.DataSource = objSDR;
                            ddl.DataBind();

                            ddl.Items.Insert(0, new ListItem("Select State", "-1"));
                            #endregion read Data

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
            }
        }
        
    }
    #endregion fill DropDownListState



    public static void FillDropDownListCountry(DropDownList ddlCountry)
    {
        throw new NotImplementedException();
    }
}