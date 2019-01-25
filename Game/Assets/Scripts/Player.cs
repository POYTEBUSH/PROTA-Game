using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Morale;
    public float Hunger;
    public float Hydration;
    public float Cleanliness;
    public float Warmth;

    public float Money;
    private int MaxMoneyStore;
    public string Name;

    public int Level = 1;
    public float Experience;
    public int DaysSurvived;

    public bool GameOver;

    //Items list
    public void Update()
    {
        if (Morale <= 0f)
            GameOver = true;

        Mathf.Clamp(Morale, 0f, 1f);
        Mathf.Clamp(Hunger, 0f, 1f);
        Mathf.Clamp(Cleanliness, 0f, 1f);
        Mathf.Clamp(Warmth, 0f, 1f);
        Mathf.Clamp(Money, 0f, MaxMoneyStore);

        Experience++;
        CheckLevelProgress();
    }

    private void CheckLevelProgress()
    {
        if(Experience >= (Level * 120))
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
