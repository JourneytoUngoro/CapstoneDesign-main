using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropdownElement : MonoBehaviour
{
    public int optionNumber;
    public GameObject itemCheckMark;

    public void InformValueChange()
    {
        Dropdown dropdown = GetComponentInParent<Dropdown>();
        dropdown.SelectOption(optionNumber);
        dropdown.onValueChanged.Invoke(optionNumber);
        transform.parent.gameObject.SetActive(false);
    }
}
