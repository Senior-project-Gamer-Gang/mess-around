using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneSwitcher : MonoBehaviour
{
    GameObject[] players;
    public Text text;
    GameObject gamemanager;
    void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Update()
    {if (this.gameObject.name != "Canvas")
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Player>().activeplayer == true &&
                    Vector3.Distance(this.gameObject.transform.position, players[i].transform.position) < 15)
                {
                    Debug.Log(players[i].name);
                    text.text = "Press Q to go to " + this.gameObject.name;
                    text.enabled = true;
                    if (players[i].GetComponent<Player>().switcher == true && this.gameObject.name == "Level1")
                    {
                        Level1Switch();
                    }
                    if (players[i].GetComponent<Player>().switcher == true && this.gameObject.name == "Level2")
                    {
                        Level2Switch();
                    }
                    if (players[i].GetComponent<Player>().switcher == true && this.gameObject.name == "Level3")
                    {
                        Level3Switch();
                    }
                    if (players[i].GetComponent<Player>().activeplayer == true &&
                    Vector3.Distance(this.gameObject.transform.position, players[i].transform.position) > 12)
                    {
                        text.enabled = false;
                    }
                }

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
    public void LevelMenuSwitch()
    {
        gamemanager.GetComponent<PauseScript>().isPaused = false;
        SceneManager.LoadScene("Main_Menu");
    }

}

