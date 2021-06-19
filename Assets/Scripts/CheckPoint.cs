using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform playerCheckpoint,SpawnArea;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponentInChildren<Transform>();
            //playerCheckpoint = col.gameObject.transform.GetChild(1).transform;
            if(playerCheckpoint.position != SpawnArea.position)
            {
                playerCheckpoint.position = SpawnArea.position;
            }
            else
            {

            }       
        }
    }
}
