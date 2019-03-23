using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Game.Editor
{
    class ModelImportWindow : EditorWindow
    {
        [MenuItem("Window/Model Import")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ModelImportWindow));
        }

        void OnGUI()
        {
            if (GUILayout.Button("Rename animations"))
            {
                ForeachSelected(RenameAnimation, "Renaming animations");
            }
        }

        void ForeachSelected(UnityAction<Object> action, string title = "Process", string info = "Please wait...")
        {
            var count = Selection.objects.Length;
            var current = 0f;
            foreach (var obj in Selection.objects)
            {
                current++;
                EditorUtility.DisplayProgressBar(title, info, current / count);
                action(obj);
            }

            EditorUtility.ClearProgressBar();
        }

        void RenameAnimation(Object obj)
        {
            var path = AssetDatabase.GetAssetPath(obj);
            var importer = AssetImporter.GetAtPath(path) as ModelImporter;
            if (importer == null)
            {
                return;
            }

            var anims = importer.clipAnimations.Length > 0 ? importer.clipAnimations : importer.defaultClipAnimations;
            var anim = anims[0];
            anim.name = obj.name;
            anim.lockRootRotation = true;
            anim.lockRootHeightY = true;
            anim.lockRootPositionXZ = true;
            importer.clipAnimations = anims;
            importer.SaveAndReimport();
        }
    }
}