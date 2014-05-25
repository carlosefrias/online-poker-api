using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finisar.SQLite;

namespace PokerAPI.database
{
    public class SQLiteDatabase
    {
        public static void initDB()
        {
            // We use these three SQLite objects:
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=PokerAPIdatabase.db;Version=3;New=False;Compress=True;");

            // open the connection:
            sqlite_conn.Open();

            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "DROP TABLE Game;";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "CREATE TABLE Game (ID integer primary key, Time varchar(100), StartValue INT, EndValue INT, EarnedValue INT, NumRaises INT);";
            sqlite_cmd.ExecuteNonQuery();

            sqlite_conn.Close();

        }
        public static void InsertGame(GameStats gs)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;

            // create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=PokerAPIdatabase.db;Version=3;New=False;Compress=True;");

            // open the connection:
            sqlite_conn.Open();

            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Game (Time, StartValue, EndValue, EarnedValue, NumRaises) VALUES("
                                        + "'" + gs.getTime() + "', " 
                                        + gs.getStartValue() + ", "
                                        + gs.getEndValue() + ", "
                                        + gs.getEarnedValue() + ", "
                                        + gs.getNumRaises() + 
                                        ");";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }
        public static List<GameStats> getALLGameRecords() 
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            sqlite_conn = new SQLiteConnection("Data Source=PokerAPIdatabase.db;Version=3;New=False;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "select * from Game;";
            sqlite_cmd.ExecuteReader();
            
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<GameStats> list = new List<GameStats>();
            while (sqlite_datareader.Read())
            {
                string time = sqlite_datareader.GetString(sqlite_datareader.GetOrdinal("Time"));
                int startValue = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("StartValue"));
                int endValue = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("EndValue"));
                int earnedValue = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("EarnedValue"));
                int numRaises = sqlite_datareader.GetInt32(sqlite_datareader.GetOrdinal("NumRaises"));

                GameStats gs = new GameStats(time, startValue, endValue, earnedValue, numRaises);
                list.Add(gs);
            }
            sqlite_datareader.Close();
            sqlite_conn.Close();
            return list;
        }
    }
}
