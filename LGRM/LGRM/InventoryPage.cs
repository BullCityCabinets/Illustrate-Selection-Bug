using Xamarin.Forms;

namespace WTFSelections
{
    public class InventoryPage : ContentPage
    {
        public InventoryPage()
        {
            BindingContext = new InventoryVM();

            Title = "Inventory Selection";
            var MainStack = new StackLayout();

            var debugHeaderA = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 12  };
            var debugLabelA1 = new Label() { Text = "Inventory's SelectionChanged... "};
            var debugLabelA2 = new Label();
            debugLabelA2.SetBinding( Label.TextProperty, "InventorySelectionChangedCount");
            var debugLabelA3 = new Label() { Text = " times!" };

            var debugHeaderB = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = 12 };
            var debugLabelB1 = new Label() { Text = "Order's SelectionChanged... " };
            var debugLabelB2 = new Label();
            debugLabelB2.SetBinding(Label.TextProperty, "OrderSelectionChangedCount");
            var debugLabelB3 = new Label() { Text = " times !" };


            debugHeaderA.Children.Add(debugLabelA1);
            debugHeaderA.Children.Add(debugLabelA2);
            debugHeaderA.Children.Add(debugLabelA3);

            debugHeaderB.Children.Add(debugLabelB1);
            debugHeaderB.Children.Add(debugLabelB2);
            debugHeaderB.Children.Add(debugLabelB3);

            var head = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var Name1Head = new Label { Text = "Name1", FontAttributes = FontAttributes.Bold, TextDecorations = TextDecorations.Underline, WidthRequest = 150, Margin = new Thickness(30, 0, 0, 0) };
            var SkuHead = new Label { Text = "Sku", FontAttributes = FontAttributes.Bold, TextDecorations = TextDecorations.Underline, WidthRequest = 50 };

            head.Children.Add(Name1Head);
            head.Children.Add(SkuHead);

            var cvInventory = new CollectionView() { SelectionMode = SelectionMode.Multiple };
            cvInventory.SetBinding(CollectionView.ItemsSourceProperty, "MyInventory");
            cvInventory.SetBinding(CollectionView.SelectedItemsProperty, "MySelectedItems");
            cvInventory.SetBinding(CollectionView.SelectionChangedCommandProperty, "MySelectionChangedCommand");
            cvInventory.SelectionChanged += (BindingContext as InventoryVM).OnMySelectionChangedCommand;

            cvInventory.ItemTemplate = new DataTemplate(() =>
            {

                var slLabels = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = new Thickness(8) };

                var lName1 = new Label { FontAttributes = FontAttributes.Bold, WidthRequest = 150, Margin = new Thickness(30,0,0,0) };
                lName1.SetBinding(Label.TextProperty, "Name1");

                var lSku = new Label { FontAttributes = FontAttributes.Bold, WidthRequest = 50 };
                lSku.SetBinding(Label.TextProperty, "Sku");

                slLabels.Children.Add(lName1);
                slLabels.Children.Add(lSku);

                return slLabels;

            }); // ... end DataTemplate
            
            MainStack.Children.Add(debugHeaderA);
            MainStack.Children.Add(debugHeaderB);
            cvInventory.Header = head; //MainStack.Children.Add(head);
            MainStack.Children.Add(cvInventory);
            Content = MainStack;

        }



    }
}