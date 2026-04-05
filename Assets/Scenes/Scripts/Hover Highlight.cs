
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverHighlight : MonoBehaviour
{
    [SerializeField] private Renderer mesh_renderer;

    public void OnFirstHoverEntered(HoverEnterEventArgs args)
    {
        if (args != null && (args.interactorObject is XRRayInteractor || args.interactorObject is XRDirectInteractor))
        {
            Renderer renderer = args.interactableObject.transform.GetComponentInChildren<Renderer>();
            //renderer.enabled = true;
            foreach (Material material in renderer.materials)
            {
                material.EnableKeyword("_EMISSION");
            }
        }
    }

    public void OnLastHoverExited(HoverExitEventArgs args)
    {
        if (args != null && (!args.interactableObject.isHovered || (args.interactableObject.interactorsHovering.Count == 1 && args.interactableObject.interactorsHovering[0] is XRSocketInteractor)))
        {
            Renderer renderer = args.interactableObject.transform.GetComponentInChildren<Renderer>();
            foreach (Material material in renderer.materials)
            {
                material.DisableKeyword("_EMISSION");

            }
        }
    }

    public void ManualHoverOn(Renderer mesh_renderer = null)
    {
        if(mesh_renderer != null)
        {
            foreach (Material material in mesh_renderer.materials)
            {
                material.EnableKeyword("_EMISSION");
            }
        }
        else
        {
            foreach (Material material in transform.GetComponentInChildren<Renderer>().materials)
            {
                material.EnableKeyword("_EMISSION");
            }
        }
        
    }

    public void ManualHoverOff(Renderer mesh_renderer = null)
    {
        if (mesh_renderer != null)
        {
            foreach (Material material in mesh_renderer.materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
        else
        {
            foreach (Material material in gameObject.GetComponentInChildren<Renderer>().materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
        
    }

}