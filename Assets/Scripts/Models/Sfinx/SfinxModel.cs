using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SfinxModel : MonoBehaviour
{
    [Header("Setup Required")]
    public GameObject GodPrefab;
    public SfinxConfig SfinxConfig;
    public GameObject Player { get; set; }
    private Rigidbody rb { get; set; } 
    public Transform StatueTransform { get; set; }
    public Transform DiskTransform { get; set; }
    public float cooldownTimer;

    private void Start()
    {
        InitializeComponents();
        InitializeTransforms();
        InitializePlayer();
    }

    void Awake()
    {
        cooldownTimer = SfinxConfig.shootingCooldown;
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.Log("Add a rigidBody to the sfinx");
        }
        else
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
            rb.velocity = Vector3.zero;
        }

        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
        {
            Debug.LogError("Sfinx has no box collider, please add it");
        }

        cooldownTimer = SfinxConfig.shootingCooldown;
    }

    private void InitializeTransforms()
    {
        transform.localScale = new Vector3(2, 2, 2);

        StatueTransform = GodPrefab.transform.Find("standbeeld");
        DiskTransform = GodPrefab.transform.Find("grondplaat");

        if (StatueTransform == null)
        {
            Debug.LogError("Standbeeld part not found in the sfinx prefab.");
        }
        if (DiskTransform == null)
        {
            Debug.LogError("Grondplaat part not found in the sfinx prefab.");
        }
    }

    private void InitializePlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.Log("Player is not in scene or player has no tag Player");
        }
    }
}