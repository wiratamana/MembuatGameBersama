using UnityEngine;
using UnityEngine.UIElements;

public abstract class MainEditorVisualElementBase : VisualElement
{
    public virtual void OnGUI() { }
    public virtual void Update() { }
}
