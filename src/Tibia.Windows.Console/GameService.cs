using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Tibia.Battle;
using Tibia.Combat;
using Tibia.Communications;
using Tibia.Communications.Channels;
using Tibia.Communications.Commands;
using Tibia.Creatures;
using Tibia.Data;
using Tibia.Data.Storage;
using Tibia.InteropServices;
using Tibia.Items;
using Tibia.Map;
using Tibia.Mounts;
using Tibia.Network;
using Tibia.Network.Game;
using Tibia.Network.Login;
using Tibia.Outfits;
using Tibia.Data.Providers.Ultimate;
using Tibia.Data.Providers.Ultimate.Services;
using Tibia.Data.Providers.OpenTibia;
using Tibia.Security.Cryptography;
using Tibia.Spawns;
using Tibia.Spells;
using Tibia.Vocations;
using Tibia.Windows.Console.Properties;

namespace Tibia.Windows.Console
{
    public class GameService
    {
        private readonly LoginService _loginService;
        private readonly LightInfo _lightInfo;
        private IRepository<IAccount, uint> _accountsRepository;
        private CharacterSpawn _characterSpawn;
        private ChatService _chatService;
        private CommandService _commandService;
        private CreatureSpawnService _creatureSpawnService;
        private List<GameConnection> _gameConnections;
        private TcpListener _gameListener;
        private ItemService _itemService;
        private List<LoginConnection> _loginConnections;
        private TcpListener _loginListener;
        private MonsterService _monsterService;
        private MountService _mountService;
        private NpcService _npcService;
        private Stopwatch _onlineTimer;
        private OutfitService _outfitService;
        private SpellService _spellService;
        private TileService _tileService;
        private TownService _townService;
        private VocationService _vocationService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameService" /> class.
        /// </summary>
        public GameService()
        {
            _loginService = new LoginService();
            _lightInfo = new LightInfo(LightLevel.Torch, LightColor.Default);
        }

        /// <summary>
        ///     Gets or sets the logger.
        /// </summary>
        /// <value>
        ///     The logger.
        /// </value>
        public virtual ILogger Logger { get; set; }

        /// <summary>
        ///     Registers the spawns.
        /// </summary>
        /// <param name="spawnSources">The spawn sources.</param>
        public virtual void RegisterSpawns(IEnumerable<SpawnSource> spawnSources)
        {
            foreach (SpawnSource spawnSource in spawnSources)
            foreach (MonsterSpawnSettings monsterSettings in spawnSource.Spawns.OfType<MonsterSpawnSettings>())
            {
                ITile tile = _tileService.GetTileByPosition(monsterSettings.AbsolutePosition);

                // TODO: This should throw an exception because it means it's an invalid position
                if (tile == null)
                    continue;

                MonsterSpawn monsterSpawn = new MonsterSpawn();
                monsterSpawn.Creature = monsterSettings.Monster;
                monsterSpawn.Direction = Direction.South;
                monsterSpawn.DrunkCondition = null;
                monsterSpawn.ExhaustCondition = null;
                monsterSpawn.Health = new Health
                {
                    Current = monsterSettings.Monster.MaxHealth,
                    Maximum = monsterSettings.Monster.MaxHealth
                };
                monsterSpawn.Id = _creatureSpawnService.TotalCount;
                monsterSpawn.IsInvisible = false;

                // TODO: Implement creatures that use existing outfits/mounts
                monsterSpawn.IsRiding = false;

                // TODO: Creatures and NPC's don't have a level
                monsterSpawn.Level = new Level
                {
                    Current = 1,
                    Experience = 1,
                    NextLevelExperience = 0
                };
                monsterSpawn.LightInfo = _lightInfo;
                monsterSpawn.Monster = monsterSettings.Monster;
                monsterSpawn.Mount = null;
                monsterSpawn.Outfit = null;
                monsterSpawn.PoisonCondition = null;
                monsterSpawn.RegenerationConditions = null;
                monsterSpawn.Skull = new Skull
                {
                    Time = TimeSpan.Zero,
                    Type = SkullType.None
                };
                monsterSpawn.Speed = monsterSettings.Monster.BaseSpeed;
                monsterSpawn.StackPosition = 1;
                monsterSpawn.Tile = tile;
                monsterSpawn.WarIcon = WarIcon.None;
                monsterSpawn.Outfit = monsterSpawn.Monster.Outfit;
                monsterSpawn.Mount = monsterSpawn.Monster.Mount;

                _creatureSpawnService.RegisterCreature(monsterSpawn);
                _tileService.RegisterCreature(monsterSpawn);
            }
        }

        private void InitializeTest()
        {
            World world = new World();
            world.IpAddress = "127.0.0.1";
            world.IsPreview = true;
            world.Port = 7172;
            world.Id = 0;
            world.Name = "Draconia";

            Character character = new Character();
            character.Id = 1;
            character.Name = "Chuitox";
            character.Status = SessionStatus.Online;
            character.Settings.CanBroadcast = true;
            character.Settings.CanChangeSex = true;
            character.Settings.CanSeeDiagnostics = true;
            character.Settings.CanSeeGhosts = true;
            character.Settings.CanTeleport = true;

            _characterSpawn = new CharacterSpawn();
            _characterSpawn.AccountId = 1;
            _characterSpawn.Amulet = null;
            _characterSpawn.AmuletId = null;
            _characterSpawn.AxeSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.Balance = 1000000;
            _characterSpawn.BattleStance = BattleStance.Defense;
            _characterSpawn.Belt = null;
            _characterSpawn.BeltId = null;
            _characterSpawn.Blessings = 7;
            _characterSpawn.Capacity = 300;
            _characterSpawn.Character = character;
            _characterSpawn.CharacterId = 1;
            _characterSpawn.ClubSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.Conditions = Conditions.Bleeding | Conditions.Burning | Conditions.Cursed | Conditions.Dazzled | Conditions.Drowning | Conditions.Strengthened;
            _characterSpawn.Container = null;
            _characterSpawn.ContainerId = null;
            _characterSpawn.CreationTime = DateTime.UtcNow;
            _characterSpawn.Creature = character;
            _characterSpawn.DeletionTime = null;
            _characterSpawn.Direction = Direction.East;
            _characterSpawn.DistanceSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.DrunkCondition = null;
            _characterSpawn.ExhaustCondition = null;
            _characterSpawn.Feet = null;
            _characterSpawn.FeetId = null;
            _characterSpawn.FishingSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.FistSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.FollowOpponent = true;
            _characterSpawn.FreeCapacity = 250;
            _characterSpawn.Guild = null;
            _characterSpawn.GuildLevel = 0;
            _characterSpawn.Head = null;
            _characterSpawn.HeadId = null;
            _characterSpawn.Health.Current = 100;
            _characterSpawn.Health.Maximum = 1000;
            _characterSpawn.Id = 0;
            //characterSpawn.IsDead = false;
            //characterSpawn.IsDrunk = false;
            //characterSpawn.IsExhausted = false;
            // TODO: This must be handled differently
            _characterSpawn.IsMountToggleExhausted = false;
            //characterSpawn.IsPoisoned = false;
            _characterSpawn.LastLogin.IpAddress = "127.0.0.1";
            _characterSpawn.LastLogin.Time = DateTime.UtcNow;
            _characterSpawn.LastLogout = null;
            _characterSpawn.LastPing = DateTime.MinValue;
            _characterSpawn.Legs = null;
            _characterSpawn.LegsId = null;
            _characterSpawn.Level.Current = 9;
            _characterSpawn.Level.Experience = 1000;
            _characterSpawn.Level.NextLevelExperience = 1100;
            _characterSpawn.LightInfo.Level = LightLevel.Torch;
            _characterSpawn.LightInfo.Color = LightColor.Default;
            _characterSpawn.MagicLevel.Base = 100;
            _characterSpawn.MagicLevel.Bonus = 10;
            _characterSpawn.MagicLevel.Experience = 1000;
            _characterSpawn.MagicLevel.NextLevelExperience = 1100;
            _characterSpawn.Mana.Current = 100;
            _characterSpawn.Mana.Maximum = 1000;
            _characterSpawn.Mana.Spent = 100;
            _characterSpawn.Mount = _mountService.GetMountBySpriteId(373);

            // TODO: This should be initialized by the constructor
            _characterSpawn.IsRiding = true;
            _characterSpawn.Mounts.AddRange(_mountService.GetAll());
            _characterSpawn.OfflineTraining.Elapsed = TimeSpan.Zero;
            _characterSpawn.OfflineTraining.Skill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.OnlineTime = TimeSpan.Zero;

            // TODO: Containers open
            //characterSpawn.OpenContainers = null;
            _characterSpawn.Outfit = _outfitService.GetOutfitBySpriteId(147);

            // TODO: This should be initialized by the constructor
            _characterSpawn.Outfits.AddRange(_outfitService.GetAll());
            _characterSpawn.PartyId = null;

            // TODO: This should be initialized by the constructor
            _characterSpawn.PartyInvitations = new IParty[0];
            _characterSpawn.PoisonCondition = null;

            // TODO: This should be initialized by the constructor
            _characterSpawn.Quests = new IQuestInfo[0];

            // TODO: This should be initialized by the constructor
            _characterSpawn.RegenerationConditions = new RegenerationCondition[0];
            _characterSpawn.Ring = null;
            _characterSpawn.RingId = null;
            _characterSpawn.Sex = Sex.Female;
            _characterSpawn.Shield = null;
            _characterSpawn.ShieldId = null;
            _characterSpawn.ShieldSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.Skull.Type = SkullType.None;
            _characterSpawn.Skull.Time = TimeSpan.Zero;
            _characterSpawn.Soul = 200;
            _characterSpawn.Speed.BaseSpeed = 100;
            _characterSpawn.Speed.BonusSpeed = 100;
            _characterSpawn.Stamina = 400;
            _characterSpawn.SwordSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };

            ITile tile = _tileService.GetTileByPosition(new Vector3(168, 493, 7));
            //Tile tile = new Tile();
            //tile.Flags = TileFlags.None;
            //tile.Position = new Vector3(168, 492, 7);
            //tile.Items.Add(new ItemSpawn
            //{
            //    Item = _itemService.GetItemById(406),
            //    ItemId = 406,
            //    LightInfo = new LightInfo
            //    {
            //        Color = LightColor.Default,
            //        Level = LightLevel.Torch
            //    },
            //    StackPosition = 0,
            //    Tile = tile
            //});

            // TODO: This has to be a valid tile of the map
            _characterSpawn.Tile = tile;
            _characterSpawn.StackPosition = 1;
            _characterSpawn.Torso = null;
            _characterSpawn.TorsoId = null;
            _characterSpawn.TownId = 1;
            _characterSpawn.Town = _townService.GetTownById(_characterSpawn.TownId);
            _characterSpawn.UseSecureMode = true;
            _characterSpawn.VocationId = 0;
            _characterSpawn.Vocation = _vocationService.GetVocationById(_characterSpawn.VocationId);
            _characterSpawn.WarIcon = WarIcon.None;
            _characterSpawn.Weapon = null;
            _characterSpawn.WeaponId = null;
            _characterSpawn.WorldId = 0;

            Friend friend = new Friend();
            friend.Character = _characterSpawn.Character;
            friend.CharacterId = _characterSpawn.Character.Id;
            friend.Id = 1;
            friend.Description = "Test";
            friend.NotifyOnLogin = true;

            Account account = new Account();
            account.ClientVersion = 100;
            account.Notification = null;
            account.OSPlatform = OSPlatform.Windows;
            account.Password = "1";
            account.PremiumExpirationDate = DateTime.UtcNow.AddYears(1);
            account.UserName = "1";

            account.Friends.Add(friend);
            friend.Account = account;
            friend.AccountId = account.Id;

            account.CharacterSpawns.Add(_characterSpawn);
            _characterSpawn.Account = account;
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        /// <returns>The task.</returns>
        public virtual async Task Start()
        {
            Logger.LogStart(">> Loading vocations");
            VocationXmlReader vocationXmlReader = new VocationXmlReader();
            IEnumerable<IVocation> vocations = await vocationXmlReader.LoadAsync(Settings.Default.Vocations_Xml);
            _vocationService = new VocationService(vocations);
            Logger.LogDone();

            Logger.LogStart(">> Loading items");
            ItemReader itemReader = new ItemReader();
            IEnumerable<IItem> items = await itemReader.LoadAsync(Settings.Default.Items_Otb);
            _itemService = new ItemService(items);
            ItemXmlReader itemXmlReader = new ItemXmlReader(_itemService);
            await itemXmlReader.LoadAsync(Settings.Default.Items_Xml);
            Logger.LogDone();

            Logger.LogStart(">> Loading spells");
            SpellXmlReader spellXmlReader = new SpellXmlReader();
            IEnumerable<ISpell> spells = await spellXmlReader.LoadAsync(Settings.Default.Spells_Xml);
            _spellService = new SpellService(spells);
            Logger.LogDone();

            Logger.LogStart(">> Loading monsters");
            MonsterXmlReader monsterXmlReader = new MonsterXmlReader(_spellService);
            IEnumerable<IMonster> monsters = await monsterXmlReader.LoadAsync(Settings.Default.Monsters_Xml);
            _monsterService = new MonsterService(monsters);
            Logger.LogDone();

            Logger.LogStart(">> Loading npcs");
            NpcXmlReader npcXmlReader = new NpcXmlReader();
            IEnumerable<INpc> npcs = await npcXmlReader.LoadAsync(Settings.Default.Npcs_Xml);
            _npcService = new NpcService(npcs);
            Logger.LogDone();

            Logger.LogStart(">> Loading map");
            MapReader mapReader = new MapReader(_itemService);
            WorldMap map = await mapReader.LoadAsync(Settings.Default.Map_Otb);
            Logger.LogDone();

            Logger.LogStart(">> Loading outfits");
            DrkOutfitReader outfitReader = new DrkOutfitReader();
            IEnumerable<IOutfit> outfits = await outfitReader.LoadAsync(Settings.Default.Outfits_Xml);
            _outfitService = new OutfitService(outfits);
            Logger.LogDone();

            Logger.LogStart(">> Loading mounts");
            DrkMountReader mountReader = new DrkMountReader();
            IEnumerable<IMount> mounts = await mountReader.LoadAsync(Settings.Default.Mounts_Xml);
            _mountService = new MountService(mounts);
            Logger.LogDone();

            Logger.LogStart(">> Loading channels");
            // TODO: Channels are broken. They should be handled by a category like ChannelType (e.g.: Public, Private, Guild, Party, etc.), then each category has an ID
            // TODO: Eventually loading channels this away should be replaced.
            // TODO Alternatively, we could move the specific implementation of LocalChannel to a scripting language (like Lua)
            // TODO: This could be also converted into a more modular approach (like a CS-Script)
            IDictionary<ChannelType, IChannel> channels = new Dictionary<ChannelType, IChannel>();
            channels.Add(ChannelType.Local, new LocalChannel());
            channels.Add(ChannelType.Loot, new LootChannel());
            channels.Add(ChannelType.Advertising, new AdvertisingChannel());
            channels.Add(ChannelType.AdvertisingRookgaard, new AdvertisingRookgaardChannel());
            channels.Add(ChannelType.English, new EnglishChannel());
            channels.Add(ChannelType.Help, new HelpChannel());
            channels.Add(ChannelType.World, new WorldChannel());
            Logger.LogDone();

            Logger.LogStart(">> Initializing town services");
            _townService = new TownService(map.Towns.Values);
            Logger.LogDone();

            Logger.LogStart(">> Initializing tile services");
            _tileService = new TileService(map.Tiles.Values);
            Logger.LogDone();

            // TODO: Remove this after project is complete
            InitializeTest();

            Logger.LogStart(">> Initializing repositories");
            _accountsRepository = new Repository<IAccount, uint>();

            // TODO: Remove this when repositories are implemented
            _accountsRepository.Create(_characterSpawn.Account);
            Logger.LogDone();

            Logger.LogStart(">> Initializing spawn services");
            SpawnXmlReader spawnXmlReader = new SpawnXmlReader(_monsterService, _npcService);
            ICollection<SpawnSource> spawnSources = (await spawnXmlReader.LoadAsync(Settings.Default.Spawns_Xml)).ToList();
            _creatureSpawnService = new CreatureSpawnService(spawnSources);
            RegisterSpawns(spawnSources);
            // TODO: Remove this when player repositories are implemented;
            _creatureSpawnService.RegisterCreature(_characterSpawn);
            Logger.LogDone();

            Logger.LogStart(">> Initializing communication services");
            _chatService = new ChatService(_accountsRepository.GetAll(), channels);
            _commandService = new CommandService();
            _commandService.Register(new TeleportCommand(_townService, _creatureSpawnService));
            _commandService.Register(new TownListCommand(_townService));
            _commandService.Register(new PositionInfoCommand());
            _commandService.Register(new ChangeSexCommand());
            _commandService.Register(new BroadcastCommand(_creatureSpawnService));
            Logger.LogDone();

            Logger.LogStart(">> Initializing game server");
            _gameConnections = new List<GameConnection>();
            _gameListener = new TcpListener(IPAddress.Any, Settings.Default.Network_Port_GameServer);
            _gameListener.Start();
            _gameListener.BeginAcceptSocket(OnGameMessageReceived, _gameListener);
            Logger.LogDone();

            Logger.LogStart(">> Initializing login server");
            _loginConnections = new List<LoginConnection>();
            _loginListener = new TcpListener(IPAddress.Any, Settings.Default.Network_Port_LoginServer);
            _loginListener.Start();
            _loginListener.BeginAcceptSocket(OnLoginMessageReceived, _loginListener);
            Logger.LogDone();

            _onlineTimer = new Stopwatch();
        }

        /// <summary>
        ///     Called when [game message received].
        /// </summary>
        /// <param name="result">The result.</param>
        private void OnGameMessageReceived(IAsyncResult result)
        {
            GameConnection connection = new GameConnection(Xtea.Create(), _chatService, _creatureSpawnService, _tileService, _spellService, _commandService, _outfitService, _mountService);
            connection.Authenticate += OnAuthenticate;
            connection.LoggingIn += OnLoggingIn;
            connection.RequestOnlineTime += OnRequestOnlineTime;
            connection.RequestLightInfo += OnRequestLightInfo;
            connection.OnMessageReceived(result);
            _gameConnections.Add(connection);

            _gameListener.BeginAcceptSocket(OnGameMessageReceived, _gameListener);
        }

        /// <summary>
        ///     Called when [request light information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LightInfoEventArgs" /> instance containing the event data.</param>
        private void OnRequestLightInfo(object sender, LightInfoEventArgs e)
        {
            e.LightInfo = _lightInfo;
        }

        /// <summary>
        ///     Called when [request online time].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="OnlineTimeEventArgs" /> instance containing the event data.</param>
        private void OnRequestOnlineTime(object sender, OnlineTimeEventArgs e)
        {
            e.TimeSpan = _onlineTimer.Elapsed;
        }

        /// <summary>
        ///     Called when [logging in].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LoggingInEventArgs" /> instance containing the event data.</param>
        private void OnLoggingIn(object sender, LoggingInEventArgs e)
        {
            // TODO: Check ALLOW CLONES settings (Can be moved to Game registered event)
            // TODO: Check single character per account & kick on login (Can be moved to Game registered event)
            //if (Creatures.ContainsKey(character.CreatureId))
            //{
            //    // You are already logged in
            //    connection.Disconnect(Server.LocalizationManager.GetString(3));
            //    return;
            //}

            // TODO: Check namelock (Can be moved to Game registered event)
            // TODO: Check game state (Can be moved to Game registered event)
            // TODO: Check character banishment (Can be moved to Game registered event)
            // TODO: Implement waiting list (Can be moved to Game registered event)
            // TODO: Implement invalid saved location (Can be moved to Game registered event)
            //if (character.SavedLocation == null || Map.GetTile(character.SavedLocation) == null)
            //    character.SavedLocation = Map.DefaultLocation;

            //Tile tile = Map.GetTile(character.SavedLocation);
            //character.Tile = tile;

            // TODO: If something went wrong, cancel
            //e.Cancel = true;

            // TODO: Remove the below!! THIS IS NOT THE CORRECT WAY OF LOADING A PLAYER
            // TODO: USE THE SERVICE INSTEAD
            GameConnection gameConnection = (GameConnection) sender;
            gameConnection.CharacterSpawn = _characterSpawn;
        }

        /// <summary>
        ///     Called when [login message received].
        /// </summary>
        /// <param name="result">The result.</param>
        private void OnLoginMessageReceived(IAsyncResult result)
        {
            LoginConnection connection = new LoginConnection(Xtea.Create());
            connection.Authenticate += OnAuthenticate;
            connection.RequestLoginData += OnRequestedLoginData;
            connection.OnMessageReceived(result);
            _loginConnections.Add(connection);

            _loginListener.BeginAcceptSocket(OnLoginMessageReceived, _loginListener);
        }

        /// <summary>
        ///     Called when [requested login data].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LoginDataEventArgs" /> instance containing the event data.</param>
        private void OnRequestedLoginData(object sender, LoginDataEventArgs e)
        {
            World world = new World();
            world.IpAddress = "127.0.0.1";
            world.IsPreview = true;
            world.Port = 7172;
            world.Id = 0;
            world.Name = "Draconia";
            e.Worlds.Add(world);

            Character character = new Character();
            character.Id = 1;
            character.Name = "Chuitox";
            character.Status = SessionStatus.Online;
            character.Settings.CanBroadcast = true;
            character.Settings.CanChangeSex = true;
            character.Settings.CanSeeDiagnostics = true;
            character.Settings.CanSeeGhosts = true;
            character.Settings.CanTeleport = true;

            _characterSpawn = new CharacterSpawn();
            _characterSpawn.AccountId = 1;
            _characterSpawn.Amulet = null;
            _characterSpawn.AmuletId = null;
            _characterSpawn.AxeSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.Balance = 1000000;
            _characterSpawn.BattleStance = BattleStance.Defense;
            _characterSpawn.Belt = null;
            _characterSpawn.BeltId = null;
            _characterSpawn.Blessings = 7;
            _characterSpawn.Capacity = 300;
            _characterSpawn.Character = character;
            _characterSpawn.CharacterId = 1;
            _characterSpawn.ClubSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.Conditions = Conditions.Bleeding | Conditions.Burning | Conditions.Cursed | Conditions.Dazzled | Conditions.Drowning | Conditions.Strengthened;
            _characterSpawn.Container = null;
            _characterSpawn.ContainerId = null;
            _characterSpawn.CreationTime = DateTime.UtcNow;
            _characterSpawn.Creature = character;
            _characterSpawn.DeletionTime = null;
            _characterSpawn.Direction = Direction.East;
            _characterSpawn.DistanceSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.DrunkCondition = null;
            _characterSpawn.ExhaustCondition = null;
            _characterSpawn.Feet = null;
            _characterSpawn.FeetId = null;
            _characterSpawn.FishingSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.FistSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.FollowOpponent = true;
            _characterSpawn.FreeCapacity = 250;
            _characterSpawn.Guild = null;
            _characterSpawn.GuildLevel = 0;
            _characterSpawn.Head = null;
            _characterSpawn.HeadId = null;
            _characterSpawn.Health.Current = 100;
            _characterSpawn.Health.Maximum = 1000;
            _characterSpawn.Id = 0;
            //characterSpawn.IsDead = false;
            //characterSpawn.IsDrunk = false;
            //characterSpawn.IsExhausted = false;
            // TODO: This must be handled differently
            _characterSpawn.IsMountToggleExhausted = false;
            //characterSpawn.IsPoisoned = false;
            _characterSpawn.LastLogin.IpAddress = "127.0.0.1";
            _characterSpawn.LastLogin.Time = DateTime.UtcNow;
            _characterSpawn.LastLogout = null;
            _characterSpawn.LastPing = DateTime.MinValue;
            _characterSpawn.Legs = null;
            _characterSpawn.LegsId = null;
            _characterSpawn.Level.Current = 9;
            _characterSpawn.Level.Experience = 1000;
            _characterSpawn.Level.NextLevelExperience = 1100;
            _characterSpawn.LightInfo.Level = LightLevel.Torch;
            _characterSpawn.LightInfo.Color = LightColor.Default;
            _characterSpawn.MagicLevel.Base = 100;
            _characterSpawn.MagicLevel.Bonus = 10;
            _characterSpawn.MagicLevel.Experience = 1000;
            _characterSpawn.MagicLevel.NextLevelExperience = 1100;
            _characterSpawn.Mana.Current = 100;
            _characterSpawn.Mana.Maximum = 1000;
            _characterSpawn.Mana.Spent = 100;
            _characterSpawn.Mount = _mountService.GetMountBySpriteId(373);

            // TODO: This should be initialized by the constructor
            _characterSpawn.IsRiding = true;
            _characterSpawn.Mounts.AddRange(_mountService.GetAll());
            _characterSpawn.OfflineTraining.Elapsed = TimeSpan.Zero;
            _characterSpawn.OfflineTraining.Skill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.OnlineTime = TimeSpan.Zero;

            // TODO: Containers open
            //characterSpawn.OpenContainers = null;
            _characterSpawn.Outfit = _outfitService.GetOutfitBySpriteId(147);

            // TODO: This should be initialized by the constructor
            _characterSpawn.Outfits.AddRange(_outfitService.GetAll());
            _characterSpawn.PartyId = null;

            // TODO: This should be initialized by the constructor
            _characterSpawn.PartyInvitations = new IParty[0];
            _characterSpawn.PoisonCondition = null;

            // TODO: This should be initialized by the constructor
            _characterSpawn.Quests = new IQuestInfo[0];

            // TODO: This should be initialized by the constructor
            _characterSpawn.RegenerationConditions = new RegenerationCondition[0];
            _characterSpawn.Ring = null;
            _characterSpawn.RingId = null;
            _characterSpawn.Sex = Sex.Male;
            _characterSpawn.Shield = null;
            _characterSpawn.ShieldId = null;
            _characterSpawn.ShieldSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };
            _characterSpawn.Skull.Type = SkullType.None;
            _characterSpawn.Skull.Time = TimeSpan.Zero;
            _characterSpawn.Soul = 200;
            _characterSpawn.Speed.BaseSpeed = 100;
            _characterSpawn.Speed.BonusSpeed = 100;
            _characterSpawn.Stamina = 400;
            _characterSpawn.SwordSkill = new Skill
            {
                Base = 10,
                Current = 100,
                Experience = 1000,
                NextLevelExperience = 1100
            };

            ITile tile = _tileService.GetTileByPosition(new Vector3(168, 493, 7));
            //Tile tile = new Tile();
            //tile.Flags = TileFlags.None;
            //tile.Position = new Vector3(168, 492, 7);
            //tile.Items.Add(new ItemSpawn
            //{
            //    Item = _itemService.GetItemById(406),
            //    ItemId = 406,
            //    LightInfo = new LightInfo
            //    {
            //        Color = LightColor.Default,
            //        Level = LightLevel.Torch
            //    },
            //    StackPosition = 0,
            //    Tile = tile
            //});

            // TODO: This has to be a valid tile of the map
            _characterSpawn.Tile = tile;
            _characterSpawn.StackPosition = 1;
            _characterSpawn.Torso = null;
            _characterSpawn.TorsoId = null;
            _characterSpawn.TownId = 1;
            _characterSpawn.Town = _townService.GetTownById(_characterSpawn.TownId);
            _characterSpawn.UseSecureMode = true;
            _characterSpawn.VocationId = 0;
            _characterSpawn.Vocation = _vocationService.GetVocationById(_characterSpawn.VocationId);
            _characterSpawn.WarIcon = WarIcon.None;
            _characterSpawn.Weapon = null;
            _characterSpawn.WeaponId = null;
            _characterSpawn.WorldId = 0;
            e.Characters.Add(_characterSpawn);

            Friend friend = new Friend();
            friend.Character = _characterSpawn.Character;
            friend.CharacterId = _characterSpawn.Character.Id;
            friend.Id = 1;
            friend.Description = "Test";
            friend.NotifyOnLogin = true;

            IAccount account = e.Account;
            account.ClientVersion = 100;
            account.Notification = null;
            account.OSPlatform = OSPlatform.Windows;
            account.Password = "1";
            account.PremiumExpirationDate = DateTime.UtcNow.AddYears(1);
            account.UserName = "1";

            account.Friends.Add(friend);
            friend.Account = account;
            friend.AccountId = account.Id;

            account.CharacterSpawns.Add(_characterSpawn);
            _characterSpawn.Account = account;
        }

        /// <summary>
        ///     Called when [authenticate].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AuthenticationEventArgs" /> instance containing the event data.</param>
        private void OnAuthenticate(object sender, AuthenticationEventArgs e)
        {
            if (!_loginService.Authenticate(e.Username, e.Password))
                e.Cancel = true;
        }
    }
}