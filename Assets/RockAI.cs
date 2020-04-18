using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAI : MonoBehaviour
{
    float secondsPerMinute = 60;


    public StatController statSleepController;
    public StatController statHungerController;
    public StatController statWaterController;
    public StatController statLoveController;
    public StatController statHygieneController;
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
    private float statGrowth = 0.5f;

    //Rates are per minute
    private float statSleepRecovery = 0.20f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        statSleep += statSleepRecovery * (Time.deltaTime / secondsPerMinute);


        statSleep = Mathf.Clamp(statSleep, 0, 1);


        statSleepController?.SetCurrentValue(statSleep);
        statHungerController?.SetCurrentValue(statHunger);
        statWaterController?.SetCurrentValue(statWater);
        statLoveController?.SetCurrentValue(statLove);
        statHygieneController?.SetCurrentValue(statHygiene);
        statGrowthController?.SetCurrentValue(statGrowth);
    }
}
