using System;
using System.Data;
using ManzelOS_data_access_layer.PaymentsData;

namespace ManzelOS_business_layer.Payments
{
    public class clsPayment
    {

        public enum enMode { enAddPayment = 1, enUpdatePayment = 2 }
        private enMode _Mode = enMode.enAddPayment;
        
        public int PaymentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PaidByTenantId { get; set; }
        public int GeneratedBillId { get; set; }
        public int ReceivedByOwnerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public short PaymentMethodId { get; set; }
        public short PaymentStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public PaymentDTO PaymentDTO 
        { 
            get 
            {
                return new PaymentDTO(this.PaymentId, this.PaymentAmount, this.PaidByTenantId, this.GeneratedBillId, this.ReceivedByOwnerId, this.PaymentDate, this.PaymentMethodId, this.PaymentStatusId,
                    this.CreatedAt);
            } 
        }

        public clsPayment()
        {
            PaymentId = -1;
            PaymentAmount = 0.0m;
            PaidByTenantId = -1;
            GeneratedBillId = -1;
            ReceivedByOwnerId = -1;
            PaymentDate = DateTime.MinValue;
            PaymentMethodId = -1;
            PaymentStatusId = -1;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddPayment;
        }

        public clsPayment(PaymentDTO paymentDTO, enMode mode = enMode.enAddPayment)
        {

            PaymentId = paymentDTO.PaymentId;
            PaymentAmount = paymentDTO.PaymentAmount;
            PaidByTenantId = paymentDTO.PaidByTenantId;
            GeneratedBillId = paymentDTO.GeneratedBillId;
            ReceivedByOwnerId = paymentDTO.ReceivedByOwnerId;
            PaymentDate = paymentDTO.PaymentDate;
            PaymentMethodId = paymentDTO.PaymentMethodId;
            PaymentStatusId = paymentDTO.PaymentStatusId;
            CreatedAt = paymentDTO.CreatedAt;

            _Mode = mode;

        }


        public static clsPayment FindPaymentById(int paymentId)
        {

            PaymentDTO paymentDTO = clsPaymentDataAccess.GetPaymentInfoById(paymentId);
            if (paymentDTO != null)
            {
                return new clsPayment(paymentDTO, enMode.enUpdatePayment);
            }
            else
            {
                return null;
            }

        }

        public static List<PaymentDTO> ListAllPayments()
        {
            return clsPaymentDataAccess.ListAllPayments();
        }

        private bool _AddNewPayment()
        {

            this.PaymentId = clsPaymentDataAccess.AddPayment(this.PaymentDTO);

            return (this.PaymentId != -1);

        }

        private bool _UpdatePayment()
        {

            return clsPaymentDataAccess.UpdatePayment(this.PaymentId, this.PaymentDTO);

        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddPayment:
                    if (_AddNewPayment())
                    {
                        _Mode = enMode.enUpdatePayment;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdatePayment:
                    return _UpdatePayment();
            }

            return false;
        }

        public static bool Delete(int paymentId)
        {
            return clsPaymentDataAccess.DeletePayment(paymentId);
        }

        public static bool IsPaymentExist(int paymentId)
        {

            return clsPaymentDataAccess.IsPaymentExist(paymentId);

        }

    }

}
