using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public List<GameObject> items;
    public List<GameObject> itemPartInShop;
    public List<int> itemcosts;
    GameObject shopkeeper;
    GameObject GameManagerOBJ;
   
    void Start()
    {
        shopkeeper = GameObject.FindGameObjectWithTag("ShopKeeper");
        GameManagerOBJ = GameObject.Find("GameManager");
        itemcosts.Add(100);
        itemcosts.Add(100);
        itemcosts.Add(100);
        itemcosts.Add(100);
        for (int i = 0; i < items.Count; i++)
        {
            int temp = 70 * (i + 1);

            if (itemPartInShop.Count != items.Count)
            {
                itemPartInShop[i].GetComponentInChildren<Text>().text = items[i].name;
                itemPartInShop[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = itemcosts[i].ToString() + " Coins";
                itemPartInShop.Add(Instantiate(itemPartInShop[i]));
                itemPartInShop[i + 1].GetComponent<Transform>().position =
                    this.transform.GetChild(2).transform.position + new Vector3(0, -temp, 0);

                itemPartInShop[i + 1].transform.parent = this.transform.GetChild(2).transform;
            }
        }
        }

    void Update()
    {
        GameManagerOBJ = GameObject.Find("GameManager");
        for (int i = 0; i < items.Count; i++)
        {
            if(GameManagerOBJ.GetComponent<GameManagerScript>().coins >= itemcosts[i] && 
                itemPartInShop[i].transform.GetChild(1).GetComponent<ShopBtn>().triggered == true)
            {
                GameManagerOBJ.GetComponent<GameManagerScript>().coins -= itemcosts[i];
                GameManagerOBJ.GetComponent<GameManagerScript>().pagesCollected += 1;
                itemPartInShop[i].GetComponentInChildren<Text>().text = "Bought";
                itemcosts.Remove(itemcosts[i]);
                items.Remove(items[i]);
                
            }
        }

    }

}
