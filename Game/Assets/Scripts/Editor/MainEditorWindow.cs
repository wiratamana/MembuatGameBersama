using System.Collections.Generic;
using Codice.Client.BaseCommands;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class MainEditorWindow : EditorWindow
{
    public enum EditType
    {
        Character,
        Ability,
        Modifier
    }

    private VisualElement root;
    private CharacterMakerEditorWindow characterMakerEditorWindow;

    private Tab tabCharacter;
    private Tab tabAbility;
    private Tab tabModifier;


    [MenuItem("Tools/My Editor Window")]
    public static void ShowExample()
    {
        MainEditorWindow wnd = GetWindow<MainEditorWindow>();
        wnd.titleContent = new GUIContent("MainEditorWindow");
        wnd.minSize = new Vector2(800, 600);
    }

    private void CreateGUI()
    {
        root = rootVisualElement;

        var tabView = new TabView();

        tabCharacter = new Tab("Character");
        tabAbility = new Tab("Ability");
        tabModifier = new Tab("Modifier");

        tabCharacter.style.display = DisplayStyle.Flex;
        tabAbility.style.display = DisplayStyle.None;
        tabModifier.style.display = DisplayStyle.None;

        tabView.Add(tabCharacter);
        tabView.Add(tabAbility);
        tabView.Add(tabModifier);
        tabView.style.flexShrink = 0;

        root.Add(tabView);
        tabView.activeTabChanged += TabView_activeTabChanged;

        characterMakerEditorWindow = new CharacterMakerEditorWindow(this);
        root.Add(characterMakerEditorWindow);
    }

    private void TabView_activeTabChanged(Tab off, Tab on)
    {
        if (off == tabCharacter) 
        {
            root.Remove(characterMakerEditorWindow);
        }

        if (on == tabCharacter)
        {
            root.Add(characterMakerEditorWindow);
        }

        on.style.display = DisplayStyle.Flex;
        off.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        characterMakerEditorWindow?.Update();
    }

    private void OnGUI()
    {
        characterMakerEditorWindow?.OnGUI();
    }
}
