using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.ViewModels.Admin
{
    public class AdminUserListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }

        public bool Status { get; set; }
    }
}
