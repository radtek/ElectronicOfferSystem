using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicOfferSystem.Dal
{
    public class ProjectDal
    {
        ElectronicOfferSystemDBContainer eosDataContext = new ElectronicOfferSystemDBContainer();

        public List<Project> GetAllProject()
        {
            return eosDataContext.ProjectSet.ToList();
        }

        public void Insert(Project project)
        {
            eosDataContext.ProjectSet.Add(project);
            eosDataContext.SaveChanges();
        }

        public void Delete(Project project)
        {
            eosDataContext.ProjectSet.Remove(project);
            eosDataContext.SaveChanges();
        }

        public void Update(Project project)
        {
            var id = eosDataContext.ProjectSet.Find(project.Id);
            var entry = eosDataContext.Entry(id);
            entry.CurrentValues.SetValues(project);
            entry.Property(p => p.Id).IsModified = false;
            eosDataContext.SaveChanges();
        }
    }
}
