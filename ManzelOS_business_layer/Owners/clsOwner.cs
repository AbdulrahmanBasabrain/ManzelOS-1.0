using ManzelOS_data_access_layer.OwnersData;
using ManzelOS_DTOs.Owners;
using ManzelOS_business_layer.People;

namespace ManzelOS_business_layer
{
    public class clsOwner : clsPeople
    {

        public enum enMode { enAddOwner = 1, enUpdateOwner = 2 }
        private enMode _Mode;

        public int OwnerId { get; set; }
        public int PersonId { get; set; }
        public bool IsBusiness { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public OwnerDTO ownerDTO { get { return new(this.OwnerId, this.PersonId, this.IsBusiness, this.CreatedAt); } }

        public clsOwner()
        {
            OwnerId = -1;
            PersonId = -1;
            IsBusiness = false;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddOwner;
        }

        public clsOwner(OwnerDTO ownerDTO, enMode mode = enMode.enAddOwner)
        {

            OwnerId = ownerDTO.OwnerId;
            PersonId = ownerDTO.PersonId;
            IsBusiness = ownerDTO.IsBusiness;
            CreatedAt = ownerDTO.CreatedAt;

            _Mode = mode;

        }

        public static clsOwner FindOwnerById(int ownerId)
        {

            OwnerDTO ownerDTO = clsOwnerDataAccess.GetOwnerInfoById(ownerId);

            if (ownerDTO != null)
            {
                return new clsOwner(ownerDTO, enMode.enUpdateOwner);
            }
            else
            {
                return null;
            }
        }

        public static List<OwnerDTO> ListAllOwners()
        {
            return clsOwnerDataAccess.ListAllOwners();
        }

        private bool _AddNewOwner()
        {

            base.Save();
            this.OwnerId = clsOwnerDataAccess.AddNewOwner(this.ownerDTO);

            return (this.OwnerId != -1);
        }

        private bool _UpdateOwner()
        {
            base.Save();
            return clsOwnerDataAccess.UpdateOwner(this.OwnerId, this.ownerDTO);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddOwner:
                    if (_AddNewOwner())
                    {
                        _Mode = enMode.enUpdateOwner;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdateOwner:
                    return _UpdateOwner();
            }
            return false;

        }
    
        public static bool DeleteOwner(int ownerId)
        {
            return clsOwnerDataAccess.DeleteOwner(ownerId);
        }
     
        public static bool IsOwnerExist(int ownerId)
        {
            return clsOwnerDataAccess.IsOwnerExist(ownerId);
        }
       
    
    }
}

