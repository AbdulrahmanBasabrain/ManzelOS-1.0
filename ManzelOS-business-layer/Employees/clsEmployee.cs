using System;
using System.Data;
using ManzelOS_data_access_layer.EmployeesData;

namespace ManzelOS_business_layer
{
    public class clsEmployee
    {

        public enum enMode { enAddNewEmployee = 1, enUpdateEmployee = 2 }
        private enMode _Mode = enMode.enAddNewEmployee;


        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public decimal Salary { get; set; }
        public string Job { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public EmployeeDTO employeeDTO { get { return new EmployeeDTO(this.EmployeeId, this.PersonId, this.Salary, this.Job, this.CreatedAt); }  }

        public clsEmployee()
        {

            EmployeeId = -1;
            PersonId = -1;
            Salary = 0;
            Job = string.Empty;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddNewEmployee;
            
        }

        public clsEmployee(EmployeeDTO employeeDTO, enMode mode = enMode.enAddNewEmployee)
        {

            EmployeeId = employeeDTO.EmployeeId;
            PersonId = employeeDTO.PersonId;
            Salary = employeeDTO.Salary;
            Job = employeeDTO.Job;
            CreatedAt = employeeDTO.CreatedAt;

            _Mode = mode;

        }


        public static clsEmployee FindEmployeeById(int employeeId)
        {

            EmployeeDTO employee = clsEmployeeDataAccess.GetEmployeeInfoById(employeeId);

            if (employee != null)
            {
                return new clsEmployee(employee, enMode.enUpdateEmployee);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewEmployee()
        {
            this.EmployeeId = clsEmployeeDataAccess.AddNewEmployee(this.employeeDTO);

            return (EmployeeId != -1);
        }

        private bool _UpdateEmployee()
        {
            return clsEmployeeDataAccess.UpdateEmployee(this.EmployeeId, this.employeeDTO);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNewEmployee:
                    if (clsPeople.IsPersonExist(PersonId))
                    {

                        if (_AddNewEmployee())
                        {
                            _Mode = enMode.enUpdateEmployee;
                            return true;
                        }
                        else
                        { return false; }
                    }
                    else
                    {
                        return false;
                    }

                case enMode.enUpdateEmployee:
                    return _UpdateEmployee();

            }

            return false;
        }

        public static bool IsEmployeeExist(int employeeId)
        {
            return clsEmployeeDataAccess.IsEmployeeExist(employeeId);
        }

        public static bool DeleteEmployee(int employeeId)
        {
            return clsEmployeeDataAccess.DeleteEmployee(employeeId);
        }

        public static List<EmployeeDTO> ListAllEmployees()
        {
            return clsEmployeeDataAccess.ListAllEmployees();
        }


    }
}
