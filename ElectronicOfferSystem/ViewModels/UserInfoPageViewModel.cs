using BusinessData;
using BusinessData.Dal;
using Common.Utils;
using Common.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicOfferSystem.ViewModels
{
    public class UserInfoPageViewModel : BindableBase
    {
        private IRegionManager RegionManager;

        private UserInfo user;
        public UserInfo User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        private bool isPopupOpen;

        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set { SetProperty(ref isPopupOpen, value); }
        }


        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand EditUserCommand { get; set; }

        UserInfoDal UserInfoDal = new UserInfoDal();

        public UserInfoPageViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;

            try
            {
                user = (UserInfo)App.Current.Properties["User"];
                user.Password = CEncoder.Decode(user.Password);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
            }

            EditUserCommand = new DelegateCommand(EditUser);

            LogoutCommand = new DelegateCommand(Logout);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        private void EditUser()
        {
            try
            {
                user.Password = CEncoder.Encode(user.Password);
                UserInfoDal.Modify(user);
                App.Current.Properties["User"] = user;

                IsPopupOpen = false;
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
            }

        }

        /// <summary>
        /// 登出
        /// </summary>
        private void Logout()
        {
            UserInfoDal.Logout(user);
            RegionManager.RequestNavigate("MainRegion", "LoginPage", NavigationComplete);
        }

        private void NavigationComplete(NavigationResult obj)
        {
            App.Current.Properties["User"] = null;
        }
    }
}
