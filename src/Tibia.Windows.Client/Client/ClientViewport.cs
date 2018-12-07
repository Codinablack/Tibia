﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Windows.Client
{
    /// <summary>
    /// Entire state of the client in memory, including map, creatures, skills etc.
    /// </summary>
    public class ClientViewport
    {
        public TibiaGameProtocol Protocol;
        public TibiaGameData GameData;

        // Client state

        #region Member Data

        /// <summary>
        /// All known creatures (150 max before 7.8 (?), 250 after that)
        /// </summary>
        public Dictionary<UInt32, ClientCreature> Creatures = new Dictionary<UInt32, ClientCreature>();

        /// <summary>
        /// Our VIP list, we must store full creature as the ID is used for knowing if we should display extra info
        /// </summary>
        public Dictionary<UInt32, ClientCreature> VIPList = new Dictionary<UInt32, ClientCreature>();

        /// <summary>
        /// ;
        /// </summary>
        public List<ClientItem> Inventory = new List<ClientItem>(12);

        /// <summary>
        /// Open containers
        /// </summary>
        public Dictionary<int, ClientContainer> Containers = new Dictionary<int, ClientContainer>();

        /// <summary>
        /// Our avatar on the map, will also be in the Creatures list.
        /// </summary>
        public ClientPlayer Player;

        /// <summary>
        /// Current position of the view on the map
        /// </summary>
        public MapPosition ViewPosition;

        /// <summary>
        /// The map around the player, does not handle the mini-map
        /// </summary>
        public ClientMap Map = new ClientMap();

        /// <summary>
        /// All open channels
        /// </summary>
        public Dictionary<UInt16, ClientChannel> Channels = new Dictionary<UInt16, ClientChannel>();
       
        /// <summary>
        /// The default chat channel
        /// </summary>
        public ClientChannel DefaultChannel;

        #endregion

        #region Events

        public delegate void LoginEvent(ClientViewport Viewport);
        public delegate void ContainerEvent(ClientViewport Viewport, ClientContainer Container);
        public delegate void VIPEvent(ClientViewport Viewport, ClientCreature Creature);

        /// <summary>
        /// 
        /// </summary>
        public event LoginEvent Login;

        /// <summary>
        /// Fired when a new container is opened.
        /// Do not store the ClientContainer as it might be replace
        /// be calls to UpdateContainer, instead store the ContanerID.
        /// </summary>
        public event ContainerEvent OpenContainer;

        /// <summary>
        /// Fired when a container has been updated.
        /// Look at the ContainerID of the passed Container to figure out what
        /// container changed, do NOT use the ClientContainer as it might be an
        /// entirely new container.
        /// </summary>
        public event ContainerEvent UpdateContainer;

        /// <summary>
        /// A container was closed, check the ContainerID to figure out which one it was.
        /// </summary>
        public event ContainerEvent CloseContainer;

        /// <summary>
        /// Fired when a VIP is added, logs in or logs out.
        /// </summary>
        public event VIPEvent VIPStatusChanged;

        #endregion

        public ClientViewport(TibiaGameData GameData, TibiaGameProtocol Protocol)
        {
            this.GameData = GameData;
            this.Protocol = Protocol;
            DefaultChannel = new ClientChannel(0, "Default");
            this.Channels[0] = DefaultChannel;

            for (int i = 0; i < 12; ++i)
                Inventory.Add(null);

            Protocol.PlayerLogin.Add(OnPlayerLogin);
            Protocol.UpdateSkills.Add(OnUpdateSkills);
            Protocol.UpdateStats.Add(OnUpdateStats);
            Protocol.PlayerIcons.Add(OnPlayerIcons);

            Protocol.MapDescription.Add(OnMapDescription);
            Protocol.MoveNorth.Add(OnMapDescription);
            Protocol.MoveWest.Add(OnMapDescription);
            Protocol.MoveSouth.Add(OnMapDescription);
            Protocol.MoveEast.Add(OnMapDescription);
            Protocol.FloorUp.Add(OnMapDescription);
            Protocol.FloorDown.Add(OnMapDescription);

            Protocol.AddThing.Add(OnAddThing);
            Protocol.RemoveThing.Add(OnRemoveThing);
            Protocol.RefreshTile.Add(OnRefreshTile);
            Protocol.TransformThing.Add(OnTransformThing);

            Protocol.CreatureMove.Add(OnCreatureMove);
            Protocol.CreatureTurn.Add(OnCreatureTurn);
            Protocol.CreatureSpeed.Add(OnCreatureSpeed);
            Protocol.CreatureShield.Add(OnCreatureShield);
            Protocol.CreatureSkull.Add(OnCreatureSkull);
            Protocol.CreatureHealth.Add(OnCreatureHealth);
            Protocol.CreatureTurn.Add(OnCreatureLight);

            Protocol.CreatureSpeak.Add(OnCreatureSpeak);
            Protocol.OpenChannel.Add(OnOpenChannel);
            Protocol.OpenPrivateChat.Add(OnOpenPrivateChat);
            Protocol.TextMessage.Add(OnTextMessage);

            Protocol.VIPState.Add(OnVIPState);
            Protocol.VIPLogin.Add(OnVIPLogin);
            Protocol.VIPLogout.Add(OnVIPLogout);

            Protocol.UpdateInventory.Add(OnUpdateInventory);
            Protocol.ClearInventory.Add(OnClearInventory);
            Protocol.OpenContainer.Add(OnOpenContainer);
            Protocol.CloseContainer.Add(OnCloseContainer);
            Protocol.ContainerAddItem.Add(OnContainerAddItem);
            Protocol.ContainerRemoveItem.Add(OnContainerRemoveItem);
            Protocol.ContainerTransformItem.Add(OnContainerTransformItem);
        }

        /// <summary>
        /// Returns the creature with this creature id, or null if none is found.
        /// </summary>
        /// <param name="CreatureID"></param>
        /// <returns></returns>
        private ClientCreature GetCreature(UInt32 CreatureID)
        {
            if (Creatures.ContainsKey(CreatureID))
                return Creatures[CreatureID];
            return null;
        }

        #region Player

        private void OnPlayerLogin(Packet props)
        {
            Player = (ClientPlayer)props["Player"];
            if (Login != null)
                Login(this);
        }

        private void OnUpdateSkills(Packet props)
        {
            if (Player == null)
                throw new ProtocolException("Received skill update before PlayerLogin");

            Player.Skill["Fist"].Value   = (int)props["Fist"];
            Player.Skill["Club"].Value   = (int)props["Club"];
            Player.Skill["Sword"].Value  = (int)props["Sword"];
            Player.Skill["Axe"].Value    = (int)props["Axe"];
            Player.Skill["Dist"].Value   = (int)props["Dist"];
            Player.Skill["Shield"].Value = (int)props["Shield"];
            Player.Skill["Fish"].Value   = (int)props["Fish"];

            Player.Skill["Fist"].Percent   = (int)props["FistPercent"];
            Player.Skill["Club"].Percent   = (int)props["ClubPercent"];
            Player.Skill["Sword"].Percent  = (int)props["SwordPercent"];
            Player.Skill["Axe"].Percent    = (int)props["AxePercent"];
            Player.Skill["Dist"].Percent   = (int)props["DistPercent"];
            Player.Skill["Shield"].Percent = (int)props["ShieldPercent"];
            Player.Skill["Fish"].Percent   = (int)props["FishPercent"];
        }

        private void OnUpdateStats(Packet props)
        {
            if (Player == null)
                throw new ProtocolException("Received stat update before PlayerLogin");

            Player.Health = (int)props["Health"];
            Player.MaxHealth = (int)props["MaxHealth"];
            Player.Mana = (int)props["Mana"];
            Player.MaxMana = (int)props["MaxMana"];
            Player.Capacity = (int)props["Capacity"];
            Player.Experience = (int)props["Experience"];

            Player.Level.Value = (int)props["Level"];
            Player.Level.Percent = (int)props["LevelPercent"];
            Player.MagicLevel.Value = (int)props["MagicLevel"];
            Player.MagicLevel.Percent = (int)props["MagicLevelPercent"];
        }

        private void OnPlayerIcons(Packet props)
        {
            Player.Conditions = (ConditionState)props["ConditionState"];
        }

        #endregion

        #region Map

        private void OnAddThing(Packet props)
        {
            MapPosition Position = (MapPosition)props["Position"];
            ClientThing Thing = (ClientThing)props["Thing"];
            Boolean Push = (Boolean)props["Push"];
            Map[Position].Add(Thing, Push);
        }

        private void OnRemoveThing(Packet props)
        {
            MapPosition Position = (MapPosition)props["Position"];
            int StackIndex = (int)props["StackIndex"];
            Map[Position].Remove(StackIndex);
        }

        private void OnRefreshTile(Packet props)
        {
            ClientTile Tile = Map[(MapPosition)props["Position"]];
            Boolean Clear = (Boolean)props["Clear"];

            if (Clear)
                Tile.Objects.Clear();
            else
                Map[Tile.Position] = (ClientTile)props["NewTile"];
        }

        private void OnTransformThing(Packet props)
        {
            MapPosition Position = (MapPosition)props["Position"];
            int StackIndex = (int)props["StackIndex"];
            ClientThing Thing = (ClientThing)props["Thing"];

            if (props.Has("Direction"))
                if (Thing is ClientCreature)
                    ((ClientCreature)Thing).Direction = (Direction)props["Direction"];

            Map[Position].Replace(StackIndex, Thing);
        }

        private void OnMapDescription(Packet props)
        {
            ViewPosition = (MapPosition)props["Center"];
            List<ClientTile> tiles = (List<ClientTile>)props["Tiles"];

            Map.Clear(ViewPosition);
            foreach (ClientTile Tile in tiles)
            {
                if (Tile != null)
                {
                    ProbeTileForCreatures(Tile);
                    Map[Tile.Position] = Tile;
                }
            }
        }

        private void ProbeTileForCreatures(ClientTile Tile)
        {
            if (Tile.CreatureCount == 0)
                return;

            foreach (ClientThing Thing in Tile.Objects)
            {
                if (Thing is ClientCreature)
                {
                    ClientCreature Creature = (ClientCreature)Thing;
                    if (!Creatures.ContainsKey(Creature.ID))
                        Creatures.Add(Creature.ID, Creature);
                }
            }
        }

        #endregion

        #region Creature

        private void OnCreatureMove(Packet props)
        {
            MapPosition OldPosition = (MapPosition)props["OldPosition"];
            int StackIndex = (int)props["OldStackIndex"];
            MapPosition ToPosition = (MapPosition)props["Position"];

            ClientTile FromTile = Map[OldPosition];
            ClientTile ToTile = Map[ToPosition];

            if (FromTile == null || ToTile == null)
            {
                Log.Warning("OnCreatureMove - Tile is missing.");
                return;
            }

            ClientCreature Creature = (ClientCreature)FromTile.GetByIndex(StackIndex);
            FromTile.Remove(Creature);
            ToTile.Add(Creature, true);

            Creature.Move(OldPosition, ToPosition);
        }

        private void OnCreatureTurn(Packet props)
        {
            ClientCreature Creature = (ClientCreature)props["Creature"];
            Creature.Direction = (Direction)props["Direction"];
        }

        private void OnCreatureHealth(Packet props)
        {
            ClientCreature Creature = (ClientCreature)props["Creature"];
            Creature.Health = (int)props["Health"];
        }

        private void OnCreatureLight(Packet props)
        {
            ClientCreature Creature = (ClientCreature)props["Creature"];
            Creature.Light.Color = (int)props["Color"];
            Creature.Light.Level = (int)props["Level"];
        }

        private void OnCreatureSpeed(Packet props)
        {
            ClientCreature Creature = (ClientCreature)props["Creature"];
            Creature.Speed = (int)props["Speed"];
        }

        private void OnCreatureShield(Packet props)
        {
            ClientCreature Creature = (ClientCreature)props["Creature"];
            Creature.Shield = (PartyShield)props["PartyShield"];
        }

        private void OnCreatureSkull(Packet props)
        {
            ClientCreature Creature = (ClientCreature)props["Creature"];
            Creature.Skull = (CreatureSkull)props["Skull"];
        }

        #endregion

        #region VIP

        private void OnVIPState(Packet props)
        {
            ClientCreature Creature = null;
            if (!VIPList.TryGetValue((UInt32)props["CreatureID"], out Creature))
            {
                Creature = new ClientCreature((UInt32)props["CreatureID"]);
                VIPList[Creature.ID] = Creature;
            }
            Creature.Name = (String)props["Name"];
            Creature.Online = (Boolean)props["Online"];
            if (VIPStatusChanged != null)
                VIPStatusChanged(this, Creature);
        }

        private void OnVIPLogin(Packet props)
        {
            ClientCreature Creature = VIPList[(UInt32)props["CreatureID"]];
            if (Creature != null)
            {
                Creature.Online = true;
                if (VIPStatusChanged != null)
                    VIPStatusChanged(this, Creature);
            }
            else
                Log.Warning("Receieved vip login for unknown creature (" + props["CreatureID"] + ")");
        }

        private void OnVIPLogout(Packet props)
        {
            ClientCreature Creature = VIPList[(UInt32)props["CreatureID"]];
            if (Creature != null)
            {
                Creature.Online = false;
                if (VIPStatusChanged != null)
                    VIPStatusChanged(this, Creature);
            }
            else
                Log.Warning("Receieved vip login for unknown creature (" + props["CreatureID"] + ")");
        }
        
        #endregion

        #region Chat

        private void OnCreatureSpeak(Packet props)
        {
            ClientMessage Message = (ClientMessage)props["Message"];

            //Log.Debug(Message.Speaker + ": " + Message.Text, this);

            ClientChannel Channel = DefaultChannel;
            if (props["ChannelID"] != null && Channels.ContainsKey((UInt16)props["ChannelID"]))
            {
                Channel = Channels[(UInt16)props["ChannelID"]];
            }
            Channel.Add(Message);
        }

        private void OnOpenChannel(Packet props)
        {
            UInt16 ChannelID = (UInt16)props["ChannelID"];
            String ChannelName = (String)props["ChannelName"];

            Channels[ChannelID] = new ClientChannel(ChannelID, ChannelName);
        }

        private void OnOpenPrivateChat(Packet props)
        {
        }

        private void OnTextMessage(Packet props)
        {
            ClientMessage Message = (ClientMessage)props["Message"];
            DefaultChannel.Add(Message);
            //Log.Debug(Message.Text, this);
        }

        #endregion

        #region Inventory

        private void OnUpdateInventory(Packet props)
        {
            int slot = (int)props["Slot"];
            Inventory[slot] = (ClientItem)props["Thing"];
        }

        private void OnClearInventory(Packet props)
        {
            int slot = (int)props["Slot"];
            Inventory[slot] = null;
        }

        private void OnOpenContainer(Packet props)
        {
            int ContainerID = (int)props["ContainerID"];
            ClientContainer Container = (ClientContainer)props["Thing"];
            Container.Name = (String)props["Name"];
            Container.MaximumVolume = (int)props["Volume"];
            Container.HasParent = (bool)props["IsChild"];
            Container.Contents = (List<ClientItem>)props["Contents"];

            bool update = Containers.ContainsKey(ContainerID);

            Containers[ContainerID] = Container;

            // Dispatch an event
            if (update)
            {
                if (UpdateContainer != null)
                    UpdateContainer(this, Container);
            }
            else
            {
                if (OpenContainer != null)
                    OpenContainer(this, Container);
            }
        }

        private void OnCloseContainer(Packet props)
        {
            // Server might send CloseContainer several times
            int ContainerID = (int)props["ContainerID"];
            if (!Containers.ContainsKey(ContainerID))
                return;

            // Remove it from the viewport
            ClientContainer Container = Containers[ContainerID];
            Containers.Remove(ContainerID);

            // Dispatch an event
            if (CloseContainer != null)
                CloseContainer(this, Container);
        }

        private void OnContainerAddItem(Packet props)
        {
            int ContainerID = (int)props["ContainerID"];
            ClientContainer Container = Containers[ContainerID];
            ClientItem item = (ClientItem)props["Item"];

            // Items are always added on index 0
            Container.Contents.Insert(0, item);

            // Dispatch an event
            if (UpdateContainer != null)
                UpdateContainer(this, Container);
        }

        private void OnContainerTransformItem(Packet props)
        {
            int ContainerID = (int)props["ContainerID"];
            ClientContainer Container = Containers[ContainerID];
            int slot = (int)props["Slot"];
            ClientItem item = (ClientItem)props["Item"];

            Container.Contents[slot] = item;

            // Dispatch an event
            if (UpdateContainer != null)
                UpdateContainer(this, Container);
        }

        private void OnContainerRemoveItem(Packet props)
        {
            int ContainerID = (int)props["ContainerID"];
            ClientContainer Container = Containers[ContainerID];
            int slot = (int)props["Slot"];

            // Just remove it
            Container.Contents.RemoveAt(slot);

            // Dispatch an event
            if (UpdateContainer != null)
                UpdateContainer(this, Container);
        }

        #endregion

    }
}
