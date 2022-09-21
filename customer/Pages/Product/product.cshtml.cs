using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace customer.Pages.Product
{
    public class productModel : PageModel
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
                    string sql = " SELECT name,email FROM Client";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo Product = new ProductInfo();

                                Product.name = reader.GetString(0);
                                Product.email = reader.GetString(1);


                                //adding the data I stored in the object in to the list
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

        public string name;
        public string email;
        public string price;
        public string description;

    }
}
