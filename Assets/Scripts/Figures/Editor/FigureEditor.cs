
using UnityEditor;

[CustomEditor(typeof(Figure))]
public class FigureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var figure = (Figure)serializedObject.targetObject;

        EditorGUILayout.LabelField("Width", figure.Width.ToString());
        EditorGUILayout.LabelField("Height", figure.Height.ToString());

        using (var vert = new EditorGUILayout.VerticalScope()) {
            for (int y = 0; y < Figure.MaxHeight; y++) {
                using (var horz = new EditorGUILayout.HorizontalScope()) {
                    EditorGUILayout.PrefixLabel(y.ToString());
                    for (int x = 0; x < Figure.MaxWidth; x++) {
                        bool oldValue = figure.At(x, y);
                        bool newValue = EditorGUILayout.Toggle(oldValue);
                        if (newValue != oldValue) {
                            Undo.RecordObject(figure, "Edit figure");
                            figure.EditorSetAt(x, y, newValue);
                            EditorUtility.SetDirty(figure);
                        }
                    }
                }
            }
        }
    }
}
