using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMP_InputFieldWithoutTouchScreenKeyboard : TMP_InputField
{
    protected override void Start()
    {
        keyboardType = (TouchScreenKeyboardType)(6);
        base.Start();
    }
}
