using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hint : MonoBehaviour
{
    [SerializeField] float timer;

    [SerializeField] GameObject pathFinder;

    float startingTimer;
    Player player;

    void Start() 
    {
        player = FindObjectOfType<Player>();
        startingTimer = timer;
    }

    void Update()
    {
        if(gameManager.instance.gameHasEnded)
        {
            return;
        }

        if(player.inPath)
        {
            return;
        }

        else
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                ShowPath();
            }

        }
    }

    void ShowPath()
    {
        bool pathShown = false;
        if(!pathShown)
        {
            Instantiate(pathFinder,player.transform.position,Quaternion.identity);
            pathFinder.GetComponent<NavMeshAgent>().destination = this.gameObject.transform.position;
            timer = startingTimer;
            pathShown = true;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            player.inPath = true;
        }

        if(other.CompareTag("PathFinder"))
        {
            other.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            player.inPath = false;
        }
    }
}
