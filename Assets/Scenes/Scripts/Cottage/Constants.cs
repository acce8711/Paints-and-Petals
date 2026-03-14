using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public enum FIELD
    {
        NONE,
        FIELD_ONE,
        FIELD_TWO,
        FIELD_THREE
    }

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
        { PIGMENT_COLOUR.RED_PIGMENT, new Color(1f,  0.153f, 0.008f, 1f) },
        { PIGMENT_COLOUR.BLUE_PIGMENT, new Color(0f, 0.129f, 0.522f, 1f) },
        { PIGMENT_COLOUR.YELLOW_PIGMENT, new Color(0.988f, 0.827f, 0f, 1f) },
        { PIGMENT_COLOUR.WHITE_PIGMENT, new Color(1f, 1f, 1f, 1f) },
        { PIGMENT_COLOUR.BLACK_PIGMENT, new Color(0f, 0f, 0f, 1f) }
    };

    public const string PIGMENT_DISPENSER_TAG = "PigmentDispenser";

    public const float ADD_ANIMATION_LENGTH = 0.5f;

    public static Color CHALKBOARD_COLOUR = new Color(0.6913365f, 0.8207547f, 0.6465379f);

    public static Vector3 ORDER_SWATCHES_LOCAL_POSITION = new Vector3(0f, -0.295f, 0f);

    public static Vector3 PREVIOUS_ORDER_SWATCHES_LOCAL_POSITION = new Vector3(-0.824f, 0.427f, 0f);

    public const float TRASH_DOOR_ANIMATION_ANGLE_X = 85f;

    public const float DELIVERY_DOOR_ANIMATION_ANGLE_Y = 140f;

    public const float DOOR_ANIMATION_DURATION = 0.5f;
    public const float DOOR_ANIMATION_HOLD_DURATION = 0.8f;
}
