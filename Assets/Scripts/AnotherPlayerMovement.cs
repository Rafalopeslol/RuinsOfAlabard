using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlayerMovement : MonoBehaviour, IPunInstantiateMagicCallback
{
    public bool inWindZone = false;
    public GameObject windZone;
    public Transform playerCheckPoint;
    private PhotonView View;
    public GameObject cam;
    public float speed = 6f;
    public Rigidbody rb;
    public float jumpForce = 50f;
    public float gravityStrength;
    public float launchForce = 1200f;
    public float groundDistance;
    private bool canJump = true;
    Collider myCol;
    public Transform Checkpoint;

    void Awake()
    {
        View = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        myCol = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        //jump = new Vector3(0f, 5f, 0f);
        cam.SetActive(View.IsMine);
        groundDistance = myCol.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gravity = new Vector3(0, gravityStrength, 0);
        Physics.gravity = gravity;
        if (!View.IsMine) return;
        Movement();      
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(direction, Space.Self);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && canJump == true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));

        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance + 0.1f);
        //return Physics.CheckCapsule(myCol.bounds.center, new Vector3(myCol.bounds.center.x, myCol.bounds.min.y-0.1f, myCol.bounds.center.z), 0.18f);
    }

    private void FixedUpdate()
    {
        if (inWindZone)
            rb.AddForce(windZone.GetComponent<windArea>().direction * windZone.GetComponent<windArea>().strenght);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "windArea")
            windZone = col.gameObject;
        inWindZone = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "windArea")
            windZone = col.gameObject;
        inWindZone = false;
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Checkpoint 1")
        {
            Checkpoint = col.transform.GetChild(0).transform;
        }
        if (col.gameObject.tag == "Checkpoint 2")
        {
            Checkpoint = col.transform.GetChild(0).transform;
        }
        if (col.gameObject.tag == "Checkpoint 3")
        {
            Checkpoint = col.transform.GetChild(0).transform;
        }
        if (col.gameObject.tag == "Death")
        {
            transform.position = Checkpoint.position;
        }
        if (col.gameObject.tag == "Trampoline")
        {
            rb.velocity = Vector3.up * launchForce;
            canJump = false;
        }
        if (col.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        if (col.gameObject.tag == "Win")
        {
            if (!PhaseManager.Instance._hasGameEnded && View.IsMine)
            {                
                PhaseManager.Instance.PlayerWins(PhotonNetwork.LocalPlayer.UserId);
            }
        }
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (View.IsMine) return;
        cam.SetActive(true);       
    }
}
