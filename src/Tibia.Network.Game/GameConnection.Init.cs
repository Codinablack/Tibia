using System;
using System.Collections.Generic;
using Tibia.Communications.Channels;
using Tibia.Communications.Commands;
using Tibia.Data;
using Tibia.Map;
using Tibia.Mounts;
using Tibia.Outfits;
using Tibia.Security.Cryptography;
using Tibia.Spawns;
using Tibia.Spells;

namespace Tibia.Network.Game
{
    public partial class GameConnection
    {
        /// <summary>
        ///     The move creature range.
        /// </summary>
        private static readonly IVector3 MoveCreatureRange = new Vector3(1, 1, 0);

        /// <summary>
        ///     The move item source range.
        /// </summary>
        private static readonly IVector3 MoveItemSourceRange = new Vector3(1, 1, 0);

        /// <summary>
        ///     The move item target range.
        /// </summary>
        private static readonly IVector3 MoveItemTargetRange = new Vector3(7, 5, 7);

        /// <summary>
        ///     The spectator range.
        /// </summary>
        private static readonly IVector3 SpectatorRange = new Vector3(10, 10, 3);

        private readonly ChatService _chatService;
        private readonly CommandService _commandService;
        private readonly CreatureSpawnService _creatureSpawnService;
        private readonly IDictionary<GameClientPacketType, Action<NetworkMessage>> _gamePacketMap;
        private readonly ICollection<uint> _knownCreatures;
        private readonly MountService _mountService;
        private readonly OutfitService _outfitService;
        private readonly SpellService _spellService;
        private readonly TileService _tileService;
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.GameConnection" /> class.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        /// <param name="chatService">The chat service.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="tileService">The tile service.</param>
        /// <param name="spellService">The spell service.</param>
        /// <param name="commandService">The command service.</param>
        /// <param name="outfitService">The outfit service.</param>
        /// <param name="mountService">The mount service.</param>
        public GameConnection(Xtea xtea, ChatService chatService, CreatureSpawnService creatureSpawnService, TileService tileService, SpellService spellService, CommandService commandService, OutfitService outfitService, MountService mountService)
            : base(xtea)
        {
            _knownCreatures = new HashSet<uint>();
            _gamePacketMap = new Dictionary<GameClientPacketType, Action<NetworkMessage>>();

            _chatService = chatService;
            _creatureSpawnService = creatureSpawnService;
            _tileService = tileService;
            _spellService = spellService;
            _commandService = commandService;
            _outfitService = outfitService;
            _mountService = mountService;

            LoggedIn += OnLoggedIn;
            RequestPingBack += OnRequestPingBack;
            AddingFriend += OnAddingFriend;
            EditingFriend += OnEditingFriend;
            RemovingFriend += OnRemovingFriend;
            OpeningChannel += OnOpeningChannel;

            IsRemoved = false;

            Initialize();
        }

        /// <summary>
        ///     Gets or sets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is removed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is removed; otherwise, <c>false</c>.
        /// </value>
        public bool IsRemoved { get; private set; }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            // TODO: Map all packets
            //_gamePacketMap.Add(GameClientPacketType.Packet0, ParsePacket0);
            //_gamePacketMap.Add(GameClientPacketType.LoginServerRequest, ParseLoginServerRequest);
            //_gamePacketMap.Add(GameClientPacketType.Packet2, ParsePacket2);
            //_gamePacketMap.Add(GameClientPacketType.Packet3, ParsePacket3);
            //_gamePacketMap.Add(GameClientPacketType.Packet4, ParsePacket4);
            //_gamePacketMap.Add(GameClientPacketType.Packet5, ParsePacket5);
            //_gamePacketMap.Add(GameClientPacketType.Packet6, ParsePacket6);
            //_gamePacketMap.Add(GameClientPacketType.Packet7, ParsePacket7);
            //_gamePacketMap.Add(GameClientPacketType.Packet8, ParsePacket8);
            //_gamePacketMap.Add(GameClientPacketType.Packet9, ParsePacket9);
            //_gamePacketMap.Add(GameClientPacketType.GameServerRequest, ParseGameServerRequest);
            //_gamePacketMap.Add(GameClientPacketType.Packet11, ParsePacket11);
            //_gamePacketMap.Add(GameClientPacketType.Packet12, ParsePacket12);
            //_gamePacketMap.Add(GameClientPacketType.Packet13, ParsePacket13);
            //_gamePacketMap.Add(GameClientPacketType.Packet14, ParsePacket14);
            _gamePacketMap.Add(GameClientPacketType.EnterGame, ParseEnterGame);
            //_gamePacketMap.Add(GameClientPacketType.Packet16, ParsePacket16);
            //_gamePacketMap.Add(GameClientPacketType.Packet17, ParsePacket17);
            //_gamePacketMap.Add(GameClientPacketType.Packet18, ParsePacket18);
            //_gamePacketMap.Add(GameClientPacketType.Packet19, ParsePacket19);
            _gamePacketMap.Add(GameClientPacketType.Logout, ParseLogout);
            //_gamePacketMap.Add(GameClientPacketType.Packet21, ParsePacket21);
            //_gamePacketMap.Add(GameClientPacketType.Packet22, ParsePacket22);
            //_gamePacketMap.Add(GameClientPacketType.Packet23, ParsePacket23);
            //_gamePacketMap.Add(GameClientPacketType.Packet24, ParsePacket24);
            //_gamePacketMap.Add(GameClientPacketType.Packet25, ParsePacket25);
            //_gamePacketMap.Add(GameClientPacketType.Packet26, ParsePacket26);
            //_gamePacketMap.Add(GameClientPacketType.Packet27, ParsePacket27);
            //_gamePacketMap.Add(GameClientPacketType.Packet28, ParsePacket28);
            _gamePacketMap.Add(GameClientPacketType.PingBack, ParsePingBack);
            _gamePacketMap.Add(GameClientPacketType.Ping, ParsePing);
            //_gamePacketMap.Add(GameClientPacketType.Packet31, ParsePacket31);
            //_gamePacketMap.Add(GameClientPacketType.Packet32, ParsePacket32);
            //_gamePacketMap.Add(GameClientPacketType.Packet33, ParsePacket33);
            //_gamePacketMap.Add(GameClientPacketType.Packet34, ParsePacket34);
            //_gamePacketMap.Add(GameClientPacketType.Packet35, ParsePacket35);
            //_gamePacketMap.Add(GameClientPacketType.Packet36, ParsePacket36);
            //_gamePacketMap.Add(GameClientPacketType.Packet37, ParsePacket37);
            //_gamePacketMap.Add(GameClientPacketType.Packet38, ParsePacket38);
            //_gamePacketMap.Add(GameClientPacketType.Packet39, ParsePacket39);
            //_gamePacketMap.Add(GameClientPacketType.Packet40, ParsePacket40);
            //_gamePacketMap.Add(GameClientPacketType.Packet41, ParsePacket41);
            //_gamePacketMap.Add(GameClientPacketType.Packet42, ParsePacket42);
            //_gamePacketMap.Add(GameClientPacketType.Packet43, ParsePacket43);
            //_gamePacketMap.Add(GameClientPacketType.Packet44, ParsePacket44);
            //_gamePacketMap.Add(GameClientPacketType.Packet45, ParsePacket45);
            //_gamePacketMap.Add(GameClientPacketType.Packet46, ParsePacket46);
            //_gamePacketMap.Add(GameClientPacketType.Packet47, ParsePacket47);
            //_gamePacketMap.Add(GameClientPacketType.Packet48, ParsePacket48);
            //_gamePacketMap.Add(GameClientPacketType.Packet49, ParsePacket49);
            //_gamePacketMap.Add(GameClientPacketType.ExtendedOpenPacketCode, ParseExtendedOpenPacketCode);
            //_gamePacketMap.Add(GameClientPacketType.Packet51, ParsePacket51);
            //_gamePacketMap.Add(GameClientPacketType.Packet52, ParsePacket52);
            //_gamePacketMap.Add(GameClientPacketType.Packet53, ParsePacket53);
            //_gamePacketMap.Add(GameClientPacketType.Packet54, ParsePacket54);
            //_gamePacketMap.Add(GameClientPacketType.Packet55, ParsePacket55);
            //_gamePacketMap.Add(GameClientPacketType.Packet56, ParsePacket56);
            //_gamePacketMap.Add(GameClientPacketType.Packet57, ParsePacket57);
            //_gamePacketMap.Add(GameClientPacketType.Packet58, ParsePacket58);
            //_gamePacketMap.Add(GameClientPacketType.Packet59, ParsePacket59);
            //_gamePacketMap.Add(GameClientPacketType.Packet60, ParsePacket60);
            //_gamePacketMap.Add(GameClientPacketType.Packet61, ParsePacket61);
            //_gamePacketMap.Add(GameClientPacketType.Packet62, ParsePacket62);
            //_gamePacketMap.Add(GameClientPacketType.Packet63, ParsePacket63);
            //_gamePacketMap.Add(GameClientPacketType.Packet64, ParsePacket64);
            //_gamePacketMap.Add(GameClientPacketType.Packet65, ParsePacket65);
            //_gamePacketMap.Add(GameClientPacketType.Packet66, ParsePacket66);
            //_gamePacketMap.Add(GameClientPacketType.Packet67, ParsePacket67);
            //_gamePacketMap.Add(GameClientPacketType.Packet68, ParsePacket68);
            //_gamePacketMap.Add(GameClientPacketType.Packet69, ParsePacket69);
            //_gamePacketMap.Add(GameClientPacketType.Packet70, ParsePacket70);
            //_gamePacketMap.Add(GameClientPacketType.Packet71, ParsePacket71);
            //_gamePacketMap.Add(GameClientPacketType.Packet72, ParsePacket72);
            //_gamePacketMap.Add(GameClientPacketType.Packet73, ParsePacket73);
            //_gamePacketMap.Add(GameClientPacketType.Packet74, ParsePacket74);
            //_gamePacketMap.Add(GameClientPacketType.Packet75, ParsePacket75);
            //_gamePacketMap.Add(GameClientPacketType.Packet76, ParsePacket76);
            //_gamePacketMap.Add(GameClientPacketType.Packet77, ParsePacket77);
            //_gamePacketMap.Add(GameClientPacketType.Packet78, ParsePacket78);
            //_gamePacketMap.Add(GameClientPacketType.Packet79, ParsePacket79);
            //_gamePacketMap.Add(GameClientPacketType.Packet80, ParsePacket80);
            //_gamePacketMap.Add(GameClientPacketType.Packet81, ParsePacket81);
            //_gamePacketMap.Add(GameClientPacketType.Packet82, ParsePacket82);
            //_gamePacketMap.Add(GameClientPacketType.Packet83, ParsePacket83);
            //_gamePacketMap.Add(GameClientPacketType.Packet84, ParsePacket84);
            //_gamePacketMap.Add(GameClientPacketType.Packet85, ParsePacket85);
            //_gamePacketMap.Add(GameClientPacketType.Packet86, ParsePacket86);
            //_gamePacketMap.Add(GameClientPacketType.Packet87, ParsePacket87);
            //_gamePacketMap.Add(GameClientPacketType.Packet88, ParsePacket88);
            //_gamePacketMap.Add(GameClientPacketType.Packet89, ParsePacket89);
            //_gamePacketMap.Add(GameClientPacketType.Packet90, ParsePacket90);
            //_gamePacketMap.Add(GameClientPacketType.Packet91, ParsePacket91);
            //_gamePacketMap.Add(GameClientPacketType.Packet92, ParsePacket92);
            //_gamePacketMap.Add(GameClientPacketType.Packet93, ParsePacket93);
            //_gamePacketMap.Add(GameClientPacketType.Packet94, ParsePacket94);
            //_gamePacketMap.Add(GameClientPacketType.Packet95, ParsePacket95);
            //_gamePacketMap.Add(GameClientPacketType.Packet96, ParsePacket96);
            //_gamePacketMap.Add(GameClientPacketType.Packet97, ParsePacket97);
            //_gamePacketMap.Add(GameClientPacketType.Packet98, ParsePacket98);
            //_gamePacketMap.Add(GameClientPacketType.Packet99, ParsePacket99);
            _gamePacketMap.Add(GameClientPacketType.AutoWalk, ParseAutoWalk);
            _gamePacketMap.Add(GameClientPacketType.MoveNorth, ParseSelfWalkNorth);
            _gamePacketMap.Add(GameClientPacketType.MoveEast, ParseSelfWalkEast);
            _gamePacketMap.Add(GameClientPacketType.MoveSouth, ParseSelfWalkSouth);
            _gamePacketMap.Add(GameClientPacketType.MoveWest, ParseSelfWalkWest);
            //_gamePacketMap.Add(GameClientPacketType.AutoWalkCancel, ParseAutoWalkCancel);
            _gamePacketMap.Add(GameClientPacketType.MoveNorthEast, ParseSelfWalkNorthEast);
            _gamePacketMap.Add(GameClientPacketType.MoveSouthEast, ParseSelfWalkSouthEast);
            _gamePacketMap.Add(GameClientPacketType.MoveSouthWest, ParseSelfWalkSouthWest);
            _gamePacketMap.Add(GameClientPacketType.MoveNorthWest, ParseMoveNorthWest);
            //_gamePacketMap.Add(GameClientPacketType.Packet110, ParsePacket110);
            _gamePacketMap.Add(GameClientPacketType.TurnNorth, ParseTurnNorth);
            _gamePacketMap.Add(GameClientPacketType.TurnWest, ParseTurnWest);
            _gamePacketMap.Add(GameClientPacketType.TurnSouth, ParseTurnSouth);
            _gamePacketMap.Add(GameClientPacketType.TurnEast, ParseTurnEast);
            //_gamePacketMap.Add(GameClientPacketType.Packet115, ParsePacket115);
            //_gamePacketMap.Add(GameClientPacketType.Packet116, ParsePacket116);
            //_gamePacketMap.Add(GameClientPacketType.Packet117, ParsePacket117);
            //_gamePacketMap.Add(GameClientPacketType.Packet118, ParsePacket118);
            //_gamePacketMap.Add(GameClientPacketType.Packet119, ParsePacket119);
            _gamePacketMap.Add(GameClientPacketType.ArtifactMove, ParseArtifactMove);
            //_gamePacketMap.Add(GameClientPacketType.ShopLookAt, ParseShopLookAt);
            //_gamePacketMap.Add(GameClientPacketType.ShopBuy, ParseShopBuy);
            //_gamePacketMap.Add(GameClientPacketType.ShopSell, ParseShopSell);
            //_gamePacketMap.Add(GameClientPacketType.ShopClose, ParseShopClose);
            //_gamePacketMap.Add(GameClientPacketType.TradeRequest, ParseTradeRequest);
            //_gamePacketMap.Add(GameClientPacketType.TradeLookAt, ParseTradeLookAt);
            //_gamePacketMap.Add(GameClientPacketType.TradeAccept, ParseTradeAccept);
            //_gamePacketMap.Add(GameClientPacketType.TradeClose, ParseTradeClose);
            //_gamePacketMap.Add(GameClientPacketType.Packet129, ParsePacket129);
            //_gamePacketMap.Add(GameClientPacketType.ItemUse, ParseItemUse);
            //_gamePacketMap.Add(GameClientPacketType.ItemUseOn, ParseItemUseOn);
            //_gamePacketMap.Add(GameClientPacketType.ItemUseOnBattleList, ParseItemUseOnBattleList);
            //_gamePacketMap.Add(GameClientPacketType.ItemRotate, ParseItemRotate);
            //_gamePacketMap.Add(GameClientPacketType.Packet134, ParsePacket134);
            //_gamePacketMap.Add(GameClientPacketType.ContainerClose, ParseContainerClose);
            //_gamePacketMap.Add(GameClientPacketType.ContainerOpenParent, ParseContainerOpenParent);
            //_gamePacketMap.Add(GameClientPacketType.WriteItem, ParseWriteItem);
            //_gamePacketMap.Add(GameClientPacketType.WriteHousePermissions, ParseWriteHousePermissions);
            //_gamePacketMap.Add(GameClientPacketType.Packet139, ParsePacket139);
            //_gamePacketMap.Add(GameClientPacketType.LookAt, ParseLookAt);
            //_gamePacketMap.Add(GameClientPacketType.LookInBattleList, ParseLookInBattleList);
            //_gamePacketMap.Add(GameClientPacketType.JoinAggression, ParseJoinAggression);
            //_gamePacketMap.Add(GameClientPacketType.Packet143, ParsePacket143);
            //_gamePacketMap.Add(GameClientPacketType.Packet144, ParsePacket144);
            //_gamePacketMap.Add(GameClientPacketType.Packet145, ParsePacket145);
            //_gamePacketMap.Add(GameClientPacketType.Packet146, ParsePacket146);
            //_gamePacketMap.Add(GameClientPacketType.Packet147, ParsePacket147);
            //_gamePacketMap.Add(GameClientPacketType.Packet148, ParsePacket148);
            //_gamePacketMap.Add(GameClientPacketType.Packet149, ParsePacket149);
            _gamePacketMap.Add(GameClientPacketType.ChatSpeech, ParseChatSpeech);
            _gamePacketMap.Add(GameClientPacketType.ChannelList, ParseChannelListRequest);
            _gamePacketMap.Add(GameClientPacketType.ChannelOpen, ParseChannelOpen);
            _gamePacketMap.Add(GameClientPacketType.ChannelClose, ParseChannelClose);
            //_gamePacketMap.Add(GameClientPacketType.ChannelPrivateOpen, ParseChannelPrivateOpen);
            //_gamePacketMap.Add(GameClientPacketType.Packet155, ParsePacket155);
            //_gamePacketMap.Add(GameClientPacketType.Packet156, ParsePacket156);
            //_gamePacketMap.Add(GameClientPacketType.Packet157, ParsePacket157);

            // TODO: ChannelType does not contain a value for NPC channel
            // TODO: Implement NPC channel, this will be replaced by normal channel ID but the channel will be listed as NPC
            //_gamePacketMap.Add(GameClientPacketType.ChannelNpcClose, ParseChannelNpcClose);

            //_gamePacketMap.Add(GameClientPacketType.Packet159, ParsePacket159);
            _gamePacketMap.Add(GameClientPacketType.CombatFightMode, ParseCombatFightMode);
            //_gamePacketMap.Add(GameClientPacketType.CombatAttack, ParseCombatAttack);
            //_gamePacketMap.Add(GameClientPacketType.CombatFollow, ParseCombatFollow);
            _gamePacketMap.Add(GameClientPacketType.PartyInvitation, ParsePartyInvitation);
            _gamePacketMap.Add(GameClientPacketType.PartyJoin, ParsePartyJoin);
            _gamePacketMap.Add(GameClientPacketType.PartyRevokeInvitation, ParsePartyRevokeInvitation);
            _gamePacketMap.Add(GameClientPacketType.PartyPassLeadership, ParsePartyPassLeadership);
            _gamePacketMap.Add(GameClientPacketType.PartyLeave, ParsePartyLeave);
            _gamePacketMap.Add(GameClientPacketType.PartySwitchSharedExperience, ParsePartySwitchSharedExperience);
            //_gamePacketMap.Add(GameClientPacketType.Packet169, ParsePacket169);
            //_gamePacketMap.Add(GameClientPacketType.ChannelPrivateCreate, ParseChannelPrivateCreate);
            //_gamePacketMap.Add(GameClientPacketType.ChannelInvite, ParseChannelInvite);
            //_gamePacketMap.Add(GameClientPacketType.ChannelRevokeAccess, ParseChannelRevokeAccess);
            //_gamePacketMap.Add(GameClientPacketType.Packet173, ParsePacket173);
            //_gamePacketMap.Add(GameClientPacketType.Packet174, ParsePacket174);
            //_gamePacketMap.Add(GameClientPacketType.Packet175, ParsePacket175);
            //_gamePacketMap.Add(GameClientPacketType.Packet176, ParsePacket176);
            //_gamePacketMap.Add(GameClientPacketType.Packet177, ParsePacket177);
            //_gamePacketMap.Add(GameClientPacketType.Packet178, ParsePacket178);
            //_gamePacketMap.Add(GameClientPacketType.Packet179, ParsePacket179);
            //_gamePacketMap.Add(GameClientPacketType.Packet180, ParsePacket180);
            //_gamePacketMap.Add(GameClientPacketType.Packet181, ParsePacket181);
            //_gamePacketMap.Add(GameClientPacketType.Packet182, ParsePacket182);
            //_gamePacketMap.Add(GameClientPacketType.Packet183, ParsePacket183);
            //_gamePacketMap.Add(GameClientPacketType.Packet184, ParsePacket184);
            //_gamePacketMap.Add(GameClientPacketType.Packet185, ParsePacket185);
            //_gamePacketMap.Add(GameClientPacketType.Packet186, ParsePacket186);
            //_gamePacketMap.Add(GameClientPacketType.Packet187, ParsePacket187);
            //_gamePacketMap.Add(GameClientPacketType.Packet188, ParsePacket188);
            //_gamePacketMap.Add(GameClientPacketType.Packet189, ParsePacket189);
            //_gamePacketMap.Add(GameClientPacketType.CombatCancelAll, ParseCombatCancelAll);
            //_gamePacketMap.Add(GameClientPacketType.Packet191, ParsePacket191);
            //_gamePacketMap.Add(GameClientPacketType.Packet192, ParsePacket192);
            //_gamePacketMap.Add(GameClientPacketType.Packet193, ParsePacket193);
            //_gamePacketMap.Add(GameClientPacketType.Packet194, ParsePacket194);
            //_gamePacketMap.Add(GameClientPacketType.Packet195, ParsePacket195);
            //_gamePacketMap.Add(GameClientPacketType.Packet196, ParsePacket196);
            //_gamePacketMap.Add(GameClientPacketType.Packet197, ParsePacket197);
            //_gamePacketMap.Add(GameClientPacketType.Packet198, ParsePacket198);
            //_gamePacketMap.Add(GameClientPacketType.Packet199, ParsePacket199);
            //_gamePacketMap.Add(GameClientPacketType.Packet200, ParsePacket200);
            //_gamePacketMap.Add(GameClientPacketType.TileUpdate, ParseTileUpdate);
            //_gamePacketMap.Add(GameClientPacketType.ContainerUpdate, ParseContainerUpdate);
            //_gamePacketMap.Add(GameClientPacketType.BrowseField, ParseBrowseField);
            //_gamePacketMap.Add(GameClientPacketType.ContainerSeekPage, ParseContainerSeekPage);
            //_gamePacketMap.Add(GameClientPacketType.Packet205, ParsePacket205);
            //_gamePacketMap.Add(GameClientPacketType.Packet206, ParsePacket206);
            //_gamePacketMap.Add(GameClientPacketType.Packet207, ParsePacket207);
            //_gamePacketMap.Add(GameClientPacketType.Packet208, ParsePacket208);
            //_gamePacketMap.Add(GameClientPacketType.Packet209, ParsePacket209);
            _gamePacketMap.Add(GameClientPacketType.WindowAppearanceRequest, ParseWindowAppearanceRequest);
            _gamePacketMap.Add(GameClientPacketType.AppearanceChange, ParseAppearanceChange);
            _gamePacketMap.Add(GameClientPacketType.MountToggle, ParseMountToggle);
            //_gamePacketMap.Add(GameClientPacketType.Packet213, ParsePacket213);
            //_gamePacketMap.Add(GameClientPacketType.Packet214, ParsePacket214);
            //_gamePacketMap.Add(GameClientPacketType.Packet215, ParsePacket215);
            //_gamePacketMap.Add(GameClientPacketType.Packet216, ParsePacket216);
            //_gamePacketMap.Add(GameClientPacketType.Packet217, ParsePacket217);
            //_gamePacketMap.Add(GameClientPacketType.Packet218, ParsePacket218);
            //_gamePacketMap.Add(GameClientPacketType.Packet219, ParsePacket219);
            //_gamePacketMap.Add(GameClientPacketType.FriendAdd, ParseFriendAdd);
            //_gamePacketMap.Add(GameClientPacketType.FriendRemove, ParseFriendRemove);
            //_gamePacketMap.Add(GameClientPacketType.FriendEdit, ParseFriendEdit);
            //_gamePacketMap.Add(GameClientPacketType.Packet223, ParsePacket223);
            //_gamePacketMap.Add(GameClientPacketType.Packet224, ParsePacket224);
            //_gamePacketMap.Add(GameClientPacketType.Packet225, ParsePacket225);
            //_gamePacketMap.Add(GameClientPacketType.Packet226, ParsePacket226);
            //_gamePacketMap.Add(GameClientPacketType.Packet227, ParsePacket227);
            //_gamePacketMap.Add(GameClientPacketType.Packet228, ParsePacket228);
            //_gamePacketMap.Add(GameClientPacketType.Packet229, ParsePacket229);
            //_gamePacketMap.Add(GameClientPacketType.ReportBug, ParseReportBug);
            //_gamePacketMap.Add(GameClientPacketType.ThankYou, ParseThankYou);
            //_gamePacketMap.Add(GameClientPacketType.ReportDebugAssert, ParseReportDebugAssert);
            //_gamePacketMap.Add(GameClientPacketType.Packet233, ParsePacket233);
            //_gamePacketMap.Add(GameClientPacketType.Packet234, ParsePacket234);
            //_gamePacketMap.Add(GameClientPacketType.Packet235, ParsePacket235);
            //_gamePacketMap.Add(GameClientPacketType.Packet236, ParsePacket236);
            //_gamePacketMap.Add(GameClientPacketType.Packet237, ParsePacket237);
            //_gamePacketMap.Add(GameClientPacketType.Packet238, ParsePacket238);
            //_gamePacketMap.Add(GameClientPacketType.Packet239, ParsePacket239);
            _gamePacketMap.Add(GameClientPacketType.QuestLogWindowRequest, ParseQuestLogWindowRequest);
            _gamePacketMap.Add(GameClientPacketType.QuestLogQuestLine, ParseQuestLogQuestLine);
            //_gamePacketMap.Add(GameClientPacketType.ReportRuleViolation, ParseReportRuleViolation);
            //_gamePacketMap.Add(GameClientPacketType.ObjectInfoRequest, ParseObjectInfoRequest);
            //_gamePacketMap.Add(GameClientPacketType.MarketLeave, ParseMarketLeave);
            //_gamePacketMap.Add(GameClientPacketType.MarketBrowse, ParseMarketBrowse);
            //_gamePacketMap.Add(GameClientPacketType.MarketCreateOffer, ParseMarketCreateOffer);
            //_gamePacketMap.Add(GameClientPacketType.MarketCancelOffer, ParseMarketCancelOffer);
            //_gamePacketMap.Add(GameClientPacketType.MarketAcceptOffer, ParseMarketAcceptOffer);
            //_gamePacketMap.Add(GameClientPacketType.OfflineTrainingWindowAnswer, ParseOfflineTrainingWindowAnswer);
            //_gamePacketMap.Add(GameClientPacketType.Packet250, ParsePacket250);
            //_gamePacketMap.Add(GameClientPacketType.Packet251, ParsePacket251);
            //_gamePacketMap.Add(GameClientPacketType.Packet252, ParsePacket252);
            //_gamePacketMap.Add(GameClientPacketType.Packet253, ParsePacket253);
            //_gamePacketMap.Add(GameClientPacketType.Packet254, ParsePacket254);
            //_gamePacketMap.Add(GameClientPacketType.Packet255, ParsePacket255);
        }
    }
}