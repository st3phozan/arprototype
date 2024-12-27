using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    public List<GameObject> bots = new List<GameObject>();
    public List<Transform> spots = new List<Transform>();

    public List<GameObject> instBots = new List<GameObject>();
    public Transform launchSpot;
    public int spawnIdx = 0; 
    public float TimeMultiplier, multiVariable = 100, errorSpeed = 1;

    public float StartTime;
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeMultiplier = 1 + ((Time.time - StartTime)/multiVariable);
    }
    public void GameStart(){
        StartCoroutine(RandomIntervalCoroutine());

    }
    private IEnumerator RandomIntervalCoroutine()
    {
        while (true) 
        {
          
            SpawnRobot();

          
            float waitTime = Random.Range(8, 10);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnRobot()
    {
	//Debug.Log("robot launched!");


        launchSpot = spots[Random.Range(0, spots.Count)];
        if (spawnIdx <= 2) {
            GameObject bot = Instantiate(bots[spawnIdx], launchSpot.position, Quaternion.identity);
            bot.GetComponent<RobotController>().speed = bot.GetComponent<RobotController>().speed * TimeMultiplier;
            Debug.Log(bot.GetComponent<RobotController>().speed);
            instBots.Add(bot);
        }
        else{
        GameObject bot = Instantiate(bots[Random.Range(0, bots.Count)], launchSpot.position, Quaternion.identity);
        bot.GetComponent<RobotController>().speed = bot.GetComponent<RobotController>().speed * TimeMultiplier;
        Debug.Log(bot.GetComponent<RobotController>().speed);
        instBots.Add(bot);
        }

        spawnIdx+=1;
    }
}
