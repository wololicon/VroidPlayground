using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public Transform sphere;
    private Material dirtyMaterial;
    private bool switchValue;
    private float rotationSpeed = .05f;

    private void Awake()
    {
        dirtyMaterial = sphere.GetComponent<Renderer>().sharedMaterial;
    }

    private void Update()
    {
        sphere.Rotate(Vector3.up, rotationSpeed);
    }

    public void ChangeDirtiness(float amount)
    {
        Debug.Log("here");
        dirtyMaterial.SetFloat("_Dirtiness", amount);
    }

    public void ChangeDirtThickness(float amount)
    {
        dirtyMaterial.SetFloat("_BumpHeight", amount);
    }

    public void ChangeDirtTransparency(float amount)
    {
        dirtyMaterial.SetFloat("_Transparency", amount);
    }

    public void ChangeDirtType()
    {
        if (switchValue)
        {
            dirtyMaterial.SetFloat("_Wetness", 1f);
            dirtyMaterial.SetFloat("_Dirtiness", .5f);
            dirtyMaterial.SetFloat("_BumpHeight", .75f);
            dirtyMaterial.SetFloat("_Transparency", .35f);
            dirtyMaterial.SetColor("_DirtColor", Color.green);
        }
        else
        {
            dirtyMaterial.SetFloat("_Wetness", .15f);
            dirtyMaterial.SetFloat("_Dirtiness", .5f);
            dirtyMaterial.SetFloat("_BumpHeight", 1f);
            dirtyMaterial.SetFloat("_Transparency", 0f);
            dirtyMaterial.SetColor("_DirtColor", new Color32(75, 44, 0, 0));
        }
        switchValue = !switchValue;
    }
}
