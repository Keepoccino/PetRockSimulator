using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject Menu;
    public GameObject OpenButton;

    private void Awake()
    {
        Menu.SetActive(false);
        OpenButton.SetActive(true);
    }

    public void OnMenuOpenClick()
    {
        Menu.SetActive(true);
        OpenButton.SetActive(false);
    }

    public void OnMenuCloseClick()
    {
        Menu.SetActive(false);
        OpenButton.SetActive(true);
    }
}
