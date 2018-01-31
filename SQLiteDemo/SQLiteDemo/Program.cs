using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo
{
    class Program
    {
        static string conString = @"data source=DB\QuanLyNhanVien.db";
        static SQLiteConnection con;
        static SQLiteCommand cmd;
        static SQLiteTransaction tran;
        static SQLiteDataAdapter da;

        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++) { InsertEmployee(); }
            GetEmployees();

            Console.ReadLine();
        }

        static void GetEmployees()
        {
            DataTable dt = new DataTable();
            try
            {
                con = new SQLiteConnection(conString);
                con.Open();
                cmd = new SQLiteCommand("select * from Employees;", con);
                da = new SQLiteDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch { }

            Output(dt);
        }

        static void InsertEmployee()
        {
            try
            {
                int id = GetKeys("Employees");
                if (id == 0) return;

                con = new SQLiteConnection(conString);
                con.Open();
                tran = con.BeginTransaction();
                cmd = new SQLiteCommand(con);
                cmd.CommandText = "";
                cmd.CommandText += string.Format("insert into Employees values({0}, \"{1}\", \"{2}\");", id++, "EMP0001", "Employee 0001");
                cmd.CommandText += string.Format("insert into Employees values({0}, \"{1}\", \"{2}\");", id++, "EMP0002", "Employee 0002");
                cmd.CommandText += string.Format("update KeyManagements set NumberOfKeys={0} where TableName = \"{1}\";", id, "Employees");
                cmd.ExecuteNonQuery();
                tran.Commit();
                con.Close();
            }
            catch (Exception ex) { tran.Rollback(); }
        }

        static void UpdateEmployee(int KeyID, string Code, string Name)
        {
            try
            {
                con = new SQLiteConnection(conString);
                con.Open();
                tran = con.BeginTransaction();
                cmd = new SQLiteCommand(con);
                cmd.CommandText = "";
                cmd.CommandText += string.Format("update Employees set Code=\"{0}\", Name=\"{1}\" where KeyID={2};", Code, Name, KeyID);
                cmd.ExecuteNonQuery();
                tran.Commit();
                con.Close();
            }
            catch (Exception ex) { tran.Rollback(); }
        }

        static int GetKeys(string tableName)
        {
            try
            {
                DataTable dt = new DataTable();
                con = new SQLiteConnection(conString);
                con.Open();
                cmd = new SQLiteCommand(string.Format("select * from KeyManagements where TableName = \"{0}\" limit 1;", tableName), con);
                da = new SQLiteDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                int id = 0;
                int.TryParse(dt.Rows[0]["NumberOfKeys"].ToString(), out id);
                return id + 1;
            }
            catch { return 0; }
        }

        static void Output(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                Console.WriteLine("No result loaded");
            else
            {
                int numOfColumns = dt.Columns.Count;
                int numOfRows = dt.Rows.Count;

                for (int i = 0; i < numOfRows; i++)
                {
                    Console.WriteLine("Row {0}:", i + 1);

                    for (int j = 0; j < numOfColumns; j++)
                    {
                        Console.WriteLine("     \"{0}\" = \"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                    }
                }
            }
        }
    }
}
