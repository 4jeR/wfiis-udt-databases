using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace TestSchoolUtils
{
    /// <summary>
    /// Summary description for UnitTestPen
    /// </summary>
    [TestClass]
    public class UnitTestPen
    {
        static string sqlconnection = @"DATA SOURCE=MSSQLServer;"
                + "INITIAL CATALOG=SchoolUtilsProject; INTEGRATED SECURITY=SSPI;";


        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "create table TestPen ( pen dbo.Pen);"
                              + "insert into TestPen (pen) values ('1,99/black/12/yes');";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "DROP TABLE TestPen;";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }



        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void TestPenToString()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Pen.ToString() as test_description from TestPen";
            String expected = "Pen-> Price: 1,99 PLN, length: 12cm, color: black, clickable: yes";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["test_description"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }


        [TestMethod]
        public void TestPenGetPrice()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select pen.GetPrice() as test_price from TestPen";
            Double expected = 1.99;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["test_price"]);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestPenGetColor()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select pen.GetColor() as test_color from TestPen";
            String expected = "black";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["test_color"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestPenGetLength()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select pen.GetLength() as test_length from TestPen";
            Int32 expected = 12;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["test_length"]);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestPenGetClickable()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select pen.GetClickable() as test_clickable from TestPen";
            Boolean expected = true;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["test_clickable"]);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }
    }
}
