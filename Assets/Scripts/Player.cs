using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // components 
    Rigidbody rigidBody;
    [SerializeField] float speedVertical, speedHorizantal, mouseControllerY, mouseControllerX;
    float pitch=0.0f;
    float yaw = 0.0f;
    private bool levelBoolen;

    [Header("Collect")]
    [SerializeField] float distanceForCollecting;

    void Start()
    {
        levelBoolen = true;
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(0, 0, speedVertical*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-speedHorizantal * Time.deltaTime, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(speedHorizantal * Time.deltaTime, 0, 0);


        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(0, 0, -speedVertical * Time.deltaTime);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            teleport();
        }
        yaw += mouseControllerX * Input.GetAxis("Mouse X");
        pitch -= mouseControllerY * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = CastRay();
            if(!hit.collider)
            {
                return;
            }
            else if(hit.collider.gameObject.CompareTag("Collectible"))
            {
                float distance = Vector3.Distance(hit.collider.gameObject.transform.position,transform.position); 
                if(distance < distanceForCollecting)
                {
                    hit.collider.gameObject.transform.parent = this.gameObject.transform;
                    hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
            }
            
        }
    }

    void teleport()
    {
        if (levelBoolen) // 1.level
        {
            float distanceX = transform.position.x - gameManager.instance.firstpos1.transform.position.x;
            float distanceZ = transform.position.z - gameManager.instance.firstpos1.transform.position.z;
            transform.position = new Vector3(gameManager.instance.firstpos2.transform.position.x + distanceX, 1.5f, 
                gameManager.instance.firstpos2.transform.position.z + distanceZ);
            levelBoolen = false;
        }
        else // 2.level
        {
            
            float distanceX = transform.position.x- gameManager.instance.firstpos2.position.x;
            float distanceZ = transform.position.z- gameManager.instance.firstpos2.position.z;
            transform.position = new Vector3(gameManager.instance.firstpos1.transform.position.x + distanceX, 1.5f,
                gameManager.instance.firstpos1.transform.position.z + distanceZ);
            levelBoolen = true;

        }
    }


    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}
