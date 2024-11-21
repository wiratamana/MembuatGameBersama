using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class MainEditorMainLayoutBase : MainEditorVisualElementBase
{
    protected VisualElement mainContainer;
    protected VisualElement leftContainer;
    protected VisualElement rightContainer;

    protected VisualElement topContainer;
    protected VisualElement botContainer;

    protected virtual void CreateLayout()
    {
        // Left Container
        leftContainer = new VisualElement();
        leftContainer.style.flexGrow = 0;
        leftContainer.style.width = 250;

        // Right Container
        rightContainer = new VisualElement();
        rightContainer.style.flexGrow = 1;
        rightContainer.style.flexDirection = FlexDirection.Column;
        rightContainer.style.paddingLeft = 5;
        rightContainer.style.paddingRight = 5;

        mainContainer = new VisualElement();
        mainContainer.style.flexDirection = FlexDirection.Row;
        mainContainer.style.flexGrow = 1;

        // Add left and right containers to the main container
        mainContainer.Add(leftContainer);
        mainContainer.Add(rightContainer);

        var space = new ToolbarSpacer();
        space.style.height = 10;
        leftContainer.Add(space);

        var button = new Button();
        button.text = "Add new character";

        topContainer = new VisualElement();
        topContainer.style.flexGrow = 0;
        topContainer.style.height = 30;
        topContainer.Add(button);

        botContainer = new VisualElement();
        botContainer.style.flexGrow = 2;
        botContainer.style.flexDirection = FlexDirection.Column;

        leftContainer.Add(topContainer);
        leftContainer.Add(botContainer);

        var space2 = new ToolbarSpacer();
        space2.style.height = 10;

        Add(mainContainer);
    }
}
