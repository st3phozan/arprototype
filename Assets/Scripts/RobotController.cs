using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    private GameObject center;
    public float speed = .25f;
    
    // Start is called before the first frame update
    void Start()
    {
     center = GameObject.FindWithTag("center");   
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime; // calculate distance to move
	transform.LookAt(center.transform);
        transform.position = Vector3.MoveTowards(transform.position, center.transform.position, step);

    }
}
