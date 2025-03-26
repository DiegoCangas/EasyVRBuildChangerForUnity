using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features.Interactions;
using UnityEngine.XR.OpenXR.Features.MetaQuestSupport;


public class AndroidTargetBuild : IPreprocessBuildWithReport//, IPostprocessBuildWithReport
{
    public int callbackOrder
    {
        get { return 0; }
    }
    private int _platform = 0;

    public void OnPreprocessBuild(BuildReport buildTarget)
    {
        OpenXRSettings settings = OpenXRSettings.ActiveBuildTargetInstance;
        #if UNITY_ANDROID
        _platform = EditorUtility.DisplayDialogComplex("Version",
            "Plataforma de Compilacion", "Meta Quest", "Otra","Pico");

        if (_platform == 1) return;
        Debug.Log("Plataforma: " + _platform);
        foreach (var featureSet in OpenXRFeatureSetManager.FeatureSetsForBuildTarget(BuildTargetGroup.Android))
        {
            if(featureSet.name.Contains("Meta Quest")) featureSet.isEnabled = (_platform == 0);
            if(featureSet.name.Contains("PICO XR")) featureSet.isEnabled = (_platform == 2);
        }
        foreach (var feature in settings.GetFeatures()) {
            //Debug.Log(feature.GetType().ToString());
           // if (feature is ) feature.enabled =(_platform == 0);
            //if (feature is ) feature.enabled =(_platform == 2);
           
            if (feature is PICO4ControllerProfile) feature.enabled =(_platform == 2);
            if (feature is PICO4UltraControllerProfile) feature.enabled =(_platform == 2);
            if (feature is PICONeo3ControllerProfile) feature.enabled =(_platform == 2);

            
            if (feature is OculusTouchControllerProfile) feature.enabled =(_platform == 0);
            if (feature is MetaQuestTouchPlusControllerProfile) feature.enabled =(_platform == 0);
            if (feature is MetaQuestTouchProControllerProfile) feature.enabled =(_platform == 0);
        }
        OpenXRFeatureSetManager.SetFeaturesFromEnabledFeatureSets(BuildTargetGroup.Android);
        EditorUtility.SetDirty(settings);
        AssetDatabase.SaveAssets();
        Debug.Log("Configuracion de OpenXR Actualizada");
        #endif
    }
    /*
    public void OnPostprocessBuild(BuildReport report)
    {
        string path = report.summary.outputPath;
    
        if (path.EndsWith(".apk")) // Android builds
        {
            string newPath = path;
            if (_platform == 1) return;
            if (_platform == 0) newPath = newPath.Replace(".apk", "_meta.apk");
            if (_platform == 2) newPath = newPath.Replace(".apk", "_pico.apk");

            if (System.IO.File.Exists(newPath)) 
                System.IO.File.Delete(newPath);
            System.IO.File.Copy(path, newPath);
        Debug.Log($"Build renamed to: {newPath}");
        }
    }*/
}
