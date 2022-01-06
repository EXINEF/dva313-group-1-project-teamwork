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
        private decimal[] tire_base_pressure = new decimal[4];
        private decimal[] tire_curr_pressure = new decimal[4];
        private string sqlStatement;
        private decimal t = 0;
         private int cov_to_sec= 0;
        private decimal p = 0;
        //private int nrOfRowsAffected;
        DateTime t2;

       /* if (reader["distance_driven_empty"] != DBNull.Value)*/

        public LifespanCalc()
        {

            string query = "SELECT tire_left_front_id,tire_left_rear_id,tire_right_front_id,tire_right_rear_id FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'"; //ADD MODEL ASWELL.
            MySqlDataReader reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_id[0] = reader["tire_left_front_id"].ToString();
            tire_id[1] = reader["tire_left_rear_id"].ToString();
            tire_id[2] = reader["tire_right_front_id"].ToString();
            tire_id[3] = reader["tire_right_rear_id"].ToString();
            /*
            //sets todays datetime.
            //getting id for each tire.
            query = "SELECT tire_left_front_id FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'"; //ADD MODEL ASWELL.
           // MySqlDataReader reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_id[0] = reader["tire_left_front_id"].ToString();

            query = "SELECT tire_left_rear_id FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'"; //ADD MODEL ASWELL.
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_id[1] = reader["tire_left_rear_id"].ToString();

            query = "SELECT tire_right_front_id FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'"; //ADD MODEL ASWELL.
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_id[2] = reader["tire_right_front_id"].ToString();

            query = "SELECT tire_right_rear_id FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'"; //ADD MODEL ASWELL.
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_id[3] = reader["tire_right_rear_id"].ToString();*/

            //getting remaining life for each tire. It is also converted into seconds.
            query = "SELECT remaining_life FROM tpms_tire WHERE id = '" + tire_id[0] + "'";
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_ls[0] = Decimal.Parse(reader["remaining_life"].ToString()) * 365 * 24 * 60 * 60;

            query = "SELECT remaining_life FROM tpms_tire WHERE id = '" + tire_id[1] + "'";
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_ls[1] = Decimal.Parse(reader["remaining_life"].ToString()) * 365 * 24 * 60 * 60;

            query = "SELECT remaining_life FROM tpms_tire WHERE id = '" + tire_id[2] + "'";
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_ls[2] = Decimal.Parse(reader["remaining_life"].ToString()) * 365 * 24 * 60 * 60;

            query = "SELECT remaining_life FROM tpms_tire WHERE id = '" + tire_id[3] + "'";
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_ls[3] = Decimal.Parse(reader["remaining_life"].ToString()) * 365 * 24 * 60 * 60;


            //getting base pressure for each tire.
            query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '" + tire_id[0] + "'";
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_base_pressure[0] = Decimal.Parse(reader["baseline_pressure"].ToString());
            query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '" + tire_id[1] + "'";
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_base_pressure[1] = Decimal.Parse(reader["baseline_pressure"].ToString());
            query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '" + tire_id[2] + "'";
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_base_pressure[2] = Decimal.Parse(reader["baseline_pressure"].ToString());
            query = "SELECT baseline_pressure FROM tpms_tire WHERE id = '" + tire_id[3] + "'";
            reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            tire_base_pressure[3] = Decimal.Parse(reader["baseline_pressure"].ToString());



        }





        private decimal Func(decimal x)
        {
            //This formula is based of using non linear regression and curve fit it into a polynomial function. This takes the precentage lost from base pressure and 
            //returns the precentage lost from lifespan. It is designed from the data given.  
            //This calculation is not fitted for when x < -30 and x > 50. 
            decimal x7 = Decimal.Multiply((decimal)Math.Pow((double)x, 7), Decimal.Multiply((decimal)Math.Pow(10, -10), (decimal)-7.9365079369092));
            decimal x6 = Decimal.Multiply((decimal)Math.Pow((double)x, 6), Decimal.Multiply((decimal)Math.Pow(10, -7), (decimal)1.2301587301849));
            decimal x5 = Decimal.Multiply((decimal)Math.Pow((double)x, 5), Decimal.Multiply((decimal)Math.Pow(10, -6), (decimal)-6.3888888888529));
            decimal x4 = Decimal.Multiply((decimal)Math.Pow((double)x, 4), Decimal.Multiply((decimal)Math.Pow(10, -5), (decimal)7.0634920631109));
            decimal x3 = Decimal.Multiply((decimal)Math.Pow((double)x, 3), (decimal)0.0036706349206332);
            decimal x2 = Decimal.Multiply((decimal)Math.Pow((double)x, 2),(decimal)-0.12829365079234);
            decimal x1 = Decimal.Multiply((decimal)x, (decimal)0.49761904761878);
            decimal constant = Decimal.Multiply((decimal)Math.Pow(10, -11), (decimal)-5.5510776531705);
            decimal val = x7+x6+x5+x4 + x3 + x2 + x1 + constant;
            //at x = -50 the function starts to increase so we have to set it to negative, as mentioned before the values will not be resonable.
            if (x == 0 || x < -50 || val > 0)
            { 
                val = val *-1;
            }
            val = val/100;
            return val;
        }

        public void CalcL()
        {
           
         
            /* LSn = LSn-1  + ∆ t * f(x) - ∆t/(LSn-1)  */

          

                t2 = DateTime.Now;
            tire_curr_pressure[0] = (decimal)SensorData.frontLeftSensorPressure;
            tire_curr_pressure[1] = (decimal)SensorData.rearLeftSensorPressure;
            tire_curr_pressure[2] = (decimal)SensorData.frontRightSensorPressure;
            tire_curr_pressure[3] = (decimal)SensorData.rearRightSensorPressure;
            /*tire_curr_pressure[0] = 475;
            tire_curr_pressure[1] = 475;
            tire_curr_pressure[2] = 475;
            tire_curr_pressure[3] = 475;*/
            cov_to_sec = 60 * 60;
            t=Decimal.Multiply((decimal)MachineBusData.machineHoursEmpty+(decimal)MachineBusData.machineHoursLoaded,(decimal)cov_to_sec);
               
            
            for(int i= 0; i < tire_ls.Length; i++)
            {

                p = tire_curr_pressure[i] / tire_base_pressure[i];
                
                if(p > 1)
                {
                    p = (p - 1)*100;
                }
                else
                {
                    p = p * -100;
                   
                }
                if (tire_ls[i] != 0)
                {
                    tire_ls[i] = tire_ls[i] + Decimal.Multiply(t, Func(p)) - (t / tire_ls[i]);
                }
                //tire_ls[i] = tire_ls[i] + Decimal.Multiply((decimal)(t2-t1).Seconds,Func(tire_curr_pressure[i]/tire_base_pressure[i])) - (((decimal)(t2-t1).Seconds) /tire_ls[i]);
                //if tire life is below 0 then we just update the column with 0.
               
                if (tire_ls[i] <= 0)
                {
                     sqlStatement = "UPDATE tmps_tire SET remaining_life = 0 WHERE id = '"+ tire_id[i] +"'";//assuming these is already a value in that column.
                }
                else
                {
                    //decimal new_val = (tire_ls[i] / (365 * 24 * 60 * 60));
                    decimal new_val = Math.Ceiling((tire_ls[i]*10000) / (365 * 24 * 60 * 60))/10000;
                     sqlStatement = "UPDATE tpms_tire SET remaining_life = "+ new_val +" WHERE id = '"+ tire_id[i] +"'";//assuming these is already a value in that column.
                }
                
               int nrOfRowsAffected = Database.SendNonQuery(sqlStatement);
            }

            return;
        }
    }
}
