using ElectronicOfferSystem.Dal;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicOfferSystem.ViewModels
{
    public class ProjectListViewModel : BindableBase
    {
        public List<Project> Projects { get; set; }

        public ProjectListViewModel()
        {
            ProjectDal projectDal = new ProjectDal();
            Projects = projectDal.GetAllProject();
        }
    }
}
