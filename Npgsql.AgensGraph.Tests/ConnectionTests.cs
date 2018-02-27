using System;
using Xunit;

namespace Npgsql.AgensGraph.Tests
{
    public class ConnectionTests
    {
        const string ConnectionString = "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=testdb;";
        [Fact]
        public void CanExecuteSimpleTest()
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            using (var command = new NpgsqlCommand("match (p:Person) return p;", connection))
            {
                connection.Open();
                connection.TypeMapper.UseAgensDb();
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    var person = (Vertex)reader[0];
                    Assert.NotNull(person);
                }
            }
        }
    }
}
