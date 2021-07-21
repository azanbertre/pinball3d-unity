using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FastRoad : MonoBehaviour
{
    public float power = 2f;
    private List<Rigidbody> ballList;
    public Transform directionObj;
    public Material disabledMaterial;
    public Material upMaterial;
    public Material downMaterial;
    
    private TextMesh textPoints;
    
    public float points = 25f;

    public enum State
    {
        Disabled,
        Up,
        Down
    };

    public State state = State.Up;
    private State startingState;

    public enum RoadType
    {
        Normal,
        ChangeDisable,
        ChangeDirection
    };
    public RoadType roadType = RoadType.Normal;
    
    private MeshRenderer renderer;
    
    private float nextTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        ballList = new List<Rigidbody>();
        renderer = gameObject.GetComponent<MeshRenderer>();
        
        startingState = state;
        
        textPoints = gameObject.GetComponentInChildren<TextMesh>();

        if (textPoints)
        {
            textPoints.text = points.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateState();
        
        if (state == State.Disabled)
        {
            renderer.material = disabledMaterial;
        }
        else
        {
            renderer.material = state == State.Up ? upMaterial : downMaterial;
            
            if (ballList.Count > 0)
            {
                foreach (Rigidbody r in ballList)
                {
                    Vector3 dir = (directionObj.position - r.transform.position).normalized;

                    if (state == State.Down)
                    {
                        dir = -dir;
                    }
                    
                    r.AddForce(power * dir);
                }
            }   
        }
    }

    private void updateState()
    {
        if (roadType == RoadType.Normal)
        {
            return;
        }
        
        if (Time.time > nextTime)
        {
            if (roadType == RoadType.ChangeDirection && state != State.Disabled)
            {
                state = state == State.Up ? State.Down : State.Up;
            }
            else if (roadType == RoadType.ChangeDisable)
            {
                state = state == State.Disabled ? startingState : State.Disabled;
            }

            nextTime += 5f;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballList.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballList.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
