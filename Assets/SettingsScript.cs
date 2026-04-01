using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SettingsScript : MonoBehaviour
{

    public GameObject teleportPads;
    public GameObject XRRig;

    public Canvas settingsUI;

    private bool isSettingsPressedDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), InputHelpers.Button.MenuButton, out bool isPressed);

        if(isPressed)
        {
            if(!isSettingsPressedDown)
            {
                settingsUI.enabled = !settingsUI.enabled;
            }

            isSettingsPressedDown= true;
        }
        else
        {
            isSettingsPressedDown = false;
        }
    }

    public void ToggleTeleportPads_OnValueChanged(bool isOn)
    {
        teleportPads.SetActive(isOn);
    }

    public void ToggleVignette_OnValueChanged(bool isOn)
    {
        Sigtrap.VrTunnellingPro.Tunnelling.instance.enabled = isOn;
    }

    public void ToggleSnapTurn_OnValueChanged(bool isOn)
    {
        if (isOn)
        {
            XRRig.GetComponent<ActionBasedContinuousTurnProvider>().enabled = false;
            XRRig.GetComponent<ActionBasedSnapTurnProvider>().enabled = true;
        }
        else
        {
            XRRig.GetComponent<ActionBasedSnapTurnProvider>().enabled = false;
            XRRig.GetComponent<ActionBasedContinuousTurnProvider>().enabled = true;
        }
    }
}
