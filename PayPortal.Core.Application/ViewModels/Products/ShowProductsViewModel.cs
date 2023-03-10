using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.ViewModels.Products
{
    public class ShowProductsViewModel
    {
        public int Id { get; set; }
        public string Identifier { get; set; }

        public string Type { get; set; }

        public string UserName { get; set; }

        public double Owed { get; set; }

        public double Amount { get; set; }


    }
}
