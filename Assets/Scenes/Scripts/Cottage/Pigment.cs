using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static Constants;
using TMPro;

public class Pigment : MonoBehaviour
{
    [SerializeField] private PIGMENT_COLOUR pigment_type;
    [SerializeField] private XRSocketInteractor colour_mixing_bucket_socket;
    [SerializeField] private GameObject pigment_cylinder;
    [SerializeField] private string accepted_flower_tag;
    [SerializeField] private TextMeshProUGUI pigment_count_text;
    [SerializeField] private GameObject add_indicator;
    [SerializeField] private Transform player_pos;

    private int pigment_count;

    //TODO: may need to add cooldown between pigment squeezes

    private void Start()
    {
        pigment_count = 12;
        pigment_count_text.text = pigment_count.ToString();
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
        pigment_count_text.text = pigment_count.ToString();
        UpdatePigmentCylinder();
    }

    public void IncreasePigmentCount()
    {
        pigment_count++;
        pigment_count_text.text = pigment_count.ToString();
        UpdatePigmentCylinder();
        gameObject.GetComponent<HoverHighlight>().OnLastHoverExited(null);
        StartCoroutine(DisplayAddFeedback());
    }

    //displays +1 when a flower has been converted into a pigment and then hides it
    IEnumerator DisplayAddFeedback()
    {
        add_indicator.SetActive(true);
        //add_indicator.transform.LookAt(new Vector3(add_indicator.transform.position.x, player_pos.position.y, add_indicator.transform.position.z));

        Vector3 direction = player_pos.position - add_indicator.transform.position;
        direction.y = 0f;
        add_indicator.transform.rotation = Quaternion.LookRotation(direction);

        yield return new WaitForSeconds(ADD_ANIMATION_LENGTH);
        add_indicator.SetActive(false);
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

    //if a flower is held near pigment then a visual effect is shown to indicate that the flower can be converted into this pigment
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(accepted_flower_tag))
        {
            gameObject.GetComponent<HoverHighlight>().OnFirstHoverEntered(null);
            other.transform.root.GetComponent<Flower>().CanBeConvertedToPigment(true, this);
        }
    }

    //if a held flower is removed from withtin the pigments radius then 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(accepted_flower_tag))
        {
            gameObject.GetComponent<HoverHighlight>().OnLastHoverExited(null);
            other.transform.root.GetComponent<Flower>().CanBeConvertedToPigment(false, null);
        }
    }
}
