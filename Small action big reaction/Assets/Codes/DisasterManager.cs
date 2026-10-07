using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    public static bool disasterStarted = false;

    void Start()
    {
        // كل مرة يبدأ الليفل تكون المنصات آمنة
        disasterStarted = false;
    }

    public void StartDisaster()
    {
        if (disasterStarted)
            return;

        disasterStarted = true;

        Debug.Log("DISASTER STARTED!");
    }
}