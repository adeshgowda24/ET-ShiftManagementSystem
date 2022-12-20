using ShiftMgtDbContext.Data;
using ShiftMgtDbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagementServises.Servises
{
    public interface ICredentialServices
    {
        UserCredential GetCredential(int UserID);

       
    }
    public class CredentialServices : ICredentialServices
    {
        private readonly ShiftManagementDbContext _userCredential;

        public CredentialServices(ShiftManagementDbContext UserCredential)
        {
            _userCredential = UserCredential;
        }

        public UserCredential GetCredential(int UserID)
        {
            return _userCredential.UserCredentials.FirstOrDefault(x => x.UserID == UserID);
        }


    }
}
