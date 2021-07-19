using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalP
{
    public class DBcon
    {
        protected static string baseDatabaseName = string.Empty;
        protected static string baseUserName = string.Empty;
        protected static string basePassword = string.Empty;

        protected static string connString = string.Empty;

        private static DBcon _instance = null;

        private static MySqlConnection connection = null;

        //Constructor
        public static DBcon Instance()
        {
            bool result = true;
            if (_instance == null)
            {
                _instance = new DBcon();

                result = !string.IsNullOrEmpty(baseDatabaseName);

                if (result)
                {
                    connString = string.Format("Server=localhost; database={0}; UID={1}; password={2}", baseDatabaseName, baseUserName, basePassword);
                    connection = new MySqlConnection(connString);
                }
            }
            return _instance;
        }
        //פונקציית התחברות לבסיס הנתונים
        private bool Connect()
        {
            bool result = true;
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
        //סגירת החיבור לבסיס הנתונים
        public void Close()
        {
            connection.Close();
        }
        //פונקציה לביצוע שאילתות פשוטות כמו הוספה עדכון והסרה
        public int ExecuteSimpleQuery(MySqlCommand command)
        {
            int result = 0;

            lock (connection) // Locks the code until the function is ended, we create a kind of queue.
            {
                if (Connect())
                {
                    // First step connect the command with the connection to the database.
                    command.Connection = connection;
                    // Second step 
                    try
                    {
                        result=command.ExecuteNonQuery();
                    }
                    catch (Exception e) // if the command was failed the result will receive false.
                    {
                        result = 0;
                    }
                    finally // very important to close the connection at the end of the method.
                    // even if the command was not successful.
                    {
                        Close();
                    }
                }

            }
            return result;
        }
        //פונקציה לביצוע שאילתות המחזירות מספר שלם כמו min max count sum
        protected int ExecuteScalarIntQuery(MySqlCommand command)
        {
            int result = -1;
            lock (connection)
            {
                if (Connect())
                {
                    command.Connection = connection;
                    try
                    {
                        result = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        result = -1;
                    }
                    finally
                    {
                        Close();
                    }
                }
            }

            return result;
        }



        //פונקציה המקבלת פקודה עם שאילתה ומחזירה לנו dataset
        protected DataSet GetMultipleQuery(MySqlCommand command)
        {
            DataSet dataset = new DataSet();
            lock (connection)
            {
                if (Connect())
                {
                    command.Connection = connection;
                    try
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(dataset);
                    }
                    catch (TypedDataSetGeneratorException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (DataException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        Close();
                    }
                }
            }
            return dataset;
        }
    }
}
