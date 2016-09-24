using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TicketReservation.Infrastructure
{
    public class CustomDataConverter
    {
        public static string DateWithDash(DateTime date)
        {
            string formattedStart = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            var newDateWithDash = formattedStart.Replace("/", "-");
            return newDateWithDash;
        }
    }
} 