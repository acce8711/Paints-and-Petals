using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PigmentCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI colour_count_text;

    private int colour_count = 0;

    public void IncreaseCount()
    {
        colour_count++;
        colour_count_text.text = colour_count.ToString();
    }

    public void SetColour(Color colour_to_set)
    {
        gameObject.GetComponent<Image>().color = colour_to_set;
    }
}
