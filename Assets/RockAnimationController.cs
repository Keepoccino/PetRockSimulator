using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAnimationController : MonoBehaviour
{
    public bool isPurring;

    private float purr_intensity = 0.02f;
    private float purr_frequency = 50;
    private float purrOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPurring)
            purrOffset = Mathf.Sin(Time.time * purr_frequency) * purr_intensity;
        else
            purrOffset = 0;

        transform.localPosition = new Vector3(purrOffset, 0);
    }

    void DoJump()
    {
    }
}
