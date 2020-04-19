using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WateringScript : MonoBehaviour
{
    public float moveTime = 0.3f;
    public float TurnDegrees = 45f;
    public float WateringRate = 0.001f;

    private ParticleSystem waterParticleSystem;
    private float currentMoveVelocity;
    private bool emmitWater;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        waterParticleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

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
        var em = waterParticleSystem.emission;
        em.enabled = emmitWater;
        transform.rotation = Quaternion.AngleAxis(Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, rotateToDegrees, ref currentMoveVelocity, moveTime), Vector3.forward);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other != RockAI.Instance.gameObject)
            return;

        RockAI.Instance.IsInteractedWith();

        int numCollisionEvents = waterParticleSystem.GetCollisionEvents(other, collisionEvents);

        int i = 0;
        while (i < numCollisionEvents)
        {
            if (collisionEvents[i].velocity.sqrMagnitude > 1)
            {
                RockAI.Instance.ModifyWater(WateringRate);
            }
            i++;
        }
    }


}
