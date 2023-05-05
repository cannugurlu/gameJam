using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // components 
    Rigidbody rigidBody;
    [Header("Movement")]
    [SerializeField] float speedVertical;
    [SerializeField] float speedHorizantal; 
    [SerializeField] float mouseControllerY;
    [SerializeField] float mouseControllerX;

    [Header("Mouse")]

    [SerializeField] float pitchMax;
    [SerializeField] float pitchMin;
    [SerializeField] float yawMax;
    [SerializeField] float yawMin;

    float pitch=0.0f;
    float yaw = 0.0f;

    private bool levelBoolen;

    [Header("Collect")]
    [SerializeField] float distanceForCollecting;

    GameObject selectedObject;


    [Header("Skills")]

    [SerializeField] float sizeChange;

    bool downScaled;

    void Start()
    {
        levelBoolen = true;
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            DownScale();
        }

        yaw += mouseControllerX * Input.GetAxis("Mouse X");
        pitch -= mouseControllerY * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch,pitchMin,pitchMax);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        if(Input.GetMouseButtonDown(0))
        {

            RaycastHit hit = CastRay();

            if(!hit.collider)
            {
                return;
            }

            else if(hit.collider.gameObject.CompareTag("Collectible") && selectedObject == null)
            {

                selectedObject = hit.collider.gameObject;
                float distance = Vector3.Distance(selectedObject.transform.position,transform.position); 
                if(distance < distanceForCollecting)
                {
                    selectedObject.transform.parent = this.gameObject.transform;

                    selectedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }

            }

            else if(selectedObject != null)
            {
                selectedObject.transform.parent = null;
                selectedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                selectedObject = null;
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

    void DownScale()
    {
        if(!downScaled)
        {
            transform.localScale= transform.localScale * sizeChange;
            distanceForCollecting = distanceForCollecting / 2 ;
            downScaled = true;
        }
        else
        {
            transform.localScale= new Vector3(1,1,1);
            distanceForCollecting = distanceForCollecting * 2 ;
            downScaled = false;
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
