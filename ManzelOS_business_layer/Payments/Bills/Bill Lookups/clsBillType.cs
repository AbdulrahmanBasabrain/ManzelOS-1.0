using ManzelOS_data_access_layer.PaymentsData;
using ManzelOS_DTOs.Payments;

namespace ManzelOS_business_layer.Payments
{
    public static class clsBillType
    {

        
        public static List<BillTypeDTO> ListAllBillTypes()
        {
            return clsBillTypeDataAccess.ListAllBillTypes();
        }
    }
}
