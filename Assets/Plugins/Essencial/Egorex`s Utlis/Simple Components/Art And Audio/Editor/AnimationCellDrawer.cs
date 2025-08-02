using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AnimationCell))]
class AnimationCellDrawer : PropertyDrawer
{
    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label
    )
    {
        var spriteProperty = property.FindPropertyRelative("sprite");
        var durationProperty = property.FindPropertyRelative("duration");

        position.width -= 45;
        EditorGUI.PropertyField(position, spriteProperty, label, true);

        position.x += position.width + 40;
        position.width = 40;
        position.x -= position.width - 5;
        position.height = EditorGUI.GetPropertyHeight(durationProperty);
        EditorGUI.PropertyField(position, durationProperty, GUIContent.none);
    }
}