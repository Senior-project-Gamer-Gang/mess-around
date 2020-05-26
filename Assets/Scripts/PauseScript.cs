using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public bool isPaused; //by default not paused
    public Text pausedText;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isPaused)
        {
            pausedText.enabled = true;

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = false;
            }
        }
        else if (!isPaused)
        {
            pausedText.enabled = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = true;
            }
        }
    }
}
