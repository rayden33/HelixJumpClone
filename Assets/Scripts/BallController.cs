using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject rotObj;
    public GameObject ball;
    public GameObject mainCamera;
    public float rotationSpeed = 5.0f;
    public Vector3 razn;
    public float razY;
    private Vector3 vel;
    public float SmoothyDamp;

    Vector3 nextPos;


    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            Vector3 rot = Input.GetTouch(0).deltaPosition;
            rotObj.transform.Rotate(0, rot.x * rotationSpeed * Time.fixedDeltaTime, 0);
        }

        nextPos = ball.transform.position + razn;

        if (mainCamera.transform.position.y + razY > ball.transform.position.y) {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, nextPos, ref vel, SmoothyDamp);
        }
    }

    
}
