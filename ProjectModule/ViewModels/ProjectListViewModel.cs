using BusinessData;
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

namespace ProjectModule.ViewModels
{
    class ProjectListViewModel : BindableBase
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


        public ProjectListViewModel()
        {
            

            CancelAddOrEditProjectDialogCommand = new DelegateCommand(() => {
                IsAddOrEditProjectDialogOpen = false;
                Project = null;
            });
            AcceptCommand = new DelegateCommand(AddProject);

            OpenAddOrEditProjectDialogCommand = new DelegateCommand<string>((string dialogTitle) => {
                DialogTitle = dialogTitle;
                Project = new Project();
                IsAddOrEditProjectDialogOpen = true;
            });

            ProjectDal projectDal = new ProjectDal();
            Projects = new ObservableCollection<Project>(projectDal.GetAllProject());
        }

        private void AddProject()
        {
            ProjectDal projectDal = new ProjectDal();
            
            Project.ID = Guid.NewGuid();
            Project.UptateTime = DateTime.Now;
            Project.Type = 1;
            Project.State = 0;

            projectDal.Insert(Project);
            IsAddOrEditProjectDialogOpen = false;
            Project = null;
            Projects = new ObservableCollection<Project>(projectDal.GetAllProject());
        }
    }
}
