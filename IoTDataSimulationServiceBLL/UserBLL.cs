using IoTDataSimulationServiceBLL.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDataSimulationServiceBLL
{
    public class UserBLL : IDisposable
    {

        private IoTDataSimulatorDBEntities DataContext;

        public UserBLL()
        {
            //  ToDo 
        }

        public UserBLL(IoTDataSimulatorDBEntities dataContext)
        {
            if (this.DataContext == null)
            {
                this.DataContext = dataContext;
            }
        }

        public void Dispose()
        {
            if (DataContext != null)
            {
                DataContext.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public User GetByID(long ID)
        {
            User user = (from user1 in DataContext.Users
                         where user1.UserID == ID
                         select user1).SingleOrDefault();

            return user;
        }


    }
}
