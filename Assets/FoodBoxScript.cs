using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBoxScript : MonoBehaviour
{
    public float moveTime = 0.3f;
    public float TurnDegrees = 45f;
    public float IncreaseRate = 0.001f;

    private bool emmitFood;
    private ParticleSystem foodParticleSystem;
    private float currentMoveVelocity;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        foodParticleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        var rotateToDegrees = 0f;
        emmitFood = false;
        if (Input.GetMouseButton(0))
        {
            rotateToDegrees = TurnDegrees;
            if (Mathf.Abs(Mathf.DeltaAngle(rotateToDegrees, transform.rotation.eulerAngles.z)) < 5f)
            {
                emmitFood = true;
            }
        }
        var em = foodParticleSystem.emission;
        em.enabled = emmitFood;
        transform.rotation = Quaternion.AngleAxis(Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, rotateToDegrees, ref currentMoveVelocity, moveTime), Vector3.forward);
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Bowl"))
            return;

        RockAI.Instance.IsInteractedWith();

        int numCollisionEvents = foodParticleSystem.GetCollisionEvents(other, collisionEvents);

        int i = 0;
        while (i < numCollisionEvents)
        {
            if (collisionEvents[i].velocity.sqrMagnitude > 1)
            {
                BowlController.Instance.IncreaseFood(IncreaseRate);
            }
            i++;
        }
    }
}
