using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class jumper : MonoBehaviour
{
    public float jumpSpeed = 1.0f;
    public float collisionLockTime = 0.1f;
    public Text txtScore;
    public GameObject menu;
    public GameObject gMainSystem;

    Rigidbody rb;
    private bool collisionLock = false;
    private float tmpTime; 

    public void MainMenu()
    {
        Time.timeScale = 0f;
        txtScore.text = GlobalData.score.ToString();
        menu.transform.Find("start").gameObject.SetActive(true);
        menu.transform.Find("new game").gameObject.SetActive(false);
    }
    public void GameOver()
    {
        GlobalData.score = 0;
        Time.timeScale = 0f;
        gMainSystem.GetComponent<BallController>().enabled = false;
        menu.transform.Find("new game").gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gMainSystem.GetComponent<BallController>().enabled = true;
        menu.transform.Find("new game").gameObject.SetActive(false);
        menu.transform.Find("start").gameObject.SetActive(false);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    private void overStair(Transform tStair)
    {
        GlobalData.score += 1;
        txtScore.text = GlobalData.score.ToString();
        Destroy(tStair.gameObject);
    }

    private void Awake()
    {
        tmpTime = collisionLockTime;
        rb = GetComponent<Rigidbody>();
    }

    private void Start() 
    {
        MainMenu();
    }

    private void FixedUpdate()
    {                                   //// timer against collision conf ////
        if (collisionLock) {
            tmpTime -= Time.deltaTime;
            if (tmpTime < 0) {
                collisionLock = false;
                tmpTime = collisionLockTime;
            }
        }
                                       ///////////////////////////////////////////
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "stair") {
            overStair(col.transform);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collisionLock)
            return;

        if (collision.gameObject.tag == "safePlane") {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            collisionLock = true;
        }

        if (collision.gameObject.tag == "redPlane") {
            GameOver();
        }

        if (collision.gameObject.tag == "finishPlane") {
            NewGame();
        }
    }
}
