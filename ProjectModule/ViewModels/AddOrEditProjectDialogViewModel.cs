using BusinessData;
using BusinessData.Dal;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModule.ViewModels
{
    class AddOrEditProjectDialogViewModel : BindableBase
    {
        public Project Project { get; set; }

        public DelegateCommand AddProjectCommand { get; set; }

        public AddOrEditProjectDialogViewModel()
        {
            ProjectDal projectDal = new ProjectDal();
            Project = new Project();

            AddProjectCommand = new DelegateCommand(() => {
                Project.ID = Guid.NewGuid();
                Project.UptateTime = DateTime.Now;
                Project.Type = 1;
                Project.State = 0;

                projectDal.Insert(Project);
            });
        }
    }
}
