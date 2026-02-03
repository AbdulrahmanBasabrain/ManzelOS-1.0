using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Tenants
{
    public class TenantDTO
    {

        public int TenantId { get; set; }
        public int PersonId { get; set; }
        public string DocumentsFilePath { get; set; }
        public DateTime CreatedAt { get; set; }

        public TenantDTO(int tenantId, int personId, string documentsFilePath, DateTime createdAt)
        {
            this.TenantId = tenantId;
            this.PersonId = personId;
            this.DocumentsFilePath = documentsFilePath;
            this.CreatedAt = createdAt;
        }


    }
}
