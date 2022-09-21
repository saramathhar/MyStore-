using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myStoreWeb.Pages.Product;
using System.Data.SqlClient;

namespace myStoreWeb.Pages.Product
{
    public class EditModel : PageModel
    {
        public ProductInfo Product = new ProductInfo();
        public String errorMsg = "";
        public String successMsg = "";
      
           
            public void OnGet()
            {
                // this method to see the data of the current Product

                //to read the id of the Product
                String id = Request.Query["id"];


                //to connect to the database
                try
                {
                    String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myStoreWeb;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        //to bring the required record from the table in DB
                        String sql = "select * from Product where id=@id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Product.id = "" + reader.GetInt32(0);
                                    Product.name = reader.GetString(1);
                                    Product.price =  reader.GetString(2);
                                Product.Category_id =  reader.GetString(3);
                            }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    return;

                }

            }

            public void OnPost()
            {
                // to update the data on the Product
            
                Product.name = Request.Form["name"];
                Product.price = Request.Form["price"];
            Product.Category_id= Request.Form["Category_id"];

                if (Product.name.Length == 0 || Product.price.Length == 0 || Product.Category_id.Length ==0)
                {
                    errorMsg = "All the fields must be filled !";
                    return;
                }


                try
                {
                    String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myStoreWeb;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "Update Product set name =@name, price=@price, Category_id=@Category_id where id=@id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@name", Product.name);
                            command.Parameters.AddWithValue("@price", Product.price);
                        command.Parameters.AddWithValue("@Category_id", Product.Category_id);
                        command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    return;

                }

                Response.Redirect("/Product/Product");

            }
        }
    }

