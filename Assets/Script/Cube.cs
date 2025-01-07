using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Transform cubeindex;
    public int cube = 1;
    public float interval = 0.1f;

    void Start()
    {
        StartCoroutine(IncreaseCubeCoroutine());
    }

    private void Update()
    {
        cubeindex.position = new Vector3(cube, 0f, 0f);
        cubeindex.rotation = Quaternion.Euler(cube, cube, cube);
    }

    IEnumerator IncreaseCubeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            cube += 1;
        }
    }
}
