using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticSphere : MonoBehaviour
{
    GameObject magneticSphere;
    PlayerControl player;
    private const float forceCoefficient = 21.5f;
    public bool isEnabled = true;
    public float radius;
    private const float speedOfDisappear = 0.075f;

    void Start()
    {
        player = PlayerControl.Instance;
        magneticSphere = gameObject;
    }

    void Update()
    {
        Vector2 distance = magneticSphere.transform.position - player.transform.position;
        Vector2 force = (forceCoefficient / distance.magnitude * distance.magnitude) * distance.normalized;
        if(distance.magnitude < radius && isEnabled)
        {
            player.Decelerate(0.075f);
            PlayerControl.Instance.rb.AddForce(force, ForceMode.Force);
        }
    }

    public IEnumerator Disappear()
    {
        isEnabled = false;
        while(transform.localScale.x >= speedOfDisappear)
        {
            Vector2 newScale = new Vector2(transform.localScale.x - speedOfDisappear, transform.localScale.x - speedOfDisappear);
            transform.localScale = newScale;

            yield return null;
        }
        transform.localScale = Vector3.zero;
    }
    public IEnumerator Recreate()
    {
        yield return new WaitForSeconds(10);
        isEnabled = true;
        while (transform.localScale.x < 0.4 - speedOfDisappear)
        {
            Vector2 newScale = new Vector2(transform.localScale.x + speedOfDisappear, transform.localScale.x + speedOfDisappear);
            transform.localScale = newScale;

            yield return null;
        }
    }
}