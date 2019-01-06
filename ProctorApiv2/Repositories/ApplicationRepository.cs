using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class ApplicationRepository : BaseSqlRepository
    {
        internal Application GetApplicationById(int id)
        {
            throw new NotImplementedException();
        }

        internal IList<Application> GetAllApplications()
        {
            throw new NotImplementedException();
        }

        internal object CreateApplication(Application application)
        {
            throw new NotImplementedException();
        }

        internal Application DeleteApplication(int id)
        {
            throw new NotImplementedException();
        }

        internal Application UpdateApplication(int id, Application application)
        {
            throw new NotImplementedException();
        }
    }
}