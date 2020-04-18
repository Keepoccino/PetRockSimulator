using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityController : MonoBehaviour
{
    public List<GameObject> UtilityPrefabs;

    private GameObject currentUtility;

    public void ChangeUtility(int num)
    {

        Destroy(currentUtility);
        currentUtility = null;
        if (num > 0)
        {
            currentUtility = Instantiate(UtilityPrefabs[num]);
        }
    }
}
