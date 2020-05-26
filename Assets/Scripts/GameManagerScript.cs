﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //Nothin much in here currently, of course, but more will be added down the line when more stuff is in the game.

    public int pagesCollected;
    public Text pagesText;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        pagesText.text = "x" + pagesCollected.ToString();
    }
}
