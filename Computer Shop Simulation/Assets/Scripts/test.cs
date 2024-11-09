using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform transformCube;
    public Transform transformSphere;
    public float x;
    public int speed;

    private void Update()
    {
        x += speed * Time.deltaTime;
        transformCube.position = new Vector3(x,2f,25f);
    }
}
