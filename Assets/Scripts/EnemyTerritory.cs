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
        }

        if (interritory == false)
        {
            basicenemy.wonder();
        }
    }
    void OnTriggerEnter(Collider col)
    {
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
