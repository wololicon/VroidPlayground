using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
public class VrmCharacterControl : MonoBehaviour
{
    Rigidbody[] Rig_Rag;
    Animator animator;
    public Rigidbody Move_Rig;
    public Vector3 HeadLookAt;
    public Avatar _avatar;
    public VRMBlendShapeProxy FaceProxy
    {
        get
        {
            if (_face == null)
                return GetComponent<VRMBlendShapeProxy>();
            else
                return _face;
        }
    }
    VRMBlendShapeProxy _face;
    public List<string> FaceBlendShape;
    public Collider[] HitBoxs;
    // Start is called before the first frame update
    void Start()
    {


        RuntimeRagdollBuilder ragBuilder = new RuntimeRagdollBuilder();
        ragBuilder.BuildStart(transform);
        animator = GetComponent<Animator>();
        _avatar = animator.avatar;
        for (int a = 0; a < _avatar.humanDescription.human.Length; a++)
        {
            print(_avatar.humanDescription.human[a].boneName);
        }
        Rig_Rag = transform.Find("Root").GetComponentsInChildren<Rigidbody>();
        HitBoxs = transform.Find("Root").GetComponentsInChildren<Collider>();
        _face = GetComponent<VRMBlendShapeProxy>();
        for (int i = 0; i < HitBoxs.Length; i++)
        {
            HitBoxs[i].gameObject.AddComponent<RagdollHitbox>();
        }
        SetRagdollActive(false);
    }

    public void SetFaceBlendShape(BlendShapeKey key, float value)
    {
        _face.ImmediatelySetValue(key, value);
    }

    public void SetRagdollActive(bool value)
    {
        animator.enabled = !value;
        for (int a = 0; a < Rig_Rag.Length; a++)
        {
            Rig_Rag[a].isKinematic = !value;
        }
    }

    private void FixedUpdate()
    {
        RigidbodyMoveSmooth();
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

    float HeadLookAtWeight;

    //void FootIk()
    //{
    //    _avatar.humanDescription.skeleton.
    //}

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(HeadLookAt);
        animator.SetLookAtWeight(HeadLookAtWeight);
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
    public float RotateSpeed;
    public float SprintSpeed, RunSpeed, WalkSpeed;
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
}
