﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAI : MonoBehaviour
{
    public float secondsPerMinute = 60;

    public List<Sprite> RockSizeSprites;

    public List<float> RockSizeRateModifiers;

    public SpriteRenderer RockSpriteRenderer;

    public StatController statSleepController;
    public StatController statHungerController;
    public StatController statWaterController;
    public StatController statLoveController;
    public StatController statHygieneController;
    public StatController statFunController;
    public StatController statGrowthController;


    [SerializeField]
    private float statSleep = 0.8f;
    [SerializeField]
    private float statHunger = 0.5f;
    [SerializeField]
    private float statWater = 0.5f;
    [SerializeField]
    private float statLove = 0.5f;
    [SerializeField]
    private float statHygiene = 0.5f;
    [SerializeField]
    private float statFun = 0.5f;
    [SerializeField]
    private float statGrowth = 0.5f;

    //Rates are per minute
    private float statSleepRecovery = 0.20f;
    private float statHungerDecay = 0.12f;
    private float statWaterDecay = 0.2f;
    private float statLoveDecay = 0.3f;
    private float statHygieneDecay = 0.3f;
    private float statFunDecay = 0.3f;
    private float statGrowthIncreaseRate = 3.15f;


    [SerializeField]
    private int rockSize;
    [SerializeField]
    private float rockSizeRateModifier;


    // Singleton Pattern
    private static RockAI _instance;

    public static RockAI Instance { get { return _instance; } }

    void Awake()
    {
        ChangeRockSize(3);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        statSleep += statSleepRecovery * (Time.deltaTime / secondsPerMinute) * rockSizeRateModifier;
        statHunger -= statHungerDecay * (Time.deltaTime / secondsPerMinute);
        statWater -= statWaterDecay * (Time.deltaTime / secondsPerMinute);
        statLove -= statLoveDecay * (Time.deltaTime / secondsPerMinute);
        statHygiene -= statHygieneDecay * (Time.deltaTime / secondsPerMinute);
        statFun -= statFunDecay * (Time.deltaTime / secondsPerMinute);
        statGrowth += statGrowthIncreaseRate * (Time.deltaTime / secondsPerMinute) * rockSizeRateModifier;

        ClampAllValues();

        if(statGrowth == 1 && rockSize < RockSizeSprites.Count - 1)
        {
            ChangeRockSize(rockSize + 1);
            if(rockSize < RockSizeSprites.Count - 1)
            {
                statGrowth = 0;
            }
        }

        statSleepController?.SetCurrentValue(statSleep);
        statHungerController?.SetCurrentValue(statHunger);
        statWaterController?.SetCurrentValue(statWater);
        statLoveController?.SetCurrentValue(statLove);
        statHygieneController?.SetCurrentValue(statHygiene);
        statFunController?.SetCurrentValue(statFun);
        statGrowthController?.SetCurrentValue(statGrowth);
    }

    void ClampAllValues()
    {
        statSleep = Mathf.Clamp(statSleep, 0, 1);
        statHunger = Mathf.Clamp(statHunger, 0, 1);
        statWater = Mathf.Clamp(statWater, 0, 1);
        statLove = Mathf.Clamp(statLove, 0, 1);
        statHygiene = Mathf.Clamp(statHygiene, 0, 1);
        statFun = Mathf.Clamp(statFun, 0, 1);
        statGrowth = Mathf.Clamp(statGrowth, 0, 1);
    }

    public void ModifySleep(float value) => statSleep += value * rockSizeRateModifier;
    public void ModifyHunger(float value) => statHunger += value * rockSizeRateModifier;
    public void ModifyWater(float value) => statWater += value * rockSizeRateModifier;
    public void ModifyHygiene(float value) => statHygiene += value * rockSizeRateModifier;
    public void ModifyFun(float value) => statFun += value * rockSizeRateModifier;
    public void ModifyGrowth(float value) => statGrowth += value * rockSizeRateModifier;

    void ChangeRockSize(int size)
    {
        RockSpriteRenderer.sprite = RockSizeSprites[size];
        Destroy(RockSpriteRenderer.GetComponent<PolygonCollider2D>());
        RockSpriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
        rockSizeRateModifier = RockSizeRateModifiers[size];
        rockSize = size;
    }
}
