using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Payments
{
    public class BillingPeriodTypeDTO
    {

        public int BillingPeriodID { get; set; }
        public string BillingPeriodName { get; set; }

        public BillingPeriodTypeDTO(int billingPeriodTypeId, string billingPeriodTypeName) { this.BillingPeriodID = billingPeriodTypeId; this.BillingPeriodName = billingPeriodTypeName; }

    }

}
