using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed = 8f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(transform.position.y >= 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
