using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO.ReadDTO.ReportDTO
{
    public class VLCReportByNameAndCityContactDTO
    {
        public string VLCName { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public List<VLCDTO> VLCs { set; get; }
    }
}
