using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatform : MonoBehaviour
{
    public float timer;
    public MeshCollider col;
    public Renderer render;
    public bool walkable;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        col = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        checkTimer();
        if (walkable && timer <3f)
        {
            render.material.SetColor("_Color", Color.blue);
            col.enabled = true;
        }
        else if (walkable && timer >= 3f)
        {
            render.material.SetColor("_Color", Color.yellow);
        }
        else
        {
            render.material.SetColor("_Color", Color.red);
            col.enabled = false;
        }
    }

    void checkTimer()
    {
       if (timer <= 5f)
       {
            timer += 1f * Time.deltaTime;
       }
       else if (timer >= 5f)
       {
            walkable = !walkable;
            timer = 0f;
       }
 
        
    }
}
