using UnityEngine;

namespace FernandoPFH_Essentials_Runtime
{
    public static class Vector3Extension
    {
        public static Vector3 MultiplyElements(this Vector3 a, Vector3 b)
            => new(
                    a.x * b.x,
                    a.y * b.y,
                    a.z * b.z
                );
    }
}
