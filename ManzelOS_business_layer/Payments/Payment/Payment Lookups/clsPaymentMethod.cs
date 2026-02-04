using ManzelOS_data_access_layer.PaymentsData;
using ManzelOS_DTOs.Payments;

namespace ManzelOS_business_layer.Payments
{
    public static class clsPaymentMethod
    {


        public static List<PaymentMethodDTO> ListAllPaymentMethods()
        {
            return clsPaymentMethodDataAccess.ListAllPaymentMethods();
        }

    }
}
