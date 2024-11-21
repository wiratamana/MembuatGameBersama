using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MainEditorMainScrollViewItem<T> : VisualElement
{
    private VisualElement labelBg;
    private Label label;

    public MainEditorMainScrollViewItem(string labelString) 
    {
        labelBg = new VisualElement();

        label = new Label(labelString);
        label.style.color = Color.black;
        labelBg.style.height = 18;
        labelBg.style.fontSize = 12;
        label.style.unityTextAlign = TextAnchor.MiddleLeft;
        labelBg.style.backgroundColor = EditorConst.ColorItemNormal;
        label.style.color = EditorConst.ColorItemFont;
        label.style.left = 35;
        label.style.flexGrow = 1; // Makes the child fill the parent's size

        labelBg.Add(label);
        Add(labelBg);
    }

    public void RegisterClickEventCallback(EventCallback<ClickEvent> cb)
    {
        labelBg.RegisterCallback(cb);
    }

    public void RegisterMouseEnterEventCallback(EventCallback<MouseEnterEvent> cb)
    {
        labelBg.RegisterCallback(cb);
    }

    public void RegisterMouseLeaveEventCallback(EventCallback<MouseLeaveEvent> cb)
    {
        labelBg.RegisterCallback(cb);
    }
}
