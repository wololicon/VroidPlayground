using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingRope : MonoBehaviour
{
    public Joint joint;
    public LineRenderer lineRenderer;
    public Transform Head;
    public GameObject ropePrefab;
    Transform ropePort;
    float offset = 0.12f;
    // Start is called before the first frame update
    void Start()
    {
        //CharacterManager._instance.OnVrmChange += VrmChange;
    }

    private void OnDisable()
    {
        if(ropePort!=null)
            ropePort.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (joint.connectedBody == null&& CharacterManager._instance.VrmObject!=null)
        {
            VrmChange();
        }
        if (joint.connectedBody != null)
        {
            Vector3[] line = new Vector3[2];
            line[0] = transform.position;
            line[1] = ropePort.position + ropePort.up * offset;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(line);
        }
    }

    void VrmChange()
    {
        Rigidbody[] rig = CharacterManager._instance.VrmObject.GetComponentsInChildren<Rigidbody>();
        print(CharacterManager._instance.VrmRagDoll.BuildRagdoll);
        for (int a = 0; a < rig.Length; a++)
        {

            if (rig[a].gameObject.name.Equals("J_Bip_C_Head"))
            {
                Head = rig[a].transform;
                GameObject ro = Instantiate(ropePrefab, Head);
                ro.transform.localPosition = new Vector3(0.0078f, -0.0031f, -0.0109f);
                ro.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, -60.246f));
                ropePort = ro.transform;
                joint.connectedBody = rig[a];
                joint.autoConfigureConnectedAnchor = false;
                float distance = (joint.transform.position - Head.transform.position).magnitude;
                //distance = (distance * 0.5f) / Mathf.Sin(3.14f * 0.5f);
                joint.connectedAnchor = (joint.transform.position - Head.transform.position) + new Vector3(0, 0, -distance * 0.5f);
                break;
            }
        }
    }
}
