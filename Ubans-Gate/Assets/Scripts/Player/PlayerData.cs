using System;

// This class is used to store player data except inventory data, Save button in Player class.
// Inventory data is in InventoryObject class, Save button in PlayerInventory class.
[Serializable]
public class PlayerData
{
    public int Level;
    public int Exp;

    public float Hp;
    public float Mp;

    public int Str;
    public int Def;
    public int Agi;
    public int Vit;
    public int Int;
    public int Lck;
    public float[] Position;

    public PlayerData (Player player)
    {
        this.Level = player.Level;
        this.Exp = player.Exp;

        this.Hp = player.Hp;
        this.Mp = player.Mp;

        this.Str = player.PlayerStr;
        this.Def = player.PlayerDef;
        this.Agi = player.PlayerAgi;
        this.Vit = player.PlayerVit;
        this.Int = player.PlayerInt;
        this.Lck = player.PlayerLck;

        Position = new float[3];
        Position[0] = player.transform.position.x;
        Position[1] = player.transform.position.y;
        Position[2] = player.transform.position.z;
    }
}