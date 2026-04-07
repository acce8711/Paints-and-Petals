using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportPadSelectDisable : MonoBehaviour
{
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        args.manager.SelectExit(args.interactorObject, args.interactableObject);
    }
}
