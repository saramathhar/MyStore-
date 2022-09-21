using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myStoreWeb.Pages.Product;
using System.Data.SqlClient;


namespace myStoreWeb.Pages.Product
{
    public class ProductModel : PageModel
    {
        public List<ProductInfo> listProduct = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myStoreWeb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = " SELECT * FROM Product";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo Product = new ProductInfo();
                                Product.id = "" + reader.GetInt32(0);
                                Product.name = reader.GetString(1);
                                Product.price = reader.GetString(2);
                                Product.Category_id = reader.GetString(3);
                                // Product.img = reader.GetString(4);        Google it 



                                //adding the info to the list
                                listProduct.Add(Product);

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }

    public class ProductInfo
    {
        public string id;
        public string name;
        public string price;
        public string Category_id;
        
        //public ImageFormatException 
    }
}