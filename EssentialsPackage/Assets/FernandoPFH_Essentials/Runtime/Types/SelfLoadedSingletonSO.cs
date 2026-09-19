using UnityEngine;
using UnityEditor;

namespace FernandoPFH_Essentials_Runtime
{
    public abstract class SelfLoadedSingletonSO<T> : BaseSelfLoadedSingletonSO where T : SelfLoadedSingletonSO<T>
    {
        [SerializeField] private DuplicatesApproach duplicatesApproach;
        protected static T Instance { get; private set; }

        protected virtual void OnEnable()
            => SetInstance();

        protected virtual void Awake()
            => SetInstance();

        protected virtual void SetInstance()
        {
            if (Instance is null)
                Instance = this as T;
            else if (duplicatesApproach is DuplicatesApproach.KeepLast)
                Instance = this as T;
        }

        enum DuplicatesApproach
        {
            KeepFirst,
            KeepLast
        }
    }

    public abstract class BaseSelfLoadedSingletonSO : ScriptableObject { }


#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class SelfLoaderScriptableObject
    {
#if UNITY_EDITOR
        const string FolderPath = "Assets/Resources";

        static SelfLoaderScriptableObject()
        {
            if (!AssetDatabase.IsValidFolder(FolderPath))
                AssetDatabase.CreateFolder(FolderPath.SplitLast("/")[0],FolderPath.SplitLast("/")[1]);

            foreach (string soGUID in AssetDatabase.FindAssets("t:BaseSelfLoadedSingletonSO", new[] { FolderPath }))
                AssetDatabase.LoadAssetAtPath<BaseSelfLoadedSingletonSO>(AssetDatabase.GUIDToAssetPath(soGUID));

            Debug.Log("BaseSelfLoadedSingletonSOs Loaded!");
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LoadInRuntime()
            => Resources.LoadAll<BaseSelfLoadedSingletonSO>("");
    }
}
