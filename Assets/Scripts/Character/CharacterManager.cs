using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance;
    public GameObject VrmObject;
    public VrmCharacterControl VrmRagDoll;
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
        VrmRagDoll = VrmObject.AddComponent<VrmCharacterControl>();
        VrmObject.GetComponent<Animator>().runtimeAnimatorController = _anim;
    }
    public void LoadVrm(string path, Vector3 pos, Vector3 rot)
    {
        DestroyImmediate(VrmObject);
        VrmObject = VrmRuntimeImport.ImportRuntime(path);
        VrmObject.transform.position = pos;
        VrmObject.transform.rotation = Quaternion.Euler(rot);
        VrmRagDoll = VrmObject.AddComponent<VrmCharacterControl>();
        VrmObject.GetComponent<Animator>().runtimeAnimatorController = _anim;
    }

    public void LoadVrmToCameraPoint(string path)
    {
        DestroyImmediate(VrmObject);
        VrmObject = VrmRuntimeImport.ImportRuntime(path);
        Transform cam = Camera.main.transform;
        Ray r = new Ray(cam.position, cam.forward);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            VrmObject.transform.position = hit.point;
        }
        else
        {
            VrmObject.transform.position = cam.position;
        }

        VrmRagDoll = VrmObject.AddComponent<VrmCharacterControl>();
        VrmObject.GetComponent<Animator>().runtimeAnimatorController = _anim;
    }

}
