using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WebContato.Models
{
    public class ContatoDAL : IContatoDAL
    {
        string connectionString = @"Data Source=DESKTOP-K2EU385\SQLEXPRESS;User ID=sa;Password=Boog@0707;Initial Catalog=CadastroDB;Integrated Security=True;";
        public IEnumerable<Contato> GetAllContatos()
        {
            List<Contato> lstcontato = new List<Contato>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ContatoId, Nome, TelefoneRes, TelefoneCel from Contatos", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Contato contato = new Contato();
                    contato.ContatoId = Convert.ToInt32(rdr["ContatoId"]);
                    contato.Nome = rdr["Nome"].ToString();
                    contato.TelefoneRes = rdr["TelefoneRes"].ToString();
                    contato.TelefoneCel = rdr["TelefoneCel"].ToString();
                    lstcontato.Add(contato);
                }
                con.Close();
            }
            return lstcontato;
        }

        public void AddContato(Contato contato)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into Contatos (Nome,TelefoneRes,TelefoneCel) Values(@Nome, @TelefoneRes, @TelefoneCel)";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", contato.Nome);
                cmd.Parameters.AddWithValue("@TelefoneRes", contato.TelefoneRes);
                cmd.Parameters.AddWithValue("@TelefoneCel", contato.TelefoneCel);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateContato(Contato contato)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Update Contatos set Nome = @Nome, TelefoneRes = @TelefoneRes, TelefoneCel = @TelefoneCel where ContatoId = @ContatoId";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ContatoId", contato.ContatoId);
                cmd.Parameters.AddWithValue("@Nome", contato.Nome);
                cmd.Parameters.AddWithValue("@TelefoneRes", contato.TelefoneRes);
                cmd.Parameters.AddWithValue("@TelefoneCel", contato.TelefoneCel);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Contato GetContato(int? id)
        {
            Contato contato = new Contato();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Contatos WHERE ContatoId= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    contato.ContatoId = Convert.ToInt32(rdr["ContatoId"]);
                    contato.Nome = rdr["Nome"].ToString();
                    contato.TelefoneRes = rdr["TelefoneRes"].ToString();
                    contato.TelefoneCel = rdr["TelefoneCel"].ToString();
                }
            }
            return contato;
        }
        public void DeleteContato(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Delete from Contatos where ContatoId = @ContatoId";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ContatoId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
