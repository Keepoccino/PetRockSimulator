using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockAI : MonoBehaviour
{
    [TextArea]
    public string FirstGrownText;

    [TextArea]
    public string FullyGrownText;

    public float secondsPerMinute = 60;

    public List<Sprite> RockSizeSprites;

    public List<GameObject> LeashPrefabs;

    public List<float> RockSizeRateModifiers;

    public SpriteRenderer RockSpriteRenderer;

    public StatController statSleepController;
    public StatController statHungerController;
    public StatController statWaterController;
    public StatController statLoveController;
    public StatController statFreedomController;
    public StatController statFunController;
    public StatController statGrowthController;

    public bool ShowLeash;


    [SerializeField]
    public float statSleep = 0.8f;
    [SerializeField]
    public float statHunger = 0.5f;
    [SerializeField]
    public float statWater = 0.5f;
    [SerializeField]
    public float statLove = 0.5f;
    [SerializeField]
    public float statFreedom = 0.5f;
    [SerializeField]
    public float statFun = 0.5f;
    [SerializeField]
    public float statGrowth = 0.5f;

    //Rates are per minute
    public float statSleepRecovery = 0.20f;
    public float statSleepDecay = 0.20f;
    public float statHungerDecay = 0.12f;
    public float statWaterDecay = 0.2f;
    public float statLoveDecay = 0.3f;
    public float statLoveIncrease = 0.6f;
    public float statFreedomDecay = 0.3f;
    public float statFunDecay = 0.3f;
    public float statGrowthIncreaseRate = 3.15f;

    public float timeTillSleep = 2;

    public Text MessageboxText;
    public GameObject MessageBox;

    private GameObject LeashObject;
    private SpriteRenderer LeashRenderer;

    private GrowthController growthController;

    [SerializeField]
    private int rockSize;
    [SerializeField]
    private float rockSizeRateModifier;

    private float lastInteraction;


    // Singleton Pattern
    private static RockAI _instance;

    public static RockAI Instance { get { return _instance; } }

    void Awake()
    {
        growthController = GetComponent<GrowthController>();
        ChangeRockSize(2);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LeashRenderer != null)
            LeashRenderer.enabled = ShowLeash;

        if (Time.time - lastInteraction > timeTillSleep)
            statSleep += statSleepRecovery * (Time.deltaTime / secondsPerMinute) * rockSizeRateModifier;
        else
            statSleep -= statSleepDecay * (Time.deltaTime / secondsPerMinute);
        statHunger -= statHungerDecay * (Time.deltaTime / secondsPerMinute);
        statWater -= statWaterDecay * (Time.deltaTime / secondsPerMinute);
        statFreedom -= statFreedomDecay * (Time.deltaTime / secondsPerMinute);
        statFun -= statFunDecay * (Time.deltaTime / secondsPerMinute);

        if (CanLoveIncrease())
            statLove += statLoveIncrease * (Time.deltaTime / secondsPerMinute) * rockSizeRateModifier;
        else
            statLove -= statLoveDecay * (Time.deltaTime / secondsPerMinute);

        if (CanGrow())
            statGrowth += statGrowthIncreaseRate * (Time.deltaTime / secondsPerMinute) * rockSizeRateModifier;

        ClampAllValues();

        if(statGrowth == 1 && rockSize < RockSizeSprites.Count - 1 && !ShowLeash)
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
        statFreedomController?.SetCurrentValue(statFreedom);
        statFunController?.SetCurrentValue(statFun);
        statGrowthController?.SetCurrentValue(statGrowth);
    }

    void ClampAllValues()
    {
        statSleep = Mathf.Clamp(statSleep, 0, 1);
        statHunger = Mathf.Clamp(statHunger, 0, 1);
        statWater = Mathf.Clamp(statWater, 0, 1);
        statLove = Mathf.Clamp(statLove, 0, 1);
        statFreedom = Mathf.Clamp(statFreedom, 0, 1);
        statFun = Mathf.Clamp(statFun, 0, 1);
        statGrowth = Mathf.Clamp(statGrowth, 0, 1);
    }

    bool CanGrow()
    {
        return growthController.IsGrowing;
    }
    bool CanLoveIncrease()
    {
        var growThreshold = 0.8;
        return statSleep > growThreshold &&
            statHunger > growThreshold &&
            statWater > growThreshold &&
            statFreedom > growThreshold &&
            statFun > growThreshold;
    }

    public void ModifySleep(float value) => statSleep += value * rockSizeRateModifier;
    public void ModifyHunger(float value) => statHunger += value * rockSizeRateModifier;
    public void ModifyWater(float value) => statWater += value * rockSizeRateModifier;
    public void ModifyFreedom(float value) => statFreedom += value * rockSizeRateModifier;
    public void ModifyLove(float value) => statLove += value * rockSizeRateModifier;
    public void ModifyFun(float value) => statFun += value * rockSizeRateModifier;
    public void ModifyGrowth(float value) => statGrowth += value * rockSizeRateModifier;

    public Transform GetLeashPoint()
    {
        return LeashObject.transform.Find("LeashPoint").transform;
    }

    void ChangeRockSize(int size)
    {
        if (size == 1)
            ShowMessage(FirstGrownText);

        if (size == RockSizeSprites.Count - 1)
            ShowMessage(FullyGrownText);

        RockSpriteRenderer.sprite = RockSizeSprites[size];
        Destroy(RockSpriteRenderer.GetComponent<PolygonCollider2D>());
        RockSpriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
        Destroy(LeashObject);
        LeashObject = Instantiate(LeashPrefabs[size], transform);
        LeashRenderer = LeashObject.GetComponent<SpriteRenderer>();
        LeashRenderer.enabled = ShowLeash;
        rockSizeRateModifier = RockSizeRateModifiers[size];
        rockSize = size;
    }

    public void IsInteractedWith()
    {
        lastInteraction = Time.time;
    }

    public void ShowMessage(string text)
    {
        MessageboxText.text = text;
        MessageBox.SetActive(true);
    }
}
