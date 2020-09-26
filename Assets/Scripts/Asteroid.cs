using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float speed = 20f;
    [SerializeField] private GameObject explosionPrefab;
    private SpawnManager spawnManager;
    private AudioSource explosionSound;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        explosionSound = GameObject.Find("Explosion Sound").GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Laser(Clone)" || other.name == "Laser" || other.name == "Laser (1)" || other.name == "Laser (2)" )
        {
            explosionSound.Play();
            GameObject temp = Instantiate(explosionPrefab,transform.position,Quaternion.identity);
            Destroy(temp,3f);
            Destroy(other.gameObject);
            spawnManager.StartSpawning();
            Destroy(this.gameObject,0.2f);
        }
    } 
}
