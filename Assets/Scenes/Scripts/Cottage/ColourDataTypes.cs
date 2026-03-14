using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

[Serializable]
 public class ColourInfo
{
    public Color final_colour;
    public string colour_name;
    public bool completed;
    public List<PigmentInfo> pigments_needed;
}

[Serializable]
 public class PigmentInfo
{
    public PIGMENT_COLOUR colour_type;
    public int required_count;

    public PigmentInfo(PIGMENT_COLOUR colour_type, int count)
    {
        this.colour_type = colour_type;
        required_count = count;
    }
}