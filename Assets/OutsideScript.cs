﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideScript : MonoBehaviour
{
    public float freedomIncreaseRate = 0.04f;
    public float funIncreaseRate = 0.04f;
    public float moveSpeed = 1f;
    public float moveFrequency = 1.3f;
    public Transform Point1;
    public Vector2 rockRespawnPosition;

    Rope rope;
    LineRenderer line;
    Rigidbody2D rock;

    bool currentlyMoving;
    float moveStartTime;
    Vector3 pointStartPos;


    private void Awake()
    {
        rope = GetComponent<Rope>();
        line = GetComponent<LineRenderer>();
    }



    // Start is called before the first frame update
    void Start()
    {
        rock = RockAI.Instance.gameObject.GetComponent<Rigidbody2D>();

        currentlyMoving = true;
        RockAI.Instance.ShowLeash = true;
        moveStartTime = Time.time; 
        rope.Point2 = RockAI.Instance.GetLeashPoint();

        pointStartPos = Point1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyMoving)
        {
            RockAI.Instance.ShowLeash = true;
            rock = RockAI.Instance.gameObject.GetComponent<Rigidbody2D>();
            float timeSinceMoveStart = Time.time - moveStartTime;
            var movement = Mathf.Max(0, Mathf.Sin(timeSinceMoveStart * moveFrequency) * moveSpeed * Time.deltaTime);
            rock.position += new Vector2(movement, 0);
            Point1.position += new Vector3(movement, 0, 0);

            RockAI.Instance.ModifyFreedom(freedomIncreaseRate * Time.deltaTime);
            RockAI.Instance.ModifyFun(funIncreaseRate * Time.deltaTime);
            RockAI.Instance.IsInteractedWith();

            if (timeSinceMoveStart > 15 && RockAI.Instance.statFreedomController.currentValue >= 1)
                Destroy(gameObject);
        }
    }

    void PutRockBack()
    {
        currentlyMoving = false;
        line.enabled = false;
        rope.enabled = false;
        RockAI.Instance.ShowLeash = false;

        rock.position = rockRespawnPosition;
        Point1.position = pointStartPos;
    }

    private void OnDestroy()
    {
        PutRockBack();
    }
}
