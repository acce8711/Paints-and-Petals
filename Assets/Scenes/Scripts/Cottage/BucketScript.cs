using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Constants;
using Scrtwpns.Mixbox;

public class BucketScript : MonoBehaviour
{
    //this gameobject can be activated have its y scaled gradually to simulate solvent+binder pouring
    [SerializeField] private GameObject liquid;

    //this changes the liquid colour based on the added pigment
    [SerializeField] private MeshRenderer liquid_colour;

    //pigment UI references
    [SerializeField] private GameObject pigment_count_UI_prefab;
    [SerializeField] private Transform pigment_colours_UI_panel;
    

    private bool is_pigment_dispenser_above_bucket;
    private bool has_binder_and_solvent;

    //dictionary contains a count of each pigment type in the bucket
    private Dictionary<PIGMENT_COLOUR, int> added_pigments;
    private Dictionary<PIGMENT_COLOUR, GameObject> added_pigments_UI;

    private int num_of_added_pigments;

    // Start is called before the first frame update
    void Start()
    {
        num_of_added_pigments = 0;
            
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

        /*MixboxLatent z1 = Mixbox.RGBToLatent(PIGMENT_COLOURS[PIGMENT_COLOUR.WHITE_PIGMENT]);
        MixboxLatent z2 = Mixbox.RGBToLatent(PIGMENT_COLOURS[PIGMENT_COLOUR.BLUE_PIGMENT]);
        MixboxLatent z3 = Mixbox.RGBToLatent(PIGMENT_COLOURS[PIGMENT_COLOUR.YELLOW_PIGMENT]);
        
              MixboxLatent zMix = (0.25f*z1 + // 30% of color1
                                   0.5f*z2 + // 60% of color2
                                   0.25f*z3); // 10% of color3
       
        Color colorMix = Mixbox.LatentToRGB(zMix);
        liquid_colour.material.color = colorMix;*/
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
            num_of_added_pigments++;

            //if this is the first time this pigment colour is being added then create a UI element for its count and add it to the panel
            if (added_pigments_UI[pigment_type] == null)
            {
                added_pigments_UI[pigment_type] = Instantiate(pigment_count_UI_prefab, pigment_colours_UI_panel);
                added_pigments_UI[pigment_type].GetComponent<PigmentCountUI>().SetColour(PIGMENT_COLOURS[pigment_type]);
            } 
            
            //increase the colour count displayed in the colour UI count circle
            added_pigments_UI[pigment_type].GetComponent<PigmentCountUI>().IncreaseCount();

            //cycle through all pigments that have been added and mix them together using Kubelka–Munk model
            MixboxLatent mixed_colour = new MixboxLatent();
            foreach (var key in added_pigments.Keys)
            {
                if (added_pigments[key] != 0)
                {
                    //multiply the colour by its ratio and add to the total mized colour
                    float cool = ((float)added_pigments[key] / (float)num_of_added_pigments);
                    mixed_colour += Mixbox.RGBToLatent(PIGMENT_COLOURS[key]) * ((float)added_pigments[key] / (float)num_of_added_pigments);
                }
            }

            liquid_colour.material.color = Mixbox.LatentToRGB(mixed_colour);

            //TO DELETE

            /*if (pigment_type == PIGMENT_COLOUR.WHITE_PIGMENT)
                LightenColour();
            else {
                UpdateLiquidColour(PIGMENT_COLOURS[pigment_type]);
            }*/

        }
    }


    //TO DELETE

    //CMYK
/*    private void UpdateLiquidColour(Color added_pigment)
    {
        Color current_colour = liquid_colour.material.color;


        Color cmy1 = new Color(1f - added_pigment.r, 1f - added_pigment.g, 1f - added_pigment.b);
        Color cmy2 = new Color(1f - current_colour.r, 1f - current_colour.g, 1f - current_colour.b);

        Color resultCmy = new Color(Mathf.Min(cmy1.r + cmy2.r, 1f),
                                    Mathf.Min(cmy1.g + cmy2.g, 1f),
                                    Mathf.Min(cmy1.b + cmy2.b, 1f));

        liquid_colour.material.color = new Color(1f - resultCmy.r, 1f - resultCmy.g, 1f - resultCmy.b);
    }

    private void LightenColour()
    {
        Color current_colour = liquid_colour.material.color;
        liquid_colour.material.color = Color.Lerp(current_colour, Color.white, 0.5f);
    }*/
}
