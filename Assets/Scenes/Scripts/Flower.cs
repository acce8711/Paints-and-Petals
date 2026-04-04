using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;



public class Flower : MonoBehaviour
{
    [SerializeField] AudioSource grab_audio_player;
    [SerializeField] AudioClip rip_clip;
    [SerializeField] AudioClip grab_clip;
    public bool grabbed_from_ground;

    private bool can_be_converted_to_pigment;
    private Pigment pigment_reference;
    

    // Start is called before the first frame update
    void Start()
    {
        can_be_converted_to_pigment = false;
        grabbed_from_ground = false;
    }

    public void CanBeConvertedToPigment(bool allow_conversion, Pigment pigment_reference)
    {
        can_be_converted_to_pigment = allow_conversion;
        this.pigment_reference = pigment_reference;
    }

    public void ConvertToPigment()
    {
        if(can_be_converted_to_pigment && pigment_reference != null)
        {
            pigment_reference.IncreasePigmentCount();
            Destroy(gameObject);
        }
    }

    public void OnFlowerGrab(SelectEnterEventArgs args)
    {
        if(!grabbed_from_ground)
        {
            grabbed_from_ground = true;
            grab_audio_player.clip = rip_clip;
            grab_audio_player.Play();
        } else
        {
            grab_audio_player.clip = grab_clip;
            grab_audio_player.Play();
        }

    }
}
