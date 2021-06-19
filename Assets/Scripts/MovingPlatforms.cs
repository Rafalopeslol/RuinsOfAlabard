using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public string Type;
    public float speed, minX, maxX, minZ, maxZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        if (Type == "Horizontal")
        {
           if (transform.position.x <= minX)
           {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
           }       
           else if (transform.position.x >= maxX)
           {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
           }
        }
        else if (Type == "Vertical")
        {
            if (transform.position.x <= minX)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (transform.position.x >= maxX)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }
}
