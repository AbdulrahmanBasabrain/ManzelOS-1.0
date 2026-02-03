using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ManzelOS_DTOs.People
{

    public class MarriageStatusDTO
    {

        public short MarriageStatusID { get; set; }
        public string MarriageStatusName { get; set; }

        public MarriageStatusDTO(short marriageStatusId, string marriageStatusName)
        {
            this.MarriageStatusID = marriageStatusId;
            this.MarriageStatusName = marriageStatusName;
        }

    }
}
