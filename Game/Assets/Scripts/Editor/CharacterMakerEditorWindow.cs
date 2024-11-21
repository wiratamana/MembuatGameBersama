using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;

public class CharacterMakerEditorWindow : MainEditorMainLayoutBase
{
    public VisualElement selectedItem { get; private set; }

    internal MainEditorWindow editor;
    private MainEditorMainScrollView<object> scrollView;

    private CharacterMaker_BasicInfo basicInfo;
    private CharacterMaker_StatsInfo statsInfo;

    public CharacterMakerEditorWindow(MainEditorWindow editor)
    {
        this.editor = editor;
        CreateLayout();
    }

    protected override void CreateLayout()
    {
        base.CreateLayout();

        basicInfo = new CharacterMaker_BasicInfo();
        basicInfo.style.flexShrink = 0;
        statsInfo = new CharacterMaker_StatsInfo();

        rightContainer.Add(basicInfo);
        rightContainer.Add(statsInfo);

        // Create a ScrollView
        scrollView = new MainEditorMainScrollView<object>();

        botContainer.Add(scrollView);
    }  

    public override void Update()
    {
        basicInfo.Update();
    }

    public override void OnGUI()
    {
        basicInfo.OnGUI();
    }
}
