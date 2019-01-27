using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{    
    [SerializeField]
    public PlayerStats playerData;

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

    Text MoneyCounter;

    float UIHunger = 100f;
    float UIHydration = 100f;
    float UICleanliness = 100f;
    float UIWarmth = 100f;

    float UIMorale = 100f;


    public bool AOEEffect = true;
    public int AOERange = 50;

    public PlayerAction actionTimer;

    private void Start()
    { 
        HungerBarProg = GameObject.Find("HungerBarProg").GetComponent<RawImage>();
        ThirstBarProg = GameObject.Find("ThirstBarProg").GetComponent<RawImage>();
        CleanBarProg = GameObject.Find("CleanlinessBarProg").GetComponent<RawImage>();
        WarmthBarProg = GameObject.Find("WarmthBarProg").GetComponent<RawImage>();
        MoraleBarProg = GameObject.Find("MoraleBarProg").GetComponent<RawImage>();

        MoneyCounter = GameObject.Find("MoneyCounter").GetComponent<Text>();

        playerData = SaveSystem.LoadPlayerData();

        ChatLogger.SendChatMessage("Game Started, Good luck!", Color.yellow);
        AOEEffect = true;
        AOERange = 75;
        actionTimer = ScriptableObject.CreateInstance<PlayerAction>();
        actionTimer.Init(15, true);
    }

    //Items list
    private void Update()
    {
        actionTimer.Update();

        if (playerData.Morale <= 0f)
            GameOver = true;

        //ReduceStats();

        playerData.Morale = (playerData.Hunger + playerData.Cleanliness + playerData.Warmth + playerData.Hydration) / 4;

        playerData.Morale = Mathf.Clamp(playerData.Morale, 0f, 100f);
        playerData.Hunger = Mathf.Clamp(playerData.Hunger, 0f, 100f);
        playerData.Cleanliness = Mathf.Clamp(playerData.Cleanliness, 0f, 100f);
        playerData.Hydration = Mathf.Clamp(playerData.Hydration, 0f, 100f);
        playerData.Warmth = Mathf.Clamp(playerData.Warmth, 0f, 100f);
        playerData.Money = Mathf.Clamp(playerData.Money, 0f, playerData.MaxMoneyStore);

        UpdateHUD();
        CheckForAOE();

        playerData.Experience++;
        CheckLevelProgress();
    }

    private void CheckForAOE()
    {
        if(AOEEffect)
        {
            GameObject.FindGameObjectWithTag("AOEDisplay").GetComponent<Light>().spotAngle = GetComponentInChildren<CapsuleCollider>().radius * 12.5f;
        }
        else
        {
            GameObject.FindGameObjectWithTag("AOEDisplay").GetComponent<Light>().spotAngle = 0;
        }
    }

    private void ReduceStats()
    {
        playerData.Hunger -= ReduceRateHunger;
        playerData.Hydration -= ReduceRateHydration;
        playerData.Cleanliness -= ReduceRateCleanliness;
        playerData.Warmth -= ReduceRateWarmth;
    }

    private void CheckLevelProgress()
    {
        if (playerData.Experience >= (playerData.Level * 120))
        {
            playerData.Level++;
            playerData.Experience = 0;
            ChatLogger.SendChatMessage("Leveled Up! Now level " + playerData.Level, Color.magenta);
        }
    }

    public void SetMaxMoney(int MaxMoney)
    {
        Mathf.Clamp(playerData.Morale, 0f, 1f);
    }

    public void UpdateHUD()
    {
        UIHunger = playerData.Hunger / 100;
        UIHydration = playerData.Hydration  / 100;
        UICleanliness = playerData.Cleanliness / 100;
        UIWarmth = playerData.Warmth / 100;
        UIMorale = playerData.Morale / 100;

        HungerBarProg.transform.localScale = new Vector3(UIHunger, 1, 1);
        ThirstBarProg.transform.localScale = new Vector3(UIHydration, 1, 1);
        CleanBarProg.transform.localScale = new Vector3(UICleanliness, 1, 1);
        WarmthBarProg.transform.localScale = new Vector3(UIWarmth, 1, 1);

        MoraleBarProg.transform.localScale = new Vector3(UIMorale, 1, 1);
        
        CultureInfo gb = CultureInfo.GetCultureInfo("en-GB");
        MoneyCounter.text = playerData.Money.ToString("c2", gb);
    }

    public void OnApplicationQuit()
    {
        SaveSystem.SavePlayerData(playerData);
    }
}
