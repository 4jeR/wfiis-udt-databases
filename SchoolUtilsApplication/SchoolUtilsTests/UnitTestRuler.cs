using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;


namespace TestSchoolUtils
{
    /// <summary>
    /// Summary description for UnitTestRuler
    /// </summary>
    [TestClass]
    public class UnitTestRuler
    {
        static string sqlconnection = @"DATA SOURCE=MSSQLServer;"
                 + "INITIAL CATALOG=SchoolUtilsProject; INTEGRATED SECURITY=SSPI;";


        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "create table TestRuler ( ruler dbo.Ruler);"
                              + "insert into TestRuler (ruler) values ('3,19/20/blue/0,1');";
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
            String sqlcommand = "DROP TABLE TestRuler;";
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
        public void TestRulerToString()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Ruler.ToString() as test_description from TestRuler";
            String expected = "Ruler-> Price: 3,19 PLN, length: 20cm, color: blue, scale: 0,1cm ";
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
        public void TestRulerGetPrice()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select ruler.GetPrice() as test_price from TestRuler";
            Double expected = 3.19;
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
        public void TestRulerGetLength()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select ruler.GetLength() as test_length from TestRuler";
            Int32 expected = 20;
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
        public void TestRulerGetColor()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select ruler.GetColor() as test_color from TestRuler";
            String expected = "blue";
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
        public void TestRulerGetScale()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select ruler.GetScale() as test_scale from TestRuler";
            Double expected = 0.1;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["test_scale"]);
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
