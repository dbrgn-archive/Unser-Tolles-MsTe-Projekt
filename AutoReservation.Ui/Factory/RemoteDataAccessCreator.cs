using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;

namespace AutoReservation.Ui.Factory
{
	class RemoteDataAccessCreator : Creator
	{
        public override IAutoReservationService CreateInstance()
        {
            return new AutoReservationService();
        }

	}
}
