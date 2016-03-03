﻿
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

    public enum DecorationType
    {
        A,
        B,
        C,
        NONE,
    }

    public ModelType modelType;
    public CostumeType costumeType;
    public DecorationType decorationType;
    public int attack;
    public int defense;
    public int speed;
}
