using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static Constants;

public class Pigment : MonoBehaviour
{
    [SerializeField] private PIGMENT_COLOUR pigment_type;
    [SerializeField] private XRSocketInteractor colour_mixing_bucket_socket;
    [SerializeField] private GameObject pigment_cylinder;

    private int pigment_count;

    //TODO: may need to add cooldown between pigment squeezes

    private void Start()
    {
        pigment_count = 12;
        UpdatePigmentCylinder();
    }

    public void DispensePigment()
    {
        //does the colour mixing socket have a bucket on it
        if (colour_mixing_bucket_socket.hasSelection)
        {
            //check if there is enough pigment
            if(pigment_count <= 0)
            {
                //TODO: add empty sound effect and maybe some sort of visual effect
                return;
            }

            //check if pigment can be added to the bucket
            XRGrabInteractable bucket = (XRGrabInteractable) colour_mixing_bucket_socket.GetOldestInteractableSelected();
            BucketScript bucket_script = bucket.gameObject.GetComponent<BucketScript>();
            if (bucket_script.CanPigmentBeAdded())
            {
                bucket_script.AddPigmentToBucket(pigment_type);
                DecreasePigmentCount();
            }

        }
    }

    //maybe add animation?
    private void DecreasePigmentCount()
    {
        pigment_count--;
        UpdatePigmentCylinder();
    }

    private void UpdatePigmentCylinder()
    {
        //if there are more than 10 pigment drops in the pigment dispenser then it will just show the dispenser as full
        float normalized_count = pigment_count / 10f;
        float pigment_cylinder_height = Mathf.Clamp(normalized_count, 0f, 1f);
        Vector3 new_cylinder_size = pigment_cylinder.transform.localScale;
        new_cylinder_size.y = pigment_cylinder_height;
        pigment_cylinder.transform.localScale = new_cylinder_size;
    }
}
