using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Npgsql;
using Tesztfeladat.Entity.Models;
using Tesztfeladat.Interfaces.Repository;

namespace Tesztfeladat.Repositorys
{
    public class FelhasznaloRepository : IdentityDbContext<Entity.DTOs.Felhasznalo>, IFelhasznaloRepository
    {
        private readonly string connectionString;

        public FelhasznaloRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("PostgreConnectionString");
        }

        public bool Create(Felhasznalo felhasznalo)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO tesztfeladat.\"Felhasznalo\" (\"Felhasznalonev\",\"Email\",\"Jelszo\")" +
                $"VALUES ('{felhasznalo.UserName}','{felhasznalo.Email}','{felhasznalo.Jelszo}')"))
                {
                    command.Connection = connection;
                    var result = command.ExecuteNonQuery();
                    if (result <= 0)
                    {
                        connection.Close();
                        throw new Exception("Can't insert to the Felhasznalo table");
                    }
                }
                connection.Close();
            }
            return true;
        }

        public bool Bejelentkezes(string felhasznalonev, string jelszo)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand($"SELECT * FROM tesztfeladat.\"Felhasznalo\" WHERE \"Felhasznalonev\" = '{felhasznalonev}' AND \"Jelszo\" = '{jelszo}'"))
                {
                    command.Connection = connection;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            connection.Close();
                            return true;
                        }
                        connection.Close();
                        return false;
                    }
                }
            }
        }

        public bool Kereses(string felhasznalonev)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand($"SELECT * FROM tesztfeladat.\"Felhasznalo\" WHERE \"Felhasznalonev\" = '{felhasznalonev}'"))
                {
                    command.Connection = connection;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            connection.Close();
                            return true;
                        }
                        connection.Close();
                        return false;
                    }
                }
            }
        }
    }
}
