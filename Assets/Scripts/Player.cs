using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput; for crossPlatform Development

public class Player : MonoBehaviour
{
    //[SerializeField] lets manipulate private variables in inspecter
    private float speed = 5f;
    [SerializeField] private GameObject laserPrefab;
    private float fireRate = 0.15f;
    private float nextFire = 0;
    private int lives = 3;
    private SpawnManager spawnManager;
    private bool isTripleShotActive = false;
    [SerializeField] private GameObject TripleShotPrefab;
    private bool isSpeedBoostActive = false;
    private bool isShieldActive = false;
    [SerializeField] private GameObject shield;
    private int score;
    private UIManager UIManager;
    [SerializeField] private GameObject rightEngineFire;
    [SerializeField] private GameObject leftEngineFire;
    private bool oneEngineIsOnFire = false;
    private int randomEngine;
    private AudioSource laserSound;
    private AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        randomEngine = Random.Range(0,2);
        laserSound = GameObject.Find("Laser Sound").GetComponent<AudioSource>();
        explosionSound = GameObject.Find("Explosion Sound").GetComponent<AudioSource>();
    }

    // Update is called once per frame (60 frames per second)
    void Update()
    {  
        Movment();
        LaserShoot();
    }
    
    void LaserShoot()
    {
        //Time.time counts how many seconds game was executing
        if(Input.GetKeyDown(KeyCode.Space) & Time.time > nextFire) //CrossPlatformInputManager.GetButtonDown("Fire")//for android
        {
            nextFire = Time.time + fireRate;
            if(!isTripleShotActive)
            {
                Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z), Quaternion.identity);
                //Quaternion.identity -> default rotaition
            }
            else
            {
                Instantiate(TripleShotPrefab, transform.position, Quaternion.identity);
            }
            laserSound.Play();
        }
    }
    
    void Movment()
    {
       float horizontalInput = Input.GetAxis("Horizontal"); //float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");//for android
       float verticalInput = Input.GetAxis("Vertical");  //float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");//for android

        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);

        //Time.deltaTime = translates mtr per frame to mtr per second
        if(isSpeedBoostActive)
        {
            transform.Translate(direction * speed * 2 * Time.deltaTime); 
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if(transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x,0,transform.position.z);
        }
        else if(transform.position.y <= -4.8f)
        {
            transform.position = new Vector3(transform.position.x,-4.8f,transform.position.z);
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

    public void Damage()
    {
        if(isShieldActive)
        {
            isShieldActive = false;
            //Deactivates gameobject
            shield.SetActive(false);
            return;
        }
        lives--;
        UIManager.UpdateLives(lives);

        if(lives < 3 && lives != 0)
        {
            if(randomEngine == 0 && !oneEngineIsOnFire)
            {
                leftEngineFire.SetActive(true);
                oneEngineIsOnFire = true;
            }
            else if(randomEngine == 1 && !oneEngineIsOnFire)
            {
                rightEngineFire.SetActive(true);
                oneEngineIsOnFire = true;
            }
            else if(oneEngineIsOnFire && randomEngine == 0)
            {
                rightEngineFire.SetActive(true);
            }
            else if(oneEngineIsOnFire && randomEngine == 1)
            {
                leftEngineFire.SetActive(true);
            }
        }

        if(lives == 0)
        {
            spawnManager.PlayerIsDead();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //Let Triple Shot to be active for 5 sconds
    IEnumerator TripleShotPowerDownRoutine()
    {
        while(isTripleShotActive)
        {
            yield return new WaitForSeconds(5.0f);
            isTripleShotActive = false;   
        }   
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    //Let Speed Boost to be active for 5 sconds
    IEnumerator SpeedBoostDownRoutine()
    {
        while(isSpeedBoostActive)
        {
            yield return new WaitForSeconds(5.0f);
            isSpeedBoostActive = false;   
        }
    }

    public void ShieldActive()
    {
        isShieldActive = true;
        //Activates gameobject
        shield.SetActive(true);
    }

    public void AddScore()
    {
        score = score + 10;
        UIManager.UpdateScore(score);
    }
}
