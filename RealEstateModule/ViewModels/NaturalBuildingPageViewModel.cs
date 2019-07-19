using BusinessData;
using BusinessData.Dal;
using Common;
using Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    class NaturalBuildingPageViewModel : BindableBase, INavigationAware
    {
        public Guid ProjectID { get; set; }

        private string buttonContent = "确认新增";
        public string ButtonContent
        {
            get { return buttonContent; }
            set { SetProperty(ref buttonContent, value); }
        }


        private NaturalBuilding naturalBuilding;
        public NaturalBuilding NaturalBuilding
        {
            get { return naturalBuilding; }
            set { SetProperty(ref naturalBuilding, value); }
        }

        public DelegateCommand AddOrEditNaturalBuildingCommand { get; set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        public DelegateCommand<object> SelectBusinessCommand { get; set; }

        NaturalBuildingDal NaturalBuildingDal = new NaturalBuildingDal();

        public NaturalBuildingPageViewModel()
        {
            //NaturalBuilding = new NaturalBuilding();

            AddOrEditNaturalBuildingCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddNaturalBuilding();
                        break;
                    case "确认修改":
                        EditNaturalBuilding();
                        break;
                    default:
                        break;
                }
            });

            // 选中自然幢列表中的一项
            SelectBusinessCommand = new DelegateCommand<object>(SelectBusiness);
            GlobalCommands.SelectBusinessCommand.RegisterCommand(SelectBusinessCommand);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Guid? projectID = navigationContext.Parameters["ProjectID"] as Guid?;
            if (projectID != null)
            {
                ProjectID = (Guid)projectID;
            }

            // 初始自然幢数据
            NaturalBuilding = new NaturalBuilding();
            // 按钮为新增状态
            ButtonContent = "确认新增";
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Guid? projectID = navigationContext.Parameters["ProjectID"] as Guid?;
            if (projectID != null)
            {
                return ProjectID != null;
            }
            else
            {
                return true;
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        /// <summary>
        /// 选择自然幢列表的一项
        /// </summary>
        /// <param name="obj"></param>
        private void SelectBusiness(object obj)
        {
            // 加载自然幢数据
            ListView listView = obj as ListView;
            Business<NaturalBuilding> business = new Business<NaturalBuilding>();
            business = listView.SelectedItem as Business<NaturalBuilding>;
            var item = listView.SelectedItem;
            if (item == null) return;
            Type type = item.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                var name = pi.Name;
                var value = pi.GetValue(item, null);
                var t = value?.GetType() ?? typeof(object);
                if (value.GetType() == typeof(NaturalBuilding))
                {
                    NaturalBuilding = value as NaturalBuilding;
                }
            }

            // 按钮为修改状态
            ButtonContent = "确认修改";
        }

        /// <summary>
        /// 新增自然幢
        /// </summary>
        private void EditNaturalBuilding()
        {
            if (NaturalBuilding == null) return;
            NaturalBuilding.ProjectID = ProjectID;
            NaturalBuilding.ID = Guid.NewGuid();
            NaturalBuilding.UpdateTime = DateTime.Now;
            NaturalBuildingDal.Add(NaturalBuilding);

            NaturalBuilding = null;
        }

        /// <summary>
        /// 编辑自然幢
        /// </summary>
        private void AddNaturalBuilding()
        {
            if (NaturalBuilding == null) return;
            NaturalBuilding.UpdateTime = DateTime.Now;
            NaturalBuildingDal.Modify(NaturalBuilding);
        }
    }
}
