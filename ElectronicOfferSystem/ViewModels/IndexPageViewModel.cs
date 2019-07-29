using Common;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicOfferSystem.ViewModels
{
    public class IndexPageViewModel : BindableBase
    {

        public DelegateCommand<string> NavigateCommand { get; set; }

        public IndexPageViewModel()
        {
            // 页面导航
            NavigateCommand = new DelegateCommand<string>(Navigate);
            GlobalCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }

        private void Navigate(string obj)
        {
        }
    }
}
