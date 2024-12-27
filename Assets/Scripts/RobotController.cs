using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    private GameObject center;

    
    public float speed = .05f, failSpeed = 1;
    public Vector3 startPos;
    public bool helped = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        center = GameObject.FindWithTag("center");
        Debug.Log("robot spawned: " + Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        //speed = speed * failSpeed;
        float step = (speed * Time.deltaTime)/1.25f; 
        if (!helped)
        {
            transform.LookAt(center.transform);
            transform.position = Vector3.MoveTowards(transform.position, center.transform.position, step);
            if(Vector3.Distance(transform.position, center.transform.position) < .15f){
                //Debug.Log("endgame");
                GameObject bounds = GameObject.FindGameObjectWithTag("bounds");
                bounds.GetComponent<LevelUI>().EndGame();
            }
        }
        else if (helped)
        {
            transform.LookAt(startPos);
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
            if (Vector3.Distance(transform.position, startPos) <=  2)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
