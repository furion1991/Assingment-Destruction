using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsCrafter : MonoBehaviour
{
    public GameObject star;
    public List<GameObject> starList;
    public int starCount = 10;

    private void Awake()
    {
        starList = new List<GameObject>();

    }

    private void Update()
    {
       
        if (SunMove.daytime == false)
        {
            for (int i = 0; i < starCount; i++)
            {
                Instantiate(star);
                starList.Add(star);
            }

            foreach (var star in starList)
            {
                float scaleVal = star.transform.localScale.x;
                Vector3 cPos = star.transform.position;

                cPos.x -= scaleVal * Time.deltaTime * CloudCrafter.S.cloudSpeedMult;

                if (cPos.x <= CloudCrafter.S.cloudPosMin.x)
                {
                    cPos.x = CloudCrafter.S.cloudPosMax.x;
                }
                star.transform.position = cPos;
            }
        }

        if (SunMove.daytime)
        {
            foreach (var item in starList)
            {
                Destroy(item);
            }
            return;
        }
    }
}
