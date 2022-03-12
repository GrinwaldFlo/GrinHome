using GrinHome.Data.Services;
using Microsoft.AspNetCore.Components;

namespace GrinHome.Pages.Admin
{
    public partial class IndexAdmin : ComponentBase
    {
        public string? Email { get; set; }
        private string? emailResult;

        [Inject] EmailSender EmailSender { get; set; } = null!;

        private static void CloseApplication()
        {
            Environment.Exit(0);
        }


        private async void SendTestMail()
        {
            if (string.IsNullOrEmpty(Email))
            {
                emailResult = ""; 
                return;
            }
            emailResult = "Please wait";
            StateHasChanged();
            await Task.Delay(1);
            emailResult = await EmailSender.Execute("Subject", "Hello world<br>Nulla suscipit, magna nec ornare auctor, tellus orci viverra erat, at semper justo est id ligula. Sed maximus nisl sem, sed porta ipsum cursus a. Nullam vel leo urna. Quisque placerat volutpat eleifend. Quisque quis ex suscipit, venenatis lorem quis, aliquet diam. Vivamus felis erat, semper eu pellentesque eu, venenatis eget arcu. Pellentesque sit amet quam vitae risus fringilla varius et ac magna. Donec sagittis nulla in erat ultricies finibus. Suspendisse egestas, diam at venenatis porta, ipsum nunc commodo turpis, a blandit ante diam ut nisl. Nam laoreet dui id mollis congue. Morbi aliquam consectetur magna."
                , "Test User", Email, "GrinHome");
            StateHasChanged();
        }
    }
}
