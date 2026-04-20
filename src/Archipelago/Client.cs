using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;

namespace Nep3ArchipelagoClient.Archipelago
{
    public class Client
    {
        private ArchipelagoSession? Session;
        private LoginResult? loginResult = null;
        public bool IsConnected => Session != null && Session.Socket.Connected;
        private long PlayerID = 0;
        private long CurrentItemNR = 0;

        public bool ConnectToServer(string destination,int port,string user, string password = "")
        {
            if (Session != null && Session.Socket.Connected)
            {
                Session.Socket.DisconnectAsync().Wait();
            }
            Session = ArchipelagoSessionFactory.CreateSession(destination, port);
            try
            {
                loginResult = Session.TryConnectAndLogin("Hyperdimension Neptunia Re;Birth3 V GENERATION", user, ItemsHandlingFlags.AllItems, password: password);
            }
            catch(Exception e)
            {
                loginResult = new LoginFailure(e.GetBaseException().Message);
            }
            if (!loginResult.Successful)
            {
                LoginFailure failure = (LoginFailure)loginResult;
                string errorMessage = $"Failed to Connect as {user}:";
                foreach (string error in failure.Errors)
                {
                    errorMessage += $"\n    {error}";
                }
                foreach (ConnectionRefusedError error in failure.ErrorCodes)
                {
                    errorMessage += $"\n    {error}";
                }
                Console.WriteLine(errorMessage);
                return false;
            }
            LoginSuccessful success = (LoginSuccessful)loginResult;
            return true;
        }

        public bool SendLocation(long id)
        {
            if (IsConnected)
            {
                Session.Locations.CompleteLocationChecks(id);
                return true;
            }
            return false;
        }

        public void update()
        {
            if (IsConnected)
            {
                if (CurrentItemNR < Session.Items.AllItemsReceived.Count)
                {

                }
            }
        }
    }
}
