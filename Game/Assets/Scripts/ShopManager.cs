using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public int ShopId;
    public List<ShopItem> ShopItems;
    public GameObject ShopItemPrefab;
    public Animator ShopAnimator;
    private bool ShopOpen;


    // Use this for initialization
    void Start()
    {

        ShopOpen = false;
        ShopItems = FileSystem.FromJson<ShopItem>("/EntityData/Shops/FoodShop1.json").ToList();
        int count = 0;
        ShopItems = ShopItems.OrderBy(i => i.Cost).ToList();
        foreach (var item in ShopItems)
        {
            item.ModifierList = new Dictionary<StatType, float>();
            string[] modifierssplit = item.Modifiers.Split(',');
            foreach (var splititem in modifierssplit)
            {
                var data = splititem.Split(':');
                item.ModifierList.Add((StatType)Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
            }

            var pref = GameObject.Instantiate(ShopItemPrefab, this.transform.GetChild(1));
            var buttonInner = pref.transform.GetChild(0);

            pref.GetComponent<Button>().onClick.AddListener(() => ShopItemBought(item.ItemName));

            var image = buttonInner.GetChild(0);
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>("EntityIcons/" + item.SpriteName);

            var textArea = buttonInner.GetChild(1).transform;
            textArea.GetChild(0).GetComponent<Text>().text = item.ItemName;
            textArea.GetChild(1).GetComponent<Text>().text = item.ItemDesc;

            var effectsBar = textArea.GetChild(2);
            effectsBar.GetChild(1).GetComponent<Text>().text = item.ModifierList.Where(i => i.Key == StatType.Hunger).Sum(i => i.Value).ToString();
            effectsBar.GetChild(3).GetComponent<Text>().text = item.ModifierList.Where(i => i.Key == StatType.Hydration).Sum(i => i.Value).ToString();
            effectsBar.GetChild(5).GetComponent<Text>().text = item.ModifierList.Where(i => i.Key == StatType.Warmth).Sum(i => i.Value).ToString();
            effectsBar.GetChild(7).GetComponent<Text>().text = item.ModifierList.Where(i => i.Key == StatType.Cleanliness).Sum(i => i.Value).ToString();


            CultureInfo gb = CultureInfo.GetCultureInfo("en-GB");
            buttonInner.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = item.Cost.ToString("c2", gb);
            count++;
        }
    }

    public void ShopItemBought(string name)
    {
        //var Player = GameObject.FindGameObjectWithTag("Player");
        //Player.GetComponent<PlayerManager>();
        var item = ShopItems.FirstOrDefault(i => i.ItemName == name);        

        if (item.Cost <= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Money)
        {
            if (item.ModifierList.All(i => CheckAddable(i)))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Money -= item.Cost;
                string change = "";
                foreach (var modifier in item.ModifierList)
                {
                    change += modifier.Key.ToString() + " " + (modifier.Value < 0 ? "decreased " : "increased ") + "by " + modifier.Value + " | ";
                    AddStat(modifier);
                }
                change = change.TrimEnd('|', ' ');
                CultureInfo gb = CultureInfo.GetCultureInfo("en-GB");
                string msg = item.ItemName + " purchased for " + item.Cost.ToString("c2", gb);
                ChatLogger.SendChatMessage(msg, Color.white);
                ChatLogger.SendChatMessage(change, Color.green);
            }
        }
        else
        {
            ChatLogger.SendChatMessage("You do not have enough money for this item", Color.red);
        }
    }

    private bool AddStat(KeyValuePair<StatType, float> modifier)
    {
        switch (modifier.Key)
        {
            case StatType.Hunger:
                {
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Hunger + modifier.Value <= 100)
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Hunger += modifier.Value;
                    else
                        return false;
                }
                return true;
            case StatType.Hydration:
                {
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Hydration + modifier.Value <= 100)
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Hydration += modifier.Value;
                    else
                        return false;
                }
                return true;
            case StatType.Warmth:
                {
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Warmth + modifier.Value <= 100)
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Warmth += modifier.Value;
                    else
                        return false;
                }
                return true;
            case StatType.Cleanliness:
                {
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Cleanliness + modifier.Value <= 100)
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Cleanliness += modifier.Value;
                    else
                        return false;
                }
                return true;
            default:
                return true;

        }
    }

    private bool CheckAddable(KeyValuePair<StatType, float> modifier)
    {
        switch (modifier.Key)
        {
            case StatType.Hunger:
                var hungerval = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Hunger + modifier.Value;
                if (hungerval <= 100 && hungerval > 0)
                {
                    return true;
                }
                else if(hungerval <= 0)
                {
                    ChatLogger.SendChatMessage("You are too hungry for this item. ("+ hungerval+"/100)", Color.red);
                    return false;
                }
                else
                {
                    ChatLogger.SendChatMessage("You are not hungry enough to eat this item. (" + hungerval + "/100)", Color.red);
                    return false;
                }

            case StatType.Hydration:
                var hydrationval = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Hydration + modifier.Value;
                if (hydrationval <= 100 && hydrationval > 0)
                {
                    return true;
                }
                else if (hydrationval <= 0)
                {
                    ChatLogger.SendChatMessage("You are too thirsty for this item. (" + hydrationval + "/100)", Color.red);
                    return false;
                }
                else
                {
                    ChatLogger.SendChatMessage("You are not thirsty enough to eat this item. (" + hydrationval + "/100)", Color.red);
                    return false;
                }

            case StatType.Warmth:
                var warmthval = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Warmth + modifier.Value;
                if (warmthval <= 100 && warmthval > 0)
                {
                    return true;
                }
                else if (warmthval <= 0)
                {
                    ChatLogger.SendChatMessage("You are too cold for this item. (" + warmthval + "/100)", Color.red);
                    return false;
                }
                else
                {
                    ChatLogger.SendChatMessage("You are to hot right now. (" + warmthval + "/100)", Color.red);
                    return false;
                }

            case StatType.Cleanliness:
                var cleanlinessval = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Cleanliness + modifier.Value;
                if (cleanlinessval <= 100 && cleanlinessval > 0)
                {
                    return true;
                }
                else if (cleanlinessval <= 0)
                {
                    ChatLogger.SendChatMessage("You are too dirty for this item. (" + cleanlinessval + "/100)", Color.red);
                    return false;
                }
                else
                {
                    ChatLogger.SendChatMessage("You do not fancy this right now. (" + cleanlinessval + "/100)", Color.red);
                    return false;
                }
            default:
                return false;

        }
    }

    public void ShopAnimManager()
    {
        if (ShopOpen == false)
        {
            ShopAnimator.SetTrigger("OpenShop");
            ShopOpen = true;
        }
        else
        {
            ShopAnimator.SetTrigger("CloseShop");
            ShopOpen = false;
        }
    }
}
