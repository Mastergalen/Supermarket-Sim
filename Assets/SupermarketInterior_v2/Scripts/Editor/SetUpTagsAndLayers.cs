using UnityEngine;
using UnityEditor;

namespace UCL.COMPGV07
{

    /// <summary>
    /// This class configures anything that must be set project wide to support the prefabs in .unitypackage, including tags and layers
    /// </summary>
    [InitializeOnLoad]
    public class SetUpTagsAndLayers
    {
        // See:
        // https://docs.unity3d.com/Manual/RunningEditorCodeOnLaunch.html
        // http://answers.unity3d.com/questions/33597/is-it-possible-to-create-a-tag-programmatically.html

        static SetUpTagsAndLayers()
        {
            // Open tag manager
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

            // For Unity 5 we need this too
            SerializedProperty layersProp = tagManager.FindProperty("layers");

            // Setting a Layer (Let's set Layer 9)
            string layerName = "Grabbable";

            // --- Unity 5 ---
            SerializedProperty sp = layersProp.GetArrayElementAtIndex(9);
            if (sp != null)
            {
                if(sp.stringValue != layerName)
                {
                    sp.stringValue = layerName;
                    // and to save the changes
                    tagManager.ApplyModifiedProperties();
                }
            }
        }
    }
}