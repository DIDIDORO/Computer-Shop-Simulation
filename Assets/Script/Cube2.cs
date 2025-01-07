using UnityEngine;

public class Cube2 : MonoBehaviour

{
    public Transform cubeindex;
    public int cube = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cubeindex.position += new Vector3(cube, 0f, 0f);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}