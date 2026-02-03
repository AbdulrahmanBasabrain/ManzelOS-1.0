using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Payments
{
    public class GeneratedBillDTO
    {

        public int GeneratedBillId { get; set; }
        public decimal GeneratedBillFees { get; set; }
        public int BillTypeId { get; set; }
        public int RentalContractId { get; set; }
        public int RentalUnitId { get; set; }
        public DateTime GeneratedBillDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public short BillPaymentStatusId { get; set; }
        public DateTime BillExpectedPaymentDate { get; set; }
        public bool IsPaid { get; set; }

        public GeneratedBillDTO(int generatedBillId, decimal generatedBillFees, int billTypeId, int rentalContractId, int rentalUnitId, DateTime generatedBillDate, DateTime createdAt, short billPaymentStatusId,
            DateTime billExpectedPaymentDate, bool isPaid)
        {

            GeneratedBillId = generatedBillId;
            GeneratedBillFees = generatedBillFees;
            BillTypeId = billTypeId;
            RentalContractId = rentalContractId;
            RentalUnitId = rentalUnitId;
            GeneratedBillDate = generatedBillDate;
            CreatedAt = createdAt;
            BillPaymentStatusId = billPaymentStatusId;
            BillExpectedPaymentDate = billExpectedPaymentDate;
            IsPaid = isPaid;

        }

    }

}
