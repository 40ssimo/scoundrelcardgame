#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CustomEditorInspector
{
    [CustomEditor(typeof(CardInstance))]
    public class CardInstanceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CardInstance cardInstance = (CardInstance)target;

            if (GUILayout.Button("Setup Card Data"))
            {
                cardInstance.SetupCardData();
                EditorUtility.SetDirty(cardInstance);
            }
        }
    }
}

#endif