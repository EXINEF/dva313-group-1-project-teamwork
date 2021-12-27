using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace CopilotApp
{
	public class TKPHloop
	{

		/*
		 * TO GET DATA FROM FB:
		 * string query = "SELECT baseline_pressure, fill_material, tread_depth FROM tpms_tire WHERE id = '1'";
		 *       MySqlDataReader reader = (Database.SendQuery(query)).Result;
		 *		  tireBaselinePressure = reader["baseline_pressure"].ToString();
		 *TO UPDATE DB:
		 *
		 *await Database.SendNonQuery(SQLCommand);
		 *
		 */

		public TKPHloop()
		{

		}

		public void run()
		{
			//This block will calculate and update DB by indicating if the real site TKPH is under/over specced or neutral.
			//Every 7th day will the DB be updated.
			double h = 0;
			DateTime endDate = DateTime.Now;
			DateTime startDate = DateTime.Now;
			TKPHCalculations tkph = new TKPHCalculations();
			
			while (true)
			{
				tkph.Track();
				endDate = DateTime.Now;
				if ((endDate - startDate).TotalDays == 7.0)
				{
					//we do not consider hours loaded and unloaded for this type of calc.
					//endDate-startDate).TotalHours....
					//h = h + (machineHoursEmpty + machineHoursLoaded); //since the beginning.
					tkph.Calc();
					startDate = DateTime.Now;
					


				}
				Thread.Sleep(50);
			}

		}

	}
}





