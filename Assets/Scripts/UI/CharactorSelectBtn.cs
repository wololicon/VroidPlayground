using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorSelectBtn : MonoBehaviour
{
    public VrmInfo _vrm;
    [SerializeField]
    Image _icon;
    [SerializeField]
    Text _name;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadVrm);
    }

    public void GetVrmInfo(VrmInfo info)
    {
        _vrm = info;
        _name.text = _vrm.name;
        _icon.sprite = Sprite.Create(_vrm.icon, new Rect(0, 0, _vrm.icon.width, _vrm.icon.height), new Vector2(0.5f, 0.5f));
    }
 
    void LoadVrm()
    {
        CharacterManager._instance.LoadVrmToCameraPoint(_vrm.path);
    }
}
