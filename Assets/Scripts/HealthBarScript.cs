using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthBarScript : MonoBehaviour
{
    private Slider healthbar;
    private float currentHP;
    private float HalfHP;
    private GameObject gamemanager;
    public Sprite[] HealthBarList;
    public Image curimage;
    GameObject[] Player;
    GameObject curplayer;
    int sceneID;
    void Awake()
    {
        healthbar = GetComponent<Slider>();
        currentHP = healthbar.maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID != 0)
        {
            Player = GameObject.FindGameObjectsWithTag("Player");
            healthbar.value = currentHP;
            if (HalfHP + 1 < currentHP)
            {
                gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.green;
            }
            if (currentHP == HalfHP + 1 || currentHP == HalfHP || currentHP == HalfHP - 1)
            {
                gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
            }
            if (currentHP < HalfHP - 1)
            {
                gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }

            for (int i = 0; i < Player.Length; i++)
            {
                if (Player[i].GetComponent<Player>().activeplayer == true)
                {
                    curplayer = Player[i];
                }
            }
            if (curplayer.name == "HandMan")
            {
                if (HalfHP + 1 < currentHP)
                {
                    curimage.sprite = HealthBarList[3];
                }
                if (currentHP == HalfHP + 1 || currentHP == HalfHP || currentHP == HalfHP - 1)
                {
                    curimage.sprite = HealthBarList[4];
                }
                if (currentHP < HalfHP - 1)
                {
                    curimage.sprite = HealthBarList[5];
                }
            }
            if (curplayer.name == "Shooter")
            {
                if (HalfHP + 1 < currentHP)
                {
                    curimage.sprite = HealthBarList[0];
                }
                if (currentHP == HalfHP + 1 || currentHP == HalfHP || currentHP == HalfHP - 1)
                {
                    curimage.sprite = HealthBarList[1];
                }
                if (currentHP < HalfHP - 1)
                {
                    curimage.sprite = HealthBarList[2];
                }
            }
            if (curplayer.name == "Jeff")
            {
                if (HalfHP + 1 < currentHP)
                {
                    curimage.sprite = HealthBarList[6];
                }
                if (currentHP == HalfHP + 1 || currentHP == HalfHP || currentHP == HalfHP - 1)
                {
                    curimage.sprite = HealthBarList[7];
                }
                if (currentHP < HalfHP - 1)
                {
                    curimage.sprite = HealthBarList[8];
                }
            }
        }
    }
    public void HPChange(float hp, float maxHP)
    {
        healthbar.maxValue = maxHP;
        HalfHP = maxHP / 2;
        currentHP = hp;
    }
}
