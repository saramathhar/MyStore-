using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myStoreWeb.Pages.Category
{
    public class IndexModel : PageModel
    {
        public CategoryInfo Category = new CategoryInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {


            Category.name = Request.Form["name"];
            Category.email = Request.Form["description"];
       
            if (Category.name.Length == 0 || Category.email.Length == 0 )
            {
                errorMsg = "All the fields must be filled !";
                    return;
            }
            // save the new category data to the data base
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Store;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open( );
                    String sql = "insert into Category" + "(name,email) values" + "(@name,@description);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        
                        command.Parameters.AddWithValue("@name", Category.name);
                        command.Parameters.AddWithValue("@description", Category.email);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg=ex.Message;
                return;

            }


   
            Category.name = "";
            Category.email = "";

            successMsg = "New Category is added successfully";

            Response.Redirect("/Category/Category");
        }
    }
}
 