using ManzelOS_DTOs.Tenants;
using ManzelOS_data_access_layer.TenantsData;

namespace ManzelOS_business_layer
{
    public class clsTenant
    {

        public enum enMode { enAddNewTenant = 1, enUpdateTenant = 2 }
        private enMode _Mode = enMode.enAddNewTenant;


        public int TenantId { get; set; }
        public int PersonId { get; set; }
        public string DocumentsFilePath { get; set; }
        public DateTime CreatedAt { get; set; }

        public TenantDTO TenantDTO { get { return new TenantDTO(this.TenantId, this.PersonId, this.DocumentsFilePath, this.CreatedAt); } }

        public clsTenant()
        {

            TenantId = -1;
            PersonId = -1;
            DocumentsFilePath = string.Empty;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddNewTenant;

        }

        public clsTenant(TenantDTO tenantDTO, enMode mode = enMode.enAddNewTenant)
        {
            TenantId = tenantDTO.TenantId;
            PersonId = tenantDTO.PersonId;
            DocumentsFilePath = tenantDTO.DocumentsFilePath;
            CreatedAt = tenantDTO.CreatedAt;

            _Mode = mode;

        }

        public static clsTenant FindTenantById(int tenantId)
        {
            TenantDTO tenant = clsTenantDataAccess.GetTenatnInfoById(tenantId);
            if (tenant != null)
            {
                return new clsTenant(tenant, enMode.enUpdateTenant);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewTenant()
        {
            this.TenantId = clsTenantDataAccess.AddNewTenant(TenantDTO);

            return (TenantId != -1);
        }

        private bool _UpdateTenant()
        {
            return clsTenantDataAccess.UpdateTenant(this.TenantId, TenantDTO);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNewTenant:
                    if (_AddNewTenant())
                    {
                        _Mode = enMode.enUpdateTenant;
                        return true;
                    }
                    else
                    { return false; }
                case enMode.enUpdateTenant:
                    return _UpdateTenant();

            }

            return false;
        }
        
        public static bool IsTenantExist(int tenantId)
        {
            return clsTenantDataAccess.IsTenantExist(tenantId);
        }

        public static bool DeleteTenant(int tenantId)
        {
            return clsTenantDataAccess.DeleteTenant(tenantId);
        }
        
        public static List<TenantDTO> ListAllTenants()
        {
            return clsTenantDataAccess.ListAllTenants();
        }
    
    }
}
