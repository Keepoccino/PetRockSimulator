using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float dampTime = 0.15f;
    public float moveSpeed = 0.3f;
    private Vector3 target;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        InstantlyGoToTarget();
    }

    private Vector3 GetDestination()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void InstantlyGoToTarget()
    {
        transform.position = GetDestination();
    }

    void FixedUpdate()
    {
        var destination = GetDestination();
        transform.position = Vector2.Lerp(transform.position, destination, moveSpeed);
    }
}
