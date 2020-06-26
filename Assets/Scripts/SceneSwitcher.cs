using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneSwitcher : MonoBehaviour
{
    GameObject[] players;
    public Text text;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Update()
    {
        for(int i = 0; i < players.Length;i++)
        {
            if(players[i].GetComponent<Player>().activeplayer == true &&
                Vector3.Distance(this.gameObject.transform.position, players[i].transform.position) < 15)
            {
                text.text = "Press Q to go to " + this.gameObject.name;
                text.enabled = true;
                if(players[i].GetComponent<Player>().switcher == true && this.gameObject.name == "Level1")
                {
                    Level1Switch();
                }
            }
            if(players[i].GetComponent<Player>().activeplayer == true && 
                Vector3.Distance(this.gameObject.transform.position, players[i].transform.position) > 15)
            {
                text.enabled = false;
            }
        }
    }
    //void OnTriggerEnter(Collider Col)
    //{
        
    //        if (Col.gameObject.tag == "Player" && this.gameObject.name == "Level1"
    //            && Col.gameObject.GetComponent<Player>().switcher == true)
    //        {
    //            Debug.Log("Works");
    //            Level1Switch();
    //        }
    //        if (Col.gameObject.tag == "Player" && this.gameObject.name == "Level2" 
    //            && Col.gameObject.GetComponent<Player>().switcher == true)
    //        {
    //            Level2Switch();
    //        }
    //        if (Col.gameObject.tag == "Player" && this.gameObject.name == "Level3" 
    //            && Col.gameObject.GetComponent<Player>().switcher == true)
    //        {
    //            Level3Switch();
    //        }
    //        if (Col.gameObject.tag == "Player" && this.gameObject.name == "Hub_Trig")
    //        {
    //            LevelHubSwitch();
    //        }
        
    //}

    void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
           
                Debug.Log("Leaving area");
                if (Col.gameObject.GetComponent<Player>().switcher == true)
                {
                Col.gameObject.GetComponent<Player>().switcher = false;
                }
            
        }
    }

    public void Level1Switch()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void Level2Switch()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void Level3Switch()
    {
        SceneManager.LoadScene("Level_3");
    }
    public void LevelHubSwitch()
    {
        SceneManager.LoadScene("Hub");
    }


}

