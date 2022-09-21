using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace customer.Pages.Category
{
    public class CategoryModel : PageModel
    {

        public List<CategoryInfo> listCategory = new List<CategoryInfo>();
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
                                CategoryInfo Category = new CategoryInfo();

                                Category.name = reader.GetString(0);
                                Category.email = reader.GetString(1);
                              

                                //adding the data I stored in the object in to the list
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

        public string name;
        public string email;
        public string price;
        public string description;

    }
}

