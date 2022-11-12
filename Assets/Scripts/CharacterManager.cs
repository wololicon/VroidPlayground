using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance;
    public GameObject VrmObject;
    public RagDollControl VrmRagDoll;
    public RuntimeAnimatorController _anim;
    public delegate void OnVrmChangeEvent();
    public OnVrmChangeEvent OnVrmChange;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        VrmRuntimeImport.GetVrmList();
    }

    GameObject temp;

    private void Update()
    {
        if (VrmObject != null && VrmObject != temp)
        {
            OnVrmChange?.Invoke();
            temp = VrmObject;
        }
    }

    public void LoadVrm(string path)
    {
        DestroyImmediate(VrmObject);
        VrmObject = VrmRuntimeImport.ImportRuntime(path);
        VrmRagDoll = VrmObject.AddComponent<RagDollControl>();
        VrmObject.GetComponent<Animator>().runtimeAnimatorController = _anim;
    }

}
