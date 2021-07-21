using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    public float defaultPos = 0f;
    public float hitPos = 45f;
    public float hitForce = 100f;
    public float damper = 150f;
    private HingeJoint hinge;
    public string inputName;
    
    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
    }

    // Update is called once per frame
    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = hitForce;
        spring.damper = damper;

        if (Input.GetAxis(inputName) == 1)
        {
            spring.targetPosition = hitPos;
        }
        else
        {
            spring.targetPosition = defaultPos;
        }

        hinge.spring = spring;
        hinge.useLimits = true;
    }
}
