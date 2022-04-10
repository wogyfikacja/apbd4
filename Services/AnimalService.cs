using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw4.Models;
using Microsoft.Extensions.Configuration;

namespace cw4.Services
{
    public class AnimalService : IAnimalService
    {
        private string connectionString = "Server=localhost,1433; Database=Animals; User Id=SA; Password=<6o5vwbap9t6v>"; // done on docker so this must be changed to valid connection string

        private readonly IConfiguration _configuration;

        public AnimalService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int AddAnimal(Animal animal)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString(connectionString))) 
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "insert into animal (idanimal, name, description, category, area) values (@1, @2, @3, @4, @5)";
                command.Parameters.AddWithValue("@1", animal.IdAnimal);
                command.Parameters.AddWithValue("@2", animal.Name);
                command.Parameters.AddWithValue("@3", animal.Description);
                command.Parameters.AddWithValue("@4", animal.Category);
                command.Parameters.AddWithValue("@5", animal.Area);
                connection.Open();

                return command.ExecuteNonQuery();
            }
        }

        public List<Animal> GetAnimals(string orderBy)
        {
            var result = new List<Animal>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString(connectionString)))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"Select * from animal order by {orderBy}";
                connection.Open();

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var animal = new Animal
                    {
                        IdAnimal = int.Parse(dataReader["idanimal"].ToString()),
                        Name = dataReader["name"].ToString(),
                        Description = dataReader["description"].ToString(),
                        Category = dataReader["category"].ToString(),
                        Area = dataReader["area"].ToString(),
                    };
                    result.Add(animal);
                }
            }

            return result;
        }
        public int UpdateAnimal(Animal animal, int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString(connectionString)))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "update animal set name = @1, description = @2, category = @3, area = @4 where idanimal = @5";
                command.Parameters.AddWithValue("@1", animal.Name);
                command.Parameters.AddWithValue("@2", animal.Description);
                command.Parameters.AddWithValue("@3", animal.Category);
                command.Parameters.AddWithValue("@4", animal.Area);
                command.Parameters.AddWithValue("@5", id);
                connection.Open();

                return command.ExecuteNonQuery();
            }
        }
        public int DeleteAnimal(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString(connectionString)))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "delete from animal where idanimal = @1";
                command.Parameters.AddWithValue("@1", id);
                connection.Open();

                return command.ExecuteNonQuery();
            }
        }
    }
}