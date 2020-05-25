using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace TestSchoolUtils
{
    /// <summary>
    /// Summary description for UnitTestScissors
    /// </summary>
    [TestClass]
    public class UnitTestScissors
    {
        static string sqlconnection = @"DATA SOURCE=MSSQLServer;"
                 + "INITIAL CATALOG=SchoolUtilsProject; INTEGRATED SECURITY=SSPI;";


        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "create table TestScissors ( scissors dbo.Scissors);"
                              + "insert into TestScissors (scissors) values ('5,49/green/amazing');";
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
            String sqlcommand = "DROP TABLE TestScissors;";
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
        public void TestScissorsToString()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Scissors.ToString() as test_description from TestScissors";
            String expected = "Scissors-> Price: 5,49 PLN, color: green, quality: amazing";
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
        public void TestScissorsGetPrice()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select scissors.GetPrice() as test_price from TestScissors";
            Double expected = 5.49;
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
        public void TestScissorsGetColor()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select scissors.GetColor() as test_color from TestScissors";
            String expected = "green";
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
        public void TestScissorsGetQuality()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select scissors.GetQuality() as test_quality from TestScissors";
            String expected = "amazing";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["test_quality"].ToString());
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
