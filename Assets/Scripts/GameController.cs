using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int maxBalls = 5;
    private int currentBalls;
    private float currentPoints = 0;
    
    public GameObject ballPrefab;
    public GameObject shooterSpawner;

    public Text pointsText;
    public GameObject ballImage;
    public HorizontalLayoutGroup ballDisplay;
    
    public GameObject roadShooterBlocker;
    private FastRoad roadShooterBlockerScript;

    public GameObject gameOverPanel;
    public Text gameOverPoints;

    private enum State
    {
        Shoot,
        Play,
        Over
    };

    private State state = State.Shoot;
    
    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Play)
        {
            roadShooterBlockerScript.state = FastRoad.State.Up;

            pointsText.text = currentPoints.ToString();
            gameOverPoints.text = currentPoints.ToString();
        }
        else
        {
            roadShooterBlockerScript.state = FastRoad.State.Disabled;
        }
    }

    public void removeBall()
    {
        currentBalls -= 1;
        
        if (currentBalls <= 0)
        {
            state = State.Over;
            gameOverPanel.SetActive(true);
        }
        else
        {
            state = State.Shoot;
            Instantiate(ballPrefab, shooterSpawner.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
            
        }
        
        Destroy(ballDisplay.transform.GetChild(0).gameObject);
    }

    public void addBall()
    {
        currentBalls += 1;
    }

    public void addPoints(float points)
    {
        currentPoints += points;
    }

    public void startPlay()
    {
        state = State.Play;
    }

    public void startGame()
    {
        currentBalls = maxBalls;
        currentPoints = 0;
        
        state = State.Shoot;

        roadShooterBlockerScript = roadShooterBlocker.gameObject.GetComponent<FastRoad>();
        
        Instantiate(ballPrefab, shooterSpawner.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);

        for (int k = 0; k < maxBalls; k++)
        {
            Instantiate(ballImage, ballDisplay.transform);   
        }
        
        gameOverPanel.SetActive(false);
        pointsText.text = currentPoints.ToString();
        gameOverPoints.text = currentPoints.ToString();
    }
}
