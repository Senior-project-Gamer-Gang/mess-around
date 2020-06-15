using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public List<GameObject> items;
    public List<GameObject> itemPartInShop;
    
    void Start()
    {

    }

    void Update()
    {
        
        for (int i = 0; i < items.Count; i++)
        {
            int temp = 70 * (i +1);
            itemPartInShop[i].GetComponentInChildren<Text>().text = items[i].name;
            if(itemPartInShop.Count != items.Count)
            {  
               itemPartInShop.Add(Instantiate(itemPartInShop[i]));
                itemPartInShop[i + 1].GetComponent<Transform>().position = 
                    this.transform.GetChild(2).transform.position + new Vector3(0, -temp, 0);

                itemPartInShop[i + 1].transform.parent = this.transform.GetChild(2).transform;
            }
        }

    }
}
