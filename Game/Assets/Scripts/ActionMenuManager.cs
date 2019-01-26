using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuManager : MonoBehaviour {

    public int ShopId;
    public List<ActionItem> ActionItems;
    public GameObject ActionItemPrefab;
    public Animation anim;
	// Use this for initialization
	void Start () {


        ActionItems = FileSystem.FromJson<ActionItem>("/EntityData/Actions/Actions1.json").ToList();
        int count = 0;
        foreach (var item in ActionItems.OrderBy(i=>i.LevelRequired))
        {
            item.ModifierList = new Dictionary<StatType, float>();
            string[] modifierssplit = item.Modifiers.Split(',');
            foreach (var splititem in modifierssplit)
            {
                var data = splititem.Split(':');
                item.ModifierList.Add((StatType)Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
            }

            var pref = GameObject.Instantiate(ActionItemPrefab, this.transform.GetChild(2));
            var buttonInner = pref.transform.GetChild(0);

            pref.GetComponent<Button>().onClick = ShopItemBought(count);

            var image = buttonInner.GetChild(0);
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>("EntityIcons/" + item.SpriteName);

            var textArea = buttonInner.GetChild(1).transform;
            textArea.GetChild(0).GetComponent<Text>().text = item.ActionName;
            textArea.GetChild(1).GetComponent<Text>().text = item.ActionDesc;

            var effectsBar = textArea.GetChild(2);
            effectsBar.GetChild(1).GetComponent<Text>().text = item.ModifierList.Where(i => i.Key == StatType.Hunger).Sum(i => i.Value).ToString();
            effectsBar.GetChild(3).GetComponent<Text>().text = item.ModifierList.Where(i => i.Key == StatType.Hyrdration).Sum(i => i.Value).ToString();
            effectsBar.GetChild(5).GetComponent<Text>().text = item.ModifierList.Where(i => i.Key == StatType.Warmth).Sum(i => i.Value).ToString();
            effectsBar.GetChild(7).GetComponent<Text>().text = item.ModifierList.Where(i => i.Key == StatType.Cleanliness).Sum(i => i.Value).ToString();
            count++;
        }

        anim.Play("SlideIn");
        int mej = 0;
    }

    private Button.ButtonClickedEvent ShopItemBought(int i)
    {
        var Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerManager>();

        Debug.Log("Button");

        return new Button.ButtonClickedEvent();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
