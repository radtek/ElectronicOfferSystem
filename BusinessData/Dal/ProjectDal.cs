using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessData.Dal
{
    public class ProjectDal : BaseDal<Project>
    {
        //ElectronicOfferSystemDBContainer ctx = new ElectronicOfferSystemDBContainer();

        //public List<Project> GetAllProject()
        //{
        //    List<Project> list = new List<Project>();
        //    try
        //    {
        //        list = ctx.ProjectSet.ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return list;
        //}

        //public void Insert(Project project)
        //{
        //    ctx.ProjectSet.Add(project);
        //    try
        //    {
        //        ctx.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void Delete(Project project)
        //{
        //    ctx.ProjectSet.Remove(project);
        //    try
        //    {
        //        ctx.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void Update(Project project)
        //{
        //    var id = ctx.ProjectSet.Find(project.ID);
        //    var entry = ctx.Entry(id);
        //    entry.CurrentValues.SetValues(project);
        //    entry.Property(p => p.ID).IsModified = false;
        //    try
        //    {
        //        ctx.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
