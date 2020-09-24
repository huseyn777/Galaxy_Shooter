using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Sprite [] liveSprites;
    [SerializeField] private Image livesImg;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;
    private GameManager gameManager;

    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        livesImg.sprite = liveSprites[currentLives];

        if(currentLives == 0)
        {
            gameOverText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
            gameManager.GameOver();
            StartCoroutine(GameOverFlickerRoutine());
        }
    }

    //Create a flickering effect for game over text with Ienumerator function
    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }

    }
}
