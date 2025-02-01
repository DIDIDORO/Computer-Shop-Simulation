using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleMenager : MonoBehaviour


{
    [Range(0f, 1f)]
    public float TimeOfDay;
    public float DayDuration = 60f;

    public Light sun;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;
        if(TimeOfDay >= 1) TimeOfDay -= 1;
        sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, 180, 0);
    }
}
