using Unity.VisualScripting;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager _instance = null;
    public static MainUIManager Instance { get { Init(); return _instance; } }

    private static EncyclopediaManager _encyclopediaManager;
    private static SoundManager _soundManager;
    
    public static EncyclopediaManager EncyclopediaManager { get { Init(); return _encyclopediaManager; } }
    public static SoundManager SoundManager { get{ Init();  return _soundManager; } }


    private static void Init()
    {
        if (_instance == null)
        {
            GameObject gameObject = FindObjectOfType(typeof(MainUIManager)) as GameObject;

            if (gameObject == null)
            {
                gameObject = new GameObject("MainUIManager");
            }

            _instance = GetOrAddComponent<MainUIManager>(gameObject);
            DontDestroyOnLoad(gameObject);

            _soundManager = CreateManager<SoundManager>();
            _encyclopediaManager = CreateManager<EncyclopediaManager>();

            _soundManager.Init();
        }

    }

    private static T CreateManager<T>() where T : UnityEngine.Component
    {
        GameObject go = new GameObject($"@{typeof(T)}");
        T result = go.AddComponent<T>();
        go.transform.SetParent(_instance.transform);

        return result;
    }

    public  static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }
}
