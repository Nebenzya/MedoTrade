using AspMvc.Models.Repository.Interface;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;

namespace AspMvc.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository()
        {
            //нужен DI
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        }

        public void Create(User user)
        {
            string commandStr = "INSERT INTO Users (Name, Phone) VALUES (@name, @phone)";
            SqlParameter[] parameters =
                { new SqlParameter("@name", user.Name),
                  new SqlParameter("@phone", user.Phone)};

            _connection.Open();

            SqlCommand command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public User GetById(int id)
        {
            string commandStr = $"SELECT * FROM Users WHERE Id = {id}";
            _connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(commandStr, _connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Books");
            User user = (from book in ds.Tables["Books"].AsEnumerable()
                         select new User()
                         {
                             Id = (int)book["Id"],
                             Name = (string)book["Name"],
                             Phone = (string)book["Phone"]
                         }).FirstOrDefault();

            _connection.Close();

            return user;
        }
    }
}