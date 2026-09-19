using UnityEngine;

namespace FernandoPFH_Essentials_Runtime
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [SerializeField] private DuplicatesApproach duplicatesApproach;
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance is null)
                Instance = this as T;
            else
                switch (duplicatesApproach)
                {
                    case DuplicatesApproach.KeepFirst:
                        Destroy(gameObject);
                        break;
                    case DuplicatesApproach.KeepLast:
                        Destroy(Instance.gameObject);
                        Instance = this as T;
                        break;
                    case DuplicatesApproach.DestroyBoth:
                        Destroy(Instance.gameObject);
                        Instance = null;
                        Destroy(gameObject);
                        break;
                }
        }

        enum DuplicatesApproach
        {
            KeepFirst,
            KeepLast,
            DestroyBoth
        }
    }
}