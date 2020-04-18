using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAI : MonoBehaviour
{
    public float secondsPerMinute = 60;


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
    private float statGrowthIncreaseRate = 0.15f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        statSleep += statSleepRecovery * (Time.deltaTime / secondsPerMinute);
        statHunger -= statHungerDecay * (Time.deltaTime / secondsPerMinute);
        statWater -= statWaterDecay * (Time.deltaTime / secondsPerMinute);
        statLove -= statLoveDecay * (Time.deltaTime / secondsPerMinute);
        statHygiene -= statHygieneDecay * (Time.deltaTime / secondsPerMinute);
        statFun -= statFunDecay * (Time.deltaTime / secondsPerMinute);
        statGrowth += statGrowthIncreaseRate * (Time.deltaTime / secondsPerMinute);


        statSleep = Mathf.Clamp(statSleep, 0, 1);
        statHunger = Mathf.Clamp(statHunger, 0, 1);
        statWater = Mathf.Clamp(statWater, 0, 1);
        statLove = Mathf.Clamp(statLove, 0, 1);
        statHygiene = Mathf.Clamp(statHygiene, 0, 1);
        statFun = Mathf.Clamp(statHygiene, 0, 1);
        statGrowth = Mathf.Clamp(statGrowth, 0, 1);


        statSleepController?.SetCurrentValue(statSleep);
        statHungerController?.SetCurrentValue(statHunger);
        statWaterController?.SetCurrentValue(statWater);
        statLoveController?.SetCurrentValue(statLove);
        statHygieneController?.SetCurrentValue(statHygiene);
        statFunController?.SetCurrentValue(statFun);
        statGrowthController?.SetCurrentValue(statGrowth);
    }
}
