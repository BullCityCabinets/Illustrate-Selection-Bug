using System.ComponentModel;

namespace WTFSelections
{
    public class Inventory : INotifyPropertyChanged
    {
        public string Name1 { get; set; }
        public int Sku { get; set; }
        float qty { get; set; }
        public float Qty 
        { 
            get => qty;
            set
            {
                qty = value;
                RaisePropertyChanged("Qty");
            } 
        }

        public Inventory(Inventory inventory)
        {
            this.Name1 = inventory.Name1;
            this.Sku = inventory.Sku;
        }

        public Inventory(/*string name1, int sku*/)
        {
            //Name1 = name1;
            //Sku = sku;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}


