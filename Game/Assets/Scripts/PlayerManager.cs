using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float Morale = 100f;
    public float Hunger = 100f;
    public float Hydration = 100f;
    public float Cleanliness = 100f;
    public float Warmth = 100f;

    public float Money;
    private int MaxMoneyStore;
    public string Name;

    public int Level = 1;
    public float Experience;
    public int DaysSurvived;

    public bool GameOver;

    private float ReduceRateHunger = 0.02f;
    private float ReduceRateHydration = 0.02f;
    private float ReduceRateCleanliness = 0.02f;
    private float ReduceRateWarmth = 0.02f;

    private void Start()
    {
        Morale = 100f;
        Hunger = 100f;
        Hydration = 100f;
        Cleanliness = 100f;
        Warmth = 100f;
    }

    //Items list
    private void Update()
    {
        if (Morale <= 0f)
            GameOver = true;

        ReduceStats();

        Morale = (Hunger + Cleanliness + Warmth + Hydration) / 4;

        Morale = Mathf.Clamp(Morale, 0f, 100f);
        Hunger = Mathf.Clamp(Hunger, 0f, 100f);
        Cleanliness = Mathf.Clamp(Cleanliness, 0f, 100f);
        Hydration = Mathf.Clamp(Hydration, 0f, 100f);
        Warmth = Mathf.Clamp(Warmth, 0f, 100f);
        Money = Mathf.Clamp(Money, 0f, MaxMoneyStore);

        Experience++;
        CheckLevelProgress();
    }

    private void ReduceStats()
    {
        Hunger -= ReduceRateHunger;
        Hydration -= ReduceRateHydration;
        Cleanliness -= ReduceRateCleanliness;
        Warmth -= ReduceRateWarmth;
    }

    private void CheckLevelProgress()
    {
        if (Experience >= (Level * 120))
        {
            Level++;
            Experience = 0;
        }
    }

    public void SetMaxMoney(int MaxMoney)
    {
        Mathf.Clamp(Morale, 0f, 1f);
    }
}
