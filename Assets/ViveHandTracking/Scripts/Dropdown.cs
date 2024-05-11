using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    public GameObject[] options;
    public UnityEvent<int> onValueChanged;
    // public GameObject selectedOption;

    private GameObject dropdownList;
    private TextMeshProUGUI dropdownLabel;
    private int selectedOption;

    private void Awake()
    {
        dropdownList = transform.Find("Dropdown List").gameObject;
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<DropdownElement>().optionNumber = i;
        }
        SelectOption(0);
    }

    public void GenerateDropdownList()
    {
        dropdownList.SetActive(!dropdownList.activeSelf);
        for (int i = 0; i < options.Length; i++)
        {
            options[i].transform.position = transform.position - new Vector3(0.0f, GetComponent<RectTransform>().sizeDelta.y * transform.parent.GetComponent<RectTransform>().localScale.y * (i + 1), 0.0f);
        }
    }

    public void SelectOption(int selectedOption)
    {
        options[this.selectedOption].GetComponent<DropdownElement>().itemCheckMark.SetActive(false);
        this.selectedOption = selectedOption;
        dropdownLabel = GetComponentInChildren<TextMeshProUGUI>();
        dropdownLabel.text = options[selectedOption].GetComponentInChildren<TextMeshProUGUI>().text;
        options[selectedOption].GetComponent<DropdownElement>().itemCheckMark.SetActive(true);
    }
}
