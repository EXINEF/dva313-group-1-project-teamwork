using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CopilotApp
{
	public class Calculations
	{


		public Calculations()
		{

		}

		public async Task run()
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
				
				//endDate = DateTime.Now;
				ls.CalcL();
				//for the purpose of testing. Set (endDate - startDate).TotalMinutes == 10.0
				//if ((endDate - startDate).TotalDays == 7.0)
				//{
				
					tkph.Calc();
				//startDate = DateTime.Now;
				Thread.Sleep(6000);
				//	}

			}
			await Task.CompletedTask;
		}

	}
}





