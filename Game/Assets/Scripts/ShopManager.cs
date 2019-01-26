using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public int ShopId;
    public List<ShopItem> ShopItems;
    public GameObject ShopItemPrefab;

	// Use this for initialization
	void Start () {


        ShopItems = FileSystem.FromJson<ShopItem>("/EntityData/Shops/FoodShop1.json").ToList();
        foreach (var item in ShopItems)
        {
            item.ModifierList = new Dictionary<StatType, float>();
            string[] modifierssplit = item.Modifiers.Split(',');
            foreach (var splititem in modifierssplit)
            {
                var data = splititem.Split(':');
                item.ModifierList.Add((StatType)Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
            }

            var pref = GameObject.Instantiate(ShopItemPrefab, this.transform.GetChild(2));

            var image = pref.transform.GetChild(0);
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>("EntityIcons/" + item.SpriteName);

            var textArea = pref.transform.GetChild(1).transform;
            textArea.GetChild(0).GetComponent<Text>().text = item.ItemName;
            textArea.GetChild(1).GetComponent<Text>().text = item.ItemDesc;

            CultureInfo gb = CultureInfo.GetCultureInfo("en-GB");
            pref.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = item.Cost.ToString("c2", gb);
        }


        int mej = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
