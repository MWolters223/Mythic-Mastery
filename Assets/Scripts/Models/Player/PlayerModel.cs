using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [HideInInspector]
    public GameObject godPrefab;

    public PlayerConfig config;
    
    [HideInInspector]
    private Rigidbody rb;

    [HideInInspector]
    public Transform statueTransform;

    [HideInInspector]
    public Transform diskTransform;

    [HideInInspector]
    public LineRenderer lineRenderer;

    [HideInInspector]
    public Vector3 velocity = Vector3.zero;

    void Start()
    {
        Debug.Log("GodController Start on " + gameObject.name);

        InitializeComponents();
        InitializeTransforms();
        InitializeLayerMasks();
        InitializeLineRenderer();
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("God has no rigidbody, please add it");
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        if (GetComponent<BoxCollider>() == null)
        {
            Debug.LogError("God has no box collider, please add it");
        }

        transform.localScale = new Vector3(2, 2, 2);
    }

    private void InitializeTransforms()
    {
        if (godPrefab == null)
        {
            Debug.LogError("God prefab not assigned.");
            return;
        }

        statueTransform = godPrefab.transform.Find("standbeeld");
        diskTransform = godPrefab.transform.Find("grondplaat");

        if (statueTransform == null)
        {
            Debug.LogError("Standbeeld part not found in the god prefab.");
        }

        if (diskTransform == null)
        {
            Debug.LogError("Grondplaat part not found in the god prefab.");
        }
    }

    private void InitializeLayerMasks()
    {
        config.groundMask = GetLayerMask("Ground");
        config.obstacleMask = GetLayerMask("Obstacle");
        config.enemyMask = GetLayerMask("Enemy");
    }

    private LayerMask GetLayerMask(string layerName)
    {
        int layerIndex = LayerMask.NameToLayer(layerName);
        if (layerIndex == -1)
        {
            Debug.LogError($"Layer '{layerName}' not found.");
            return 0;
        }
        return 1 << layerIndex;
    }

    private void InitializeLineRenderer()
    {
        GameObject lineRendererObject = GameObject.Find("Line");
        if (lineRendererObject == null)
        {
            Debug.LogError("GameObject 'Line' not found in scene, please add it.");
            return;
        }

        lineRenderer = lineRendererObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("No LineRenderer component found on 'Line'.");
        }
    }
}
