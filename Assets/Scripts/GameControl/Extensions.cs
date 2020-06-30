using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        public static void RemoveCloneFromName(this GameObject gameObject)
        {
            gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
        }
    }
}
