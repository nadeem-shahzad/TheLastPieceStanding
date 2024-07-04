using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    // Property to access the singleton instance
    public static T Instance
    {
        get
        {
            // Check if the instance is null (not yet set)
            if (instance == null)
            {
                // Try to find an existing instance in the scene
                instance = FindObjectOfType<T>();

                // If no instance found, create a new GameObject with the script attached
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    // Optionally add your singleton-related methods and properties here

    private void Awake()
    {
        // Ensure only one instance of the singleton exists
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
        }
    }
}