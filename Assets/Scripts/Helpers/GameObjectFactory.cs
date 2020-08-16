using UnityEngine;

namespace NGTT.Helpers
{
    public static class GameObjectFactory
    {
        public static T Get<T>() where T : MonoBehaviour => new GameObject(typeof(T).Name).AddComponent<T>();

        public static T GetPrimitive<T>(PrimitiveType primitiveType) where T : MonoBehaviour
        {
            var gameObject = GameObject.CreatePrimitive(primitiveType);
            gameObject.name = typeof(T).Name;
            return gameObject.AddComponent<T>();
        }
    }
}