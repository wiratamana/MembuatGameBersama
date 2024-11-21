using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MainEditorMainScrollView<T> : VisualElement
{
    private ScrollView scrollView;
    private VisualElement selectedItem;

    public MainEditorMainScrollView()
    {
        scrollView = new ScrollView();

        // Set the ScrollView's scrolling direction (optional)
        scrollView.verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible;
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        for (int i = 0; i < 120; i++)
        {
            var scrollItem = new MainEditorMainScrollViewItem<T>($"Item {i + 1}");
            scrollItem.RegisterClickEventCallback(OnClick);
            scrollItem.RegisterMouseEnterEventCallback(OnMouseEnter);
            scrollItem.RegisterMouseLeaveEventCallback(OnMouseLeave);
            scrollView.Add(scrollItem);
        }

        Add(scrollView);
    }

    private void OnClick(ClickEvent evt)
    {
        var vi = evt.currentTarget as VisualElement;
        var lbl = vi.Children().First(x => x is Label);
        vi.style.backgroundColor = EditorConst.ColorItemSelected;
        lbl.style.color = EditorConst.ColorItemFont;

        if (selectedItem != null)
        {
            selectedItem.style.backgroundColor = EditorConst.ColorItemNormal;
            selectedItem.style.color = EditorConst.ColorItemFont;
        }

        selectedItem = vi;
    }

    private void OnMouseEnter(MouseEnterEvent evt)
    {
        var vi = evt.currentTarget as VisualElement;
        var lbl = vi.Children().First(x => x is Label);
        if (selectedItem == vi)
        {
            return;
        }

        vi.style.backgroundColor = EditorConst.ColorItemHover;
        lbl.style.color = EditorConst.ColorItemFont;
    }

    private void OnMouseLeave(MouseLeaveEvent evt)
    {
        var vi = evt.currentTarget as VisualElement;
        var lbl = vi.Children().First(x => x is Label);
        if (selectedItem == vi)
        {
            return;
        }
        vi.style.backgroundColor = EditorConst.ColorItemNormal;
        lbl.style.color = EditorConst.ColorItemFont;
    }
}
