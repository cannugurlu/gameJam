using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    [Header("Collect")]
    [SerializeField] float distanceForCollecting;

    public GameObject selectedObject;


    [Header("Skills")]

    [SerializeField] float sizeChange;
    [SerializeField] float scalingTime;
    [SerializeField] float increasedSpeed;
    bool levelBool;
    public bool canTeleport;
    
    float startingSpeed;
    bool downScaled;
    

    // bools
    public static bool isPlayerMoving;
    public bool inPath = true;

    bool canMove = true;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        

        startingSpeed = speedVertical;
    }

    void Update()
    {
        Debug.Log(levelBool);
        if(gameManager.instance.gameHasEnded)
        {
            isPlayerMoving = false;
            return;
        }

        transform.position = new Vector3(transform.position.x, 4.36f, transform.position.z);

        if(canMove)
        {
            MoveCharacter();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && canTeleport)
        {
            teleport();
        }

        if (Input.GetKeyDown(KeyCode.E) && !levelBool)
        {
            RunFaster();
        }

        if (Input.GetKeyDown(KeyCode.E) && levelBool)
        {
            DownScale();
        }

        if(!gameManager.instance.gameHasEnded)
        {
            yaw += mouseControllerX * Input.GetAxis("Mouse X");
            pitch -= mouseControllerY * Input.GetAxis("Mouse Y");

            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit = CastRay();

            if (!hit.collider)
            {
                return;
            }

            else if (hit.collider.gameObject.CompareTag("Collectible") && selectedObject == null)
            {
                selectedObject = hit.collider.gameObject;
                float distance = Vector3.Distance(selectedObject.transform.position, transform.position);

                Debug.Log(selectedObject.name);
                if (distance < distanceForCollecting)
                {

                    selectedObject.transform.parent = this.gameObject.transform;

                    selectedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

                    selectedObject.GetComponent<BoxCollider>().size *= 2;
                }

            }

            else if (selectedObject != null)
            {

                selectedObject.transform.parent = null;
                selectedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                selectedObject.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                selectedObject = null;
            }

        }
    }

    private void MoveCharacter()
    {
        rigidBody.velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(0, 0, speedVertical * Time.deltaTime);
            isPlayerMoving  = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-speedHorizantal * Time.deltaTime, 0, 0);
            isPlayerMoving  = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(speedHorizantal * Time.deltaTime, 0, 0);
            isPlayerMoving  = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(0, 0, -speedVertical * Time.deltaTime);
            isPlayerMoving  = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isPlayerMoving  = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            isPlayerMoving  = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isPlayerMoving  = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isPlayerMoving  = false;
        }
    }

    void ChangeCharacter()
    {
        speedVertical = startingSpeed;

        transform.localScale = new Vector3(2.9f,2.9f,2.9f);

        downScaled = false;
    }

    //skills
    void teleport()
    {
        if (!levelBool) // 1.level
        {
            float distanceX = transform.position.x - gameManager.instance.firstpos1.transform.position.x;
            float distanceZ = transform.position.z - gameManager.instance.firstpos1.transform.position.z;
            transform.position = new Vector3(gameManager.instance.firstpos2.transform.position.x + distanceX, 4.36f, 
                gameManager.instance.firstpos2.transform.position.z + distanceZ);

            gameManager.instance.AncientMaze.SetActive(true);
            gameManager.instance.futureMaze.SetActive(false);
            levelBool = true;
            ChangeCharacter();

        }
        else // 2.level
        {
            float distanceX = transform.position.x- gameManager.instance.firstpos2.position.x;
            float distanceZ = transform.position.z- gameManager.instance.firstpos2.position.z;
            transform.position = new Vector3(gameManager.instance.firstpos1.transform.position.x + distanceX, 4.36f,
                gameManager.instance.firstpos1.transform.position.z + distanceZ);
            levelBool = false;
            gameManager.instance.AncientMaze.SetActive(false);
            gameManager.instance.futureMaze.SetActive(true);
            ChangeCharacter();
        }

        canTeleport = false;
    }

    void DownScale()
    {
        if(gameManager.instance.gameHasEnded)
        {
            return;
        }
        if(!downScaled)
        {
            transform.DOScale(0.5f, scalingTime); 
            distanceForCollecting = 0.8f ;
            downScaled = true;
        }
        else
        {
            transform.DOScale(2.9f, scalingTime);
            distanceForCollecting = 4.5f ;
            downScaled = false;
        }
    }

    private void RunFaster()
    {
        speedVertical = increasedSpeed;
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


    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Walls"))
        {
            canMove = false;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("Walls"))
        {
            canMove = true;
        }
        
    }

    public void KillPlayer()
    {
       
    }
}
