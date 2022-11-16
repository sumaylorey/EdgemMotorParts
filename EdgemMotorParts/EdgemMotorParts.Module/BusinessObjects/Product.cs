using DevExpress.Xpo;
using EdgemMotorPart.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgemMotorParts.Module.BusinessObjects
{
    public class Product : EMPBaseObject
    {
        public Product(Session session) : base(session)
        {
        }

        decimal? amount;
        int quantity;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        public int Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }

       
        public decimal? Amount
        {
            get => amount;
            set => SetPropertyValue(nameof(Amount), ref amount, value);
        }
    }
}
