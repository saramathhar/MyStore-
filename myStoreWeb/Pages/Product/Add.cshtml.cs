using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace myStoreWeb.Pages.Product
{
    public class AddModel : PageModel
    {
       
        public ProductInfo Product = new ProductInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            Product.name = Request.Form["name"];
            Product.price = Request.Form["price"];
            Product.Category_id = Request.Form["Category_id"];

            if (Product.name.Length == 0 || Product.price.Length == 0 || Product.Category_id.Length == 0)
            {
                errorMsg = "All the fields must be filled !";
                return;
            }
            // save the new Product to the data base
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myStoreWeb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into Product" + "(name, price, Category_id) values" + "(@name,@price, @Category_id);";
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


            Product.name = "";
            Product.price = "";
            Product.Category_id = "";

            successMsg = "New Product is added successfully";

            Response.Redirect("/Product/Product");
        }
    }
}
 