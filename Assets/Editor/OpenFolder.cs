using UnityEditor;
using UnityEngine;

public class OpenFolderExtension : MonoBehaviour
{
    [MenuItem("Mineself/OpenPersistentDataPath")]
    public static void OpenPersistentDataPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
