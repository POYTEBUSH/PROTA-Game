using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopListManager : MonoBehaviour {

    // Use this for initialization
    List<Shop> Shops;
    public GameObject ShopPrefab;

	void Start () {
        Shops = FileSystem.FromJson<Shop>("/EntityData/Shops/StoreListing.json").ToList();

        for (int i = 0; i < Shops.Count; i++)
        {
            var newShop = Instantiate(ShopPrefab, transform);
            var newShopHolder = newShop.transform.GetChild(0).GetChild(0);
            newShopHolder.GetComponent<ShopManager>().ShopPath = Shops[i].StoreName;
            newShopHolder.GetComponent<ShopManager>().LoadShopItems();

            newShopHolder.transform.GetChild(0).GetComponent<Text>().text = Shops[i].StoreName;

            var startPosY = newShopHolder.transform.GetChild(2).transform.position.y - (64 * Shops.Count-1);

            newShopHolder.transform.GetChild(2).transform.position = new Vector3(newShopHolder.transform.GetChild(2).transform.position.x,
                startPosY + (64*i),
                newShopHolder.transform.GetChild(2).transform.position.z);

            newShopHolder.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("EntityIcons/" + Shops[i].StoreSprite);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}