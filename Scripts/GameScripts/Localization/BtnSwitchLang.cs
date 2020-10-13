using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSwitchLang : MonoBehaviour
{
    [SerializeField]
    private LocalizationManager localizationManager;
    Text textButton;
    private void Start()
    {
        textButton = GetComponentInChildren<Text>();
        ChangeTextOnButton();
    }
    public void OnButtonClick()
    {
        if(localizationManager.CurrentLanguage == "en_US")
            localizationManager.CurrentLanguage = "ru_RU";
        else
            localizationManager.CurrentLanguage = "en_US";
        ChangeTextOnButton();
        localizationManager.LoadLocalizedText(localizationManager.CurrentLanguage);
    }
    void ChangeTextOnButton()
    {
        if (localizationManager.CurrentLanguage == "en_US")
            textButton.text = "EN";
        else
            textButton.text = "RU";
    }
}
