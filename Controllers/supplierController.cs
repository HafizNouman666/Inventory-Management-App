using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystems;

namespace InventoryManagementSystem
{
    class supplierController
    {
        static supplierModel supplier = new supplierModel();
        static List<supplierModel> supplierList = new List<supplierModel>();
        
        //Insert New Supplier
        public static void InsertNewSupplier(string company, string connectionPerson, string phoneno1, string phoneno2, string address, string ntn, int status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_insertSupplier", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@company", company);
                cmd.Parameters.AddWithValue("@conPerson", connectionPerson);
                cmd.Parameters.AddWithValue("@phone1", phoneno1);
                cmd.Parameters.AddWithValue("@phone2", phoneno2);
                cmd.Parameters.AddWithValue("@address ", address);
                cmd.Parameters.AddWithValue("@ntn ", ntn);
                cmd.Parameters.AddWithValue("@status ", status);
                MainClass.cnn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Supplier Added successfully!");
                MainClass.cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                MainClass.cnn.Close();

            }

        }

        //Get All Supplier
        public static List<supplierModel> getallSupplier()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_getSupplierData", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                MainClass.cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    supplier = new supplierModel();
                    supplier.sup_id= reader.GetInt32(0);
                    supplier.sup_company= reader.GetString(1);
                    supplier.sup_contactPerson = reader.GetString(2);
                    supplier.sup_phone1 = reader.GetString(3);
                    supplier.sup_phone2 = reader.GetString(4);
                    supplier.sup_address = reader.GetString(5);
                    supplier.sup_ntn = reader.GetString(6);
                    supplier.get_status = reader.GetString(7);
                    supplierList.Add(supplier);
                }

                MainClass.cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e);
                MainClass.cnn.Close();

            }

            return supplierList;
        }

        //Update Supplier
        public static void UpdateSupplier(int id, string company, string connectionPerson, string phoneno1, string phoneno2, string address, string ntn, int status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_updateSupplier", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@company", company);
                cmd.Parameters.AddWithValue("@conPerson", connectionPerson);
                cmd.Parameters.AddWithValue("@phone1", phoneno1);
                cmd.Parameters.AddWithValue("@phone2", phoneno2);
                cmd.Parameters.AddWithValue("@address ", address);
                cmd.Parameters.AddWithValue("@ntn ", ntn);
                cmd.Parameters.AddWithValue("@status ", status);
                MainClass.cnn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Supplier Updated successfully!");
                MainClass.cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                MainClass.cnn.Close();

            }

        }

        //Delete Supplier
        public static void DeleteSupplier(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("st_deleteSupplier", MainClass.cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                MainClass.cnn.Open();
                int deletedrecord = cmd.ExecuteNonQuery();
                if (deletedrecord == 0)
                {
                    Console.WriteLine("Supplier Not Found!");
                    MainClass.cnn.Close();
                }
                else
                {
                    Console.WriteLine("Supplier deleted successfully!");
                    MainClass.cnn.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                MainClass.cnn.Close();

            }

        }
       

    }
}
