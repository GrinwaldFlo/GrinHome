using Microsoft.AspNetCore.Components;

namespace GrinHome.Pages.Shared
{

    public partial class NavMenu : ComponentBase
    {
        [Inject] NavigationManager Nav { get; set; } = null!;

        private bool collapseNavMenu = true;

        public string SelectedItem { get; private set; } = "";

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            SelectedItem = Nav.Uri.Replace(Nav.BaseUri, "");
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        protected override bool ShouldRender()
        {
      
            return base.ShouldRender();
        }

        
    }
}
