using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairGenerator : MonoBehaviour
{
    public GameObject gStairPrefab;
    public GameObject gCoreCylinder;
    public Material mRedPlane;
    public Material mFinishPlane;
    public float stairStep = 4.0f;
    public int planeCount = 8;

    private Vector3 v3StairNextPos;
    private bool isFirstPlane = true;

    public void stairGeneration()
    {
        int delPlaneNum;
        int redPlaneNum;
        float coreY = gCoreCylinder.transform.position.y;
        v3StairNextPos = new Vector3(0, 0 - stairStep, 0);
        while (v3StairNextPos.y >= coreY - gCoreCylinder.transform.localScale.y)
        {
            GameObject tmpObj = Instantiate(gStairPrefab);
            tmpObj.transform.position = v3StairNextPos;
            tmpObj.transform.parent = gCoreCylinder.transform;
            v3StairNextPos = new Vector3(0, v3StairNextPos.y - stairStep, 0);
            if (!(v3StairNextPos.y >= coreY - gCoreCylinder.transform.localScale.y))
            {
                for (int i = 1; i <= planeCount; i++)
                {
                    tmpObj.transform.Find(i.ToString()).GetComponent<Renderer>().material = mFinishPlane;
                    tmpObj.transform.Find(i.ToString()).tag = "finishPlane";
                }
            }
            else
            {
                delPlaneNum = Random.Range(1, planeCount);
                Destroy(tmpObj.transform.Find(delPlaneNum.ToString()).gameObject);
                if (isFirstPlane)
                {
                    isFirstPlane = false;
                    continue;
                }
                redPlaneNum = ((delPlaneNum + Random.Range(1, planeCount / 2)) % 8) + 1;
                tmpObj.transform.Find(redPlaneNum.ToString()).tag = "redPlane";
                tmpObj.transform.Find(redPlaneNum.ToString()).GetComponent<Renderer>().material = mRedPlane;

                redPlaneNum = ((redPlaneNum + Random.Range(1, planeCount / 2 - 1)) % 8) + 1;
                tmpObj.transform.Find(redPlaneNum.ToString()).tag = "redPlane";
                tmpObj.transform.Find(redPlaneNum.ToString()).GetComponent<Renderer>().material = mRedPlane;
            }
        }

    }

    private void Start()
    {
        stairGeneration();
    }
}
