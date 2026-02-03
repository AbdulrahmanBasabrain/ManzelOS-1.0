using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Payments
{
    public class PaymentMethodDTO
    {

        public int PaymentMethodId { get; set; }
        public string PaymentStatusName { get; set; }
        public PaymentMethodDTO(int paymentMethodId, string paymentStatusName)
        {
            PaymentMethodId = paymentMethodId;
            PaymentStatusName = paymentStatusName;
        }

    }
}
