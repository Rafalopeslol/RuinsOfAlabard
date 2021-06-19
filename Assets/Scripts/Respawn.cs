using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform playerCheckPoint;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            
            col.gameObject.transform.position = playerCheckPoint.position;
        }
    }
}
