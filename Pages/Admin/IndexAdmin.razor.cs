using GrinHome.Data.Services;
using Microsoft.AspNetCore.Components;

namespace GrinHome.Pages.Admin
{
    public partial class IndexAdmin : ComponentBase
    {
        public string? Email { get; set; }

        [Inject] EmailSender emailSender { get; set; } = null!;

        private void CloseApplication()
        {
            Environment.Exit(0);
        }


        private void SendTestMail()
        {
            if (string.IsNullOrEmpty(Email))
                return;

            //emailSender.Execute("Subject", "Hello world", )

        }
    }
}
