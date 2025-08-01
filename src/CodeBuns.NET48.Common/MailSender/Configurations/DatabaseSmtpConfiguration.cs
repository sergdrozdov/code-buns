using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CodeBuns.NET48.Common
{
    public class DatabaseSmtpConfiguration : ISmtpConfiguration
    {
        private readonly string _connectionString;

        public DatabaseSmtpConfiguration(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SmtpSettings GetSettings()
        {
            var settings = new SmtpSettings();
            var providers = new List<SmtpProvider>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM SmtpProviders", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        providers.Add(new SmtpProvider
                        {
                            Name = reader["Name"].ToString(),
                            Host = reader["Host"].ToString(),
                            Port = int.Parse(reader["Port"].ToString()),
                            EnableSsl = bool.Parse(reader["EnableSsl"].ToString()),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString()
                        });
                    }
                }
            }

            if (providers.Any())
            {
                settings.DefaultSmtpProvider = providers[0].Name;
            }
            else
            {
                // Log an error or set a default provider. E.g., "No SMTP providers found in the database."
                // For simplicity, we can set a default provider name

                settings.DefaultSmtpProvider = "DefaultSmtpProvider";
            }

            settings.SmtpProviders = providers;

            return settings;
        }
    }
}
