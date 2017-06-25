using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AI_TEST_01
{
    public class DBController
    {
        private SqlConnection conn;
        public event Action<string> onToShowOnConsole;

        public DBController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
            conn.Open();
        }

        public KeyValuePair<int, string> GetWordStatus(string word)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = string.Format("SELECT * FROM Word WHERE value = '{0}'", word);
            SqlDataReader sdr = command.ExecuteReader();

            if (sdr.Read())
            {
                KeyValuePair<int, string> kvp = new KeyValuePair<int, string>(sdr.GetInt32(0), sdr.GetString(2));
                sdr.Close();
                return kvp;
            }
            else
            {
                sdr.Close();

                command = conn.CreateCommand();
                command.CommandText = string.Format("insert into Word (value, status) values ('{0}', '{1}');", word, "{}");
                command.ExecuteNonQuery();

                command = conn.CreateCommand();
                command.CommandText = string.Format("SELECT id FROM Word WHERE value = '{0}'", word);
                sdr = command.ExecuteReader();

                if (sdr.Read())
                {
                    KeyValuePair<int, string> kvp = new KeyValuePair<int, string>(sdr.GetInt32(0), "{}");
                    sdr.Close();
                    return kvp;
                }
            }

            return new KeyValuePair<int, string>();
        }

        public string GetWordById(int id)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = string.Format("SELECT value FROM Word WHERE id = '{0}'", id);
            SqlDataReader sdr = command.ExecuteReader();

            string result = "";
            if (sdr.Read())
            {
                result = sdr.GetString(0);
            }

            sdr.Close();

            return result;
        }

        public void UpdateWord(int id, string status)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = string.Format("UPDATE Word SET status = '{1}' WHERE id = '{0}'; ", id, status);
            command.ExecuteNonQuery();
        }

        public void PrintAllConversations()
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from Conversation";
            SqlDataReader sdr = command.ExecuteReader();

            string debug = "";
            while (sdr.Read())
            {
                debug += ((string)sdr.GetString(1) + "\n");
            }

            if (onToShowOnConsole != null)
                onToShowOnConsole(debug);

            sdr.Close();
        }

        public bool CheckIfExisits(string s)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "Select * from Conversation";
            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                if (s == (string)sdr.GetString(1))
                {
                    sdr.Close();
                    return true;
                }
            }

            sdr.Close();
            return false;
        }

        public int AddText(string text)
        {
            text = text.Replace("\"", "");
            text = text.Replace("'", "");

            SqlCommand command = conn.CreateCommand();
            command.CommandText = string.Format("insert into Conversation (text) values ('{0}');", text);
            return command.ExecuteNonQuery();
        }
    }
}