using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BucketManager : MonoBehaviour
{
    [SerializeField] private GameObject bucket_prefab;
    [SerializeField] private XRInteractionManager interaction_manager;

    //when a bucket is destroyed, this should be set to null
    private GameObject current_bucket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //creates a new bucket when the player clicks on the bucket stack
    public void NewBucket(ActivateEventArgs args)
    {
        //only create a new bucket if another one doesn't already exist
        if (current_bucket != null)
            return;

        current_bucket = Instantiate(bucket_prefab);
        interaction_manager.SelectEnter((IXRSelectInteractor)args.interactorObject, current_bucket.GetComponent<XRGrabInteractable>());
    }

    //Destroy a bucket if it goes in the trash or gets delivered
    public void DestroyBucket()
    {
        Destroy(current_bucket);
        current_bucket = null;
    }

    //get refernce to the current created bucket
    public GameObject GetCurrentBucket()
    {
        return current_bucket;
    }
}
