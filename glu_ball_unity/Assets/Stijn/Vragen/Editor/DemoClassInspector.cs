using UnityEditor;

namespace GLUBall
{
    [CustomEditor(typeof(DemoClass))]
    public class DemoClassInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            //Cache all the data
            serializedObject.Update();

            //Draw default inspector
            base.OnInspectorGUI();

            //Custom inspector
            SerializedProperty showGameObjectListProperty = serializedObject.FindProperty("m_ShowGameObjectList");
            if (showGameObjectListProperty.boolValue == true)
            {
                SerializedProperty gameObjectListProperty = serializedObject.FindProperty("m_GameObjectList");
                EditorGUILayout.PropertyField(gameObjectListProperty);
            }

            //Alternative Custom Inspector (needs publicly available methods!)
            //DemoClass demoClass = (DemoClass)target;
            //if (demoClass.ShowgameObjectList == true) { }

            //Apply changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}