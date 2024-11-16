using UnityEngine;

public class Cube3 : MonoBehaviour

{
    public Transform cubeindex;
    public float w;
    public float a;
    public float s;
    public float d;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cubeindex.position += new Vector3(0.01f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            cubeindex.position += new Vector3(0f, 0f, 0.01f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cubeindex.position += new Vector3(-0.01f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cubeindex.position += new Vector3(0f, 0f, -0.01f);
        }
    }
}
