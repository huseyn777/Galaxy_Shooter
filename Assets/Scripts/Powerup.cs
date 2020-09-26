using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float speed = 3f;
    // id = 0 -> triple shot ; id = 1 -> speed ; id = 0 -> shield 
    [SerializeField] private int powerupID;
    private AudioSource powerupSound;

    void Start()
    {
        powerupSound = GameObject.Find("Powerup Sound").GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y <= -8f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            powerupSound.Play();
            //triple shot
            if(powerupID == 0)
            {
                other.GetComponent<Player>().TripleShotActive();
            }

            //speed
            else if(powerupID == 1)
            {
                other.GetComponent<Player>().SpeedBoostActive();
            }

            else if(powerupID == 2)
            {
                other.GetComponent<Player>().ShieldActive();
            }

            Destroy(this.gameObject);
        }
    } 
}
