using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMaker_BasicInfo : MainEditorVisualElementBase
{
    private VisualElement baseContainer;

    private VisualElement iconContainer;
    private VisualElement iconTexture;

    private VisualElement infoContainer;
    private TextField infoChrNameTextField;

    private CurveField testCurveField;
    private AnimationCurve testAnimationCurve;

    private Texture2D pickedTexture;
    private VisualElement characterTexture;
    //public Texture2D CharacterTexture => characterTexture;

    public readonly int IconHeight = 100;
    public readonly int IconWidth = 100;

    public CharacterMaker_BasicInfo()
    {
        SetupBaseContainer();
        SetupIconContainer();
        SetupInfoContainer();
        SetupInfoChrName();

        Add(baseContainer);
    }

    private void SetupBaseContainer()
    {
        baseContainer = new VisualElement();
        baseContainer.style.flexDirection = FlexDirection.Row;
        baseContainer.style.height = IconHeight;
        baseContainer.style.marginTop = 5;
        //baseContainer.style.backgroundColor = Color.red;
    }

    private void SetupIconContainer()
    {
        iconContainer = new VisualElement();

        iconContainer.style.flexDirection = FlexDirection.Row;
        iconContainer.style.backgroundImage = Texture2D.whiteTexture;
        iconContainer.style.height = IconHeight;
        iconContainer.style.width = IconWidth;
        iconContainer.RegisterCallback<ClickEvent>(TexturePickUp);
        characterTexture = iconContainer;

        baseContainer.Add(iconContainer);
    }

    private void SetupInfoContainer()
    {
        infoContainer = new VisualElement();

        //infoContainer.style.backgroundColor = Color.black;
        infoContainer.style.flexDirection = FlexDirection.Column;
        infoContainer.style.flexGrow = 1;
        infoContainer.style.marginLeft = 5;

        baseContainer.Add(infoContainer);
    }

    private void SetupInfoChrName()
    {
        infoChrNameTextField = new TextField("Name");
        infoChrNameTextField.style.height = 24;
        infoChrNameTextField.style.fontSize = 18;
        infoChrNameTextField.style.marginLeft = 0;

        var label = infoChrNameTextField.Children().First(x => x is Label);
        label.style.unityFontStyleAndWeight = FontStyle.Bold;

        //testAnimationCurve = new AnimationCurve();
        //testCurveField = new CurveField("ATK");
        //testCurveField.style.height = 100;
        //testCurveField.style.fontSize = 18;
        //testCurveField.style.marginLeft = 0;
        //testCurveField.value = testAnimationCurve;

        //label = infoChrNameTextField.Children().First(x => x is Label);
        //label.style.unityFontStyleAndWeight = FontStyle.Bold;
        //label.style.height = 24;

        infoContainer.Add(infoChrNameTextField);
        infoContainer.Add(testCurveField);
    }

    private void TexturePickUp(ClickEvent evt)
    {
        EditorGUIUtility.ShowObjectPicker<Texture2D>(pickedTexture, false, "", 0);
    }

    public override void OnGUI()
    {
        if (Event.current != null && Event.current.commandName == EditorConst.ObjectSelectorUpdated)
        {
            // Update the texture with the selected object
            var newTexture = EditorGUIUtility.GetObjectPickerObject() as Texture2D;
            if (newTexture != null && newTexture != pickedTexture)
            {
                pickedTexture = newTexture;
                characterTexture.style.backgroundImage = pickedTexture;
            }

            if (newTexture == null)
            {
                characterTexture.style.backgroundImage = Texture2D.whiteTexture;
            }
        }
    }
}
