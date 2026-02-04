using ManzelOS_data_access_layer.PaymentsData;
using ManzelOS_DTOs.Payments;

namespace ManzelOS_business_layer.Payments
{
    public static class clsPaymentStatus
    {

        public static List<PaymentStatusDTO> ListAllPaymentStatuses()
        {

            return clsPaymentStatusDataAccess.ListAllPaymentStatuses();

        }

    }
}
