// https://github.com/NRatel/NRFramework.UI

using UnityEngine;
using UnityEditor;

namespace NRFramework
{
    public class EditorSetting : ScriptableObject
    {
        public bool enableOpElementHierarchy = true;

        public string uiPrefabRootDir = "Timer/Prefabs/UI";

        // UI类生成根目录（相对于 Application.dataPath）
        // 将在相对路径下创建对应基类。
        // 将在相对路径下创建快捷类。创建后应自行改名（避免覆盖）。
        public string generatedBaseUIRootDir = "Timer/Scripts/UIBase";
        public string generatedTempUIRootDir = "Timer/Scripts/UIMain";

        private const string kAssetPath = "Assets/NRFramework/Editor/EditorSetting.asset";

        private static EditorSetting sm_Instance = null;
        public static EditorSetting Instance
        {
            get
            {
                if (sm_Instance == null)
                {
                    sm_Instance = AssetDatabase.LoadAssetAtPath<EditorSetting>(kAssetPath);
#if UNITY_EDITOR
                    if (sm_Instance == null)
                    {
                        sm_Instance = CreateInstance<EditorSetting>();
                        AssetDatabase.CreateAsset(sm_Instance, kAssetPath);
                    }
#else
                    Debug.Assert(sm_Instance != null);
#endif
                }
                return sm_Instance;
            }
        }

#if UNITY_EDITOR
        [MenuItem("NRFramework/EditorSetting", false, 999)]
        public static void Select()
        {
            Debug.Log("Application.dataPath: " + Application.dataPath);
            Selection.activeObject = Instance;
        }
#endif
    }
}
