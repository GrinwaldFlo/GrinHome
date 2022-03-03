using Microsoft.AspNetCore.Components;

namespace GrinHome.Pages
{
    public partial class AppClose : ComponentBase
    {

        protected override void OnInitialized()
        {
            Environment.Exit(0);

            base.OnInitialized();
        }

    }
}
