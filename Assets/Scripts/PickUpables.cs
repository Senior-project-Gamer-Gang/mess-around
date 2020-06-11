using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpables : MonoBehaviour
{
    GameObject[] hpPickup;
    GameObject[] coinPickup;
    GameObject[] redcoinsPickup;
    private float rotationSpeed = .5f;
    GameManagerScript GM;
    void Start()
    {
        //if(this.gameObject.tag == "Heart")
        //    hpPickup = GameObject.FindGameObjectsWithTag("HpPickup");
        //if (this.gameObject.tag == "Coin")
        //    coinPickup = GameObject.FindGameObjectsWithTag("CoinPickup");
        //if (this.gameObject.tag == "RedCoin")
        //    redcoinsPickup = GameObject.FindGameObjectsWithTag("RedCoinsPickup");

        GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), rotationSpeed);
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(this.gameObject.tag == "Heart")
            {
                col.GetComponent<Player>().hp += 1;
                Destroy(this.gameObject);
            }
            if (this.gameObject.tag == "Coin")
            {
                GM.coins += 1;
                Destroy(this.gameObject);
            }
            if (this.gameObject.tag == "RedCoin")
            {
                GM.redcoins += 1;
                Destroy(this.gameObject);
            }
        }
    }
}
