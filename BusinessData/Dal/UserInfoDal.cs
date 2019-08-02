using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData.Dal
{
    public class UserInfoDal : BaseDal<UserInfo>
    {
        public UserInfo Login(string account, string password)
        {
            UserInfo user = GetModel(u => u.Account.Equals(account) && u.Password.Equals(password));
            if (user != null)
            {
                user.LastLoginTime = DateTime.Now;
                Modify(user);
            }
            return user;
        }

        public void Logout(UserInfo user)
        {
            if (user != null)
            {
                user.LastLogoutTime = DateTime.Now;
                Modify(user);
            }
        }
    }
}
