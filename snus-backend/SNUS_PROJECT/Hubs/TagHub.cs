using Microsoft.AspNetCore.SignalR;
using SNUS_PROJECT.DTO;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Hubs
{
    public class TagHub:Hub
    {
        public async Task SendTag(TagDto tag)
        {
            await Clients.All.SendAsync("ReceiveTag", tag);
        }
    }
}
