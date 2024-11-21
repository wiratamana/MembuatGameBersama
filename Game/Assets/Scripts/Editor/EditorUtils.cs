using UnityEngine;
using UnityEngine.UIElements;

public static class EditorUtils
{
    public static AnimationCurve AnimationCurve01
    {
        get
        {
            // Create keyframes
            Keyframe key1 = new Keyframe(0, 0, 1, 1); // First keyframe at (0, 0) with tangent 1
            Keyframe key2 = new Keyframe(1, 1, 1, 1); // Second keyframe at (1, 1) with tangent 1

            // Create an AnimationCurve with the keyframes
            AnimationCurve curve = new AnimationCurve(key1, key2);

            return curve;
        }
    }

    public static TextField DefaultTextField
    {
        get
        {
            var tf = new TextField();
            tf.style.flexGrow = 1;
            tf.style.height = 24;
            tf.style.fontSize = 18;

            return tf;
        }
    }

    public static Label CreateLabel(string text, bool bold = false)
    {
        var label = new Label(text);
        if (bold)
        {
            label.style.unityFontStyleAndWeight = FontStyle.Bold;
        }
        label.style.height = 24;
        label.style.fontSize = 18;
        return label;
    }

    public static Label CreateLabelBold(string text)
    {
        return CreateLabel(text, true);
    }

    public static TextField CreateTextFieldWithLabel(string label)
    {
        var tf = DefaultTextField;
        tf.label = label;
        return tf;
    }
}
