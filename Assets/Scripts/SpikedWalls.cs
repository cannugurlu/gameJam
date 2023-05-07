using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikedWalls : MonoBehaviour
{
    [SerializeField] bool sideWall, upWall;

    [SerializeField] float slideTime;
    
    Vector3 targetVector;
    private void OnTriggerEnter(Collider other)     
    {
        if(other.CompareTag("Player"))
        {
            StartWallMovement();
            
            if(sideWall)
            {
                targetVector = new Vector3(transform.position.x, transform.position.y, transform.position.z +  15.0f);
            }
            else if(upWall)
            {
                targetVector = new Vector3(transform.position.x +  15.0f, transform.position.y, transform.position.z);
            }

        }
    }

    private void StartWallMovement()
    {
        if(sideWall)
        {

            transform.DOMoveX(targetVector.x,slideTime).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });

        }

        else if(upWall)
        {

            transform.DOMoveZ(targetVector.z,slideTime).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });

        }
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().KillPlayer();
        }
    }
}
