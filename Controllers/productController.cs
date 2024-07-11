using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using InventoryManagementSystems;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InventoryManagementSystem
{
    class productController
    {
       static  productModel product = new productModel();
       static List<productModel> productlist = new List<productModel>();
        
        //Insert New Product
        public static void InsertNewProduct(string product,string barcode,float price,int catID, DateTime? expiry=null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_productInsert", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", product);
                cmd.Parameters.AddWithValue("@barcode", barcode);
                cmd.Parameters.AddWithValue("@price", price);
                if(expiry == null ) 
                {
                cmd.Parameters.AddWithValue("@expiry", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@expiry", expiry);
                }
                cmd.Parameters.AddWithValue("@catID", catID);
                MainClass.cnn.Open();
                cmd.ExecuteNonQuery();
                MainClass.showMSG(product + " added to system successfully", "Success...", "Success");
                MainClass.cnn.Close();
            }
            catch (Exception e)
            {
                MainClass.showMSG(e.Message, "Error...", "Error");
                MainClass.cnn.Close();
                
            }

        }
        //Get All Products
        public static List<productModel> getAllProducts(DataGridView gv, DataGridViewColumn proidGV, DataGridViewColumn pronameGV, DataGridViewColumn expiryGV, DataGridViewColumn priceGV, DataGridViewColumn catGV, DataGridViewColumn barcodeGV,DataGridViewColumn catidGV)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getProductswithCat", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                proidGV.DataPropertyName = dt.Columns["Product ID"].ToString();
                pronameGV.DataPropertyName = dt.Columns["Product"].ToString();
                expiryGV.DataPropertyName = dt.Columns["Expiry"].ToString();
                priceGV.DataPropertyName = dt.Columns["Price"].ToString();
                barcodeGV.DataPropertyName = dt.Columns["Barcode"].ToString();
                catGV.DataPropertyName = dt.Columns["Category"].ToString();
                catidGV.DataPropertyName = dt.Columns["Category ID"].ToString();
                gv.DataSource = dt;



                MainClass.cnn.Open();
                MainClass.cnn.Close();

            }
            catch (Exception e)
            {
                MainClass.cnn.Close();
            }

            return productlist;
        }

        //Update Product
        public static void UpdateProduct(int proID, string product, string barcode, float price, int catID, DateTime? expiry=null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_updateProducts", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", proID);
                cmd.Parameters.AddWithValue("@name", product);
                cmd.Parameters.AddWithValue("@barcode", barcode);
                cmd.Parameters.AddWithValue("@price", price);
                if (expiry == null)
                {
                    cmd.Parameters.AddWithValue("@expiry", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@expiry", expiry);
                }
                cmd.Parameters.AddWithValue("@catID", catID);

                MainClass.cnn.Open();
                cmd.ExecuteNonQuery();
                MainClass.showMSG(product + " Updated to system successfully", "Success...", "Success");
                MainClass.cnn.Close();
            }
            catch (Exception e)
            {
                MainClass.showMSG("Product not updated", "Error...", "Error");
                MainClass.cnn.Close();

            }

        }

        //Delete Product
        public static void DeleteProduct(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("deleteProducts", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                MainClass.cnn.Open();
                int deletedrecord=cmd.ExecuteNonQuery();
                if (deletedrecord == 0)
                {
                    MainClass.showMSG("User not deleted!", "Error", "Error");
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
                MainClass.showMSG(e.Message, "Error...", "Error");
                MainClass.cnn.Close();
            }

        }
       


    }
}
