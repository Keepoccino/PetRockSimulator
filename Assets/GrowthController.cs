using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GrowthController : MonoBehaviour
{
    public float GrowthThreshold = 0.5f;
    public Text NoGrowthReasons;
    public GameObject NoGrowthObject;
    public GameObject GrowthObject;
    public bool IsGrowing;

    public Dictionary<string, string> reasons = new Dictionary<string, string>
    {
        { "Sleep", "Needs more sleep"},
        { "Hunger", "Hungry"},
        { "Water", "Needs water"},
        { "Love", "Wants some love"},
        { "Freedom", "Wants to go outside"},
        { "Fun", "Wants to play"},
    };

    // Update is called once per frame
    void Update()
    {
        bool canGrow = true;
        StringBuilder sb = new StringBuilder(150);
        if (RockAI.Instance.statSleep < GrowthThreshold)
        {
            sb.Append(" - ").AppendLine(reasons["Sleep"]);
            canGrow = false;
        }
        if (RockAI.Instance.statHunger < GrowthThreshold)
        {
            sb.Append(" - ").AppendLine(reasons["Hunger"]);
            canGrow = false;
        }
        if (RockAI.Instance.statWater < GrowthThreshold)
        {
            sb.Append(" - ").AppendLine(reasons["Water"]);
            canGrow = false;
        }
        if (RockAI.Instance.statLove < GrowthThreshold)
        {
            sb.Append(" - ").AppendLine(reasons["Love"]);
            canGrow = false;
        }
        if (RockAI.Instance.statFreedom < GrowthThreshold)
        {
            sb.Append(" - ").AppendLine(reasons["Freedom"]);
            canGrow = false;
        }
        if (RockAI.Instance.statFun < GrowthThreshold)
        {
            sb.Append(" - ").AppendLine(reasons["Fun"]);
            canGrow = false;
        }
        IsGrowing = canGrow;
        NoGrowthReasons.text = sb.ToString();
        NoGrowthObject.SetActive(!IsGrowing);
        GrowthObject.SetActive(IsGrowing);
    }
}
