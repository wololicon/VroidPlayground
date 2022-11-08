using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWindow : MonoBehaviour
{
    public void SetPanelActive(bool value)
    {
        if (value)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(0, 0, 0);
    }
}
