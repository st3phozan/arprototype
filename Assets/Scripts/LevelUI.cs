using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelUI : MonoBehaviour
{
    public PlayerControls pc;

    public GameObject StartScreen, InGameScreen, EndScreen, InGameUI;
    public RobotSpawner rs;
    public TextMeshProUGUI finalScore;
    // Start is called before the first frame update
    void Start()
    {
        StartScreen.SetActive(true);
        //Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame(){
        InGameScreen.SetActive(true);
        InGameUI.SetActive(true);
        StartScreen.SetActive(false);
        //Time.timeScale = 1;
        rs.GameStart();
    }
    public void EndGame(){
        Debug.Log("found");
        InGameScreen.SetActive(false);
        InGameUI.SetActive(false);
        EndScreen.SetActive(true);
        finalScore.text = "You made $" + pc.score + " dollars!";
    }
    public void ReloadGame(){
        SceneManager.LoadScene("arProjectFInal");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        if (other.gameObject.tag == "bot"){
            EndGame();
        }
    }
}
