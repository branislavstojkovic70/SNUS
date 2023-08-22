using Microsoft.AspNetCore.SignalR;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Hubs
{
    public class AlarmHub : Hub
    {
        public async Task SendAlarm(Alarm alarm)
        {
            await Clients.All.SendAsync("ReceiveAlarm", alarm);
        }
    }
}
