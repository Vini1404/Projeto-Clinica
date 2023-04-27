using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoCLinica.Models
{
    public class clAtendimento
    {
        public string codAtend { get; set; }

        public string dataAtend { get; set; }

        public string horaAtend { get; set; }

        public string confAgendamento { get; set; }

        public string codMedico { get; set; }

        public string codPac { get; set; }
    }
}