using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Payments
{
    public class BillTypeDTO
    {
        public int BillTypeId { get; set; }
        public string BillingTypeName { get; set; }

        public BillTypeDTO(int billTypeId, string billTypeName) { this.BillTypeId = billTypeId; this.BillingTypeName = billTypeName; }

    }

}
