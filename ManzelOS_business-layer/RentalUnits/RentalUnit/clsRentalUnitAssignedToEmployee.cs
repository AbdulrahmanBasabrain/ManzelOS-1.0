using System;
using ManzelOS_data_access_layer.RentalUnitsData.RentalUnitsData;

namespace ManzelOS_business_layer.RentalUnits.RentalUnit
{
    public class clsRentalUnitAssignedToEmployee
    {

        public enum enMode { enAddNewAssignment = 1, enUpdateNewAssignment = 2 }
        private enMode _Mode = enMode.enAddNewAssignment;

        public int AssignmentId { get; set; }
        public int EmployeeId { get; set; }
        public int RentalUnitID { get; set; }
        public DateTime AssignmentDate { get; set; }
        public AssignEmployeeToPropertyDTO AssignendEmployeeToPropertyDTO { get { return new AssignEmployeeToPropertyDTO(this.AssignmentId, this.EmployeeId, this.RentalUnitID, this.AssignmentDate); } }         
        
        public clsRentalUnitAssignedToEmployee()
        {

            AssignmentId = -1;
            EmployeeId = -1;
            RentalUnitID = -1;
            AssignmentDate = DateTime.Now;
            _Mode = enMode.enAddNewAssignment;

        }

        public clsRentalUnitAssignedToEmployee(AssignEmployeeToPropertyDTO assignEmployeeToPropertyDTO, enMode mode = enMode.enAddNewAssignment)
        {

            AssignmentId = assignEmployeeToPropertyDTO.AssignmentId;
            EmployeeId = assignEmployeeToPropertyDTO.EmployeeId;
            RentalUnitID = assignEmployeeToPropertyDTO.RentalUnitID;
            AssignmentDate = assignEmployeeToPropertyDTO.AssignmentDate;

            _Mode = mode;

        }
        
        public static  clsRentalUnitAssignedToEmployee FindPropertyEmployeeAssignmentById(int assignmentId)
        {
            AssignEmployeeToPropertyDTO assignEmployeeToProperty = clsRentalUnitAssignedToEmployeeDataAccess.FindEmployeeRentalUnitAssignment(assignmentId);

            if (assignEmployeeToProperty != null)
            {
                return new clsRentalUnitAssignedToEmployee(assignEmployeeToProperty, enMode.enUpdateNewAssignment);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewAssignment()
        {

            this.AssignmentId = clsRentalUnitAssignedToEmployeeDataAccess.AssignEmployeeToProperty(this.AssignendEmployeeToPropertyDTO);
            return (AssignmentId != -1);
            
        }

        private bool _UpdateAssignment()
        {

            return clsRentalUnitAssignedToEmployeeDataAccess.ChangePropertyEmployee(this.AssignmentId, this.AssignendEmployeeToPropertyDTO);

        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNewAssignment:

                        if (_AddNewAssignment())
                        {
                            _Mode = enMode.enUpdateNewAssignment;
                            return true;
                        }
                        else
                        { return false; }

                case enMode.enUpdateNewAssignment:
                    return _UpdateAssignment();

            }

            return false;
        }
        
        public static bool DeleteAssignment(int assignmentId)
        {

            return clsRentalUnitAssignedToEmployeeDataAccess.DeleteAssignment(assignmentId);

        }

    }
}
