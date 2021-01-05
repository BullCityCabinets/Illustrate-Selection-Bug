using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace WTFSelections
{
    public partial class App : Application
    {
        // Test code
        public static ObservableCollection<Inventory> InventoryRepo { get; set; }
        public static List<int> OrderItemSkus { get; set; }
        public static int OrderUpateAttempts { get; set; }






        //////public static SQLiteDataService MySQLite { get; } = new SQLiteDataService();
        //////public static VersionService V { get; } = new VersionService(new MockRemoteDataService());
        //////public static ObservableCollection<Grocery> Groceries { get; set; }
        //////public static Dictionary<int, Kind> IngredientScratchList { get; set; }


        public static bool PageIsLoading { get; set; }

        public App()
        {
            //////CompareVersion();

            InitializeComponent();


            InventoryRepo = new ObservableCollection<Inventory>() 
            {
                new Inventory(){ Name1 = "Product #11", Sku = 11  },
                new Inventory(){ Name1 = "Product #22", Sku = 22  },
                new Inventory(){ Name1 = "Product #33", Sku = 33  },
                new Inventory(){ Name1 = "Product #44", Sku = 44  },
                new Inventory(){ Name1 = "Product #55", Sku = 55  },                                
                new Inventory(){ Name1 = "Product #66", Sku = 66  },
                new Inventory(){ Name1 = "Product #77", Sku = 77  },
                new Inventory(){ Name1 = "Product #88", Sku = 88  },
                new Inventory(){ Name1 = "Product #99", Sku = 99  },
                new Inventory(){ Name1 = "Product #111", Sku = 111 },
                new Inventory(){ Name1 = "Product #222", Sku = 222 },
                new Inventory(){ Name1 = "Product #333", Sku = 333 },
                new Inventory(){ Name1 = "Product #444", Sku = 444 },
                new Inventory(){ Name1 = "Product #555", Sku = 555 },
                new Inventory(){ Name1 = "Product #666", Sku = 666 },
                new Inventory(){ Name1 = "Product #777", Sku = 777 },
                new Inventory(){ Name1 = "Product #888", Sku = 888 },
                new Inventory(){ Name1 = "Product #999", Sku = 999 },
                new Inventory(){ Name1 = "Product #1111", Sku = 1111 },
                new Inventory(){ Name1 = "Product #2222", Sku = 2222 }

            };

            OrderUpateAttempts = 0;

            MainPage = new NavigationPage(new HomePage());

        }


        //////public void CompareVersion()
        //////{
        //////    if (!V.DbIsUpdated) // Install SQLites Groceries catalog
        //////    {
        //////        var createTable = Task.Run(() => MySQLite.CreateTableOfGroceriesAsync());
        //////        createTable.Wait();

        //////        var populateTableFromJson = Task.Run(() => MySQLite.PopulateTableOfGroceriesAsync(V.ShippedCatalog));
        //////        populateTableFromJson.Wait();

        //////        V.UpdateVersion();

        //////    }

        //////}

    }
}
