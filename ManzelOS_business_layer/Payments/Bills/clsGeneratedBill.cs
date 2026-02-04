using ManzelOS_data_access_layer.PaymentsData;
using ManzelOS_DTOs.Payments;

namespace ManzelOS_business_layer.Payments
{
    public class clsGeneratedBill
    {

        public enum enMode { enAddGeneratedBill = 1, enUpdateGeneratedBill = 2 }

        private enMode _Mode = enMode.enAddGeneratedBill;

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
        
        public GeneratedBillDTO generatedBillDTO 
        { 
            get 
            {
                return new GeneratedBillDTO(this.GeneratedBillId, this.GeneratedBillFees, this.BillTypeId, this.RentalContractId, this.RentalUnitId, this.GeneratedBillDate, this.CreatedAt,
                    this.BillPaymentStatusId, this.BillExpectedPaymentDate, this.IsPaid);
            } 
        }

        public clsGeneratedBill()
        {
            GeneratedBillId = -1;
            GeneratedBillFees = 0.0m;
            BillTypeId = -1;
            RentalContractId = -1;
            RentalUnitId = -1;
            GeneratedBillDate = DateTime.MinValue;
            CreatedAt = DateTime.Now;
            BillPaymentStatusId = -1;
            BillExpectedPaymentDate = DateTime.MinValue;
            IsPaid = false;

            _Mode = enMode.enAddGeneratedBill;

        }

        public clsGeneratedBill(GeneratedBillDTO generatedBillDTO, enMode mode = enMode.enAddGeneratedBill)
        {

            GeneratedBillId = generatedBillDTO.GeneratedBillId;
            GeneratedBillFees = generatedBillDTO.GeneratedBillFees;
            BillTypeId = generatedBillDTO.BillTypeId;
            RentalContractId = generatedBillDTO.RentalContractId;
            RentalUnitId = generatedBillDTO.RentalUnitId;
            GeneratedBillDate = generatedBillDTO.GeneratedBillDate;
            CreatedAt = generatedBillDTO.CreatedAt;
            BillPaymentStatusId = generatedBillDTO.BillPaymentStatusId;
            BillExpectedPaymentDate = generatedBillDTO.BillExpectedPaymentDate;
            IsPaid = generatedBillDTO.IsPaid;


            _Mode = mode;

        }

        public static clsGeneratedBill FindGeneratedBillById(int generatedBillId)
        {

            GeneratedBillDTO generatedBillDTO = clsGeneratedBillDataAccess.GetGeneratedBillInfoById(generatedBillId);

            if (generatedBillDTO != null)
            {
                return new clsGeneratedBill(generatedBillDTO, enMode.enUpdateGeneratedBill);
            }
            else
            {
                return null;
            }

        }

        public static List<GeneratedBillDTO> ListAllGeneratedBills()
        {
            return clsGeneratedBillDataAccess.ListAllGeneratedBills();
        }

        public static List<GeneratedBillDTO> ListAllGeneratedBillsWithContractId(int contractId)
        {
            return clsGeneratedBillDataAccess.ListAllGeneratedBillsWithContract(contractId);
        }

        public static List<GeneratedBillDTO> ListAllUnpaidBillsWithContractId(int contractId)
        {
            return clsGeneratedBillDataAccess.ListAllUnpaidBillsWithContract(contractId);
        }

        private bool _AddNewGeneratedBill()
        {
            this.GeneratedBillId = clsGeneratedBillDataAccess.AddNewGeneratedBill(this.generatedBillDTO);

            return (this.GeneratedBillId != -1);
        }

        private bool _UpdateGeneratedBill()
        {
            return clsGeneratedBillDataAccess.UpdateGeneratedBill(this.GeneratedBillId, this.generatedBillDTO);
        }

        public bool Save()
        {

            switch (_Mode)
            {

                case enMode.enAddGeneratedBill:
                    if (_AddNewGeneratedBill())
                    {
                        _Mode = enMode.enUpdateGeneratedBill;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.enUpdateGeneratedBill:
                    return _UpdateGeneratedBill();



            }

            return false;

        }

        public static bool Delete(int generatedBillId)
        {
            return clsGeneratedBillDataAccess.DeleteGeneratedBill(generatedBillId);
        }

        public static bool IsGeneratedBillExist(int generatedBillId)
        {
            return clsGeneratedBillDataAccess.IsGeneratedBillExist(generatedBillId);
        }


    }
}
