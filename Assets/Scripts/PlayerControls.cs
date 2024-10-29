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
    public RobotSpawner botSpawn;
    public Transform launcher;
    public Button checkButton; 
    public GameObject rightTxt, leftTxt;
    public TextMeshProUGUI cash;
    private int score = 0;     

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        if (checkButton != null)
        {
            checkButton.onClick.AddListener(CheckForBot);
        }
    }

    void Update()
    {
        launcher = botSpawn.launchSpot;
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
	cash.text = "Money Made: $" + score;
    }

    public void CheckForBot()
    {
        Vector2 screenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        if (arRaycastManager != null && arRaycastManager.Raycast(screenPosition, hits, TrackableType.FeaturePoint | TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Debug.Log("AR hit detected at position: " + hitPose.position);
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.CompareTag("bot"))
            {
                //Debug.Log(hit.transform.name + " : " + hit.transform.tag);
                score += 20;
                //Debug.Log("Score: " + score);
		Destroy(hit.transform.gameObject);
            }
        }
    }

    private float GetSignedAngleToTarget()
    {
        Vector3 directionToTarget = (launcher.position - transform.position).normalized;
        float angleDifference = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);
        return angleDifference;
    }
}
