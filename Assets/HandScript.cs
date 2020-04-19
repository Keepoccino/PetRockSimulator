using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public float MaxSpeed = 3f;
    public float MinSpeed = 2f;
    public float PettingRadius = 0.5f;
    public float FunIncreaseRate = 0.01f;
    public float LoveIncrease = 0.03f;

    private FollowMouse followMouse;

    private void Awake()
    {
        followMouse = GetComponent<FollowMouse>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (followMouse.currentVelocity.sqrMagnitude > MinSpeed && followMouse.currentVelocity.sqrMagnitude < MaxSpeed)
        {
            if (Helpers.Caster(transform.position, "Rock", PettingRadius))
            {
                RockAI.Instance.IsInteractedWith();
                RockAI.Instance.ModifyFun(FunIncreaseRate * Time.deltaTime);
                RockAI.Instance.ModifyLove(LoveIncrease * Time.deltaTime);
            }
        }
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}
