using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour
{
    public bool isPaused; //by default not paused
    public Text pausedText;

    public GameObject PauseObject;
    int sceneID;

    void Start()
    {
        isPaused = false;
    }
    void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID == 0)
        {
            PauseObject.SetActive(false);
        }
            if (sceneID != 0)
        {
            if (isPaused)
            {

                PauseObject.SetActive(true);
                pausedText.enabled = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isPaused = false;
                }
            }
            else if (!isPaused)
            {

                PauseObject.SetActive(false);
                pausedText.enabled = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isPaused = true;
                }
            }
        }
    }
}
