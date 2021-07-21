using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Controller").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Starter"))
        {
            gameController.startPlay();
        }

        if (other.gameObject.CompareTag("Destroyer"))
        {
            gameController.removeBall();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameController.addPoints(other.gameObject.GetComponent<Obstacle>().points);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Road"))
        {
            FastRoad road = other.gameObject.GetComponent<FastRoad>();

            if (road.state != FastRoad.State.Disabled)
            {
                gameController.addPoints(road.points);   
            }
        }
    }
}
