using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

public class EnvirmentScripts : MonoBehaviour
{
    public Animator _animator;
    public HangingRope _rpoe;
    bool IsOpen;
    public void EventTrigger()
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            if (CharacterManager._instance.VrmRagDoll != null)
            {
                CharacterManager._instance.VrmRagDoll.SetRagdollActive(false);
                //CharacterManager._instance.VrmRagDoll.SetFaceBlendShape(BlendShapeKey.CreateUnknown("Surprised"), 1);
            }
            _animator.SetBool("Start", true);
        }
        else
        {
            _animator.SetBool("Start", false);
        }
    }

    public void RopeSwitch()
    {
        _rpoe.gameObject.SetActive(!_rpoe.gameObject.activeSelf);
    }
}
