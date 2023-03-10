using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.ViewModels.Admin
{
    public class AdminViewModel
    {
        public int TotalTransactions { get; set; }
        public int TodayTransactions {get; set;}
        public int TotalPays {get; set;}
        public int TodayPays {get; set;}
        public int ActiveClient {get; set;}
        public int DisableClient{get; set;}
        public int Products {get; set;}

        public int Savings { get; set; }
        public int Loans { get; set; }
        public int Credits { get; set; }



    }
}
