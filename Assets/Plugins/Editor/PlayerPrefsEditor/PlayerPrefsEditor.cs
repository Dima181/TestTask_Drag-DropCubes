using UnityEditor;
using UnityEngine;

namespace Editor.PlayerPrefsEditor
{
    public static class PlayerPrefsEditor
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        private static void ClearPlayerPrefs()
        {
            if (EditorUtility.DisplayDialog("PlayerPrefs?", "Clear PlayerPrefs?",
                 "Yes", "Cancel"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                Debug.Log("All PlayerPrefs removed!");
            }
        }
    }
}
