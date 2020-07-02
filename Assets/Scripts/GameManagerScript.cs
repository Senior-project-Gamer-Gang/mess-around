using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public GameObject healthBar;
    int sceneID;
    void Start()
    {
        this.gameObject.transform.position = new Vector3(-188.1165f, 72.53726f, 2067.603f);
    }
    void Update()
    {
        if (sceneID != 0)
        {
            healthBar.SetActive(true);
        }
        sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID == 0)
        {
            healthBar.SetActive(false);
        }
       
        if (sceneID == 2)
        {
            ShopKeeper = GameObject.FindGameObjectWithTag("ShopKeeper");
            Players = GameObject.FindGameObjectsWithTag("Player");
        }
        pagesText.text = pagesCollected.ToString();
        CoinTXT.text = coins.ToString();
        if (ComicPage_RedCoin != null && sceneID == 2)
        {
            if (redcoins < 8)
            {
                ComicPage_RedCoin.SetActive(false);
            }
            if (redcoins >= 8)
            {
                ComicPage_RedCoin.transform.position = new Vector3(-172.4f, 5.23674f, -188.1165f);
                ComicPage_RedCoin.SetActive(true);
            }
        }
    }
    public void ShopOpen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShopKeeper.GetComponent<DoritoManScript>().Talking();
        ShopUI.SetActive(true);
    }
    public void ShopClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].GetComponent<Player>().inshop == true)
            {
                Players[i].GetComponent<Player>().inshop = false;
            }
        }
        ShopKeeper.GetComponent<DoritoManScript>().TalkingNot();
        ShopUI.SetActive(false);
    }
}
