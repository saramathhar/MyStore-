using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace myStoreWeb.Pages.Category
{
    public class EditModel : PageModel
    {
        public CategoryInfo Category = new CategoryInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
            // this method to see the data of the current category
         
            //to read the id of the category
            String id = Request.Query["id"];


            //to connect to the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Store;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //to bring the required record from the table in DB
                    String sql = "select * from Category where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                      using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                              
                                Category.id = reader.GetString(0);
                                Category.name = reader.GetString(1);
                                Category.description =reader.GetString(2);
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
            // to update the data on the category
            Category.id = Request.Form["id"];
            Category.name = Request.Form["name"];
            Category.description = Request.Form["description"];

            if (Category.name.Length == 0 || Category.description.Length == 0)
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
                    String sql = "Update Category set name =@name, description=@description where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", Category.id);
                        command.Parameters.AddWithValue("@name", Category.name);
                        command.Parameters.AddWithValue("@description", Category.description);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;

            }

            Response.Redirect("/Category/Category");

        }
    }
}
