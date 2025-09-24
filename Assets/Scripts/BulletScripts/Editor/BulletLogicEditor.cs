using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(BulletlogicScript))]
public class BulletlogicScriptEditor : Editor
{
    private SerializedProperty bulletPrefabProp;
    private GameObject[] prefabs;
    private string[] prefabNames;
    private SerializedProperty scriptProp;

    private void OnEnable()
    {
        bulletPrefabProp = serializedObject.FindProperty("bulletPrefab");
        scriptProp = serializedObject.FindProperty("m_Script"); // internal Script reference

        // Load all prefabs in the folder
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Objects/BulletObjects" });
        prefabs = guids.Select(g => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(g))).ToArray();
        prefabNames = prefabs.Select(p => p.name).ToArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the Script field first (faded out)
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(scriptProp);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        // Draw the prefab dropdown
        if (prefabs.Length > 0)
        {
            int currentIndex = System.Array.IndexOf(prefabs, bulletPrefabProp.objectReferenceValue);
            if (currentIndex < 0) currentIndex = 0;

            int newIndex = EditorGUILayout.Popup("Bullet Prefab", currentIndex, prefabNames);
            bulletPrefabProp.objectReferenceValue = prefabs[newIndex];

            // Show real prefab preview
            if (bulletPrefabProp.objectReferenceValue != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Prefab Preview:");

                // Get the asset preview texture
                Texture2D previewTexture = AssetPreview.GetAssetPreview(bulletPrefabProp.objectReferenceValue);

                if (previewTexture != null)
                {
                    // Draw the preview texture with a fixed size
                    GUILayout.Label(previewTexture, GUILayout.Width(128), GUILayout.Height(128));
                }
                else
                {
                    EditorGUILayout.LabelField("Preview not available yet (Unity generates it asynchronously)");
                }
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No prefabs found in Assets/Objects/BulletObjects", MessageType.Warning);
        }

        EditorGUILayout.Space();

        // Draw all other fields except bulletPrefab and script
        DrawPropertiesExcluding(serializedObject, "bulletPrefab", "m_Script");

        serializedObject.ApplyModifiedProperties();
    }
}