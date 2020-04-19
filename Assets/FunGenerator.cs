﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunGenerator : MonoBehaviour
{
    public float FunRadius = 2f;
    public float FunIncreaseRate = 0.0002f;
    public float MinSpeed = 2f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.sqrMagnitude > MinSpeed)
            caster(transform.position, FunRadius);
    }

    void caster(Vector2 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Rock")
            {
                RockAI.Instance.ModifyFun(FunIncreaseRate);
            }
            i++;
        }
    }
}