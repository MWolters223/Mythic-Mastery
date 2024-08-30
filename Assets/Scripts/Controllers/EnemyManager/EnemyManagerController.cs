using UnityEngine;

public class EnemyManagerController : MonoBehaviour
{

    private EnemyManagerModel model;

    private EnemyManagerRegistrationController enemyManagerRegistrationController;

    public static EnemyManagerController Instance { get; private set; }

    public EnemyManagerRegistrationController EnemyManagerRegistrationController => enemyManagerRegistrationController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        model = GetComponent<EnemyManagerModel>();
        enemyManagerRegistrationController = gameObject.AddComponent<EnemyManagerRegistrationController>();
        enemyManagerRegistrationController.Initialize(model);
    }
}