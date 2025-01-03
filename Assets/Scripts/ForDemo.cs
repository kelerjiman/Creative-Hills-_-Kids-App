using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ForDemo : MonoBehaviour
{
    public static ForDemo Instance;
    public event Action ChangeSceneEvent;
    [SerializeField] private ScreenOrientation orientation;
    [SerializeField] private bool isFirstScene;
    [SerializeField] private Transition transition;
    [SerializeField] private bool isSplash;
    private bool _canTouch;

    private void Awake()
    {
        Instance = this;
        transform.position = new Vector3(0, 0, transform.position.z);
        _canTouch = true;
    }

    private void Start()
    {
        // Screen.orientation = orientation;
        // Screen.autorotateToPortrait =
        //     orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.AutoRotation;
        // Screen.autorotateToLandscapeLeft =
        //     orientation != ScreenOrientation.Portrait && orientation == ScreenOrientation.AutoRotation;
        if(isSplash)
            return;
        transition.Enter();
    }

    private void Update()
    {
        if (isFirstScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }

    public void ChangeOrientation(ScreenOrientation or)
    {
        Screen.orientation = or;
        Screen.autorotateToPortrait = Screen.orientation == ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = Screen.orientation != ScreenOrientation.LandscapeLeft;
    }

    public void LoadScene(string sceneName)
    {
        if(!_canTouch)
            return;
        _canTouch = false;
        ChangeSceneEvent?.Invoke();
        transition.Exit(() => Addressables.LoadSceneAsync(sceneName));
    }
    
    public void LoadSceneImmediately(string sceneName)
    {
        if(!_canTouch)
            return;
        _canTouch = false;
        ChangeSceneEvent?.Invoke();
        transition.Exit(() => Addressables.LoadSceneAsync(sceneName));
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }
}