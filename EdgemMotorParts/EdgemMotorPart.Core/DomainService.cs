using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgemMotorPart.Core
{
    public class DomainService
    {
        public DomainService()
        {
        }
        private static DomainService instance = null;
        public static DomainService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DomainService();
                }
                return instance;
            }
        }
        public XafApplication Application;
    }
}
