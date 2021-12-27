using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CopilotApp
{
    public partial class TKPHCalculations
    {
        //private static Dictionary<string, double> k1Values = new Dictionary<string, double>(); <-- Exists in TKPHCalculations.cs
        private static string k1FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "k1values.csv");
        
        public static async Task LoadK1Data()
        {
            //Check if file exists
            if (File.Exists(k1FilePath))
            { 
                //Load data from file
                var fileReader = new StreamReader(k1FilePath);
                while (!fileReader.EndOfStream)
                {
                    var line = fileReader.ReadLine();

                    //Skip empty lines
                    if (line == null)
                    {
                        continue;
                    }

                    // split line, eg.: "1, 1.05"  =>  values[0] = 1, values[1] = 1.05
                    var values = line.Split(',');

                    string distance = values[0];
                    double value = double.Parse(values[1]);

                    k1Values.Add(distance, value);
                }

                fileReader.Close();
            }
            else
            {
                //If no file exists use these hard values(should only happen the first run of the app).
                k1Values.Add("1", 1.0);
                k1Values.Add("2", 1.0);
                k1Values.Add("3", 1.0);
                k1Values.Add("4", 1.0);
                k1Values.Add("5", 1.0);
                k1Values.Add("6", 1.04);
                k1Values.Add("7", 1.06);
                k1Values.Add("8", 1.09);
                k1Values.Add("9", 1.10);
                k1Values.Add("10", 1.12);
                k1Values.Add("11", 1.13);
                k1Values.Add("12", 1.14);
                k1Values.Add("13", 1.15);
                k1Values.Add("14", 1.16);
                k1Values.Add("15", 1.16);
                k1Values.Add("16", 1.17);
                k1Values.Add("17", 1.17);
                k1Values.Add("18", 1.18);
                k1Values.Add("19", 1.18);
                k1Values.Add("20", 1.19);
                k1Values.Add("21", 1.19);
                k1Values.Add("22", 1.19);
                k1Values.Add("23", 1.20);
                k1Values.Add("24", 1.20);
                k1Values.Add("25", 1.20);
                k1Values.Add("26", 1.20);
                k1Values.Add("27", 1.21);
                k1Values.Add("28", 1.21);
                k1Values.Add("29", 1.21);
                k1Values.Add("30", 1.21);
                k1Values.Add("31", 1.21);
                k1Values.Add("32", 1.21);
                k1Values.Add("33", 1.22);
                k1Values.Add("34", 1.22);
                k1Values.Add("35", 1.22);
                k1Values.Add("36", 1.22);
                k1Values.Add("37", 1.22);
                k1Values.Add("38", 1.22);
                k1Values.Add("39", 1.22);
                k1Values.Add("40", 1.22);
                k1Values.Add("41", 1.23);
                k1Values.Add("42", 1.23);
                k1Values.Add("43", 1.23);
                k1Values.Add("44", 1.23);
                k1Values.Add("45", 1.23);
                k1Values.Add("46", 1.23);
                k1Values.Add("47", 1.23);
                k1Values.Add("48", 1.23);
                k1Values.Add("49", 1.23);
                k1Values.Add("50", 1.23);
            }

            /***************************************************
             * Check for new values on the database and update *
             ***************************************************/

            //Prep SQL Query
            string SQLQuery = "SELECT distance, value FROM tpms_k1";

            //Send Query and get the results into the reader
            
            MySqlDataReader reader = Database.SendQuery(SQLQuery);

            //Extract the SQL data row by row
            while (reader.Read())
            {
                string distance = reader["distance"].ToString();
                double value = double.Parse(reader["value"].ToString());

                Console.WriteLine(distance + " : " + value.ToString());

                //If key (k1Values[distance]) is already in the dictionary, update the value for the key, otherwise add new entry
                if (k1Values.ContainsKey(distance))
                {
                    k1Values[distance] = value;
                }
                else
                {
                    k1Values.Add(distance, value);
                }
            }

            //Save the dictionary to a file, comma separeted:   | key, value  |
            //                                                  |_____________|
            //                                                  |   5, 1.0    |
            //                                                  |   6, 1.04   |
            //                                                  |   7, 1.04   |
            //                                                  |   8, 1.06   | 
            StreamWriter fileWriter = new StreamWriter(File.Create(k1FilePath));

            foreach (var entry in k1Values)
            {
                string line = entry.Key + "," + entry.Value.ToString();
                fileWriter.WriteLine(line);
            }

            fileWriter.Close();


            await Task.CompletedTask;
        }

    }
}
