using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Player" && this.gameObject.name == "Level1_Trig")
        {
            Level1Switch();
        }
        if (Col.gameObject.tag == "Player" && this.gameObject.name == "Level2_Trig")
        {
            Level2Switch();
        }
        if (Col.gameObject.tag == "Player" && this.gameObject.name == "Level3_Trig")
        {
            Level3Switch();
        }
        if (Col.gameObject.tag == "Player" && this.gameObject.name == "Hub_Trig")
        {
            LevelHubSwitch();
        }

    }



    public void Level1Switch()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level2Switch()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Level3Switch()
    {
        SceneManager.LoadScene("Level3");
    }
        public void LevelHubSwitch()
    {
        SceneManager.LoadScene("Hub");
    }
}

