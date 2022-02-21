using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace DemoBlazorSignalR_ServerSide.Pages
{
    public partial class Chat
    {
        List<string> messages = new List<string>();

        private HubConnection _hubConnection;
        [Inject]
        public NavigationManager navigation { get; set; }

        [Inject]
        public IJSRuntime js { get; set; }

        public string UserName { get; set; }
        public string Message { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(navigation.ToAbsoluteUri("/chat")).Build();

            _hubConnection.On<string, string>("FromServer", async (userName, message) =>
            {
                string CompleteMessage = userName + " : " + message;
                messages.Add(CompleteMessage);

                await js.InvokeVoidAsync("alert", "vous avez un nouveau message");

                StateHasChanged();
            });

            await _hubConnection.StartAsync();
        }

        public async Task Send()
        {
            await _hubConnection.SendAsync("FromClient", UserName, Message);
        }

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;
    }
}
