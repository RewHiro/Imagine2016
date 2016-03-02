
[System.Serializable]
public struct CharacterParameter
{
    public enum ModelType
    {
        HUMAN,
        ROBO,
        BEAST,
        NONE,
    }

    public enum CostumeType
    {
        A,
        B,
        C,
        NONE,
    }

    public ModelType modelType;
    public CostumeType costumeType;
    public uint attack;
    public uint defense;
    public uint speed;
}
