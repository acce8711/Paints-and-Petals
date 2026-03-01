using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{

    private bool can_be_converted_to_pigment;
    private Pigment pigment_reference;

    // Start is called before the first frame update
    void Start()
    {
        can_be_converted_to_pigment = false;
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
}
