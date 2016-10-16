using LogUtils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHutWinSvcAccountManager
{
    public class AccountManager
    {
        public AccountManager()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {

        }

        public AccountManager(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public bool CreateUser(String strUsername, String strEmail, String strPassword)
        {
            bool bFlag = false;
            try
            {
                ApplicationLog.Instance.WriteDebug(String.Format("Creating user with {0}", strUsername));
                var user = new ApplicationUser() { UserName = strUsername, Email = strEmail };
                IdentityResult result = UserManager.Create(user, strPassword);
                if (result != null)
                {
                    bFlag = result.Succeeded;
                    if (!bFlag)
                    {
                        ApplicationLog.Instance.WriteError(String.Format("User not created with username {0}", strEmail));
                        result.Errors.ToList().WriteFromList(null, "WriteWarning");
                    }
                    else
                        ApplicationLog.Instance.WriteInfo(String.Format("User created with username {0}", strUsername));
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bFlag;
        }

        public bool AddUserToRole(String strUsername, String strRole)
        {
            bool bFlag = false;
            try
            {
                ApplicationLog.Instance.WriteDebug(String.Format("Adding email {0} to Role {1}", strUsername, strRole));
                var user = UserManager.FindByName(strUsername);
                if (user != null)
                {
                    IdentityResult result = UserManager.AddToRole(user.Id, strRole);
                    if (result != null)
                    {
                        bFlag = result.Succeeded;
                        if (!bFlag)
                        {
                            result.Errors.ToList().WriteFromList(null, "WriteWarning");
                        }
                        else
                            ApplicationLog.Instance.WriteInfo(String.Format("User with username {0} added to role SERVICEREQUESTUSER", strUsername));
                    }
                }
                else
                    ApplicationLog.Instance.WriteError(String.Format("User not found with username {0}", strUsername));
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bFlag;
        }

        public bool DeleteUser(String strUserName)
        {
            bool bFlag = false;
            try
            {
                ApplicationUser user = UserManager.FindByName(strUserName);
                IdentityResult result = UserManager.Delete(user);
                if (result != null)
                {
                    bFlag = result.Succeeded;
                    //TODO:: Get Errors if !result.Succeeded
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bFlag;
        }
    }

    public class ApplicationUser : IdentityUser
    {

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }
    }
}
