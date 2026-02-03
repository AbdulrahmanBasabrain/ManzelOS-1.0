using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Payments
{
    public class PaymentDTO
    {

        public int PaymentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PaidByTenantId { get; set; }
        public int GeneratedBillId { get; set; }
        public int ReceivedByOwnerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public short PaymentMethodId { get; set; }
        public short PaymentStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public PaymentDTO(int paymentId, decimal paymentAmount, int paidByTenantId, int generatedBillId, int receivedByOwnerId,
            DateTime paymentDate, short paymentMethodId, short paymentStatusId, DateTime createdAt)
        {

            PaymentId = paymentId;
            PaymentAmount = paymentAmount;
            PaidByTenantId = paidByTenantId;
            GeneratedBillId = generatedBillId;
            ReceivedByOwnerId = receivedByOwnerId;
            PaymentDate = paymentDate;
            PaymentMethodId = paymentMethodId;
            PaymentStatusId = paymentStatusId;
            CreatedAt = createdAt;

        }

    }

}
