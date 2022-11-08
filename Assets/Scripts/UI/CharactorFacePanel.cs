using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
public class CharactorFacePanel : SecondWindow
{
    VRMBlendShapeProxy proxy;
    List<BlendValueInfo> InfoList;

    public Transform panel;
    public Transform content;
    public GameObject sliderPrefab;
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager._instance.OnVrmChange += LoadFaceKey;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeValue(BlendValueInfo info, float value)
    {
        proxy.ImmediatelySetValue(info.key, value);

    }

    void LoadFaceKey()
    {
        proxy = CharacterManager._instance.VrmObject.GetComponent<RagDollControl>().FaceProxy;
        InfoList = new List<BlendValueInfo>();
        for (int a = 0; a < proxy.BlendShapeAvatar.Clips.Count; a++)
        {
            if (a > content.childCount-1)
            {
                BlendValueInfo info = new BlendValueInfo();
                info.name = proxy.BlendShapeAvatar.Clips[a].Key.Name;
                info.key = proxy.BlendShapeAvatar.Clips[a].Key;
                InfoList.Add(info);
                GameObject go = Instantiate(sliderPrefab, content);
                CharactorFaceSlider slider = go.GetComponent<CharactorFaceSlider>();
                slider.GetInfo(info);
                slider._faceControl = this;
            }
            else
            {
                BlendValueInfo info = new BlendValueInfo();
                info.name = proxy.BlendShapeAvatar.Clips[a].Key.Name;
                info.key = proxy.BlendShapeAvatar.Clips[a].Key;
                InfoList.Add(info);
                GameObject go = content.GetChild(a).gameObject;
                CharactorFaceSlider slider = go.GetComponent<CharactorFaceSlider>();
                slider.GetInfo(info);
                slider._faceControl = this;
            }
        }
    }
}

public class BlendValueInfo
{
    public string name;
    public BlendShapeKey key;
}
