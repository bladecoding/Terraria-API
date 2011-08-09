namespace TerrariaAPI
{
    // Terraria Protocol: https://spreadsheets.google.com/spreadsheet/ccc?key=0AtMbaBHfhV44dE4zYnVad05lVXFwbjczdk03QnNCWHc
    // Terraria Content: https://spreadsheets.google.com/spreadsheet/ccc?key=0AtMbaBHfhV44dF9DN192dnUxZFA1d0dmSVpQTFpkQnc

    public enum PacketTypes
    {
        ConnectRequest = 1,
        Disconnect = 2,
        ContinueConnecting = 3,
        PlayerInfo = 4,
        PlayerSlot = 5,
        ContinueConnecting2 = 6,
        WorldInfo = 7,
        TileGetSection = 8,
        Status = 9,
        TileSendSection = 10,
        TileFrameSection = 11,
        PlayerSpawn = 12,
        PlayerUpdate = 13,
        PlayerActive = 14,
        SyncPlayers = 15,
        PlayerHp = 16,
        Tile = 17,
        TimeSet = 18,
        DoorUse = 19,
        TileSendSquare = 20,
        ItemDrop = 21,
        ItemOwner = 22,
        NpcUpdate = 23,
        NpcItemStrike = 24,
        ChatText = 25,
        PlayerDamage = 26,
        ProjectileNew = 27,
        NpcStrike = 28,
        ProjectileDestroy = 29,
        TogglePvp = 30,
        ChestGetContents = 31,
        ChestItem = 32,
        ChestOpen = 33,
        TileKill = 34,
        EffectHeal = 35,
        Zones = 36,
        PasswordRequired = 37,
        PasswordSend = 38,
        ItemUnknown = 39,
        NpcTalk = 40,
        PlayerAnimation = 41,
        PlayerMana = 42,
        EffectMana = 43,
        PlayerKillMe = 44,
        PlayerTeam = 45,
        SignRead = 46,
        SignNew = 47,
        LiquidSet = 48,
        PlayerSpawnSelf = 49,
        PlayerBuff = 50,
        NpcSpecial = 51,
        ChestUnlock = 52,
        NpcAddBuff = 53,
        NpcUpdateBuff = 54,
        PlayerAddBuff = 55,
    }

    public enum TileCommand
    {
        KillTile = 0,
        PlaceTile = 1,
        KillWall = 2,
        PlaceWall = 3,
        KillTileNoItem = 4
    }
}