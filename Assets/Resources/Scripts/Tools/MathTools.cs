using UnityEngine;
using System.Collections;

public class MathTools {

	public static int GetZnak(float val)
    {
        if (val < 0)
            return -1;
        else
            return 1;
    }

    // unclamped version of Lerp, to allow value to exceed the from-to range
    public static float ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }
}
