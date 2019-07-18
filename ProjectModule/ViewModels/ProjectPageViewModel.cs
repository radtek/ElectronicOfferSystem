﻿using BusinessData;
using BusinessData.Dal;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProjectModule.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectModule.ViewModels
{
    class ProjectPageViewModel : BindableBase
    {
        private Project project;
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        private bool isAddOrEditProjectDialogOpen;
        public bool IsAddOrEditProjectDialogOpen
        {
            get { return isAddOrEditProjectDialogOpen; }
            set { SetProperty(ref isAddOrEditProjectDialogOpen, value); }
        }

        private string dialogTitle;
        public string DialogTitle
        {
            get { return dialogTitle; }
            set { SetProperty(ref dialogTitle, value); }
        }

        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects
        {
            get { return projects; }
            set { SetProperty(ref projects, value); }
        }

        public DelegateCommand<string> OpenAddOrEditProjectDialogCommand { get; private set; }
        public DelegateCommand AcceptCommand { get; set; }
        public DelegateCommand CancelAddOrEditProjectDialogCommand { get; set; }
        public DelegateCommand<object> SelectProjectCommand { get; set; }

        public ProjectPageViewModel()
        {
            // 选中ListView中的一项
            SelectProjectCommand = new DelegateCommand<object>((obj) => {
                ListView listView = obj as ListView;
                Project = listView.SelectedItem as Project;

            });

            // 关闭模态框
            CancelAddOrEditProjectDialogCommand = new DelegateCommand(() => {
                IsAddOrEditProjectDialogOpen = false;
                Project = null;
            });

            // 模态框的确认按钮
            AcceptCommand = new DelegateCommand(()=> {
                switch (DialogTitle)
                {
                    case "新增项目":
                        AddProject();
                        break;
                    case "编辑项目":
                        UpdProject();
                        break;
                    default:
                        break;
                }
            });

            // 打开模态框
            OpenAddOrEditProjectDialogCommand = new DelegateCommand<string>((string dialogTitle) => {
                DialogTitle = dialogTitle;
                if ("新增项目".Equals(DialogTitle))
                {
                    Project = new Project();
                }
                IsAddOrEditProjectDialogOpen = true;
            });

            ProjectDal projectDal = new ProjectDal();
            Projects = new ObservableCollection<Project>(projectDal.GetListBy((p) => p.Type == 1));
        }

        ProjectDal projectDal = new ProjectDal();

        private void AddProject()
        {
            // 新增项目的初始化
            Project.ID = Guid.NewGuid();
            Project.UptateTime = DateTime.Now;
            Project.Type = 1;
            Project.State = 0;

            projectDal.Add(Project);
            CloseDialogAndRefreshProjectList();
        }

        private void UpdProject()
        {  
            if (Project != null)
            {
                projectDal.Modify(Project);
                CloseDialogAndRefreshProjectList();
            }
        }

        private void CloseDialogAndRefreshProjectList()
        {
            IsAddOrEditProjectDialogOpen = false;
            Project = null;
            Projects = new ObservableCollection<Project>(projectDal.GetListBy((p)=>p.Type == 1));
        }
    }
}
