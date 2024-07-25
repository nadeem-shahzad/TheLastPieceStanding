using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[CustomEditor(typeof(GoogleAdsSettings))]
public class GoogleAdsSettingsEditor : UnityEditor.Editor
{
    private SerializedProperty m_BannerID;
    private SerializedProperty m_InterstitialID;
    private SerializedProperty m_RewardedID;
    private SerializedProperty m_IsTestAds;
    
    
    [MenuItem("Assets/Google Mobile Ads/Set IDs...")]
    public static void OpenInspector()
    {
        Selection.activeObject = GoogleAdsSettings.LoadInstance();
    }
    
    public void OnEnable()
    {
        m_BannerID = serializedObject.FindProperty("m_BannerId");
        m_InterstitialID = serializedObject.FindProperty("m_IntersititialId");
        m_RewardedID = serializedObject.FindProperty("m_RewardedId");
        m_IsTestAds = serializedObject.FindProperty("m_IsTestAds");
    }
    
    public override void OnInspectorGUI()
        {
            // Make sure the Settings object has all recent changes.
            serializedObject.Update();

            var settings = (GoogleAdsSettings)target;

            if(settings == null)
            {
              UnityEngine.Debug.LogError("GoogleAdsSettings is null.");
              return;
            }

            EditorGUILayout.LabelField("Google Mobile Ad IDs", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(m_BannerID, new GUIContent("Banner ID"));

            EditorGUILayout.PropertyField(m_InterstitialID, new GUIContent("Interstitial ID"));
            EditorGUILayout.PropertyField(m_RewardedID, new GUIContent("Rewarded Ad ID"));

            EditorGUILayout.HelpBox(
                    "Place Ad Ids for All Required Ads",
                    MessageType.Info);

            EditorGUI.indentLevel--;
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Test Ads", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(m_IsTestAds,
                                          new GUIContent("Use Test Ads"));

            serializedObject.ApplyModifiedProperties();
        }
}
