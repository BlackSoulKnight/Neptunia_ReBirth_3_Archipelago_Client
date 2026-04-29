using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Nep3ArchipelagoClient.src.Hooks;

namespace Nep3ArchipelagoClient.Archipelago
{
    public class APClient
    {
        public const long TreasureBaseID = 1_000_000;
        public const long DungeonBaseID = 2_000_000;
        public const long EnemyBaseID = 3_000_000;
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

        public void GetItemName(long id,ref byte[] output)
        {
            for (int i = 0; i< output.Length; i++)
            {
                output[i] = 0;
            }

            if (IsConnected)
            {
                var result = Session.Locations.ScoutLocationsAsync(id).Result;
                if (!result.ContainsKey(id))
                {
                    "Location not Found"u8.ToArray().CopyTo(output, 0);
                    return;
                }
                var itemName = result[id].ItemName;
                int idx = 0;
                if (String.IsNullOrEmpty(itemName))
                    "No Itemname Found"u8.ToArray().CopyTo(output, 0);
                else
                    foreach (char c in itemName)
                    {
                        if (!(idx < output.Length)) break;
                        output[idx] = ((byte)c);
                        idx++;
                    }
            }
            else
            {
                "Not Connected"u8.ToArray().CopyTo(output, 0);
            }
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
        public static bool collectedFirstItem = false;
        public void update()
        {
            if (IsConnected && collectedFirstItem)
            {
                if (CurrentItemNR < Session.Items.AllItemsReceived.Count)
                {
                    var itemId = Session.Items.AllItemsReceived[(int)CurrentItemNR].ItemId;
                    if (itemId > DungeonBaseID && itemId < DungeonBaseID + 1_000_000)
                        Mod.SaveGame.AddDungeon((byte)(itemId - DungeonBaseID));
                    else
                        ItemCollection._addItemFunction.GetWrapper()((uint)itemId, 1, (char)1);
                    CurrentItemNR++;
                }
            }
        }
    }
}
