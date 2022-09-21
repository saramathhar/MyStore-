using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myStoreWeb.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientsInfo> listClients = new List<ClientsInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myStoreWeb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = " SELECT * FROM Client";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientsInfo client = new ClientsInfo();
                                client.id = "" + reader.GetInt32(0);
                                client.name = reader.GetString(1);
                                client.email = reader.GetString(2);
                                client.password = reader.GetString(3);
                                client.phone = reader.GetString(4);
                                client.address = reader.GetString(5);

                                //adding the info to the list
                                listClients.Add(client);

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

    public class ClientsInfo
    {
        public string id;
        public string name;
        public string email;
        public string password;
        public string phone;
        public string address;
    }
}
