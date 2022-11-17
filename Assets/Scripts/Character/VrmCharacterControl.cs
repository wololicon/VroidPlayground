using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
public class VrmCharacterControl : MonoBehaviour
{
    public int HP = 100;
    public int HP_Max = 100;
    public bool CharAlive;
    Rigidbody[] Rig_Rag;
    Animator animator;
    public Rigidbody Move_Rig;
    public Collider Move_Colldier;
    public Vector3 HeadLookAt;
    public Avatar _avatar;


    public float dirtiness;


    public SkinnedMeshRenderer FaceProxy
    {
        get
        {
            if (_face == null)
                return transform.Find("Face").GetComponent<SkinnedMeshRenderer>();
            else
                return _face;
        }
    }
    SkinnedMeshRenderer _face;
    //public List<BlendValueInfo> FaceBlendShape = new List<BlendValueInfo>();
    public Collider[] HitBoxs;
    // Start is called before the first frame update
    void Start()
    {
        CharAlive = true;
        RuntimeRagdollBuilder ragBuilder = new RuntimeRagdollBuilder();
        ragBuilder.BuildStart(transform);
        animator = GetComponent<Animator>();
        _avatar = animator.avatar;
        Move_Rig = GetComponent<Rigidbody>();
        Move_Colldier = GetComponent<Collider>();
        Rig_Rag = transform.Find("Root").GetComponentsInChildren<Rigidbody>();
        HitBoxs = transform.Find("Root").GetComponentsInChildren<Collider>();
        _face = transform.Find("Face").GetComponent<SkinnedMeshRenderer>();
        for (int i = 0; i < HitBoxs.Length; i++)
        {
            HitBoxs[i].gameObject.AddComponent<RagdollHitbox>();
        }

        SetRagdollActive(false);
    }

    public void SetFaceBlendShape(int id, float value)
    {
        _face.SetBlendShapeWeight(id, value);
    }
    public float GetFaceBlendShapeValue(int id)
    {
        return _face.GetBlendShapeWeight(id);
    }
    public void SetRagdollActive(bool value)
    {
        CharAlive = !value;
        animator.enabled = !value;
        if (value)
        {
            Destroy(Move_Rig);
            Destroy(Move_Colldier);
        }
        for (int a = 0; a < Rig_Rag.Length; a++)
        {
            Rig_Rag[a].isKinematic = !value;
        }
        //VRMSpringBone[] vsb = GetComponentsInChildren<VRMSpringBone>();
        //for (int i = 0; i < vsb.Length; i++)
        //{
        //    vsb[i].ColliderGroups = null;
        //}
    }
    private void FixedUpdate()
    {
        if (CharAlive)
        {
            RigidbodyMoveSmooth();
            SetDirtyValue((1 - ((float)HP / HP_Max)) * 0.5f);
            if (HP <= 0)
            {
                SetRagdollActive(true);
            }
        }
    }
    public void SetHeadLookPosition(Vector3 pos, bool IsOpen = true)
    {
        float angle = Vector3.Dot(transform.forward, pos);
        HeadLookAt = pos;
        if (angle > 20)
        {
            if (HeadLookAtWeight < 1)
                HeadLookAtWeight += 0.01f;
        }
        else
        {
            //HeadLookAt = transform.forward * 100 + Vector3.up * 2;
            if (HeadLookAtWeight > 0)
                HeadLookAtWeight -= 0.01f;
        }
    }
    #region FootIK
    //public Transform FootL, FootR;
    //Vector3 FootL_IKPOS, FootR_IKPOS;
    //public float Ik_Distance;
    //void SetFootIk()
    //{

    //    Vector3 ori, dir;

    //    ori = FootL.position;
    //    dir = -FootL.up;

    //    Ray ray = new Ray(ori, dir);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit, Ik_Distance, LayerMask.GetMask("Terrain")))
    //    {
    //        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
    //        animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + Vector3.up * Ik_Distance);
    //    }
    //    else
    //    {
    //        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
    //    }
    //    ori = FootR.position;
    //    dir = -FootR.up;

    //    ray = new Ray(ori, dir);
    //    if (Physics.Raycast(ray, out hit, Ik_Distance, LayerMask.GetMask("Terrain")))
    //    {
    //        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
    //        animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + Vector3.up * Ik_Distance);
    //    }
    //    else
    //    {
    //        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Vector3 ori, dir;

    //    ori = FootL.position;
    //    dir = -FootL.up;
    //    Gizmos.DrawRay(ori, dir);
    //}
    #endregion
    float HeadLookAtWeight;

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(HeadLookAt);
        animator.SetLookAtWeight(HeadLookAtWeight, HeadLookAtWeight * 0.2f, HeadLookAtWeight, HeadLookAtWeight);
        //SetFootIk();
    }

    Vector3 offset;
    void RigidbodyMoveSmooth()
    {
        if (Mathf.Abs(ForwardSpeed - TargetSpeed) > 0.01f)
        {
            ForwardSpeed = Mathf.Lerp(ForwardSpeed, TargetSpeed, Acceleration);
        }
        else
        {
            ForwardSpeed = Mathf.Lerp(ForwardSpeed, TargetSpeed, Acceleration);
        }

        Move_Rig.MovePosition(transform.position + transform.forward.normalized * ForwardSpeed);
        animator.SetFloat("ForwardSpeed", ForwardSpeed * 10);
    }
    public Vector2 MoveDirection;
    public float RotateSpeed = 1;
    public float SprintSpeed = 1.5f, RunSpeed = 0.9f, WalkSpeed = 0.3f;
    public float MoveSpeedMax = 0.1f, ForwardSpeed;
    public float TargetSpeed, Acceleration = 0.05f;

    public void SetMoveDirection(Vector3 direct, float speed)
    {
        float angle1 = Vector3.Dot(transform.right, direct);
        float angle2 = Vector3.Dot(transform.forward, direct);
        if (angle1 != 0)
        {

            transform.Rotate(transform.up, RotateSpeed * Mathf.Sign(angle1));
        }
        else if (angle2 < 0)
        {
            transform.Rotate(transform.up, -RotateSpeed);
        }


        TargetSpeed = speed * MoveSpeedMax;

    }

    public void SetDirtyValue(float value)
    {

        if (dirtiness == value)
        {
            return;
        }
        dirtiness = value;
        SkinnedMeshRenderer[] mr = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int a = 0; a < mr.Length; a++)
        {
            for (int b = 0; b < mr[a].materials.Length; b++)
            {
                mr[a].materials[b].SetFloat("_Dirtiness", value);
            }
        }
    }
}
