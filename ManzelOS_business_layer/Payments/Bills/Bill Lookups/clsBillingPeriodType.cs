using ManzelOS_DTOs.Payments;
using ManzelOS_data_access_layer.PaymentsData;

namespace ManzelOS_business_layer.Payments
{
    public static class clsBillingPeriodType
    {






        public static List<BillingPeriodTypeDTO> ListAllBillingPeriods()
        {
            return clsBillingPeriodTypeDataAccess.ListAllBillingPeriodTypes();
        }






    }
}
