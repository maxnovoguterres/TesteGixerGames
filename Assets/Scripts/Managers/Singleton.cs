using UnityEngine;
using UnityEngine.Events;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    readonly UnityAction<T> awake;
    public Singleton (UnityAction<T> awake = null)
    {
        this.awake = awake;
    }

    private static T instance;
    public static T Instance
    {
        get => instance;
        set => instance = instance ?? value;
    }

    protected virtual void Awake ()
    {
        GetInstance();

        DontDestroyOnLoad(this);

        awake?.Invoke(this as T);
    }

    private void GetInstance ()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this as T;
        }
    }
}
