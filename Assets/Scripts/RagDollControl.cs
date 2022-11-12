using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
public class RagDollControl : MonoBehaviour
{
    Rigidbody[] Rig_Rag;
    Animator animator;
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
    public bool BuildRagdoll=false;
    // Start is called before the first frame update
    void Start()
    {
        //if (BuildRagdoll)
        //{
        RuntimeRagdollBuilder ragBuilder = new RuntimeRagdollBuilder();
        ragBuilder.BuildStart(transform);
        //}
        animator = GetComponent<Animator>();
        Rig_Rag = GetComponentsInChildren<Rigidbody>();
        HitBoxs = GetComponentsInChildren<Collider>();
        _face = GetComponent<VRMBlendShapeProxy>();
        for (int i = 0; i < HitBoxs.Length; i++)
        {
            HitBoxs[i].gameObject.AddComponent<RagdollHitbox>();

        }
        RagdollDisactive();
        BuildRagdoll = true;
    }

    public void SetFaceBlendShape(BlendShapeKey key, float value)
    {
        _face.ImmediatelySetValue(key, value);
    }

    public void RagdollActive()
    {
        animator.enabled = false;
        for (int a = 0; a < Rig_Rag.Length; a++)
        {
            Rig_Rag[a].isKinematic = false;
        }
    }
    public void RagdollDisactive()
    {
        animator.enabled = true;
        for (int a = 0; a < Rig_Rag.Length; a++)
        {
            Rig_Rag[a].isKinematic = true;
        }
    }
}
