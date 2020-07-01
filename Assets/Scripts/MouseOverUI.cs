using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseOverUI : MonoBehaviour
{
    public Sprite Starting, Ending, startingimg,Endingimg;
    public Image img;
    public void OnMouseOver()
    {
        this.gameObject.GetComponent<Button>().image.sprite = Ending;
        img.sprite = Endingimg;
    }
    public void OnMouseExit()
    {
        this.gameObject.GetComponent<Button>().image.sprite = Starting;
        img.sprite = startingimg;
    }
}
