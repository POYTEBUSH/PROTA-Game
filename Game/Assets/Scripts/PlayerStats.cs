using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerStats {

    public float Morale;
    public float Hunger;
    public float Hydration;
    public float Cleanliness;
    public float Warmth;

    public float Money;
    public float MaxMoneyStore;
    public string Name;

    public int Level;
    public float Experience;
    public int DaysSurvived;
}
