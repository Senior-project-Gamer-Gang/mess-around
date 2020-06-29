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
    Scene curscene;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (curscene.name == "Main_Menu")
        {
            isPaused = false;
        }
            if (curscene.name != "Main_Menu")
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
