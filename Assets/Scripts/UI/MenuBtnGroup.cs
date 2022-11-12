using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtnGroup : MonoBehaviour
{
    public SecondWindow[] SecondWindowList;

    public void OpenSecondWindow(SecondWindow sw)
    {
        for (int a = 0; a < SecondWindowList.Length; a++)
        {
            SecondWindowList[a].SetPanelActive(SecondWindowList[a] == sw);
        }
        transform.localScale = new Vector3(0, 0, 0);
    }

    public void CloseSecondWindow(SecondWindow sw)
    {

        sw.SetPanelActive(false);
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
