using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.Dtos.Email
{
    public class EmailRequest
    {
        #region Propiedades utilizadas para enviar el correo
        public string To { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }
        public string From { get; set; }

        #endregion

    }
}
