using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorSelectPanel : SecondWindow
{
    public Transform _panel;
    public Transform _content;
    public GameObject _btnPrefab;
    VrmInfo[] VrmInfoList;
    // Start is called before the first frame update
    void Start()
    {
        VrmInfoList = VrmRuntimeImport.GetVrmList();
        for(int a=0;a<VrmInfoList.Length;a++)
        {
            GameObject go = Instantiate(_btnPrefab, _content);
            go.GetComponent<CharactorSelectBtn>().GetVrmInfo(VrmInfoList[a]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
