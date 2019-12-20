using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGenerator : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<MeshCollider>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
}
