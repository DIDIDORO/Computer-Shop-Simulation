using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level123 : MonoBehaviour
{ 
    public Transform parent;
    public Sprite[] level1;
    public Sprite[] level2;
    public Sprite[] level3;
    void Start()
    {

        int n = 0;
        for(int i = 0; i<level1.Length;i++, n++)
        {
            parent.GetChild(n).GetChild(0).GetComponent<Image>().sprite = level1[i];
        }

        for (int i = 0; i < level2.Length; i++, n++)
        {
            parent.GetChild(n).GetChild(0).GetComponent<Image>().sprite = level2[i];
        }

        for (int i = 0; i < level3.Length; i++, n++)
        {
            parent.GetChild(n).GetChild(0).GetComponent<Image>().sprite = level3[i];
        }
    }
}