using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CopilotApp
{
    class LifespanCalc
    {
        //declaring array where:
        //0 = left front
        //1 = left rear
        //2 = right front
        //3 = right rear
        private string[] tire_id = new string[4];
        private decimal[] tire_ls = new decimal[4];
        private double[] tire_base_pressure = new double[4];
        private double[] tire_curr_pressure = new double[4];
        private double[] ls = new double[4];
        private string sqlStatement;
        //private int nrOfRowsAffected;
        DateTime t2;


        public LifespanCalc()
        {
            //getting id for each tire.
             string query = "SELECT tire_left_front_id FROM tpms_vehicle WHERE id = '"+ MachineData.machineID +"'"; //ADD MODEL ASWELL.
             MySqlDataReader reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_id[0] = reader["tire_left_front_id"].ToString();
             query = "SELECT tire_left_rear_id FROM tpms_vehicle WHERE id = '"+ MachineData.machineID +"'"; //ADD MODEL ASWELL.
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_id[1] = reader["tire_left_rear_id"].ToString();
             query = "SELECT tire_right_front_id FROM tpms_vehicle WHERE id = '"+ MachineData.machineID +"'"; //ADD MODEL ASWELL.
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_id[2] = reader["tire_right_front_id"].ToString();
             query = "SELECT tire_right_rear_id FROM tpms_vehicle WHERE id = '"+ MachineData.machineID +"'"; //ADD MODEL ASWELL.
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_id[3] = reader["tire_right_rear_id"].ToString();
            
            //getting remaining life for each tire. It is also converted into seconds.
             query = "SELECT remaining_life FROM tpms_tire WHERE id = '"+ tire_id[0] +"'"; 
             reader = (Database.SendQuery(query));
            while(reader == null)
            {
                reader = (Database.SendQuery(query));
            }
             reader.Read();
             tire_ls[0] = Decimal.Parse(reader["remaining_life"].ToString())*365*24*60*60;
             query = "SELECT remaining_life FROM tpms_tire WHERE id = '"+ tire_id[1] +"'"; 
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_ls[1] = Decimal.Parse(reader["remaining_life"].ToString()) * 365 * 24 * 60*60;
             query = "SELECT remaining_life FROM tpms_tire WHERE id = '"+ tire_id[2] +"'"; 
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_ls[2] = Decimal.Parse(reader["remaining_life"].ToString()) * 365 * 24 * 60*60; 
             query = "SELECT remaining_life FROM tpms_tire WHERE id = '"+ tire_id[3] +"'"; 
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_ls[3] = Decimal.Parse(reader["remaining_life"].ToString()) * 365 * 24 * 60*60;


            //getting base pressur for each tire.
             query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '"+ tire_id[0] +"'"; 
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_base_pressure[0] = Double.Parse(reader["baseline_pressure"].ToString());
             query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '"+ tire_id[1] +"'"; 
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_base_pressure[1] = Double.Parse(reader["baseline_pressure"].ToString());
             query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '"+ tire_id[2] +"'"; 
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_base_pressure[2] = Double.Parse(reader["baseline_pressure"].ToString());
             query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '"+ tire_id[3] +"'"; 
             reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             tire_base_pressure[3] = Double.Parse(reader["baseline_pressure"].ToString());

            
      

        }


       


        private double Func(double x)
        {
            //This formula is based of using non linear regression and curve fit it into a polynomial function. This takes the precentage lost from base pressure and 
            //returns the precentage lost from lifespan.
            return Math.Pow(x, 4) + 3.743 * Math.Pow(x, 3) - 3.882 * Math.Pow(x, 2) - 0.2472 * x + 4.436 * Math.Pow(10, -5);  
        }

        public DateTime CalcL(DateTime t1)
        {
            //t1 is the previous time,  t2 todays time.
            /*SELECT base*/
            //sql take base pressure from all 4 wheels.
           // precentage = frontRightTireBaselinePressure/baselinepressure

            /*IF NOT EXISTS(select* __)
                begin
                    
                end
         */
            /* LSn = LSn-1  + ∆ t * f(x) - ∆t/(LSn-1)  */
            
            //sets todays datetime.
            t2 = DateTime.Now;
            tire_curr_pressure[0] = SensorData.frontLeftSensorPressure;
            tire_curr_pressure[1] = SensorData.rearLeftSensorPressure;
            tire_curr_pressure[2] = SensorData.frontRightSensorPressure;
            tire_curr_pressure[3] = SensorData.rearRightSensorPressure;
            
               
            //assuming the remaining life in the database is measured in hours.
            for(int i= 0; i < tire_ls.Length; i++)
            {
                
                tire_ls[i] = (decimal)tire_ls[i] + ((((decimal)(t2-t1).Seconds) * (decimal)Func(tire_curr_pressure[i]/tire_base_pressure[i])) - ((((decimal)(t2-t1).Seconds))) /(decimal)tire_ls[i]);
                //if tire life is below 0 then we just update the column with 0.
               
                if (tire_ls[i] < 0)
                {
                     sqlStatement = "UPDATE tmps_tire SET remaining_life = 0 WHERE id = '"+ tire_id[i] +"'";//assuming these is already a value in that column.
                }
                else
                {
                    decimal new_val = (tire_ls[i] / (365 * 24 * 60 * 60));
                     sqlStatement = "UPDATE tpms_tire SET remaining_life = "+ new_val +" WHERE id = '"+ tire_id[i] +"'";//assuming these is already a value in that column.
                }
                
               int nrOfRowsAffected = Database.SendNonQuery(sqlStatement);
            }

            return t2;
        }
    }
}
