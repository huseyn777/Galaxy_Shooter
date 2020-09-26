using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4f;
    private Player player;
    private Animator animator;
    private AudioSource explosionSound;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        explosionSound = GameObject.Find("Explosion Sound").GetComponent<AudioSource>();
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
        if( GetComponent<BoxCollider2D>().isTrigger == false)
        {
            return;
        }
        
        if(other.name == "Player")
        {   
            explosionSound.Play();
            animator.SetTrigger("OnEnemyDeath");
            other.GetComponent<Player>().Damage();
            speed = 0;
            //Turn off trigger functionality of enemy
            GetComponent<BoxCollider2D>().isTrigger = false;
            //Destroys object in 2.3 seconds
            Destroy(this.gameObject,2.3f);
            
        }

        //Debug.Log("HIT " + other.transform.name);

        if(other.name == "Laser(Clone)" || other.name == "Laser" || other.name == "Laser (1)" || other.name == "Laser (2)" )
        {
            Destroy(other.gameObject);
            explosionSound.Play();
            animator.SetTrigger("OnEnemyDeath");
            player.AddScore();
            speed = 0;
            //Turn off trigger functionality of enemy
            GetComponent<BoxCollider2D>().isTrigger = false;
            //Destroys object in 2.3 seconds
            Destroy(this.gameObject,2.3f);
        }
    }
}
