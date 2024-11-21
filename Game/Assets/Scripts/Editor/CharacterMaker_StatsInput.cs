using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMaker_StatsInput : MainEditorVisualElementBase
{
    private VisualElement baseContainer;

    private Label label;
    private VisualElement minMax;
    private TextField valueMin;
    private TextField valueMax;
    private AnimationCurve animationCurve;
    private CurveField curveField;

    private string labelString;
    private bool labelBold;

    public CharacterMaker_StatsInput(string label, bool labelBold = false)
    {
        labelString = label;
        this.labelBold = labelBold;

        SetupBaseContainer();
        SetupValue();

        Add(baseContainer);
    }

    private void SetupBaseContainer()
    {
        baseContainer = new VisualElement();
        baseContainer.style.flexDirection = FlexDirection.Row;
        baseContainer.style.flexGrow = 1;
        baseContainer.style.marginTop = 5;
        baseContainer.style.marginLeft = 25;
    }

    private void SetupValue()
    {
        label = EditorUtils.CreateLabel(labelString, labelBold);
        label.style.width = 250;

        minMax = new VisualElement();
        minMax.style.flexGrow = 1;
        minMax.style.flexDirection = FlexDirection.Row;

        valueMin = EditorUtils.DefaultTextField;
        valueMin.value = 1.ToString();
        valueMax = EditorUtils.DefaultTextField;
        valueMax.value = 100.ToString();

        valueMin.style.flexBasis = new StyleLength(new Length(45, LengthUnit.Percent));
        valueMax.style.flexBasis = new StyleLength(new Length(45, LengthUnit.Percent));

        minMax.Add(valueMin);
        minMax.Add(valueMax);

        curveField = new CurveField();
        curveField.style.width = 50;
        curveField.style.fontSize = 18;
        curveField.style.marginLeft = 0;
        curveField.value = EditorUtils.AnimationCurve01;

        baseContainer.Add(label);
        baseContainer.Add(minMax);
        baseContainer.Add(curveField);
    }
}
