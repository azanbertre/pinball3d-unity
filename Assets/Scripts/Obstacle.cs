using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float power = 10f;
    private List<Rigidbody> ballList;
    private TextMesh textPoints;

    public float points = 25f;

    // Start is called before the first frame update
    void Start()
    {
        ballList = new List<Rigidbody>();
        
        textPoints = gameObject.GetComponentInChildren<TextMesh>();

        if (textPoints)
        {
            textPoints.text = points.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ballList.Count > 0)
        {
            foreach (Rigidbody r in ballList)
            {
                Vector3 dir = (r.transform.position - transform.position).normalized;
                r.AddForce(power * dir);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody ball = other.gameObject.GetComponent<Rigidbody>();
            ballList.Add(ball);
            
            // add points
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballList.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
