using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseOverUI : MonoBehaviour
{
    public GameObject Character;
    public Sprite Starting, Ending, startingimg,Endingimg;
    public Image img;
    public void OnMouseOver()
    {
        Character.SetActive(true);
        this.gameObject.GetComponent<Button>().image.sprite = Ending;
        img.sprite = Endingimg;
    }
    public void OnMouseExit()
    {
        Character.SetActive(false);
        this.gameObject.GetComponent<Button>().image.sprite = Starting;
        img.sprite = startingimg;
    }
}
