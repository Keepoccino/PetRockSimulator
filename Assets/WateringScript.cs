using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WateringScript : MonoBehaviour
{
    public float moveTime = 0.3f;
    public float TurnDegrees = 45f;
    public ParticleSystem WaterParticleSystem;

    private float currentMoveVelocity;
    private bool emmitWater;


    // Update is called once per frame
    void Update()
    {
        var rotateToDegrees = 0f;
        emmitWater = false;
        if (Input.GetMouseButton(0))
        {
            rotateToDegrees = TurnDegrees;
            if(Mathf.DeltaAngle(rotateToDegrees, transform.rotation.eulerAngles.z) < 5f){
                emmitWater = true;
            }
        }
        var em = WaterParticleSystem.emission;
        em.enabled = emmitWater;
        transform.rotation = Quaternion.AngleAxis(Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, rotateToDegrees, ref currentMoveVelocity, moveTime), Vector3.forward);
    }
}
