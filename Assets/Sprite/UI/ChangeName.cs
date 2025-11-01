using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeName : MonoBehaviour
{
    public GameObject namePanel;
    public TMP_InputField nameInputField;
    public TextMeshProUGUI nameText;
    void Start()
    {
        UpdateNameUI();
    }
    public void ChangeNameButton()
    {
        namePanel.SetActive(true);
    }
    public void CloseNamePanel()
    {
        PlayerPrefs.SetString("Name", nameInputField.text);
        namePanel.SetActive(false);
        UpdateNameUI();
    }
    void UpdateNameUI() 
    { 
    string? name=PlayerPrefs.GetString("Name", "-");
    nameText.text =  name;
    }
}
