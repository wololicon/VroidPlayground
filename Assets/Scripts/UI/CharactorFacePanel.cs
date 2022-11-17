using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
public class CharactorFacePanel : SecondWindow
{
    VrmCharacterControl charataer;
    List<BlendValueInfo> InfoList;

    public Transform panel;
    public Transform content;
    public GameObject sliderPrefab;
    // Start is called before the first frame update
    void Start()
    {
        onWindowOpen += LoadFaceKey;
    }

    public void ChangeValue(BlendValueInfo info, float value)
    {
        charataer.SetFaceBlendShape(info.id, value);

    }

    void LoadFaceKey()
    {
        charataer = CharacterManager._instance._playerControl._player.GetComponent<VrmCharacterControl>();
        if (charataer == null)
            return;
        InfoList = new List<BlendValueInfo>();
        Mesh mesh = charataer.FaceProxy.sharedMesh;
        for (int a = 0; a < mesh.blendShapeCount; a++)
        {

            if (a > content.childCount - 1)
            {
                BlendValueInfo info = new BlendValueInfo();
                info.id = a;
                info.name = charataer.FaceProxy.sharedMesh.GetBlendShapeName(a);
                info.value = charataer.FaceProxy.GetBlendShapeWeight(a);
                InfoList.Add(info);
                GameObject go = Instantiate(sliderPrefab, content);
                CharactorFaceSlider slider = go.GetComponent<CharactorFaceSlider>();
                slider.GetInfo(info);
                slider._faceControl = this;
            }
            else
            {
                BlendValueInfo info = new BlendValueInfo();
                info.id = a;
                info.name = charataer.FaceProxy.sharedMesh.GetBlendShapeName(a);
                info.value = charataer.FaceProxy.GetBlendShapeWeight(a);
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
    public int id;
    public string name;
    public float value;
}
