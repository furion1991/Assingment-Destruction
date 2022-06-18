using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("Set in inspector")]                            //a
    public GameObject cloudSphere;
    public int numSpheresMin = 6;
    public int numSpheresMax = 10;
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    public float scaleYMin = 2f; 

    private List<GameObject> spheres;                       //b

    private void Start()
    {
        spheres = new List<GameObject>();
        
        int num = Random.Range(numSpheresMin, numSpheresMax); //c

        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate(cloudSphere); //d

            spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(transform);

            // set random location
            Vector3 offset = Random.insideUnitSphere; //e
            offset.x = sphereOffsetScale.x;
            offset.y = sphereOffsetScale.y;
            offset.z = sphereOffsetScale.z;

            spTrans.localPosition = offset;         //f

            Vector3 scale = Vector3.one;            //g
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);  //h
            scale.y = Mathf.Max(scale.y, scaleYMin);

            spTrans.localScale = scale; //i

        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Restart();    //j
        //}
    }

    public void Restart()
    {
        foreach (var sp in spheres)
        {
            Destroy(sp);   //k
        }
        Start();
    }
}
