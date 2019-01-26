using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour {

	public static void SavePlayerData(PlayerStats playerdata)
    {
        PlayerPrefs.SetString("PlayerName", playerdata.Name);

        PlayerPrefs.SetFloat("PlayerHunger", playerdata.Hunger);
        PlayerPrefs.SetFloat("PlayerHydration", playerdata.Hydration);
        PlayerPrefs.SetFloat("PlayerWarmth", playerdata.Warmth);
        PlayerPrefs.SetFloat("PlayerCleanliness", playerdata.Cleanliness);

        PlayerPrefs.SetFloat("PlayerMorale", playerdata.Morale);
        PlayerPrefs.SetFloat("PlayerMoney", playerdata.Money);

        PlayerPrefs.SetInt("PlayerLevel", playerdata.Level);
        PlayerPrefs.SetFloat("PlayerExperience", playerdata.Experience);
        PlayerPrefs.SetInt("PlayerDaysSurvived", playerdata.DaysSurvived);
    }

    public static PlayerStats LoadPlayerData()
    {
        PlayerStats playerdata = new PlayerStats();
        playerdata.Name = PlayerPrefs.GetString("PlayerName");

        playerdata.Hunger = PlayerPrefs.GetFloat("PlayerHunger");
        playerdata.Hydration = PlayerPrefs.GetFloat("PlayerHydration");
        playerdata.Warmth = PlayerPrefs.GetFloat("PlayerWarmth");
        playerdata.Cleanliness = PlayerPrefs.GetFloat("PlayerCleanliness");

        playerdata.Morale = PlayerPrefs.GetFloat("PlayerMorale");
        playerdata.Money = PlayerPrefs.GetFloat("PlayerMoney");

        playerdata.Level = PlayerPrefs.GetInt("PlayerLevel");
        playerdata.Experience = PlayerPrefs.GetFloat("PlayerExperience");
        playerdata.DaysSurvived = PlayerPrefs.GetInt("PlayerDaysSurvived");

        return playerdata;
    }
}
