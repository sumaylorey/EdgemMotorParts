using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgemMotorParts.Module.NonpersistentBO
{
    public interface IStock
    {
         int Quantity { get; set; }
         string Item { get; set; }
         decimal? Price { get; set; }
    }
    public class PurchaseDTO: NonPersistentBaseObject,IStock
    {

        decimal? change;
        decimal? amount;
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
            get => amount;
            set
            {
                if (amount == value)
                    return;
                amount = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        
        public decimal? Change
        {
            get => change;
            set
            {
                if (change == value)
                    return;
                change = value;
                OnPropertyChanged(nameof(Change));
            }
        }
        

    }
}
