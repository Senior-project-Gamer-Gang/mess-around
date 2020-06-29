using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int pagesCollected;
    public Text pagesText;
    public Text CoinTXT;
    //temp amount just for texting purposes 
    public int coins = 200;
    public int redcoins;
    public GameObject ComicPage_RedCoin;
    public GameObject ShopUI;
    GameObject ShopKeeper;
    GameObject[] Players;
    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        ShopKeeper = GameObject.FindGameObjectWithTag("ShopKeeper");
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        pagesText.text = "Pages Aquired: " + pagesCollected.ToString();
        CoinTXT.text = "Coin amount: " + coins.ToString();
        if (ComicPage_RedCoin != null)
        {
            if (redcoins >= 8)
            {
                ComicPage_RedCoin.SetActive(true);
            }
        }
    }
    public void ShopOpen()
    {
       
        ShopKeeper.GetComponent<DoritoManScript>().Talking();
        ShopUI.SetActive(true);
    }
    public void ShopClose()
    {
        for(int i = 0; i < Players.Length; i++)
        {
            if(Players[i].GetComponent<Player>().inshop == true)
            {
                Players[i].GetComponent<Player>().inshop = false;
            }
        }
        ShopKeeper.GetComponent<DoritoManScript>().TalkingNot();
        ShopUI.SetActive(false);
    }
}
