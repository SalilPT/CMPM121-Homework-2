using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DayNightButtonScript : MonoBehaviour
{
    private VisualElement frame;
    private Button button;

    public string dayNightScriptName;
    private MonoBehaviour dayNightScript;
    public Light sunLight;
    public Light moonLight;
    public Material skyboxMaterialForNoCycle;
    public Material skyboxMaterialForCycle;
    private bool isOn = false;

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

        // Disable lights and day/night script
        sunLight.enabled = false;
        moonLight.enabled = false;
        dayNightScript = gameObject.GetComponent(dayNightScriptName) as MonoBehaviour;
        dayNightScript.enabled = false;

        // Create event listener that calls ToggleDayNightScript() when pressed
        button.RegisterCallback<ClickEvent>(ev => ToggleDayNightScript());
    }

    void ToggleDayNightScript()
    {
        isOn = !isOn;
        sunLight.enabled = isOn;
        moonLight.enabled = isOn;
        dayNightScript.enabled = isOn;
        RenderSettings.skybox = isOn ? skyboxMaterialForCycle : skyboxMaterialForNoCycle;
        return;
    }
}
