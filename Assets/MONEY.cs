using UnityEngine;
using UnityEngine.Advertisements;

public class MONEY : MonoBehaviour
{
    public string androidGameId;
    public string iOsGameId;
    public bool testMode = true;
    public bool enablePerPlacementMode = true;
    public string gameId => (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOsGameId
            : androidGameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(gameId, testMode, enablePerPlacementMode);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
}
