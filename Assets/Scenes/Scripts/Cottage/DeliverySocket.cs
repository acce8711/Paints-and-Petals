using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DeliverySocket : MonoBehaviour
{
    public void CheckBucketColourMix(SelectEnterEventArgs args)
    {
        //retrive bucket and it's currently mixed colour and call the method in the Order manager that checks the colour mix
        BucketScript bucket = args.interactableObject.transform.gameObject.GetComponent<BucketScript>();
        if(bucket != null)
        {
            OrderManager.Instance.CheckColourMix(bucket.GetColourMix());
        }
    }
}
