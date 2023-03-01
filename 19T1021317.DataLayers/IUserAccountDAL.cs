using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021317.DomainModels;

namespace _19T1021317.DataLayers
{
    /// <summary>
    ///     Definition of processing related to user account data
    /// </summary>
    public interface IUserAccountDal
    {
        /// <summary>
        ///     Check username and password to authenticate
        /// </summary>
        /// <param name="userName"> Username </param>
        /// <param name="password"> Password </param>
        /// <returns> UserAccount object if authentication is successful, otherwise null </returns>
        UserAccount Authenticate(string userName, string password);

        /// <summary>
        ///     Change password of user
        /// </summary>
        /// <param name="userName"> Username </param>
        /// <param name="oldPassword"> Old password </param>
        /// <param name="newPassword"> New password </param>
        /// <returns> True if change password is successful, otherwise false </returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

}
