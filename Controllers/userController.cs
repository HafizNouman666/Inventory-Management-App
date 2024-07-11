using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystem;
using InventoryManagementSystems;

namespace InventoryManagementSystem
{
    class userController
    {
        public static userModel us = new userModel();
        public static List<userModel> users = new List<userModel>();
       
        //Insert New User
        public static void InsertNewUser(string name,string uname,string password,string phoneno,string email,Int16 status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertUsers", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@username", uname);
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.Parameters.AddWithValue("@phone", phoneno);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@status", status);
                MainClass.cnn.Open();
                cmd.ExecuteNonQuery();   
                MainClass.showMSG(name+" added to system successfully", "Success...", "Success");
                MainClass.cnn.Close();

            }
            catch (Exception e)
            {
                
                MainClass.showMSG(e.Message, "Error...", "Error");
                MainClass.cnn.Close();
                
            }

        }

        //Get All Users
        public static List<userModel> getallUsers(DataGridView gv,DataGridViewColumn userIDGV, DataGridViewColumn nameGV, DataGridViewColumn unameGV, DataGridViewColumn passGV, DataGridViewColumn phoneGV, DataGridViewColumn emailGV, DataGridViewColumn statusGV,string data=null)
        {
            try
            {
                SqlCommand cmd;
                if (data == null)
                {
                    cmd= new SqlCommand("st_getUsersData", MainClass.cnn);
                }
                else
                {
                     cmd = new SqlCommand("st_getuserdataLike", MainClass.cnn);
                    cmd.Parameters.AddWithValue("@data", data);
                }
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                userIDGV.DataPropertyName = dt.Columns["ID"].ToString();
                nameGV.DataPropertyName = dt.Columns["Name"].ToString();
                unameGV.DataPropertyName = dt.Columns["Username"].ToString();
                passGV.DataPropertyName = dt.Columns["Password"].ToString();
                phoneGV.DataPropertyName = dt.Columns["Phone"].ToString();
                emailGV.DataPropertyName = dt.Columns["Email"].ToString();
                statusGV.DataPropertyName = dt.Columns["Status"].ToString();
                gv.DataSource = dt;
                MainClass.cnn.Open();

                MainClass.cnn.Close();

            }
            catch (Exception e)
            {
                
                MainClass.cnn.Close();

            }

            return users;
        }
        
        //Update User
        public static void UpdateUser(int id,string name, string uname, string password, string phoneno, string email,Int16 status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_updateUsers", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@username", uname);
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.Parameters.AddWithValue("@phone", phoneno);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@status", status);
           
                MainClass.cnn.Open();
                int ucheck=cmd.ExecuteNonQuery();
                if (ucheck > 0)
                {
                     MainClass.showMSG(name+" updated in system successfully", "Success...", "Success");
                    MainClass.cnn.Close();
                }
                else
                {
                    MainClass.showMSG("User not updated", "Error...", "Error");

                    MainClass.cnn.Close();
                }
                
            }
            catch (Exception e)
            {
                
                MainClass.cnn.Close();
                MainClass.showMSG(e.Message, "Error...", "Error");


            }

        }

        //Delete New User
        public static void DeleteUser(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_deleteUser", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                MainClass.cnn.Open();
                int deletedrecord = cmd.ExecuteNonQuery();
                if (deletedrecord == 0)
                {
                    MainClass.showMSG("User not deleted!", "Error...", "Error");
                    MainClass.cnn.Close();
                }
                else
                {
                    
                    MainClass.showMSG("User deleted!", "Success...", "Success");

                    MainClass.cnn.Close();
                }
                
            }
            catch (Exception e)
            {
                MainClass.cnn.Close();
                MainClass.showMSG(e.Message, "Error...", "Error");

            }

        }

    }
}
