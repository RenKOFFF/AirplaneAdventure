using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public class KeystorePasswordSetter : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            PlayerSettings.Android.keystorePass = "immortals";

            PlayerSettings.Android.keyaliasName = "kick";
            PlayerSettings.Android.keyaliasPass = "immortals";

            Debug.Log("Keystore password and key alias password have been set automatically.");
        }
    }
}