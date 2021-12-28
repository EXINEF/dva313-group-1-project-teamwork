using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

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
        private double[] tire_ls = new double[4];
        private double[] tire_base_pressure = new double[4];
        private double[] tire_curr_pressure = new double[4];
        private double[] ls = new double[4];
        private string sqlStatement;
        private int nrOfRowsAffected;
        DateTime t2;


        public LifespanCalc()
        {
            //getting id for each tire.
             string query = "SELECT tire_left_front_id FROM tpms_vehicle WHERE id = '123'"; //ADD MODEL ASWELL.
             MySqlDataReader reader = (Database.SendQuery(query)).Result;
             tire_id[0] = reader["id"].ToString();
             query = "SELECT tire_left_rear_id FROM tpms_vehicle WHERE id = '123'"; //ADD MODEL ASWELL.
             reader = (Database.SendQuery(query)).Result;
             tire_id[1] = reader["id"].ToString();
             query = "SELECT tire_right_front_id FROM tpms_vehicle WHERE id = '123'"; //ADD MODEL ASWELL.
             reader = (Database.SendQuery(query)).Result;
             tire_id[2] = reader["id"].ToString();
             query = "SELECT tire_right_rear_id FROM tpms_vehicle WHERE id = '123'"; //ADD MODEL ASWELL.
             reader = (Database.SendQuery(query)).Result;
             tire_id[3] = reader["id"].ToString();
            
            //getting remaining life for each tire.
             query = "SELECT remaining_life FROM tpms_tire WHERE id = '"+ tire_id[0] +"'"; 
             reader = (Database.SendQuery(query)).Result;
             tire_ls[0] = Double.Parse(reader["remaining_life"].ToString());
             query = "SELECT remaining_life FROM tpms_tire WHERE id = '"+ tire_id[1] +"'"; 
             reader = (Database.SendQuery(query)).Result;
             tire_ls[1] = Double.Parse(reader["remaining_life"].ToString());
             query = "SELECT remaining_life FROM tpms_tire WHERE id = '"+ tire_id[2] +"'"; 
             reader = (Database.SendQuery(query)).Result;
             tire_ls[2] = Double.Parse(reader["remaining_life"].ToString());
             query = "SELECT remaining_life FROM tpms_tire WHERE id = '"+ tire_id[3] +"'"; 
             reader = (Database.SendQuery(query)).Result;
             tire_ls[3] = Double.Parse(reader["remaining_life"].ToString());


            //getting base pressur for each tire.
             query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '"+ tire_id[0] +"'"; 
             reader = (Database.SendQuery(query)).Result;
             tire_base_pressure[0] = Double.Parse(reader["baseline_pressure"].ToString());
             query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '"+ tire_id[1] +"'"; 
             reader = (Database.SendQuery(query)).Result;
             tire_base_pressure[1] = Double.Parse(reader["baseline_pressure"].ToString());
             query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '"+ tire_id[2] +"'"; 
             reader = (Database.SendQuery(query)).Result;
             tire_base_pressure[2] = Double.Parse(reader["baseline_pressure"].ToString());
             query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '"+ tire_id[3] +"'"; 
             reader = (Database.SendQuery(query)).Result;
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
            for(int i= 0; i+1 > tire_ls.Length; i++)
            {
                tire_ls[i] = tire_ls[i] + ((t2-t1).Hours * Func(tire_curr_pressure[i]/tire_base_pressure[i])) - ((t2-t1).Hours/tire_ls[i]);
                sqlStatement = "UPDATE tpms_vehicle SET tkph =  "+ tire_ls[i] +" WHERE id = '"+ tire_id[i] +"'";//assuming these is a value for that id.
                nrOfRowsAffected = Database.SendNonQuery(sqlStatement).Result;
            }

            return t2;
        }
    }
}
