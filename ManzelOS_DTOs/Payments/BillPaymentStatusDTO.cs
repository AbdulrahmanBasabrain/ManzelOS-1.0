using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Payments
{
    public class BillPaymentStatusDTO
    {
        public short BillPaymentStatusId { get; set; }
        public string BillPaymentStatusName { get; set; }

        public BillPaymentStatusDTO(short billPaymentStatusId, string billPaymentStatusName) { this.BillPaymentStatusId = billPaymentStatusId; this.BillPaymentStatusName = billPaymentStatusName; }

    }
}
