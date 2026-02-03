using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Payments
{
    public class PaymentStatusDTO
    {

        public short PaymentStatusId { get; set; }
        public string PaymentStatusName { get; set; }

        public PaymentStatusDTO(short paymentStatusId, string paymentStatusName)
        {
            PaymentStatusId = paymentStatusId;
            PaymentStatusName = paymentStatusName;
        }

        
    }

}
