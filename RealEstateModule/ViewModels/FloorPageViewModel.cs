using BusinessData;
using BusinessData.Dal;
using BusinessData.Models;
using Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RealEstateModule.ViewModels
{
    class FloorPageViewModel : BindableBase, INavigationAware
    {

        #region Properties
        IEventAggregator EA;
        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// 新增/修改按钮内容
        /// </summary>
        private string buttonContent = "确认新增";
        public string ButtonContent
        {
            get { return buttonContent; }
            set { SetProperty(ref buttonContent, value); }
        }

        private Floor floor;
        public Floor Floor
        {
            get { return floor; }
            set { SetProperty(ref floor, value); }
        }

        #region 命令
        /// <summary>
        /// 新增或修改层
        /// </summary>
        public DelegateCommand AddOrEditFloorCommand { get; set; }
        /// <summary>
        /// 在项目列表选择一个项目
        /// </summary>
        public DelegateCommand<object> SelectProjectCommand { get; set; }
        /// <summary>
        /// 在层列表选择一个层
        /// </summary>
        public DelegateCommand<object> SelectBusinessCommand { get; set; }
        #endregion 

        FloorDal FloorDal = new FloorDal();

        #endregion

        #region ctor
        public FloorPageViewModel(IEventAggregator ea)
        {
            EA = ea;
            // 新增或修改层信息
            AddOrEditFloorCommand = new DelegateCommand(() => {
                switch (ButtonContent)
                {
                    case "确认新增":
                        AddFloor();
                        break;
                    case "确认修改":
                        EditFloor();
                        break;
                    default:
                        break;
                }
            });

            // 选中层列表中的一项
            SelectBusinessCommand = new DelegateCommand<object>(SelectBusiness);
            GlobalCommands.SelectBusinessCommand.RegisterCommand(SelectBusinessCommand);
        }
        #endregion


        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取选中项目
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                Project = project;
            }

            // 初始层数据
            InitialFloor();
            // 按钮为新增状态
            ButtonContent = "确认新增";
        }



        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                return project != null;
            }
            else
            {
                return true;
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        private void InitialFloor()
        {
            Floor = new Floor();
        }

        /// <summary>
        /// 选择层列表中一项
        /// </summary>
        /// <param name="obj"></param>
        private void SelectBusiness(object obj)
        {
            // 加载层数据
            ListView listView = obj as ListView;
            Business business = new Business();
            business = listView.SelectedItem as Business;
            Floor = business?.Floor;

            // 按钮为修改状态
            ButtonContent = "确认修改";
        }

        private void EditFloor()
        {
            if (Floor == null) return;
            Floor.UpdateTime = DateTime.Now;
            FloorDal.Modify(Floor);

            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<PubSubEvent<string>>().Publish("FloorPage");
        }

        private void AddFloor()
        {
            if (Project == null) return;
            if (Floor == null) return;
            Floor.ProjectID = Project.ID;
            Floor.ID = Guid.NewGuid();
            Floor.UpdateTime = DateTime.Now;
            FloorDal.Add(Floor);

            Floor = null;
            // 发送通知，点击业务的导航页，也就是新增页，更新业务列表
            EA.GetEvent<PubSubEvent<string>>().Publish("FloorPage");
        }
    }
}
