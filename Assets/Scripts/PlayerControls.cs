using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
     private Vector2 firstPressPos;
    private Vector2 secondPressPos;

    
    public AudioSource interactionAudio;
    public AudioClip correct, incorrect;
    public Animator screenFlash;
  
    public RobotSpawner botSpawn;
    public Transform launcher;
    public Button checkButton; 
    public GameObject rightTxt, leftTxt;
    public TextMeshProUGUI cash;
    public int score = 0;     
    

    void Start()
    {
       arRaycastManager = FindObjectOfType<ARRaycastManager>();
        /*if (checkButton != null)
        {
            checkButton.onClick.AddListener(CheckForBot);
        }*/
    }

    void Update()
    {
       
        if(botSpawn.instBots.Count > 0){
        launcher = botSpawn.instBots[0].transform;
        float angleDifference = GetSignedAngleToTarget();
        if (angleDifference > 10)
        {
	    rightTxt.SetActive(true);
	    leftTxt.SetActive(false);


        }
        else if (angleDifference < -10)
        {
	    rightTxt.SetActive(false);
	    leftTxt.SetActive(true);

        }
        else
        {
	    rightTxt.SetActive(false);
	    leftTxt.SetActive(false);

        }
        }
	cash.text = "Money Made: $" + score;
    }


    public void CheckForGreenBot()
    {
        Vector2 screenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        if (arRaycastManager != null && arRaycastManager.Raycast(screenPosition, hits, TrackableType.FeaturePoint | TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            //Debug.Log("AR hit detected at position: " + hitPose.position);
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500))
        {
            if (hit.transform.CompareTag("greenBot"))
            {
                hit.transform.gameObject.GetComponent<RobotController>().helped = true;
                screenFlash.SetTrigger("correct");
                botSpawn.instBots.RemoveAt(0);
                //Debug.Log(hit.transform.name + " : " + hit.transform.tag);
            CorrectHit();
                score += 20;
                //Destroy(hit.transform.gameObject);
                
                //Debug.Log("Score: " + score);
		
            }
            else if (hit.transform.CompareTag("redBot") || hit.transform.CompareTag("blueBot"))
            {
                hit.transform.gameObject.GetComponent<RobotController>().speed += 1f;
                Debug.Log(hit.transform.gameObject.GetComponent<RobotController>().failSpeed);
                screenFlash.SetTrigger("incorrect");
                //hit.transform.gameObject.GetComponent<RobotController>().helped = true;
                //Debug.Log(hit.transform.name + " : " + hit.transform.tag);
                IncorrectHit();
                score -= 20;
                //Destroy(hit.transform.gameObject);
                
                //Debug.Log("Score: " + score);
		
            }
        }
    }
     public void CheckForRedBot()
    {
        Vector2 screenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        if (arRaycastManager != null && arRaycastManager.Raycast(screenPosition, hits, TrackableType.FeaturePoint | TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            ////Debug.Log("AR hit detected at position: " + hitPose.position);
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500))
        {
            if (hit.transform.CompareTag("redBot"))
            {
                hit.transform.gameObject.GetComponent<RobotController>().helped = true;
                screenFlash.SetTrigger("correct");
                botSpawn.instBots.RemoveAt(0);
                ////Debug.Log(hit.transform.name + " : " + hit.transform.tag);
               
                score += 20;
                CorrectHit();
                //Destroy(hit.transform.gameObject);
                
                ////Debug.Log("Score: " + score);
		
            }
            else if (hit.transform.CompareTag("greenBot") || hit.transform.CompareTag("blueBot"))
            {
                hit.transform.gameObject.GetComponent<RobotController>().speed += 1f;
                Debug.Log(hit.transform.gameObject.GetComponent<RobotController>().failSpeed);
                screenFlash.SetTrigger("incorrect");
                //hit.transform.gameObject.GetComponent<RobotController>().helped = true;
                ////Debug.Log(hit.transform.name + " : " + hit.transform.tag);
               
                score -= 20;
                IncorrectHit();
                //Destroy(hit.transform.gameObject);
                
                ////Debug.Log("Score: " + score);
		
            }
        }
    }
     public void CheckForBlueBot()
    {
        Vector2 screenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        if (arRaycastManager != null && arRaycastManager.Raycast(screenPosition, hits, TrackableType.FeaturePoint | TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            //Debug.Log("AR hit detected at position: " + hitPose.position);
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500))
        {
            if (hit.transform.CompareTag("blueBot"))
            {
                hit.transform.gameObject.GetComponent<RobotController>().helped = true;
                screenFlash.SetTrigger("correct");
                botSpawn.instBots.RemoveAt(0);
                ////Debug.Log(hit.transform.name + " : " + hit.transform.tag);
               
                score += 20;
                CorrectHit();
                //Destroy(hit.transform.gameObject);
                
                ////Debug.Log("Score: " + score);
		
            }
            else if (hit.transform.CompareTag("redBot") || hit.transform.CompareTag("greenBot"))
            {
                hit.transform.gameObject.GetComponent<RobotController>().speed += 1f;
                Debug.Log(hit.transform.gameObject.GetComponent<RobotController>().failSpeed);
                screenFlash.SetTrigger("incorrect");
                ////Debug.Log(hit.transform.name + " : " + hit.transform.tag);
               
                score -= 20;
                IncorrectHit();
                //Destroy(hit.transform.gameObject);
                
                ////Debug.Log("Score: " + score);
		
            }
        }
    }
public void CorrectHit(){
    interactionAudio.clip = correct;
    interactionAudio.Play();
}
public void IncorrectHit(){
    interactionAudio.clip = incorrect;
    interactionAudio.Play();
}

    private float GetSignedAngleToTarget()
    {
        Vector3 directionToTarget = (launcher.position - transform.position).normalized;
        float angleDifference = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);
        return angleDifference;
    }
}
