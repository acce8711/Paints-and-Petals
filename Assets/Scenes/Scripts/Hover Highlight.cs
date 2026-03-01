using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverHighlight : MonoBehaviour
{
    [SerializeField] private Shader default_shader;
    [SerializeField] private Shader highlighted_shader;
    [SerializeField] private Renderer mesh_renderer;

    public void OnFirstHoverEntered(HoverEnterEventArgs args)
    {
        if (args != null)
        {
            Renderer renderer = args.interactableObject.transform.GetComponentInChildren<Renderer>();
            foreach (Material material in renderer.materials)
            {
                material.shader = highlighted_shader;
            }
        } else if (mesh_renderer != null)
        {
            foreach (Material material in mesh_renderer.materials)
            {
                material.shader = highlighted_shader;
            }
        }
    }

    public void OnLastHoverExited(HoverExitEventArgs args)
    {
        if (args != null && !args.interactableObject.isHovered)
        {
            Renderer renderer = args.interactableObject.transform.GetComponentInChildren<Renderer>();
            foreach (Material material in renderer.materials)
            {
                material.shader = default_shader;
            }
        } else if (mesh_renderer != null)
        {
            foreach (Material material in mesh_renderer.materials)
            {
                material.shader = default_shader;
            }
        }
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
