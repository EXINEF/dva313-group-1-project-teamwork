using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace CopilotApp
{
	public class Calculations
	{


		public Calculations()
		{

		}

		public void run()
		{
			//This block will calculate and update DB by indicating if the real site TKPH is under/over specced or neutral.
			//Every 7th day will the DB be updated.
			DateTime t_ls = DateTime.Now; //this variable is used to store time since we checked tire lifespan 
			DateTime endDate = DateTime.Now;
			DateTime startDate = DateTime.Now;
			TKPHCalculations tkph = new TKPHCalculations();
			LifespanCalc ls = new LifespanCalc();
			
			while (true)
			{
				tkph.Track();
				endDate = DateTime.Now;
				t_ls = ls.CalcL(t_ls);
				if ((endDate - startDate).TotalDays == 7.0)
				{
				
					tkph.Calc();
					startDate = DateTime.Now;
					
				}
				Thread.Sleep(50);
			}

		}

	}
}





