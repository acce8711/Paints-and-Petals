using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public TextMeshProUGUI order_colour_name;
    public Transform current_order_swatches;
    public List<ColourInfo> colours;

    public TextMeshProUGUI previous_order_colour_name;
    public Transform previous_order_colour_swatches;

    //Prefabs
    public GameObject colour_swatch;
    public GameObject plus_sign;
    public GameObject equal_sign_and_question_mark;

    private ColourInfo current_order;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        DisplayOrder(colours[0]);
        DisplayPreviousOrder(colours[0]);
    }

    //TO DO: handle new order creation that randomized an order from the available colours that have not been complete yet
    //For paint checking you can compare the count and type of pigment in current_order.pigments_needed TO THE added_pigments in the BucketScript.cs. If their pigments and pigment counts match then the order is correct

    //method displays the passed in colour as a order on the chalkboard
    public void DisplayOrder(ColourInfo colour_to_display)
    {
        order_colour_name.text = colour_to_display.colour_name;

        for (int i = 0; i < colour_to_display.pigments_needed.Count; i++)
        {
            var pigment_info = colour_to_display.pigments_needed[i];
            var created_colour_swatch = Instantiate(colour_swatch, current_order_swatches);
            created_colour_swatch.transform.GetChild(1).GetComponent<Image>().color = PIGMENT_COLOURS[pigment_info.colour_type];
            created_colour_swatch.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pigment_info.required_count.ToString();

            //if this is not the last swatch then also add a plus sign
            if (i < colour_to_display.pigments_needed.Count - 1)
            {
                Instantiate(plus_sign, current_order_swatches);
            }
        }

        //add an equal sign and question mark at the end
        Instantiate(equal_sign_and_question_mark, current_order_swatches);
    }

    //method displays the passed in colour as a PREVIOUS order on the chalkboard
    public void DisplayPreviousOrder(ColourInfo colour_to_display)
    {
        previous_order_colour_name.text = colour_to_display.colour_name;

        for (int i = 0; i < colour_to_display.pigments_needed.Count; i++)
        {
            var pigment_info = colour_to_display.pigments_needed[i];
            var created_colour_swatch = Instantiate(colour_swatch, previous_order_colour_swatches);
            created_colour_swatch.transform.GetChild(1).GetComponent<Image>().color = PIGMENT_COLOURS[pigment_info.colour_type];
            created_colour_swatch.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pigment_info.required_count.ToString();
            created_colour_swatch.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = CHALKBOARD_COLOUR;

            //if this is not the last swatch then also add a plus sign
            if (i < colour_to_display.pigments_needed.Count - 1)
            {
                Instantiate(plus_sign, current_order_swatches);
            }
        }

        //add an equal sign and final colour mix at the end
        var swatch_ending = Instantiate(equal_sign_and_question_mark, previous_order_colour_swatches);
        swatch_ending.transform.GetChild(1).gameObject.SetActive(false);
        var final_colour_swatch = Instantiate(colour_swatch, previous_order_colour_swatches);
        final_colour_swatch.transform.GetChild(1).GetComponent<Image>().color = colour_to_display.final_colour;
        final_colour_swatch.transform.GetChild(0).gameObject.SetActive(false);
    }

}
