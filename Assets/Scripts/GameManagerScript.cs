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
    public int coins;
    public int redcoins;

    GameObject[] purpleCoins = new GameObject[8];
    bool[] activepurcoins = new bool[8];

    public GameObject ComicPage_RedCoin;
    public GameObject[] ComicBooks = new GameObject[25];
    public bool[] ComicBookCollected = new bool[25];

    public GameObject ShopUI;
    GameObject ShopKeeper;
    GameObject[] Players;
    public GameObject healthBar;
    int sceneID;
    bool shopopen;
    void Start()
    {
        this.gameObject.transform.position = new Vector3(-359.147f, 79.20001f, 1875.099f);
    }
    void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID == 0 || shopopen == true && this.gameObject.GetComponent<PauseScript>().isPaused == false
            || this.gameObject.GetComponent<PauseScript>().isPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (sceneID != 0 && shopopen == false && this.gameObject.GetComponent<PauseScript>().isPaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (sceneID != 0)
        {
            healthBar.SetActive(true);
        }

        if (sceneID == 0)
        {
            healthBar.SetActive(false);
        }

        if (sceneID == 2)
        {



            ComicPage_RedCoin = GameObject.Find("Red_Page");
            ShopKeeper = GameObject.FindGameObjectWithTag("ShopKeeper");
            Players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < ComicBooks.Length; i++)
            {
                if (ComicBooks[i] == null)
                    ComicBooks = GameObject.FindGameObjectsWithTag("ComicPage");

                if (ComicBookCollected.Length < ComicBooks.Length)
                    ComicBookCollected[i] = ComicBooks[i].GetComponent<PickUpables>().collected;

                if (ComicBooks[i].GetComponent<PickUpables>().collected == true)
                    ComicBookCollected[i] = true;

                if (ComicBooks.Length == ComicBookCollected.Length)
                    if (ComicBookCollected[i] == true)
                        ComicBooks[i].GetComponent<PickUpables>().collected = ComicBookCollected[i];
            }
            //what this does is, if you collect purple coins in the scene then switch scenes 
            //it'll make the purple coins that you collected in the last scene not spawn
            for (int i = 0; i < purpleCoins.Length; i++)
            {
                if (purpleCoins[i] == null)
                    purpleCoins = GameObject.FindGameObjectsWithTag("RedCoin");

                if (activepurcoins.Length < purpleCoins.Length)
                    activepurcoins[i] = purpleCoins[i].GetComponent<PickUpables>().collected;

                if (purpleCoins[i].GetComponent<PickUpables>().collected == true)
                    activepurcoins[i] = true;

                if (purpleCoins.Length == activepurcoins.Length)
                    if (activepurcoins[i] == true)
                        purpleCoins[i].GetComponent<PickUpables>().collected = activepurcoins[i];
            }
        }
        pagesText.text = pagesCollected.ToString();
        CoinTXT.text = coins.ToString();

        if (redcoins >= 8)
        {
            ComicPage_RedCoin.transform.position = new Vector3(-390.0734f, 88, 1874.154f);
        }

    }
    public void ShopOpen()
    {
        shopopen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShopKeeper.GetComponent<DoritoManScript>().Talking();
        ShopUI.SetActive(true);
    }
    public void ShopClose()
    {
        shopopen = false;
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
