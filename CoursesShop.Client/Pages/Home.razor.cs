namespace CoursesShop.Client.Pages
{
    public partial class Home
    {
        protected override async void OnInitialized()
        {
            var type = await LocalStoreage.GetItemAsync<string>("type");
            if (type is not null)
            {

                NavigationManager.NavigateTo($"/{type}");

            }
            base.OnInitialized();
        }
    }
}
