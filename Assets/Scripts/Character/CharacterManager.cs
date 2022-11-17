using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance;
    public PlayerControl _playerControl;
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

    //private void Update()
    //{
    //    if (VrmObject != null && VrmObject != temp)
    //    {
    //        OnVrmChange?.Invoke();
    //        temp = VrmObject;
    //    }
    //}

    //public void LoadVrm(string path)
    //{
    //    DestroyImmediate(VrmObject);
    //    VrmObject = VrmRuntimeImport.ImportRuntime(path);
    //    VrmRagDoll = VrmObject.AddComponent<VrmCharacterControl>();
    //    VrmObject.GetComponent<Animator>().runtimeAnimatorController = _anim;
    //}
    public GameObject LoadVrm(string path, Vector3 pos, Vector3 rot)
    {
        //DestroyImmediate(VrmObject);
        GameObject VrmObject = VrmRuntimeImport.ImportRuntime(path);
        VrmObject.transform.parent = transform;
        VrmObject.transform.position = pos;
        VrmObject.transform.rotation = Quaternion.Euler(rot);
        VrmObject.AddComponent<VrmCharacterControl>();
        VrmObject.GetComponent<Animator>().runtimeAnimatorController = _anim;
        return VrmObject;
    }

    public GameObject LoadVrmToCameraPoint(string path)
    {
        //DestroyImmediate(VrmObject);
        GameObject VrmObject = VrmRuntimeImport.ImportRuntime(path);
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


        VrmObject.layer = 6;
        CapsuleCollider MoveCollider = VrmObject.AddComponent<CapsuleCollider>();
        MoveCollider.center = new Vector3(0, 0.8f, 0);
        MoveCollider.radius = 0.3f;
        MoveCollider.height = 1.6f;
        Rigidbody MoveRigdibody = VrmObject.AddComponent<Rigidbody>();
        MoveRigdibody.mass = 80;
        MoveRigdibody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        VrmObject.GetComponent<Animator>().runtimeAnimatorController = _anim;
        VrmObject.AddComponent<VrmCharacterControl>();
        VrmObject.transform.parent = transform;
        return VrmObject;
    }

    public void SetPlayerModel(string path)
    {
        VrmCharacterControl _player = LoadVrmToCameraPoint(path).GetComponent<VrmCharacterControl>();
        print(_player);
        _playerControl.SetPlayer(_player);
    }

}
