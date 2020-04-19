using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private const float emptyThreshold = 0.1f;
    private const float fullThreshold = 0.8f;
    public GameObject BowlEmpty;
    public GameObject BowlHalf;
    public GameObject BowlFull;

    public float foodLeft;
    public float foodIncrease;
    public float loveIncrease;
    public float foodBowlDecrease;

    // Singleton Pattern
    private static BowlController _instance;

    public static BowlController Instance { get { return _instance; } }

    void Awake()
    {
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
        foodLeft = Mathf.Min(1, foodLeft);

        BowlEmpty.SetActive(foodLeft < emptyThreshold);
        BowlHalf.SetActive(foodLeft >= emptyThreshold && foodLeft < fullThreshold);
        BowlFull.SetActive(foodLeft >= fullThreshold);

        if(foodLeft > foodBowlDecrease && RockAI.Instance.statHungerController.currentValue < 0.995f)
        {
            foodLeft -= foodBowlDecrease * Time.deltaTime;
            RockAI.Instance.ModifyHunger(foodIncrease * Time.deltaTime);
        }
    }

    public void IncreaseFood(float amount)
    {
        if (foodLeft < fullThreshold)
            RockAI.Instance.ModifyLove(loveIncrease);

        foodLeft += amount;
    }
}
