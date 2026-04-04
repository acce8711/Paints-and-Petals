using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public List<ColourInfo> colours;

    public int colourCount;

    //current order chalkboard references
    public TextMeshProUGUI order_colour_name;
    public Transform order_container;
    public AudioSource order_notification_audio;
   
    //previous order chalkboard references
    public TextMeshProUGUI previous_order_colour_name;
    public Transform previous_order_container;

    //trash and delivery door references
    public Transform left_trash_door;
    public Transform right_trash_door;
    public AudioSource trash_audio_source;
    public AudioSource trash_can_audio_source;

    public Transform left_delivery_door;
    public Transform right_delivery_door;
    public AudioSource delivery_audio_source;

    public AudioClip open_door_sound;
    public AudioClip close_door_sound;

    public GameObject correctFeedback;
    public GameObject incorrectFeedback;
    public Transform bucketStationTransform;

    public AudioSource board_feedback_audio;
    public AudioClip correct_sound;
    public AudioClip incorrect_sound;

    //Prefabs
    public GameObject order_colour_swatches_prefab;
    public GameObject previous_order_colour_swatches_prefab;
    public GameObject colour_swatch_prefab;
    public GameObject plus_sign_prefab;
    public GameObject equal_sign_and_question_mark_prefab;


    //private variables
    private ColourInfo current_order;
    private ColourInfo previous_order;

    private GameObject current_order_chalkboard_swatches;
    private GameObject previous_order_chalkboard_swatches;

    private int ordersCompleted;

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
        //this is just placeholder.Instead, it should randomize what colour is chosen for the order. It should call SelectNewOrder()
        ordersCompleted = -1;
        colourCount = colours.Count;
        SelectNewOrder();
    }

    //method should be called from the bucketScript.cs and passed in the addeed_pigments dictionary as a paramater
    public void CheckColourMix(Dictionary<PIGMENT_COLOUR, int> mixed_colour)
    {
        //loop through the current_order required pigments and check if there are any mixed_colour pigments that don't match the required pigments for the final colour
        foreach (PigmentInfo pigment_info in current_order.pigments_needed)
        {
            //if the mixed colour does not contain the required pigment then reject the order immediately
            if (!mixed_colour.ContainsKey(pigment_info.colour_type))
            {
                RejectOrder();
                return;
            }

            //if the mixed colour pigment does not have the same count as the required pigment count then reject the order immediately
            if (mixed_colour[pigment_info.colour_type] != pigment_info.required_count)
            {
                RejectOrder();
                return;
            }
        }

        //if the above loop passes that means that there is an exact colour match and we can accept the order
        StartCoroutine(AcceptOrder());
    }

    //TO DO: implement order rejection flow
    //this method is called if the colour mix is wrong
    public void RejectOrder()
    {
        //TO DO: display feedback on chalkboard above
        incorrectFeedback.SetActive(true);
        AnimateTrashHinges();

        //TO DO: add animation of bucket going down
        bucketStationTransform.DOMove(new Vector3(1.9283f, 0.2f, -1.616f), 0.4f);

    }

    //TO DO: implement order acceptance flow
    //this method is called if the colour mix is correct
    public IEnumerator AcceptOrder()
    {
        //TO DO: display feedback on chalkboard above
        correctFeedback.SetActive(true);
        AnimateDeliveryHinges();

        //TO DO: add animation of bucket going through delivery
        bucketStationTransform.DOMove(new Vector3(2.642f, 0.6927f, -1.616f), 1f);

        yield return new WaitForSeconds(4f);

        previous_order = current_order;
        current_order = SelectNewOrder();

        DisplayOrderOnChalkboard(current_order);
        DisplayPreviousOrderOnChalkboard(previous_order);
    }

    //TO DO: handle new order creation that randomized an order from the available colours (ColourInfo) that have the completed varaible set to false (ColourInfo.completed)
    public ColourInfo SelectNewOrder()
    {
        ordersCompleted++;
        int randomNumber = Random.Range(0, colourCount);
        if (colours[randomNumber].completed == true && ordersCompleted < colourCount)
        {
            while (colours[randomNumber].completed == true)
            {
                randomNumber = Random.Range(0, colourCount);
            }
        }
        current_order = colours[randomNumber];
        order_notification_audio.Play();
        DisplayOrderOnChalkboard(current_order);
        colours[randomNumber].completed = true;

        return current_order;
    }

    //method animates the delivery hinges to open and then close after a delay
    public void AnimateDeliveryHinges()
    {
        delivery_audio_source.clip = open_door_sound;
        delivery_audio_source.Play();

        //left door
        Sequence left_delivery_door_animation_sequence = DOTween.Sequence();
        //open door
        left_delivery_door_animation_sequence.Append(left_delivery_door.DORotate(new Vector3(0, DELIVERY_DOOR_ANIMATION_ANGLE_Y, 0), DOOR_ANIMATION_DURATION));

        //wait before closing door
        left_delivery_door_animation_sequence.AppendInterval(DOOR_ANIMATION_HOLD_DURATION)
            .OnComplete(() => { delivery_audio_source.clip = close_door_sound;
                                delivery_audio_source.Play();});

        //close door
        left_delivery_door_animation_sequence.Append(left_delivery_door.DORotate(Vector3.zero, DOOR_ANIMATION_DURATION));


        //right door
        Sequence right_delivery_door_animation_sequence = DOTween.Sequence();
        //open door
        right_delivery_door_animation_sequence.Append(right_delivery_door.DORotate(new Vector3(0, DELIVERY_DOOR_ANIMATION_ANGLE_Y * (-1), 0), DOOR_ANIMATION_DURATION));

        //wait before closing door
        right_delivery_door_animation_sequence.AppendInterval(DOOR_ANIMATION_HOLD_DURATION);

        //close door
        right_delivery_door_animation_sequence.Append(right_delivery_door.DORotate(Vector3.zero, DOOR_ANIMATION_DURATION));

        right_delivery_door_animation_sequence.OnComplete(() => {
            board_feedback_audio.clip = correct_sound;
            board_feedback_audio.Play();
            clearBoard();
        });

        //play animation
        left_delivery_door_animation_sequence.Play();
        right_delivery_door_animation_sequence.Play();

        
    }

    // method clears board of correct/incorrect feedback
    void clearBoard()
    {
        StartCoroutine(ClearFeedbackBoardAfterDelay());
        BucketManager.Instance.DestroyBucket();
        bucketStationTransform.position = new Vector3(1.9283f, 0.6927f, -1.616f);
    }

    IEnumerator ClearFeedbackBoardAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        incorrectFeedback.SetActive(false);
        correctFeedback.SetActive(false);
    }


    //method animates the trash hinges to open and then close after a delay
    public void AnimateTrashHinges()
    {
        trash_audio_source.clip = open_door_sound;
        trash_audio_source.Play();

        //left door
        Sequence left_trash_door_animation_sequence = DOTween.Sequence();
        //open door
        left_trash_door_animation_sequence.Append(left_trash_door.DORotate(new Vector3(TRASH_DOOR_ANIMATION_ANGLE_X * (-1), 0, 0), DOOR_ANIMATION_DURATION)
            .OnComplete(() => trash_can_audio_source.Play()));

        //wait before closing door
        left_trash_door_animation_sequence.AppendInterval(DOOR_ANIMATION_HOLD_DURATION);

        left_trash_door_animation_sequence.AppendCallback(() => 
            {
                trash_audio_source.clip = close_door_sound;
                trash_audio_source.Play();
            });

        //close door
        left_trash_door_animation_sequence.Append(left_trash_door.DORotate(Vector3.zero, DOOR_ANIMATION_DURATION));


        //right door
        Sequence right_trash_door_animation_sequence = DOTween.Sequence();
        //open door
        right_trash_door_animation_sequence.Append(right_trash_door.DORotate(new Vector3(TRASH_DOOR_ANIMATION_ANGLE_X, 0, 0), DOOR_ANIMATION_DURATION));

        //wait before closing door
        right_trash_door_animation_sequence.AppendInterval(DOOR_ANIMATION_HOLD_DURATION);

        //close door
        right_trash_door_animation_sequence.Append(right_trash_door.DORotate(Vector3.zero, DOOR_ANIMATION_DURATION));

        right_trash_door_animation_sequence.OnComplete(() => {
            board_feedback_audio.clip = incorrect_sound;
            board_feedback_audio.Play();
            clearBoard();
        });

        //play animation
        left_trash_door_animation_sequence.Play();
        right_trash_door_animation_sequence.Play();
    }


    //method displays the passed in colour as a order on the chalkboard
    public void DisplayOrderOnChalkboard(ColourInfo colour_to_display)
    {
        if (colour_to_display == null)
            return;

        if(current_order_chalkboard_swatches != null)
        {
            Destroy(current_order_chalkboard_swatches);
        }

        current_order_chalkboard_swatches = Instantiate(order_colour_swatches_prefab, order_container);
        current_order_chalkboard_swatches.transform.localPosition = ORDER_SWATCHES_LOCAL_POSITION;

        order_colour_name.text = colour_to_display.colour_name;

        for (int i = 0; i < colour_to_display.pigments_needed.Count; i++)
        {
            var pigment_info = colour_to_display.pigments_needed[i];
            var created_colour_swatch = Instantiate(colour_swatch_prefab, current_order_chalkboard_swatches.transform);
            created_colour_swatch.transform.GetChild(1).GetComponent<Image>().color = PIGMENT_COLOURS[pigment_info.colour_type];
            created_colour_swatch.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pigment_info.required_count.ToString();

            //if this is not the last swatch then also add a plus sign
            if (i < colour_to_display.pigments_needed.Count - 1)
            {
                Instantiate(plus_sign_prefab, current_order_chalkboard_swatches.transform);
            }
        }

        //add an equal sign and question mark at the end
        Instantiate(equal_sign_and_question_mark_prefab, current_order_chalkboard_swatches.transform);
    }

    //method displays the passed in colour as a PREVIOUS order on the chalkboard
    public void DisplayPreviousOrderOnChalkboard(ColourInfo colour_to_display)
    {
        if (colour_to_display == null)
            return;

        if (previous_order_chalkboard_swatches != null)
        {
            Destroy(previous_order_chalkboard_swatches);
        }

        previous_order_chalkboard_swatches = Instantiate(previous_order_colour_swatches_prefab, previous_order_container);
        previous_order_chalkboard_swatches.transform.localPosition = PREVIOUS_ORDER_SWATCHES_LOCAL_POSITION;

        previous_order_colour_name.text = colour_to_display.colour_name;

        for (int i = 0; i < colour_to_display.pigments_needed.Count; i++)
        {
            var pigment_info = colour_to_display.pigments_needed[i];
            var created_colour_swatch = Instantiate(colour_swatch_prefab, previous_order_chalkboard_swatches.transform);
            created_colour_swatch.transform.GetChild(1).GetComponent<Image>().color = PIGMENT_COLOURS[pigment_info.colour_type];
            created_colour_swatch.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pigment_info.required_count.ToString();
            created_colour_swatch.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = CHALKBOARD_COLOUR;

            //if this is not the last swatch then also add a plus sign
            if (i < colour_to_display.pigments_needed.Count - 1)
            {
                Instantiate(plus_sign_prefab, previous_order_chalkboard_swatches.transform);
            }
        }

        //add an equal sign and final colour mix at the end
        var swatch_ending = Instantiate(equal_sign_and_question_mark_prefab, previous_order_chalkboard_swatches.transform);
        swatch_ending.transform.GetChild(1).gameObject.SetActive(false);
        var final_colour_swatch = Instantiate(colour_swatch_prefab, previous_order_chalkboard_swatches.transform);
        final_colour_swatch.transform.GetChild(1).GetComponent<Image>().color = colour_to_display.final_colour;
        final_colour_swatch.transform.GetChild(0).gameObject.SetActive(false);
    }

}
