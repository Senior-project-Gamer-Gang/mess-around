using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpables : MonoBehaviour
{
    GameObject[] hpPickup;
    GameObject[] coinPickup;
    GameObject[] redcoinsPickup;
    private float rotationSpeed = .5f;
    public GameManagerScript GM;
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }
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
