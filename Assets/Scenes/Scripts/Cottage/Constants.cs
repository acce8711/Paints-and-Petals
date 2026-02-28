using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public enum PIGMENT_COLOUR {
        NONE,
        RED_PIGMENT,
        BLUE_PIGMENT,
        YELLOW_PIGMENT,
        WHITE_PIGMENT,
        BLACK_PIGMENT
    }

    public static readonly Dictionary<PIGMENT_COLOUR, Color> PIGMENT_COLOURS = new Dictionary<PIGMENT_COLOUR, Color>
    {
        { PIGMENT_COLOUR.NONE, new Color(0f, 0f, 0f, 0f) },
        { PIGMENT_COLOUR.RED_PIGMENT, new Color(1f, 0.29f, 0.29f, 1f) },
        { PIGMENT_COLOUR.BLUE_PIGMENT, new Color(0.19f, 0.56f, 0.96f, 1f) },
        { PIGMENT_COLOUR.YELLOW_PIGMENT, new Color(1f, 0.87f, 0.28f, 1f) },
        { PIGMENT_COLOUR.WHITE_PIGMENT, new Color(1f, 1f, 1f, 1f) },
        { PIGMENT_COLOUR.BLACK_PIGMENT, new Color(0f, 0f, 0f, 1f) }
    };

    public const string PIGMENT_DISPENSER_TAG = "PigmentDispenser";
}
