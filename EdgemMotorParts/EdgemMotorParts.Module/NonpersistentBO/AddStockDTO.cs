using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using EdgemMotorPart.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgemMotorParts.Module.NonpersistentBO
{
    [DomainComponent, DefaultClassOptions]
    public class AddStockDTO : EMPNonpPersistentBaseObject, IStock
    {
        decimal? price;
        int quantity;
        string item;

        public string Item
        {
            get => item;
            set
            {
                if (item == value)
                    return;
                item = value;
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (quantity == value)
                    return;
                quantity = value;
            }
        }

        public decimal? Price
        {
            get => price;
            set
            {
                if (price == value)
                    return;
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

    }
}
