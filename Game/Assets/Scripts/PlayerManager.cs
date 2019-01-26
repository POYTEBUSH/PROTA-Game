using System;
using UnityEngine;
using UnityEngine.UI;

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

    RawImage HungerBarProg;

    RawImage ThirstBarProg;
    RawImage CleanBarProg;
    RawImage WarmthBarProg;

    RawImage MoraleBarProg;

    public float UIHunger = 100f;
    public float UIHydration = 100f;
    public float UICleanliness = 100f;
    public float UIWarmth = 100f;

    public float UIMorale = 100f;

    private void Start()
    {
        HungerBarProg = GameObject.Find("HungerBarProg").GetComponent<RawImage>();
        ThirstBarProg = GameObject.Find("ThirstBarProg").GetComponent<RawImage>();
        CleanBarProg = GameObject.Find("CleanlinessBarProg").GetComponent<RawImage>();
        WarmthBarProg = GameObject.Find("WarmthBarProg").GetComponent<RawImage>();
        MoraleBarProg = GameObject.Find("MoraleBarProg").GetComponent<RawImage>();

        Morale = 100f;
        Hunger = 100f;
        Hydration = 100f;
        Cleanliness = 100f;
        Warmth = 100f;

        ChatLogger.SendChatMessage("Game Started, Good luck!", Color.yellow);
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

        UpdateHUD();

        Experience++;
        CheckLevelProgress();
    }

    private void ReduceStats()
    {
        Hunger -= ReduceRateHunger;
        Hydration -= ReduceRateHydration;
        Cleanliness -= ReduceRateCleanliness;
        Warmth -= ReduceRateWarmth;

        if(Hunger <= 0f)
            ChatLogger.SendChatMessage("You are super hungry, eat some food fast!", Color.red);
        if (Hydration <= 0f)
            ChatLogger.SendChatMessage("You are super thirsty, drink something soon!", Color.red);
        if (Cleanliness <= 0f)
            ChatLogger.SendChatMessage("You are dirty, oh so dirty!", Color.red);
        if (Warmth <= 0f)
            ChatLogger.SendChatMessage("You are bloody freezing", Color.red);
    }

    private void CheckLevelProgress()
    {
        if (Experience >= (Level * 120))
        {
            Level++;
            Experience = 0;
            ChatLogger.SendChatMessage("Leveled Up! Now level " + Level, Color.magenta);
        }
    }

    public void SetMaxMoney(int MaxMoney)
    {
        Mathf.Clamp(Morale, 0f, 1f);
    }

    public void UpdateHUD()
    {
        UIHunger = Hunger / 100;
        UIHydration = Hydration  / 100;
        UICleanliness = Cleanliness / 100;
        UIWarmth = Warmth / 100;
        UIMorale = Morale / 100;

        HungerBarProg.transform.localScale = new Vector3(UIHunger, 1, 1);
        ThirstBarProg.transform.localScale = new Vector3(UIHydration, 1, 1);
        CleanBarProg.transform.localScale = new Vector3(UICleanliness, 1, 1);
        WarmthBarProg.transform.localScale = new Vector3(UIWarmth, 1, 1);

        MoraleBarProg.transform.localScale = new Vector3(UIMorale, 1, 1);


        //HungerBarProg.transform.position = new Vector3(-727.2, 1, 1);
        //ThirstBarProg.transform.position = new Vector3(-727.2, 1, 1);
        //CleanBarProg.transform.position = new Vector3(-727.2, 1, 1);
        //WarmthBarProg.transform.position = new Vector3(-727.2, 1, 1);

        //MoraleBarProg.transform.position = new Vector3(UIMorale, 1, 1);

    }
}
