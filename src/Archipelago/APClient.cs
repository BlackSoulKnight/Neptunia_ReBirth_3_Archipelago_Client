using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using Nep3ArchipelagoClient.Neptunia_Data;
using Newtonsoft.Json.Linq;

namespace Nep3ArchipelagoClient.Archipelago
{
    public class APClient
    {
        //checks
        public const long TreasureBaseID = 1_000_000;
        public const long EnemyBaseID = 2_000_000;
        //items
        public const long DungeonBaseID = 2_000_000;
        const long ChracterBaseID = 3_000_000;
        const long ProgressiveGearID = 3_500_000;
        const long EventBaseID = 4_000_000;
        private ArchipelagoSession? Session;
        private LoginResult? loginResult = null;
        public bool IsConnected => Session != null && Session.Socket.Connected;
        private long PlayerID = 0;
        private Dictionary<long, ScoutedItemInfo> ItemAtLocation = new();
        internal int StartingCharacter = 1;
        internal HashSet<long> CheckedLocation = new();
        public string Game = "Hyperdimension Neptunia Re;Birth3 V GENERATION";
        public bool ConnectToServer(string destination,int port,string user, string password = "")
        {
            if (Session != null && Session.Socket.Connected)
            {
                Session.Socket.DisconnectAsync().Wait();
            }
            Session = ArchipelagoSessionFactory.CreateSession(destination, port);
            try
            {
                loginResult = Session.TryConnectAndLogin(Game, user, ItemsHandlingFlags.AllItems, password: password);
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
            InitalizeItemNameLookup();
            InitSlotData(success.SlotData);
            if(success.SlotData.ContainsKey("options"))
                Mod.SaveGame.Options.ParseOptions((JObject)success.SlotData["options"]);
            return true;
        }
        private void InitSlotData(Dictionary<string,object> slotData)
        {
            if (slotData.ContainsKey("start_character"))
                StartingCharacter = (int)(long)slotData["start_character"];
        }

        private async void InitalizeItemNameLookup()
        {
            long[] locations = Session.Locations.AllLocations.ToArray();
            ItemAtLocation = new(await Session.Locations.ScoutLocationsAsync(locations));
        }
        public void SaveEvent(short id)
        {
            if (IsConnected)
            {
                Session.DataStorage[Scope.Slot, $"Event {id}"] = true;
                Mod.SaveGame.Events.UnlockedEvents.Add(id);
            }
        }
        public bool CheckEvent(short id)
        {
            if (IsConnected)
            {
                return (bool)Session.DataStorage[Scope.Slot, $"Event {id}"];
            }
            return false;
        }
        void UpdateEventStorage()
        {
            var evnt = Mod.SaveGame.Events;
            foreach (var eventId in evnt.GetUnlockableEvents)
            {
                if (evnt.UnlockedEvents.Contains(eventId))
                    continue;
                if((bool)Session.DataStorage[Scope.Slot, $"Event {eventId}"])
                    evnt.UnlockedEvents.Add(eventId);
            }
        }

        internal int GetStartingCharacter() => StartingCharacter;
        public string GetItemName(long id)
        {
            if (IsConnected)
            {
                if (!ItemAtLocation.ContainsKey(id))
                {
                    return "Location not Found";
                }
                var itemName = ItemAtLocation[id].ItemName;
                if (String.IsNullOrEmpty(itemName))
                    return "No Itemname Found";
                else
                    return itemName;
            }
            else
            {
                return "Not Connected";
            }
        }
        public void CheckIfGoaled(long id)
        {
            //currently only Rei kill
            if(Mod.SaveGame.GoalAchieved(id))
                Session.SetGoalAchieved();
                
        }
        public bool SendLocation(long id)
        {
            if (IsConnected)
            {
                CheckedLocation.Add(id);
                CheckIfGoaled(id);
                Session.Locations.CompleteLocationChecks(id);
                return true;
            }
            return false;
        }
        double _lastUpdate = 0;
        public void Update(double deltaTime)
        {
            if (IsConnected && Mod.SaveGame.IsInit)
            {
                _lastUpdate += deltaTime;
                int currentItemNr = Mod.SaveGame.GetCurrentApItemCount();
                if (currentItemNr < Session.Items.AllItemsReceived.Count)
                {
                    var itemId = Session.Items.AllItemsReceived[currentItemNr].ItemId;
                    if (itemId > DungeonBaseID && itemId < DungeonBaseID + 1_000_000)
                        Mod.SaveGame.AddDungeon((short)(itemId - DungeonBaseID));
                    else if (itemId > ChracterBaseID && itemId < ProgressiveGearID)
                        Mod.SaveGame.AddPartyMember((int)(itemId - ChracterBaseID));
                    else if (itemId > ProgressiveGearID && itemId < EventBaseID)
                        ProgressiveGear.ProgressiveGears[(int)(itemId - ProgressiveGearID)].IncreaseGearTier();
                    else if (itemId > EventBaseID)
                        SaveEvent((short)(itemId - EventBaseID));
                    else
                        Mod.Inventory.AddItem((int)itemId, 1);
                    Mod.SaveGame.IncrementCurrentApItemCount();
                }
                if(_lastUpdate > 1000)
                {
                    _lastUpdate = 0;
                    UpdateEventStorage();
                }
            }
        }
    }
}
