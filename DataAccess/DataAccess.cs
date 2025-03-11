using ACEBackEnd.Modal;
using Microsoft.Data.SqlClient;

namespace ACEBackEnd.DataAccess
{
    public class DataAccessService
    {
        private readonly IConfiguration _configuration;

        public DataAccessService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DatabaseConnection") ?? "";
        }


        public async Task<List<WeatherType>> GetDataAsync()
        {
            var result = new List<WeatherType>();
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                var query = "SELECT ID, Type FROM WeatherType";  

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new WeatherType
                            {
                                ID = reader.GetInt32(0),
                                Type = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return result;
        }
    }

}
