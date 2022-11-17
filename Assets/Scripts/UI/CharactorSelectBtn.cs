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
    Button _btn;
    public delegate void CharBtnEvent(string path);
    public CharBtnEvent _btnEvent;
    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(ButtonEvent);
    }

    public void GetVrmInfo(VrmInfo info)
    {
        _vrm = info;
        _name.text = _vrm.name;
        _icon.sprite = Sprite.Create(_vrm.icon, new Rect(0, 0, _vrm.icon.width, _vrm.icon.height), new Vector2(0.5f, 0.5f));
    }

    void ButtonEvent()
    {
        _btnEvent?.Invoke(_vrm.path);
    }
}
