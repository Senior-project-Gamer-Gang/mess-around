using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour
{
    GameObject Jeff, HandMan, Shooter;
    public bool interritory = false;
    public BoxCollider territory;
    Enemy_AI basicenemy;
    GameObject playerinzone;
    string playername;
    List<Collider> PlayerColliding = new List<Collider>();
    Collider tempcol;
    void Start()
    {
        Jeff = GameObject.Find("Jeff");
        HandMan = GameObject.Find("HandMan");
        Shooter = GameObject.Find("Shooter");
        basicenemy = this.gameObject.transform.parent.gameObject.
            GetComponent<Enemy_AI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interritory == true)
        {

                if (basicenemy.enemyshooter == false)
                {
                    basicenemy.curplay = playerinzone; 
                    basicenemy.player = playerinzone.transform;
                    basicenemy.EnemyStates = Enemy_AI.enemystates.attacking;
                }
                if (basicenemy.enemyshooter == true)
                {
                    basicenemy.curplay = playerinzone;
                    basicenemy.player = playerinzone.transform;
                    basicenemy.EnemyStates = Enemy_AI.enemystates.attacking;
                }
            
            for (int i = 0; i < PlayerColliding.Count; i++)
            {
                //changes the player that the enemies are lookign at based
                //on who's the active player 
                if (PlayerColliding[i].GetComponent<Player>().activeplayer == true)
                {
                    playerinzone = PlayerColliding[i].gameObject;
                }
                if (i >= PlayerColliding.Count)
                    i = 0;
            }
            if (Vector3.Distance(gameObject.GetComponentInParent<Transform>().position,
           tempcol.transform.position) > 15)
            {
                PlayerColliding.Remove(tempcol);
                interritory = false;
            }
        }

    }
    void OnTriggerEnter(Collider col)
    {
        //if the player enters the box and it isn't 
        //already in the list it get added to it

        if (!PlayerColliding.Contains(col) &&
            col.gameObject.tag == "Player")
        {
            playername = col.name;
            PlayerColliding.Add(col);
            tempcol = col;
        }
        
        if (Jeff.GetComponent<Player>().activeplayer == true && 
            col.gameObject == Jeff)
        {
            
            playerinzone = Jeff;
            interritory = true;
        }

        if (HandMan.GetComponent<Player>().activeplayer == true && 
            col.gameObject == HandMan)
        {
            
            playerinzone = HandMan;
            interritory = true;
        }

        if (Shooter.GetComponent<Player>().activeplayer == true && 
            col.gameObject == Shooter)
        {
            
            playerinzone = Shooter;
            interritory = true;
        }
       

    }
}
