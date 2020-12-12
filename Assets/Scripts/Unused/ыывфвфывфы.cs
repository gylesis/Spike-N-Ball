using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ыывфвфывфы : MonoBehaviour
{
     public GameObject a;
    public GameObject ab;

    void Update()
    {
        ab.transform.position = a.transform.position - (a.transform.position - ab.transform.position);
        //https://www.youtube.com/watch?v=G1IbRujko-A&t=
    }
}
