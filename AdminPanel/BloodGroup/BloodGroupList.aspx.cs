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

public partial class AdminPanel_BloodGroup_BloodGroupList : System.Web.UI.Page
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
            FillGridViewBloodGroup();
        }
    }
    #endregion Load Event

    #region FillGridView
    private void FillGridViewBloodGroup()
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
                    objCmd.CommandText = "PR_BloodGroup_SelectAllByUserID";
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
                            gvBloodGroup.DataSource = objSDR;
                            gvBloodGroup.DataBind();
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
        Response.Redirect("~/AdminPanel/BloodGroup/BloodGroupAddEdit.aspx");
    }
    #endregion Button : Add

    #region gvBloodGroup : RowCommand
    protected void gvBloodGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument != null)
            {
                DeleteBloodGroup(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                
            }


        }
    }
    #endregion gvBloodGroup : RowCommand

    #region Delete Record
    private void DeleteBloodGroup(SqlInt32 BloodGroupID)
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
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                using (SqlCommand objCmd = objConn.CreateCommand())
                {
                    #region Set Connection & Command Object
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_BloodGroup_DeleteByPKByUserID";

                    objCmd.Parameters.Add("@BloodGroupID", SqlDbType.Int).Value = BloodGroupID;
                    objCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    #endregion Set Connection & Command Object

                    objCmd.ExecuteNonQuery();
                    lblSuccess.Text = "-Data Deleted succsessfully";
                    FillGridViewBloodGroup();

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
