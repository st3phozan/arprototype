using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    public List<GameObject> bots = new List<GameObject>();
    public List<Transform> spots = new List<Transform>();
    public Transform launchSpot;
    // Start is called before the first frame update
    void Start()
    {
	StartCoroutine(RandomIntervalCoroutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator RandomIntervalCoroutine()
    {
        while (true) // Infinite loop
        {
            // Call the action
            SpawnRobot();

            // Wait for a random amount of time between minInterval and maxInterval
            float waitTime = Random.Range(5, 10);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnRobot()
    {
	//Debug.Log("robot launched!");
        // Your action logic here
        launchSpot = spots[Random.Range(0, spots.Count)];
        Instantiate(bots[Random.Range(0, bots.Count)], launchSpot.position, Quaternion.identity);
    }
}
