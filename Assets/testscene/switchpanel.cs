using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchpanel : MonoBehaviour
{
    [SerializeField] GameObject circle;
    [SerializeField] GameObject point_left;
    [SerializeField] GameObject point_right;




    public void StartGame()
    {
        Debug.Log("qqqq");
       // StartCoroutine(MoveCircle());

    }


    IEnumerator MoveCircle()
    {
        print("start");
        yield return new WaitForSeconds(2f);
        print("again start");
        while (circle.transform.position.x <= point_right.transform.position.x)
        {
            circle.transform.position = new Vector3(circle.transform.position.x + 0.1f, 0, 0);
            yield return new WaitForSeconds(0.1f);
        }

    }


}
