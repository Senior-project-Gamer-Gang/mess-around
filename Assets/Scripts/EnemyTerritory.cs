using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour
{
    GameObject jeff, handman, shooter;
    bool interritory = false;
    public BoxCollider territory;
    public GameObject enemy;
    Enemy_AI basicenemy;
    GameObject playerinzone;

    List<Collider> PlayerColliding = new List<Collider>();

    void Start()
    { 
        jeff = GameObject.Find("Jeff");
        handman = GameObject.Find("HandMan");
        shooter = GameObject.Find("Shooter");
        basicenemy = enemy.GetComponent<Enemy_AI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interritory == true)
        {
            basicenemy.MoveToPlayer(playerinzone.transform);
            for(int i = 0; i < PlayerColliding.Count; i++)
            {
                //changes the player that the enemies are lookign at based
                //on who's the active player 
                if(PlayerColliding[i].GetComponent<Player>().activeplayer == true)
                {
                    playerinzone = PlayerColliding[i].gameObject;
                }
                if (i >= PlayerColliding.Count)
                    i = 0;
            }
        }

        if (interritory == false)
        {
            basicenemy.wonder();
        }

    }
    void OnTriggerEnter(Collider col)
    {
        //if the player enters the box and it isn't 
        //already in the list it get added to it
        if(!PlayerColliding.Contains(col) && 
            col.gameObject.tag == "Player")
        {
            PlayerColliding.Add(col);
        }

        if (jeff.GetComponent<Player>().activeplayer == true && col.gameObject == jeff)
        {
            playerinzone = jeff;
            interritory = true;
        }
        
        if(handman.GetComponent<Player>().activeplayer == true && col.gameObject == handman)
        {
            playerinzone = handman;
            interritory = true;
        }

        if (shooter.GetComponent<Player>().activeplayer == true && col.gameObject == shooter)
        {
            playerinzone = shooter;
            interritory = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        //if the player leaves the box take it out of the list 
        if (PlayerColliding.Contains(col))
        {
            PlayerColliding.Remove(col);
        }


        if (jeff.GetComponent<Player>().activeplayer == true && col.gameObject == jeff)
        {
            interritory = false;
        }
        if (handman.GetComponent<Player>().activeplayer == true && col.gameObject == handman)
        {
            interritory = false;
        }
        if (shooter.GetComponent<Player>().activeplayer == true && col.gameObject == shooter)
        {
            interritory = false;
        }
    }
}
