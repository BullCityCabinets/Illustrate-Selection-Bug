using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace WTFSelections
{
    public class InventoryVM : INotifyPropertyChanged
    {
        //~~ Data Source...                
        public ObservableCollection<Inventory> MyInventory { get; set; }        

        ObservableCollection<object> _mySelectedItems { get; set; } 
        public ObservableCollection<object> MySelectedItems 
        {
            get => _mySelectedItems;
            set
            {
                if (_mySelectedItems != value)
                {
                    _mySelectedItems = value;
                    OnPropertyChanged("MySelectedItems");
                }
            }
        }



        public InventoryVM() 
        {         
            // Data Source...
            MyInventory = new ObservableCollection<Inventory>();
            foreach (var item in App.InventoryRepo) { MyInventory.Add(item); }

            // Preselected Items...
            MySelectedItems = new ObservableCollection<object>();

            // Preselected Items... Get list from App static lists (used to share across View Models)
            var itemsToPreselect = new List<Inventory>();                        
            for (int i = 0; i < App.OrderItemSkus.Count; i++)
            {
                var inv = App.InventoryRepo.FirstOrDefault(inv => inv.Sku == App.OrderItemSkus[i]);
                itemsToPreselect.Add(inv);
            }

            // Preselected Items... use index of items in list bound to view in order to add them to list of highlighted items
            if (itemsToPreselect.Count() > 0)
            {
                foreach (Inventory itemToPreselect in itemsToPreselect)
                {
                    foreach (Inventory displayedItem in this.MyInventory)
                    {
                        if (itemToPreselect.Sku == displayedItem.Sku)
                        {
                            var indexToSelect = this.MyInventory.IndexOf(displayedItem);
                            MySelectedItems.Add(this.MyInventory[indexToSelect]);
                        }
                    }
                }
            }

            App.OrderUpateAttempts = 0; // To illustrate bug.... updates "OrderSelectionChangedCount" displayed at top of page
            MessagingCenter.Subscribe<OrderVM, object>(this, "OrderUpdatedCount", OnOrderUpdated); 

        }



        //public ICommand MySelectionChangedCommand => new Command(OnMySelectionChangedCommand);
        public void OnMySelectionChangedCommand(object sender, SelectionChangedEventArgs e)
        {
            InventorySelectionChangedCount++;

            if (!App.PageIsLoading)
            {
                int skuChanged;
                bool toBeAdded;

                if (e.PreviousSelection.Count + e.CurrentSelection.Count != 0) //Check for misfire due to ....?
                {
                    if (e.PreviousSelection.Count < e.CurrentSelection.Count) // add item ...
                    {
                        skuChanged = ((e.CurrentSelection.Except(e.PreviousSelection).ToList()[0]) as Inventory).Sku; // ... should only be 1 item in list
                        toBeAdded = true;
                    }
                    else // remove item ...
                    {
                        skuChanged = ((e.PreviousSelection.Except(e.CurrentSelection).ToList()[0]) as Inventory).Sku; // ... should only be 1 item in list
                        toBeAdded = false;
                    }

                    var ResolveData = new object[] { skuChanged, toBeAdded };
                    MessagingCenter.Send<InventoryVM, object>(this, "UpdateOrder", ResolveData);

                }

            }

        }




        // for Debugging...
        private int inventorySelectionChangedCount { get; set; } = 0;
        public int InventorySelectionChangedCount
        {
            get => inventorySelectionChangedCount;
            set
            {
                inventorySelectionChangedCount = value;
                OnPropertyChanged("InventorySelectionChangedCount");

            }
        }

        private int orderSelectionChangedCount { get; set; } = 0; 
        public int OrderSelectionChangedCount
        {
            get => orderSelectionChangedCount;
            set
            {
                orderSelectionChangedCount = value;
                OnPropertyChanged("OrderSelectionChangedCount");
            }
        }

        private void OnOrderUpdated(OrderVM sender, object count)
        {
            OrderSelectionChangedCount = (int)count; // Received from OrderVM
        }




        // Data Source...
        public ObservableCollection<Inventory> GetInventory()
        {
            var inv = new ObservableCollection<Inventory>();
            foreach (var item in App.InventoryRepo) { MyInventory.Add(item); }
            return inv;

        }

        // INotifyPropertyChanged...
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}
