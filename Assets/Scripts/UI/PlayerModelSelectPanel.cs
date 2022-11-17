using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModelSelectPanel : SecondWindow
{
    public Transform _panel;
    public Transform _content;
    public GameObject _btnPrefab;
    VrmInfo[] VrmInfoList;
    // Start is called before the first frame update
    void Start()
    {
        VrmInfoList = VrmRuntimeImport.GetVrmList();
        for (int a = 0; a < VrmInfoList.Length; a++)
        {
            GameObject go = Instantiate(_btnPrefab, _content);
            CharactorSelectBtn btn = go.GetComponent<CharactorSelectBtn>();
            btn.GetVrmInfo(VrmInfoList[a]);
            btn._btnEvent = SetPlayerModel;
        }
    }

    // Update is called once per frame
    void SetPlayerModel(string path)
    {
        CharacterManager._instance.SetPlayerModel(path);
    }
}
