/*
 * MIT License
 * 
 * Copyright (c) 2025 Diego Cangas Moldes
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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


public class AndroidTargetBuild : IPreprocessBuildWithReport
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
}
