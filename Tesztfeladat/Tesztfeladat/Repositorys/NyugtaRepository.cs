using Tesztfeladat.Entity.Models;
using Npgsql;
using Tesztfeladat.Interfaces.Repository;

namespace Tesztfeladat.Repositorys
{
    public class NyugtaRepository : INyugtaRepository
    {
        private readonly string connectionString;

        public NyugtaRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("PostgreConnectionString");
        }

        public int Create(Nyugta nyugta)
        {
            int ujNyugtaszanm;
            using (var connection = new NpgsqlConnection(connectionString))
            {

                using (var command = new NpgsqlCommand("INSERT INTO tesztfeladat.\"Nyugta\" (\"Letrehozas\",\"Osszeg\")" +
                    "VALUES(@letrehozas,@osszeg)"))
                {
                    command.Parameters.AddWithValue("@letrehozas", NpgsqlTypes.NpgsqlDbType.Timestamp, nyugta.Letrehozas);
                    command.Parameters.AddWithValue("@osszeg", NpgsqlTypes.NpgsqlDbType.Integer, nyugta.Osszeg);
                    command.Connection = connection;

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    if (result <= 0)
                    {
                        connection.Close();
                        throw new Exception("Insert to the Nyugta failed");
                    }
                }

                using (var command = new NpgsqlCommand($"SELECT \"Azonosito\" FROM tesztfeladat.\"Nyugta\" WHERE \"Letrehozas\" = @letrehozas AND \"Osszeg\" = @osszeg"))
                {
                    command.Parameters.AddWithValue("@letrehozas", NpgsqlTypes.NpgsqlDbType.Timestamp, nyugta.Letrehozas);
                    command.Parameters.AddWithValue("@osszeg", NpgsqlTypes.NpgsqlDbType.Integer, nyugta.Osszeg);


                    command.Connection = connection;

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read(); 
                        ujNyugtaszanm = reader.GetInt32(0);
                    }
                }
                connection.Close();
            }
            return ujNyugtaszanm;
        }

        public IEnumerable<Nyugta> GetAll()
        {
            List<Nyugta> list = new List<Nyugta>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT \"Azonosito\",\"Letrehozas\",\"Osszeg\" FROM tesztfeladat.\"Nyugta\"", connection))
                {
                    command.Connection = connection;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = new Nyugta();
                            tmp.Azonosito = reader.GetInt32(0);
                            tmp.Letrehozas = reader.GetDateTime(1);
                            tmp.Osszeg = reader.GetInt32(2);
                            list.Add(tmp);
                        }
                    }
                }

                connection.Close();
            }
            return list;
        }
    }
}
