using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] lets manipulate private variables in inspecter
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate = 0.15f;
    private float nextFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame (60 frames per second)
    void Update()
    {  
        Movment();
        LaserShoot();
    }
    void LaserShoot()
    {
        if(Input.GetKeyDown(KeyCode.Space) & Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), Quaternion.identity);
            //Quaternion.identity -> default rotaition
        }
    }
    void Movment()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");

       Vector3 direction = new Vector3(horizontalInput,verticalInput,0);

       //Time.deltaTime = translates mtr per frame to mtr per second
       transform.Translate(direction * speed * Time.deltaTime);

       if(transform.position.y >= 0)
       {
           transform.position = new Vector3(transform.position.x,0,transform.position.z);
       }
       else if(transform.position.y <= -3.8f)
       {
           transform.position = new Vector3(transform.position.x,-3.8f,transform.position.z);
       }
       //Alternative for boundaries above 
       //transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),transform.position.z);

       if(transform.position.x > 11.3f)
       {
           transform.position = new Vector3(-11.3f,transform.position.y,transform.position.z);
       }

       else if(transform.position.x < -11.3f)
       {
            transform.position = new Vector3(11.3f,transform.position.y,transform.position.z);
       }
    }
}
