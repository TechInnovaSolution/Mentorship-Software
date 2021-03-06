﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


namespace WebApplication1
{
    public partial class MenteeRegister : System.Web.UI.Page
    {
        string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Session["username"].ToString();
            user = username;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            isValid = IsValid(isValid);

            try
            {
            if ((IsPostBack)&&(isValid==true))
                {
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                    "Initial Catalog=MentorshipNetwork;" +
                    "persist security info=true;" +
                    "Integrated Security=SSPI;";
                connect.Open();

                string updateUserQuery = "UPDATE Mentee SET MenteeLastName=@FLName, MenteeFirstName=@FFName, MenteeDoB=@fDoB, MenteeGender=@FGen, MenteePhone=@FPhone, MenteeLocation = @FLoc, MenteeUni=@FUni, MenteeYearOfStudy = @FYoS, MenteeQualification = @FQual WHERE MenteeUsername = @Fuser";

                SqlCommand comm = new SqlCommand(updateUserQuery, connect);
                comm.Parameters.AddWithValue("@FLName", txtLName.Text);
                comm.Parameters.AddWithValue("@FFName", txtFName.Text);
                comm.Parameters.AddWithValue("@fDoB", txtDoB.Text);
                comm.Parameters.AddWithValue("@FGen", txtGender.Text);
                comm.Parameters.AddWithValue("@FPhone", txtPhone.Text);
                comm.Parameters.AddWithValue("@FLoc", txtLocation.Text);
                comm.Parameters.AddWithValue("@FUni", txtUni.Text);
                comm.Parameters.AddWithValue("@FYoS", txtYoS.Text);
                comm.Parameters.AddWithValue("@FQual", txtQualification.Text);
                comm.Parameters.AddWithValue("@Fuser", user);

                comm.ExecuteNonQuery();
                Response.Write("Succesful");

                Session["username"] = user;
               Response.Redirect("UpdatePreference.aspx");

                connect.Close();
            }
            }
            catch (Exception ex)
            {
                Response.Write("Error\n" + ex.ToString());
            }
        }
        
        

    bool IsValid(bool isValid)
    {
        /*if (txtUserName.Text == "")
        {
            isValid = false;
            lblIncUsername.Text = "Please enter a username";
            lblIncUsername.Visible = true;
        }

        if (txtEmail.Text == "")
        {
            isValid = false;
            lblInvalidEmail.Text = "Please enter your email address";
            lblInvalidEmail.Visible = true;
        }

        if ((txtPassword.Text == ""))
        {
            isValid = false;
            lblInvalidPass.Text = "Please enter a valid password with 8 characters";
            lblInvalidPass.Visible = true;
        }

        if(txtPassword0.Text != txtPassword.Text)
        {
            isValid = false;
            lblInvalidPass0.Text = "Password confirmation does not match Original password";
            lblInvalidPass0.Visible = true;
        }*/

        return isValid;
    }
    }
}