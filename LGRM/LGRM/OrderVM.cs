using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace WTFSelections
{
    public class OrderVM : INotifyPropertyChanged
    {

        //~~ Data Source...                
        
        ObservableCollection<Inventory> orderItemsDisplayed { get; set; }
        public ObservableCollection<Inventory> OrderItemsDisplayed
        {
            get => orderItemsDisplayed;
            set
            {
                orderItemsDisplayed = value;
                OnPropertyChanged("OrderItemsDisplayed");
            }
        }

        //public ICommand MySelectionChangedCommand => new Command(OnMySelectionChangedCommand); //Binding to Selectoin ChangedCommandProperty did not solve this, for me.

        public OrderVM()
        {
            App.OrderItemSkus = new List<int>();
            this.OrderItemsDisplayed = new ObservableCollection<Inventory>();
            foreach (var item in App.OrderItemSkus)
            {
                OrderItemsDisplayed.Add(new Inventory(App.InventoryRepo.FirstOrDefault(inv => inv.Sku == item)));
            }

            MessagingCenter.Subscribe<InventoryVM, object>(this, "UpdateOrder", OnUpdateOrder);   // Updates from List of Inventory
            

        }

        public void OnUpdateOrder(object sender, object resolve)
        {
            App.OrderUpateAttempts++; //This number does not update correctly when in the buggy repeating loop

            var dataReceived = (object[])resolve;
            var sku = (int)dataReceived[0];
            var toBeAdded = (bool)dataReceived[1];
            
            var itemChanged = new Inventory(App.InventoryRepo.FirstOrDefault( inv => inv.Sku == sku)); // Use Sku sent to build the item to add

            if (toBeAdded)
            {
                if (!App.OrderItemSkus.Contains(itemChanged.Sku))
                {
                    App.OrderItemSkus.Add(itemChanged.Sku);
                    OrderItemsDisplayed.Add(itemChanged);   //This retains the count from the first Order View Model, even though this is a new instance!
                }
            }
            else  // Remove
            {
                if (App.OrderItemSkus.Contains(itemChanged.Sku))
                {
                    App.OrderItemSkus.Remove(App.OrderItemSkus.FirstOrDefault(sku => sku == itemChanged.Sku));
                    OrderItemsDisplayed.Remove(OrderItemsDisplayed.FirstOrDefault(ing => ing.Sku == itemChanged.Sku));
                }
            }

            MessagingCenter.Send<OrderVM, object>(this, "OrderUpdatedCount", App.OrderUpateAttempts);   // ...this does not send an accurate number
        }









        // INotifyPropertyChanged...
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}
