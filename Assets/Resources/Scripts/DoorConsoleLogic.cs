using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DoorConsoleLogic : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private List<TextMeshProUGUI> numbersList;
    [SerializeField] private Door mainDoorFirst;
    [SerializeField] private Door mainDoorSecond;
    [SerializeField] private TextMeshProUGUI numberPasswordEnter;
    private bool isFirstDoorOpen;
    private bool isSecondDoorOpen;

    [SerializeField] private string passwordNow;
    [SerializeField] private string passwordEnter;
    private float timer;
    void Start()
    {
        progressBar.fillAmount = 1;
        ChangePassword();
    }

    // Update is called once per frame
    void Update()
    {
        if (String.Compare(passwordNow, passwordEnter, StringComparison.Ordinal) == 0)
        {
            passwordEnter = "";
            if (!isFirstDoorOpen)
            {
                mainDoorFirst.DoorOpen();
                isFirstDoorOpen = true;
                return;
            }
            else if(!isSecondDoorOpen)
            {
                mainDoorSecond.DoorOpen();
                isSecondDoorOpen = true;
                return;
            }
        }

        numberPasswordEnter.text = passwordEnter;

        progressBar.fillAmount -= Time.deltaTime / 32;

        if (progressBar.fillAmount <= 0)
        {
            ChangePassword();
            progressBar.fillAmount = 1;
        }
    }

    private void ChangePassword()
    {
        var strPassword = new StringBuilder();
        foreach (var number in numbersList)
        {
            var rnd = Random.Range(0, 9);
            if (number.text != rnd.ToString())
                number.text = rnd.ToString();
            else
                number.text = "9";

            strPassword.Append(number.text);
        }
        
        passwordNow = strPassword.ToString();
        passwordEnter = "";
    }

    public void EnterPassword(int number)
    {

        if (passwordEnter.Length == 6)
            passwordEnter = "";
        var strBuild = new StringBuilder(passwordEnter);
        strBuild.Append(number.ToString());

        passwordEnter = strBuild.ToString();
        Debug.Log($"{passwordEnter}");
    }
}
