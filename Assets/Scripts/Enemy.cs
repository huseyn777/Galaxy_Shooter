using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4f;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y <= -7f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f),7,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.name == "Player")
        {   
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }

        //Debug.Log("HIT " + other.transform.name);

        if(other.name == "Laser(Clone)" || other.name == "Laser" || other.name == "Laser (1)" || other.name == "Laser (2)" )
        {
            Destroy(other.gameObject);
            player.AddScore();
            Destroy(this.gameObject);
        }
    }
}
