using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hint : MonoBehaviour
{
    [SerializeField] float timer;

    [SerializeField] GameObject pathFinder;

    Player player;

    void Start() 
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(player.inPath)
        {
            return;
        }
        else
        {
            timer -= Time.deltaTime;
            if(timer == 0)
            {
                ShowPath();
            }
        }
    }

    void ShowPath()
    {
        Instantiate(pathFinder,player.transform.position,Quaternion.identity);
        pathFinder.GetComponent<NavMeshAgent>().SetDestination(this.gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other) 
    {
        player.inPath = true;
    }

    private void OnTriggerExit(Collider other) 
    {
        player.inPath = false;
    }
}
