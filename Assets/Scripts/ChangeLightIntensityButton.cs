using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeLightIntensityButton : MonoBehaviour
{
    private VisualElement frame;
    private Button button;

    public List<Light> lights;
    private string[] lightStates = {"on", "dim", "off"};
    private uint currentLightsStateIndex = 0;
    private Dictionary<string, float[]> intensityValuesDict = new Dictionary<string, float[]>();

    // Functionality for this method heavily copied from ChangeCameraButton.cs
    void OnEnable()
    {
        // get the UIDocument component (make sure this name matches!)
        var uiDocument = GetComponent<UIDocument>();
        // get the rootVisualElement (the frame component)
        var rootVisualElement = uiDocument.rootVisualElement;
        frame = rootVisualElement.Q<VisualElement>("Frame");
        // get the button, which is nested in the frame
        button = frame.Q<Button>("Button");

        // Add arrays to dict for intensity values
        intensityValuesDict.Add("on", new float[lights.Count]);
        intensityValuesDict.Add("dim", new float[lights.Count]);
        intensityValuesDict.Add("off", new float[lights.Count]);
        // Get the base intensity value of every light in the list of lights
        for (int i = 0; i < lights.Count; i++) {
            intensityValuesDict["on"][i] = lights[i].intensity;
            intensityValuesDict["dim"][i] = lights[i].intensity / 2;
        }

        // Create event listener that calls CycleLightIntensity() when pressed
        button.RegisterCallback<ClickEvent>(ev => CycleLightIntensity());
    }

    void CycleLightIntensity()
    {
        currentLightsStateIndex = (currentLightsStateIndex + 1) % (uint)lightStates.Length;
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].intensity = intensityValuesDict[lightStates[currentLightsStateIndex]][i];
        }
    }
}
