using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverHighlight : MonoBehaviour
{
    public void OnFirstHoverEntered(HoverEnterEventArgs args)
    {
        if (args != null && (args.interactorObject is XRRayInteractor || args.interactorObject is XRDirectInteractor))
        {
            Outline outline = args.interactableObject.transform.GetComponentInChildren<Outline>();
            outline.enabled = true;
        } 
    }

    public void OnLastHoverExited(HoverExitEventArgs args)
    {
        if (args != null && (!args.interactableObject.isHovered || (args.interactableObject.interactorsHovering.Count == 1 && args.interactableObject.interactorsHovering[0] is XRSocketInteractor)))
        {
            Outline outline = args.interactableObject.transform.GetComponentInChildren<Outline>();
            outline.enabled = false;
        } 
    }

    public void ManualHoverOn(Outline provided_object_outline)
    {
        provided_object_outline.enabled = true;
    }

    public void ManualHoverOff(Outline provided_object_outline)
    {
        provided_object_outline.enabled = false;
    }

    /*    public void OnSelectEntered()
        {
            if (mesh_renderer != null)
            {
                mesh_renderer.material = highlighted_shader;
            }
        }

        public void OnSelectExited(SelectExitEventArgs args)
        {
            if (mesh_renderer != null && !args.interactableObject.isSelected)
            {
                mesh_renderer.material = default_shader;
            }
        }*/

}
