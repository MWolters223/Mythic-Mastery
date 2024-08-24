using UnityEngine;
using UnityEditor;

public class CopyComponents : MonoBehaviour
{
    [MenuItem("Tools/Copy Components From Selected")]
    static void CopyComponentsFromSelected()
    {
        if (Selection.gameObjects.Length != 2)
        {
            Debug.LogError("Please select exactly two GameObjects.");
            return;
        }

        GameObject source = Selection.gameObjects[0];
        GameObject target = Selection.gameObjects[1];

        foreach (Component component in source.GetComponents<Component>())
        {
            UnityEditorInternal.ComponentUtility.CopyComponent(component);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(target);
        }

        Debug.Log("Components copied from " + source.name + " to " + target.name);
    }
}