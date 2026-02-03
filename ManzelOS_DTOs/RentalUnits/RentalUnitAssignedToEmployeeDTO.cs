using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.RentalUnits
{

    public class AssignEmployeeToPropertyDTO
    {

        public int AssignmentId { get; set; }
        public int EmployeeId { get; set; }
        public int RentalUnitID { get; set; }
        public DateTime AssignmentDate { get; set; }

        public AssignEmployeeToPropertyDTO(int assignmentId, int employeeId, int rentalUnitId, DateTime assignmentDate)
        {

            AssignmentId = assignmentId;
            EmployeeId = employeeId;
            RentalUnitID = rentalUnitId;
            AssignmentDate = assignmentDate;

        }

    }

}
