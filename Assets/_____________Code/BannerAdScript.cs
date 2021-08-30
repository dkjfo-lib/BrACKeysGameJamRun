using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour
{
    public string placementId = "bannerPlacement";
    public BannerPosition bannerPosition = BannerPosition.TOP_CENTER;

    void Start()
    {
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }
        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Show(placementId);
    }
}
