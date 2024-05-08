using Npgsql;
using System.Text;
using Tesztfeladat.Entity.Models;
using Tesztfeladat.Interfaces.Repository;

namespace Tesztfeladat.Repositorys
{
    public class TetelRepository : ITetelRepository
    {
        private readonly string connectionString;

        public TetelRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("PostgreConnectionString");
        }

        public bool Create(Tetel tetel)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO public.\"Tetel\" (\"Nyugta\",\"Mennyises\",\"Mertekegyseg\",\"Ar\",\"Sorszam\", \"Nev\")" +
                    $"VALUES ({tetel.Nyugta},{tetel.Mennyiseg},'{tetel.Mertekegyseg}',{tetel.Ar}, {tetel.Sorszam},'{tetel.Nev}')"))
                {
                    command.Connection = connection;

                    var result = command.ExecuteNonQuery();
                    if (result <=0)
                    {
                        connection.Close();
                        throw new Exception("Can't insert to the Tetel table");
                    }
                }
                connection.Close();
            }
            return true;
        }

        public int CreateMultiple(IEnumerable<Tetel> tetelList)
        {
            int result;
            using (var connection = new NpgsqlConnection(connectionString))
            {
                StringBuilder sql = new StringBuilder("INSERT INTO public.\"Tetel\" (\"Nyugta\",\"Mennyiseg\",\"Mertekegyseg\",\"Ar\",\"Sorszam\",\"Nev\")\n VALUES");
                bool first = true;
                foreach (var tetel in tetelList)
                {
                    if (!first)
                    {
                        sql.Append(",");
                    }
                    else
                    {
                        first = false;
                    }
                    sql.AppendLine($"({tetel.Nyugta},{tetel.Mennyiseg},'{tetel.Mertekegyseg}',{tetel.Ar},{tetel.Sorszam},'{tetel.Nev}')");
                }
                sql.Append(";");

                connection.Open();
                using (var command = new NpgsqlCommand(sql.ToString()))
                {
                    command.Connection = connection;

                    result = command.ExecuteNonQuery();
                    if (result <= 0)
                    {
                        connection.Close();
                        throw new Exception("Can't insert to the Tetel table");
                    }
                }
                connection.Close();
            }
            return result;
        }

        public IEnumerable<Tetel> GetAll()
        {
            List<Tetel> result = new List<Tetel>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM public.\"Tetel\""))
                {
                    command.Connection = connection;

                    using (var reader =  command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = new Tetel();
                            tmp.Azonosito = reader.GetInt32(0);
                            tmp.Nyugta = reader.GetInt32(1);
                            tmp.Mennyiseg = reader.GetInt32(2);
                            tmp.Ar = reader.GetInt32(3);
                            tmp.Sorszam = reader.GetInt32(4);
                            tmp.Mertekegyseg = reader.GetString(5);
                            tmp.Nev = reader.GetString(6);
                            result.Add(tmp);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        public IEnumerable<Tetel> GetByNyugta(int nyugtaszam)
        {
            List<Tetel> result = new List<Tetel>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand($"SELECT * FROM public.\"Tetel\" WHERE \"Nyugta\" = {nyugtaszam}"))
                {
                    command.Connection = connection;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = new Tetel();
                            tmp.Azonosito = reader.GetInt32(0);
                            tmp.Nyugta = reader.GetInt32(1);
                            tmp.Mennyiseg = reader.GetInt32(2);
                            tmp.Ar = reader.GetInt32(3);
                            tmp.Sorszam = reader.GetInt32(4);
                            tmp.Mertekegyseg = reader.GetString(5);
                            tmp.Nev = reader.GetString(6);
                            result.Add(tmp);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
    }
}
