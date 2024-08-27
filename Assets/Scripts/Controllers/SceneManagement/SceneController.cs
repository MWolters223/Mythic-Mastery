using UnityEngine;

public class SceneController : MonoBehaviour
{
    private SceneModel model;
    private SceneView view;

    private SceneInputController sceneInputController;
    private SceneTransitionController sceneTransitionController;
    private SceneAnimationController sceneAnimationController;
    private SceneLoadingController sceneLoadingController;

    public static SceneController Instance { get; private set; }

    //  Getters to make controllers accesible from instance
    public SceneInputController SceneInputController => sceneInputController;
    public SceneTransitionController SceneTransitionController => sceneTransitionController;
    public SceneAnimationController SceneAnimationController => sceneAnimationController;
    public SceneLoadingController SceneLoadingController => sceneLoadingController;

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
        model = GetComponent<SceneModel>();
        view = GetComponent<SceneView>();

        sceneAnimationController = gameObject.AddComponent<SceneAnimationController>();
        sceneAnimationController.Initialize(model, view);

        sceneLoadingController = gameObject.AddComponent<SceneLoadingController>();
        sceneLoadingController.Initialize(view, sceneAnimationController);

        sceneInputController = gameObject.AddComponent<SceneInputController>();
        sceneInputController.Initialize(model, view);

        sceneTransitionController = gameObject.AddComponent<SceneTransitionController>();
        sceneTransitionController.Initialize(model, view, sceneAnimationController, sceneLoadingController);
    }

    private void Update()
    {
        sceneInputController.HandleInput();
    }
}