using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour
{
    public static bool daytime;
    public Camera cameraMain;
    public GameObject sun;
    public GameObject sunAnchor;
    public GameObject moon;
    public float sunVelocity = 0.00001f;
    public static Vector3 rightBorder;
    public static Vector3 leftBorder;
    public float width;
    public float height;
    public static float radius;
    public float timeCounter;

    private void Awake()
    {
        cameraMain = GetComponentInParent<Camera>();
        
        timeCounter = 0;

        rightBorder = new Vector3(80, 0, 12);
        leftBorder = new Vector3(0, 0, 12);

        sun = Instantiate(sun);
        sunAnchor = Instantiate(sun);
        moon = Instantiate(moon);

        Vector3 startPos = rightBorder;
        sun.transform.position = startPos;

        Vector3 center = new Vector3(0, 25, 0);
        sunAnchor.transform.position = center;
        
        radius = Vector3.Distance(rightBorder, leftBorder) / 2;
        
    }
    private void Update()
    {
        daytime = false;

        sunVelocity = 1f;
        timeCounter += Time.deltaTime * sunVelocity;

        Vector3 pos = sun.transform.position;
        pos.x = Mathf.Cos(timeCounter) * radius * 2 + 20;
        pos.y = Mathf.Sin(timeCounter) * radius;
        pos.z = 150;
        sun.transform.position = pos;

        Vector3 posMoon = sun.transform.position;
        posMoon.x = -Mathf.Cos(timeCounter) * radius * 2 + 35;
        posMoon.y = -pos.y;
        posMoon.z = 150;
        moon.transform.position = posMoon;

        Vector3 pos2 = sunAnchor.transform.position;
        pos2.x = Mathf.Cos(timeCounter) * radius * 2 + 20;
        pos2.y = Mathf.Sin(timeCounter) * radius;
        pos2.z = -150;
        sunAnchor.transform.position = pos2;

        Debug.Log(cameraMain.backgroundColor.g.ToString());

        SunsetSunriseColorer();
    }

    void SunsetSunriseColorer()
    {
        if (sun.transform.position.y <= 0)
        {
            daytime = false;
            cameraMain.backgroundColor = Color.black;
        }

        if (sun.transform.position.y < 40 && sun.transform.position.y > 0)
        {
            daytime = true;
            Color dayColor = new Color(167, 207, 201);
            Color tempColor = cameraMain.backgroundColor;
            

            if (tempColor.r > 167 || tempColor.g > 207 || tempColor.b > 201)
            {
                tempColor = dayColor;
            }

            else
            { 
                cameraMain.backgroundColor = dayColor;
                tempColor.r += 0;
                tempColor.g += 5;
                tempColor.b += 5;
                cameraMain.backgroundColor = tempColor;
            }
        }

        //if (sun.transform.position.y < 15 && sun.transform.position.y > 0)
        //{
        //    Color sunSetRiseColor = new Color(174, 31, 6);
        //    Color tempColor = cameraMain.backgroundColor;
        //    tempColor.r += 0.05f;
        //    tempColor.g += 0.05f;
        //    tempColor.b += 0.05f;
        //    cameraMain.backgroundColor = tempColor;
        //}


    }

    




}
