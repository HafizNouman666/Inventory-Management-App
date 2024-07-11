using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagementSystems;
using System.Data;


namespace InventoryManagementSystem
{
    class categoriesController
    {
        static categoriesModel category = new categoriesModel();
        static List<categoriesModel> catlist = new List<categoriesModel>();
        //Insert New Category
        public static void InsertNewCategory(string name, int isActive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertCategory", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@isActive", isActive);            
                MainClass.cnn.Open();
                cmd.ExecuteNonQuery();
                MainClass.showMSG(name + " added to system successfully", "Success...", "Success");
                MainClass.cnn.Close();

            }
            catch (Exception e)
            {

                MainClass.showMSG(e.Message, "Error...", "Error");
                MainClass.cnn.Close();

            }

        }

        //Category list
        public static void getCategoriesList(string proc, ComboBox cb,string displayMember,string valueMembar)
        {
            try
            {
                cb.Items.Clear();
                cb.DataSource = null;
              
                SqlCommand cmd = new SqlCommand(proc, MainClass.cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] {0, "Select ... " };
                dt.Rows.InsertAt(dr,0);
                cb.DisplayMember = displayMember;
                cb.ValueMember = valueMembar;
                cb.DataSource = dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get All Categories
        public static List<categoriesModel> getCategoryData(DataGridView gv, DataGridViewColumn catidGV, DataGridViewColumn nameGV, DataGridViewColumn statusGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getCategoriesData", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                catidGV.DataPropertyName = dt.Columns["ID"].ToString();
                nameGV.DataPropertyName = dt.Columns["Category"].ToString();
                statusGV.DataPropertyName = dt.Columns["Status"].ToString();
                gv.DataSource = dt;

                MainClass.cnn.Open();
                MainClass.cnn.Close();

            }
            catch (Exception e)
            {
                MainClass.showMSG(e+"Unable To Show Data ","ERROR","ERORR");
                MainClass.cnn.Close();

            }

           

            return catlist;
        }

         //Update Category
        public static void UpdateCategory(int id,string name,int isActive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_updateCategory", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@isActive", isActive);        
                MainClass.cnn.Open();
                cmd.ExecuteNonQuery();
                int ucheck = cmd.ExecuteNonQuery();
                if (ucheck > 0)
                {
                    MainClass.showMSG(name + " Category in system successfully", "Success...", "Success");
                    MainClass.cnn.Close();
                }
                else
                {
                    MainClass.showMSG(" Category not updated", "Error...", "Error");
                    MainClass.cnn.Close();
                }
                
                MainClass.cnn.Close();
            }
            catch (Exception e)
            {
                MainClass.showMSG(e.Message, "Error...", "Error");
                MainClass.cnn.Close();

            }

        }

        //Delete Category
        public static void DeleteCategory(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_deleteCategory", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                MainClass.cnn.Open();
                int deletedrecord = cmd.ExecuteNonQuery();
                if (deletedrecord == 0)
                {
                    MainClass.showMSG("Category not deleted!", "Error...", "Error");
                    MainClass.cnn.Close();
                }
                else

                {
                    MainClass.showMSG("Category deleted!", "Success...", "Success");
                    MainClass.cnn.Close();
                }
            }
            catch (Exception e)
            {
                MainClass.showMSG(e.Message, "Error...", "Error");
                MainClass.cnn.Close();

            }

        }

       


    }
}
