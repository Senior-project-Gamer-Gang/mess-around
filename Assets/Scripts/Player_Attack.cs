using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    GameObject Jeff, handman, shooter;
   public bool Hand_Attack;
    void Start()
    {
        Jeff = GameObject.Find("Jeff");
        handman = GameObject.Find("HandMan");
        shooter = GameObject.Find("Shooter");
    }
    private void OnTriggerEnter(Collider Col)
    {
        //this is so big hand_man can break walls 
        Hand_Attack = handman.GetComponent<Player>().handattack;
        if (this.gameObject.name == ("Hand_Attack(Clone)") && Col.gameObject.tag == "Wall" && Hand_Attack == true)
        {
            Destroy(Col.gameObject);
            Destroy(this.gameObject);
        }

        if (this.gameObject.name == ("bullet(Clone)") && Col.gameObject.tag != "enemy" && 
            Col.gameObject.name != "Enemy_Box" && Col.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
