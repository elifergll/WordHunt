using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WordHunt.Data;

namespace WordHunt.Services
{
    public class MainService
    {
        private readonly IConfiguration configuration;

        public MainService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<WordInfo> GetWordAsync(int id)
        {
            WordInfo word = new WordInfo();
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("WordConnection"));

            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetWordByID";
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });

            SqlDataReader wordRdr = await cmd.ExecuteReaderAsync();
            if (wordRdr.HasRows)
            {
                wordRdr.Read();
                word.Id = Convert.ToInt32(wordRdr["ID"]);
                word.Word = wordRdr["WORD"].ToString();
                word.Translation = wordRdr["TRANSLATION"].ToString();
            }
            return word;
        }
        public async Task<List<Question>> GetQuestionAsync()
        {
            List<Question> questions = new List<Question>();
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("WordConnection"));

            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetQuestion";
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();

            SqlDataReader questionRdr = await cmd.ExecuteReaderAsync();
            if (questionRdr.HasRows)
            {
                while (questionRdr.Read())
                {
                    Question question = new Question();
                    question.Id = Convert.ToInt32(questionRdr["ID"]);
                    question.Questions = questionRdr["QUESTIONS"].ToString();
                    question.Options = questionRdr["OPTIONS"].ToString();
                    questions.Add(question);
                }
            }
            return questions;
        }
    }
}
