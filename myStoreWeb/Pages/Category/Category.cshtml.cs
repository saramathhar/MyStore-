using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myStoreWeb.Pages.Category
{
    public class CategoryModel : PageModel
    {
        public List<CategoryInfo> listCategory = new List<CategoryInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Store;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = " SELECT * FROM Client";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CategoryInfo Category = new CategoryInfo();
                               
                                Category.id = "" + reader.GetString(0);
                                Category.name = reader.GetString(1);
                                Category.email = reader.GetString(2);

                                //adding the info to the list
                                listCategory.Add(Category);

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

    public class CategoryInfo
    {
        public string id;
        public string name;
        public string email;
    }
}
