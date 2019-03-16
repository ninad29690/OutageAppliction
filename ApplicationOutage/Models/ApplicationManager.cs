using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace ApplicationOutage.Models
{
    public class ApplicationManager
    {
        public List<Application> GetApplicationList()
        {
            ApplicationOutageEntities entities = new ApplicationOutageEntities();
            return entities.Applications.ToList();
        }
       
            
    }
}