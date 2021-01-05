using Xamarin.Forms;

namespace WTFSelections
{
    public class OrderPage : ContentPage
    {
        public OrderPage()
        {
            BindingContext = new OrderVM();

            Title = "Order Manifest";
            var MainStack = new StackLayout();



            var navToInventory = new Button() { Text = "Add items to order..." };
            navToInventory.Command = new Command(async () => {
                App.PageIsLoading = true;
                await Application.Current.MainPage.Navigation.PushAsync(new InventoryPage());
                App.PageIsLoading = false;
            });




            var head = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var Name1Head = new Label { Text = "Name1", FontAttributes = FontAttributes.Bold, TextDecorations = TextDecorations.Underline, WidthRequest = 150, Margin = new Thickness(30, 0, 0, 0) };
            var SkuHead = new Label { Text = "Sku", FontAttributes = FontAttributes.Bold, TextDecorations = TextDecorations.Underline, WidthRequest = 50 };
            head.Children.Add(Name1Head);
            head.Children.Add(SkuHead);





            var cvInventory = new CollectionView() { SelectionMode = SelectionMode.None, EmptyView = "No items selected..." };
            cvInventory.SetBinding(CollectionView.ItemsSourceProperty, "OrderItemsDisplayed");
            cvInventory.ItemTemplate = new DataTemplate(() =>
            {

                var slLabels = new StackLayout() { Orientation = StackOrientation.Horizontal, Margin = new Thickness(8) };

                var lName1 = new Label { FontAttributes = FontAttributes.Bold, WidthRequest = 150, Margin = new Thickness(30, 0, 0, 0) };
                lName1.SetBinding(Label.TextProperty, "Name1");

                var lSku = new Label { FontAttributes = FontAttributes.Bold, WidthRequest = 50 };
                lSku.SetBinding(Label.TextProperty, "Sku");

                slLabels.Children.Add(lName1);
                slLabels.Children.Add(lSku);

                return slLabels;

            }); // ... end DataTemplate


            MainStack.Children.Add(navToInventory);
            MainStack.Children.Add(head);            
            MainStack.Children.Add(cvInventory);

            Content = MainStack;

        }
    }

    
}
