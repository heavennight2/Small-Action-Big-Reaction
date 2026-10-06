using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    public Transform player;

    // كم تكون المنصة خلف اللاعب قبل ما تطيح
    public float collapseDistance = 3f;

    private bool disasterStarted = false;
    private FallingPlatform[] fallingPieces;

    void Start()
    {
        // يلقى كل المنصات تلقائياً
        fallingPieces = FindObjectsByType<FallingPlatform>(
            FindObjectsSortMode.None
        );
    }

    public void StartDisaster()
    {
        disasterStarted = true;

        Debug.Log("DISASTER STARTED!");
    }

    void Update()
    {
        if (!disasterStarted || player == null)
            return;

        foreach (FallingPlatform piece in fallingPieces)
        {
            if (piece == null)
                continue;

            // إذا اللاعب تقدم عن المنصة بمسافة معينة
            if (player.position.x > piece.transform.position.x + collapseDistance)
            {
                piece.Fall();
            }
        }
    }
}