using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWindow : MonoBehaviour
{
    public delegate void OnWindowOpen();
    public delegate void OnWindowClose();
    public event OnWindowOpen onWindowOpen;
    public event OnWindowClose onWindowClose;
    public void SetPanelActive(bool value)
    {
        if (value)
        {
            transform.localScale = new Vector3(1, 1, 1);
            onWindowOpen?.Invoke();
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
            onWindowClose?.Invoke();
        }
    }
}
