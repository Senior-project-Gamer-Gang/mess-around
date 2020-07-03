using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MouseOverUI : MonoBehaviour
{
    public GameObject Character;
    public Sprite Starting, Ending, startingimg, Endingimg;
    public Image img;
    int sceneID;

    void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
    }
    public void OnMouseOver()
    {
        if (sceneID == 0)
        {
            if (Character != null)
            {
                Character.SetActive(true);
                this.gameObject.GetComponent<Button>().image.sprite = Ending;
                img.sprite = Endingimg;
            }
        }
    }
    public void OnMouseExit()
    {
        if (sceneID == 0)
        {
            if (Character != null)
            {
                Character.SetActive(false);
                this.gameObject.GetComponent<Button>().image.sprite = Starting;
                img.sprite = startingimg;
            }
        }
    }
}
