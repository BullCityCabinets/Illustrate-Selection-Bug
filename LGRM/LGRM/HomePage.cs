using Xamarin.Forms;

namespace WTFSelections
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {

            BindingContext = this;

            Title = "Home Page";
            var MainStack = new StackLayout();

            var blurb = new Label()
            {
                Margin = 10, TextColor=Color.Black,
                Text = 
                " To see bug in action:\n " +
                "  1) Navigate to Inventory Page...\n" +
                "   2) Return here... \n" +
                "   3) Again, navigate to Inventory Page and select an item!\n \n" +
                "   For each time this process is repeated, \n" +
                "     the Order View Model will \n " +
                "    fire it's update method for additional turn! \n \n" +
                "   Weirder even, is that the Order's list of items\n" +
                "     for the Collection View ( OrderVM.OrderItemsDisplayed )\n" +
                "     will reatin the number of the first instance, \n" +
                "     even though every OrderVM is ''new'' from OrderPage.cs"
                
            };

            var navToOrder = new Button() { Text = "Create new order..." };
            navToOrder.Command = new Command(async () => {
                await Application.Current.MainPage.Navigation.PushAsync(new OrderPage());
            });


            MainStack.Children.Add(navToOrder);
            MainStack.Children.Add(blurb);
            Content = MainStack;
        }
    }
}