using AspMvc.Models.Repository.Interface;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace AspMvc.Models.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly SqlConnection _connection;

        public BookRepository()
        {
            //нужен DI
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        }

        public void Create(Book book)
        {
            string commandStr = "INSERT INTO Books (Title, Description) VALUES (@title, @description)";
            SqlParameter[] parameters =
                { new SqlParameter("@title", book.Title),
                  new SqlParameter("@description", book.Description)};

            Execute(commandStr, parameters);
        }

        public IEnumerable<Book> GetAll()
        {
            string commandStr = "SELECT * FROM Books";
            _connection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(commandStr, _connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Books");
            List<Book> books = new List<Book>(from book in ds.Tables["Books"].AsEnumerable()
                                              select new Book()
                                              {
                                                  Id = (int)book["Id"],
                                                  Title = (string)book["Title"],
                                                  Description = (string)book["Description"]
                                              });
            _connection.Close();

            return books;
        }

        public Book GetById(int id)
        {
            return GetAll().Where(b => b.Id == id).FirstOrDefault();
        }

        public void Update(Book book)
        {
            string commandStr = $"UPDATE Books SET Title=@title, Description=@description WHERE Id=@id";

            SqlParameter[] parameters =
                { new SqlParameter("@id", book.Id),
                  new SqlParameter("@title", book.Title),
                  new SqlParameter("@description", book.Description)};

            Execute(commandStr, parameters);
        }

        private void Execute(string  commandStr, SqlParameter[] parameters)
        {
            _connection.Open();
            var command = new SqlCommand(commandStr, _connection);
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}