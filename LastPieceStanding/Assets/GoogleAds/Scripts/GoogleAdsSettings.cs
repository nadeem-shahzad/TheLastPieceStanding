using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GoogleAdsSettings : ScriptableObject
{
    private const string MobileAdsSettingsResDir = "Assets/GoogleAds/Resources";

    private const string MobileAdsSettingsFile = "GoogleAdsSettings";

    private const string MobileAdsSettingsFileExtension = ".asset";
    
    
    public static GoogleAdsSettings LoadInstance()
    {
        //Read from resources.
        var instance = Resources.Load<GoogleAdsSettings>(MobileAdsSettingsFile);
#if UNITY_EDITOR
        //Create instance if null.
        if (instance == null)
        {
            Directory.CreateDirectory(MobileAdsSettingsResDir);
            instance = ScriptableObject.CreateInstance<GoogleAdsSettings>();
            string assetPath = Path.Combine(
                MobileAdsSettingsResDir,
                MobileAdsSettingsFile + MobileAdsSettingsFileExtension);
            UnityEditor.AssetDatabase.CreateAsset(instance, assetPath);
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif

        return instance;
    }


    [SerializeField] private string m_BannerId = string.Empty;
    [SerializeField] private string m_IntersititialId = string.Empty;
    [SerializeField] private string m_RewardedId = string.Empty;
    [SerializeField] private bool m_IsTestAds = true;

    private string t_BannerID = "ca-app-pub-3940256099942544/6300978111";
    private string t_InterstitialID= "ca-app-pub-3940256099942544/1033173712";
    private string t_RewardedAdId= "ca-app-pub-3940256099942544/5224354917";

    public string BannerID => IsTestAds ? t_BannerID : m_BannerId;

    public string InterstitialID => IsTestAds ? t_InterstitialID : m_IntersititialId;

    public string RewardedID => IsTestAds ? t_RewardedAdId : m_RewardedId;

    private bool IsTestAds => m_IsTestAds;
}
