using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectSchoolUtils
{
    class Program
    {
        static Dictionary<string, Command> _commands = new Dictionary<string, Command>();
        static Dictionary<int, string> _names = new Dictionary<int, string>();
        static Dictionary<int, string> _prescriptions = new Dictionary<int, string>();


        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine(
                @"
####################################
#                                  #
#  MAIN MENU - ProjectSchoolUtils  #
#                                  #
#                                  #
#  @ Długosz Bartłomiej - 2020     #
#                                  #
####################################
#                                  #
#  Available UDT to work on:       #
#                                  #
# 1. Calculator.                   #
# 2. Glue.                         #
# 3. Notebook.                     #
# 4. Pen.                          #
# 5. Rubber.                       #
# 6. Ruler.                        #
# 7. Scissors.                     #
#                                  #
# 0. Quit application.             #
#                                  #
####################################
                                  
Pick an option (0-7): 
"
            );
        }

        private static void Setup()
        {
            // key - name of UDT type
            _names.Add(1, "Calculator");
            _names.Add(2, "Glue");
            _names.Add(3, "Notebook");
            _names.Add(4, "Pen");
            _names.Add(5, "Rubber");
            _names.Add(6, "Ruler");
            _names.Add(7, "Scissors");

            _prescriptions.Add(1, "\nINSERTING DATA\n\nPrice[PLN]/Brand/Width[cm]/Height[cm]/DigitsDisplayed \nexample data record -----> 12,99/Casio/9/14/9\n\nEnter record: ");
            _prescriptions.Add(2, "\nINSERTING DATA\n\nPrice[PLN]/Radius[cm]/Height[cm] \nexample data record -----> 4,59/2/10\n\nEnter record: ");
            _prescriptions.Add(3, "\nINSERTING DATA\n\nPrice[PLN]/PagesCount/Type(in lines, checkered, clear)/Is hard covered(yes, no) \nexample data record -----> 5,99/60/in lines/B5/yes\n\nEnter record: ");
            _prescriptions.Add(4, "\nINSERTING DATA\n\nPrice[PLN]/Color(blue, green, etc...)/Length[cm]/Is it clickable(yes, no) \nexample data record -----> 3,29/red/12/no\n\nEnter record: ");
            _prescriptions.Add(5, "\nINSERTING DATA\n\nPrice[PLN]/Width[cm]/Thickness[cm]/Height[cm] \nexample data record -----> 2,54/2/2/2\n\nEnter record: ");
            _prescriptions.Add(6, "\nINSERTING DATA\n\nPrice[PLN]/Length[cm]/Color(blue, green, etc...)/Scaling[cm] \nexample data record -----> 4,19/30/pink/0,1\n\nEnter record: ");
            _prescriptions.Add(7, "\nINSERTING DATA\n\nPrice[PLN]/Color(blue, green, etc...)/Quality(medium,good,amazing,excellent) \nexample data record -----> 3,99/blue/excellent\n\nEnter record: ");


            _commands.Add(_names[1] + "_insert", new Command("insert into " + _names[1] + " (" + _names[1].ToLower() + ") values ", Command.Type.Insert, _prescriptions[1], new List<string>() { }));
            _commands.Add(_names[1] + "_select", new Command("select " + _names[1] + ".ToString()", Command.Type.SelectAll, "Fetching data...", new List<string>() { _names[1].ToLower() }));
            _commands.Add(_names[1] + "_select_prices", new Command("select *", Command.Type.SortByPrice, "Fetching data...", new List<string>() { _names[1].ToLower() }));

            _commands.Add(_names[2] + "_insert", new Command("insert into " + _names[2] + " (" + _names[2].ToLower() + ") values ", Command.Type.Insert, _prescriptions[2], new List<string>() { }));
            _commands.Add(_names[2] + "_select", new Command("select " + _names[2] + ".ToString()", Command.Type.SelectAll, "Fetching data...", new List<string>() { _names[2].ToLower() }));
            _commands.Add(_names[2] + "_select_prices", new Command("select *", Command.Type.SortByPrice, "Fetching data...", new List<string>() { _names[2].ToLower() }));


            _commands.Add(_names[3] + "_insert", new Command("insert into " + _names[3] + " (" + _names[3].ToLower() + ") values ", Command.Type.Insert, _prescriptions[3], new List<string>() { }));
            _commands.Add(_names[3] + "_select", new Command("select " + _names[3] + ".ToString()", Command.Type.SelectAll, "Fetching data...", new List<string>() { _names[3].ToLower() }));
            _commands.Add(_names[3] + "_select_prices", new Command("select *", Command.Type.SortByPrice, "Fetching data...", new List<string>() { _names[3].ToLower() }));


            _commands.Add(_names[4] + "_insert", new Command("insert into " + _names[4] + " (" + _names[4].ToLower() + ") values ", Command.Type.Insert, _prescriptions[4], new List<string>() { }));
            _commands.Add(_names[4] + "_select", new Command("select " + _names[4] + ".ToString()", Command.Type.SelectAll, "Fetching data...", new List<string>() { _names[4].ToLower() }));
            _commands.Add(_names[4] + "_select_prices", new Command("select *", Command.Type.SortByPrice, "Fetching data...", new List<string>() { _names[4].ToLower() }));


            _commands.Add(_names[5] + "_insert", new Command("insert into " + _names[5] + " (" + _names[5].ToLower() + ") values ", Command.Type.Insert, _prescriptions[5], new List<string>() { }));
            _commands.Add(_names[5] + "_select", new Command("select " + _names[5] + ".ToString()", Command.Type.SelectAll, "Fetching data...", new List<string>() { _names[5].ToLower() }));
            _commands.Add(_names[5] + "_select_prices", new Command("select *", Command.Type.SortByPrice, "Fetching data...", new List<string>() { _names[5].ToLower() }));


            _commands.Add(_names[6] + "_insert", new Command("insert into " + _names[6] + " (" + _names[6].ToLower() + ") values ", Command.Type.Insert, _prescriptions[6], new List<string>() { }));
            _commands.Add(_names[6] + "_select", new Command("select " + _names[6] + ".ToString()", Command.Type.SelectAll, "Fetching data...", new List<string>() { _names[6].ToLower() }));
            _commands.Add(_names[6] + "_select_prices", new Command("select *", Command.Type.SortByPrice, "Fetching data...", new List<string>() { _names[6].ToLower() }));


            _commands.Add(_names[7] + "_insert", new Command("insert into " + _names[7] + " (" + _names[7].ToLower() + ") values ", Command.Type.Insert, _prescriptions[7], new List<string>() { }));
            _commands.Add(_names[7] + "_select", new Command("select " + _names[7] + ".ToString()", Command.Type.SelectAll, "Fetching data...", new List<string>() { _names[7].ToLower() }));
            _commands.Add(_names[7] + "_select_prices", new Command("select *", Command.Type.SortByPrice, "Fetching data...", new List<string>() { _names[7].ToLower() }));

        }

        static void SendCommand(Command c, int num)
        {
            string sqlconnection = @"DATA SOURCE=MSSQLServer;"
                + "INITIAL CATALOG=SchoolUtilsProject; INTEGRATED SECURITY=SSPI;";

            string sqlcommand = c.GetCommand();
            if (c.GetCommandType().Equals(Command.Type.Insert))
            {
                StringBuilder br = new StringBuilder(c.GetCommand());
                br.Append("('");
                Console.WriteLine(c.GetPrompt());
                br.Append(Console.ReadLine() + "');");
                sqlcommand = br.ToString();

            }

            else if (c.GetCommandType().Equals(Command.Type.SelectAll))
            {

                sqlcommand = c.GetCommand() + " from " + _names[num].ToLower();

                if (sqlcommand == null) return;
            }

            else if (c.GetCommandType().Equals(Command.Type.SortByPrice))
            {

                sqlcommand = c.GetCommand() + " from get_" + _names[num].ToLower() + "_prices() order by price";

                if (sqlcommand == null) return;
            }


            SqlConnection connection = new SqlConnection(sqlconnection);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();

                while (datareader.Read())
                {

                    Console.Write(datareader[0].ToString());


                    Console.Write("\n");
                }
                Console.WriteLine("OPERATION DONE SUCESSFULLY");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("\nError!");
                String mess = ex.Message.Split(new string[] { "Exception: " }, StringSplitOptions.None)[1].Split('\n')[0];
                Console.WriteLine(mess);
            }
            finally { connection.Close(); }
        }

        public static void PrintUtilMenu(int num)
        {

            Console.Clear();
            int pick = -1;
            Console.WriteLine(
@"
####################################" + "\n\t"
+ _names[num] +
"\n####################################"
+
@"
#                                  #
#  Pick an action:                 #
# 1. Insert data.                  #
# 2. Select all data from          # 
#    this table.                   #
# 3. Sort records by price.        #
#                                  #
# 0. Quit application.             #
#                                  #
####################################
                                  
Pick an option (0-3): 
"
            );

            do
            {
                pick = int.Parse(Console.ReadLine());
            }
            while (!(0 <= pick && pick <= 9));



            Command res;
            switch (pick)
            {
                case 1:
                    if (_commands.TryGetValue(_names[num] + "_insert", out res))
                    {
                        SendCommand(res, num);
                        Console.ReadLine();

                    }
                    break;

                case 2:
                    Console.WriteLine("All records: ");
                    if (_commands.TryGetValue(_names[num] + "_select", out res))
                    {
                        SendCommand(res, num);
                        Console.ReadLine();
                    }

                    break;

                case 3:
                    Console.WriteLine("All records sorted by price: ");
                    if (_commands.TryGetValue(_names[num] + "_select_prices", out res))
                    {
                        SendCommand(res, num);
                        Console.ReadLine();
                    }

                    break;

                case 0:
                    System.Environment.Exit(1);
                    break;
                default:
                    Console.Write("\nNieznana opcja!");
                    break;
            }



        }

        static void Main(string[] args)
        {
            Setup();

            int pick = 0;
            bool running = true;
            while (running)
            {


                PrintMenu();
                do
                {
                    pick = int.Parse(Console.ReadLine());

                }
                while (!(0 <= pick && pick <= 7));


                if (pick == 0)
                {
                    running = false;
                    break;
                }
                else
                    PrintUtilMenu(pick);


            }

        }
    }
}
