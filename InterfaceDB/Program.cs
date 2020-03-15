using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDB
{
    class Program
    {
        static readonly SqlConnection Connect = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);

        static object executeQuery(string query, int option)
        {
            int execute;
            string response = string.Empty;

            if(option == 1)
            {
                SqlCommand command = new SqlCommand(query, Connect);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetValue(i) + " | ");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
            }
            else if(option == 2)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = Connect;
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    execute = command.ExecuteNonQuery();
                }
                if(execute == 0)
                {
                    response = "Failed To Execute Query Successfully";                  
                }
                else 
                {
                    response = "Query Executed Successfully";
                }
            }
            else
            {
                response = "Invalid Option Selected";
            }

            return response;
        }

        static void Main(string[] args)
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["UsePassword"]))
            {
                Console.WriteLine("Welcome User, Please Input password");

                if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["MatchPassword"]))
                {
                    string fixedPassword = System.Configuration.ConfigurationManager.AppSettings["Password"];
                    string inputtedPassword = Console.ReadLine();
                    if (inputtedPassword != fixedPassword)
                    {
                        Console.WriteLine("Invalid Password, Terminating Application Now");
                        Console.ReadKey();
                        return;
                    }
                }
                else
                {
                    string fixedPassword = "ECSTOUNDING123@.INNOVATIVE.COM";
                    string inputtedPassword = Console.ReadLine();
                    if(inputtedPassword != fixedPassword)
                    {
                        Console.WriteLine("Invalid Password, Terminating Application Now");
                        Console.ReadKey();
                        return;
                    }
                }
            }            
            Console.WriteLine("Console App to execute queries on database.");
            Console.WriteLine();
        begin:
            Connect.Close();
            Connect.Open();
            Console.WriteLine("Select Query Option");
            Console.WriteLine("Press 1 for SELECT QUERY");
            Console.WriteLine("Press 2 for Non-SelectQuery");

            string option = Console.ReadLine();
            try
            {
                if (Convert.ToInt32(option) == 1)
                {
                    Console.WriteLine("Input Query String on next line");
                    string query = Console.ReadLine();

                    Console.WriteLine(executeQuery(query, Convert.ToInt32(option)));
                    Connect.Close();
                    Console.WriteLine();
                    Console.WriteLine("Would you like to end program");
                    Console.WriteLine("Type Y for yes AND N for no");
                    Console.WriteLine();
                    ConsoleKeyInfo result = Console.ReadKey();
                    if ((result.KeyChar == 'Y') || (result.KeyChar == 'y'))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Program Ends");
                        Console.ReadKey();
                        return;
                    }
                    else if ((result.KeyChar == 'N') || (result.KeyChar == 'n'))
                    {
                        Console.WriteLine();
                        goto begin;
                    }
                }
                else if(Convert.ToInt32(option) == 2)
                {
                    Console.WriteLine("Input Query String on next line");
                    string query = Console.ReadLine();

                    Console.WriteLine(executeQuery(query, Convert.ToInt32(option)));
                    Connect.Close();
                    Console.WriteLine();
                    Console.WriteLine("Would you like to end program");
                    Console.WriteLine("Type Y for yes AND N for no");
                    Console.WriteLine();
                    ConsoleKeyInfo result = Console.ReadKey();
                    if ((result.KeyChar == 'Y') || (result.KeyChar == 'y'))
                    {
                        Console.WriteLine("Program Ends");
                        Console.ReadKey();
                        return;
                    }
                    else if ((result.KeyChar == 'N') || (result.KeyChar == 'n'))
                    {
                        Console.WriteLine();
                        goto begin;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid Option");
                    Console.WriteLine();
                    Console.WriteLine("Would you like to end program");
                    Console.WriteLine("Type Y for yes AND N for no");
                    Console.WriteLine();
                    ConsoleKeyInfo result = Console.ReadKey();
                    if ((result.KeyChar == 'Y') || (result.KeyChar == 'y'))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Program Ends");
                        Console.ReadKey();
                        return;
                    }
                    else if ((result.KeyChar == 'N') || (result.KeyChar == 'n'))
                    {
                        Console.WriteLine();
                        goto begin;
                    }
                }

            }
            catch(Exception e)
            {
                Console.Write("Error Occured: " + e.Message + " " + e.GetType());
                Console.WriteLine();
                Console.WriteLine("Would you like to end program");
                Console.WriteLine("Type Y for yes AND N for no");
                ConsoleKeyInfo result = Console.ReadKey();
                if ((result.KeyChar == 'Y') || (result.KeyChar == 'y'))
                {
                    Console.WriteLine();
                    Console.WriteLine("Program Ends");
                    Console.ReadKey();
                    return;
                }
                else if ((result.KeyChar == 'N') || (result.KeyChar == 'n'))
                {
                    Console.WriteLine();
                    goto begin;
                }
                goto begin;
            }

        }
    }
}
