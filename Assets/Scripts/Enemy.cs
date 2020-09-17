﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y <= -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f),7,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.name == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damage();
            Destroy(this.gameObject);
        }

        //Debug.Log("HIT " + other.transform.name);

        if(other.name == "Laser(Clone)")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
