using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType{
    Hunger,
    Hyrdration,
    Warmth,
    Cleanliness
}

[Serializable]
public class ShopItem {
    public string ItemName;
    public string ItemDesc;
    public string SpriteName;
    public float Cost;

    public string Modifiers;
    public Dictionary<StatType, float> ModifierList;
}

[Serializable]
public class ActionItem
{
    public string ActionName;
    public string ActionDesc;
    public string SpriteName;

    public string Modifiers;
    public int LevelRequired;
    public Dictionary<StatType, float> ModifierList;
}