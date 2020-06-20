﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int pagesCollected;
    public Text pagesText;
    //temp amount just for texting purposes 
    public int coins = 200;
    public int redcoins;
    public GameObject ComicPage_RedCoin;
    public GameObject ShopUI;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        pagesText.text = "x" + pagesCollected.ToString();
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
        ShopUI.SetActive(true);
    }
    public void ShopClose()
    {
        ShopUI.SetActive(false);
    }
}
