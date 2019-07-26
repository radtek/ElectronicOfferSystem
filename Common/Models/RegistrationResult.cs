using BusinessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RegistrationResult
    {
        public String Xmmc { get; set; }
        public String CaseNum { get; set; }
        public String Kfsmc { get; set; }

        public List<Applicant> Sqrs { get; set; }
    }
}
