using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Ui.Factory
{
	abstract class Creator
	{
		public static Creator GetCreator()
		{
			return new LocalAccessDataCreator();
		}

		public IAutoReservationService CreateInstance();
	}
}
