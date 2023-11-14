using Ingresso.Application.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Ingresso.Application.MyHubs
{
    public class GeneralHub : Hub
    {
        private readonly IUserManagementService userManagementService;

        public GeneralHub(IUserManagementService userManagementService)
        {
            this.userManagementService = userManagementService;
        }

        public async Task CheckEmailAlreadyExists(string email)
        {
            var user = await userManagementService.CheckEmailAlreadyExists(email);
            if (user.IsSucess)
            {
                await Clients.Clients(Context.ConnectionId).SendAsync("email-check-result", true);
            }
            else if (!user.IsSucess)
            {
                await Clients.Clients(Context.ConnectionId).SendAsync("email-check-result", false);
            }
        }
    }
}
