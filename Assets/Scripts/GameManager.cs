using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver)
            {
                //SceneManager.LoadScene( SceneManager.GetActiveScene().name);
                SceneManager.LoadScene(1);// line above which is commented out does the same thing;for SceneManager.LoadScene(1) to work scene should be added from build settings and id (in this case 1) or name of scene should be used
            }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
