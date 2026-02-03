using System;
using System.Data;
using ManzelOS_data_access_layer.PaymentsData;

namespace ManzelOS_business_layer.Payments
{
    public static class clsBillPaymentStatus
    {

        public static List<BillPaymentStatusDTO> ListAllBillPaymentStatuses()
        {
            return ManzelOS_data_access_layer.PaymentsData.clsBillPaymentsStatusDataAccess.ListAllBillPaymentStatuses();
        }

    }

}

