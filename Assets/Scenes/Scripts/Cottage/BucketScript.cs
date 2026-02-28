using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Constants;

public class BucketScript : MonoBehaviour
{

    public GameObject bucketPrefab;
    public Vector3 bucketTransform = new Vector3(2.148f, 1.0853f, 1.8135f);
    public bool bucketSummoned = false;

    //pigment UI references
    [SerializeField] private GameObject pigment_count_UI_prefab;
    [SerializeField] private Transform pigment_colours_UI_panel;

    private bool is_pigment_dispenser_above_bucket;
    private bool has_binder_and_solvent;

    //dictionary contains a count of each pigment type in the bucket
    private Dictionary<PIGMENT_COLOUR, int> added_pigments;
    private Dictionary<PIGMENT_COLOUR, GameObject> added_pigments_UI;

    // Start is called before the first frame update
    void Start()
    {
        //init the pigments in the bucket
        added_pigments = new Dictionary<PIGMENT_COLOUR, int>();
        added_pigments.Add(PIGMENT_COLOUR.RED_PIGMENT, 0);
        added_pigments.Add(PIGMENT_COLOUR.BLUE_PIGMENT, 0);
        added_pigments.Add(PIGMENT_COLOUR.YELLOW_PIGMENT, 0);
        added_pigments.Add(PIGMENT_COLOUR.BLACK_PIGMENT, 0);
        added_pigments.Add(PIGMENT_COLOUR.WHITE_PIGMENT, 0);

        //init the pigment UI
        added_pigments_UI = new Dictionary<PIGMENT_COLOUR, GameObject>();
        added_pigments_UI.Add(PIGMENT_COLOUR.RED_PIGMENT, null);
        added_pigments_UI.Add(PIGMENT_COLOUR.BLUE_PIGMENT, null);
        added_pigments_UI.Add(PIGMENT_COLOUR.YELLOW_PIGMENT, null);
        added_pigments_UI.Add(PIGMENT_COLOUR.BLACK_PIGMENT, null);
        added_pigments_UI.Add(PIGMENT_COLOUR.WHITE_PIGMENT, null);

        //TEMP TO REMOVE
        has_binder_and_solvent = true;
}

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if the pigment dispenser has entered the bucket trigger then that means its above the bucket
        if (other.CompareTag(PIGMENT_DISPENSER_TAG))
        {
            is_pigment_dispenser_above_bucket = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if the pigment dispenser has exited the bucket trigger then that means its no longer above the bucket
        if (other.CompareTag(PIGMENT_DISPENSER_TAG))
        {
            is_pigment_dispenser_above_bucket = false;
        }
    }

    public void summonBucket()
    {
        GameObject clone;

        if (bucketSummoned == false)
        {
            clone = Instantiate(bucketPrefab, bucketTransform, Quaternion.identity);
            //clone.transform.SetParent(interactParent.transform, true);
            //Instantiate(bucketPrefab, bucketTransform, Quaternion.identity);
            bucketSummoned = true;
        }
    }

    //pigment can only be added if a pigment dispenser is above a bucket and the bucket already has binder and solvent added
    public bool CanPigmentBeAdded()
    {
        return is_pigment_dispenser_above_bucket && has_binder_and_solvent;
    }

    //method adds a pigment to the bucket and updates the pigment count above the bucket
    public void AddPigmentToBucket(PIGMENT_COLOUR pigment_type)
    {
        if (added_pigments.ContainsKey(pigment_type))
        {
            added_pigments[pigment_type] += 1;

            //if this is the first time this pigment colour is being added then create a UI element for its count and add it to the panel
            if(added_pigments_UI[pigment_type] == null)
            {
                added_pigments_UI[pigment_type] = Instantiate(pigment_count_UI_prefab, pigment_colours_UI_panel);
            } 
            
            //increase the colour count displayed in the colour UI count circle
            added_pigments_UI[pigment_type].GetComponent<PigmentCountUI>().IncreaseCount();
        }
    }

}
