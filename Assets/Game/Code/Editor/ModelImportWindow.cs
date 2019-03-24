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

            if (GUILayout.Button("Fix root motion"))
            {
                ForeachSelected(FixRootMotion, "Fixing root motion");
            }
        }

        void ForeachSelected(UnityAction<Object> action, string title = "Process", string info = "Please wait... ")
        {
            int count = Selection.objects.Length;
            var current = 0;
            foreach (var obj in Selection.objects)
            {
                current++;
                EditorUtility.DisplayProgressBar(title, info + current + "/" + count, (float)current / count);
                action(obj);
            }

            EditorUtility.ClearProgressBar();
        }

        void ModifyAnimation(Object obj, UnityAction<ModelImporterClipAnimation> action)
        {
            var path = AssetDatabase.GetAssetPath(obj);
            var importer = AssetImporter.GetAtPath(path) as ModelImporter;
            if (importer == null)
            {
                return;
            }

            var anims = importer.clipAnimations.Length > 0 ? importer.clipAnimations : importer.defaultClipAnimations;
            var anim = anims[0];
            action(anim);
            importer.clipAnimations = anims;
            importer.SaveAndReimport();
        }

        void RenameAnimation(Object obj)
        {
            ModifyAnimation(obj, anim =>anim.name = obj.name);
        }

        void FixRootMotion(Object obj)
        {
            ModifyAnimation(obj, anim =>
                {
                    anim.lockRootRotation = true;
                    anim.lockRootHeightY = true;
                    anim.lockRootPositionXZ = false;
                    anim.keepOriginalOrientation = true;
                    anim.keepOriginalPositionY = true;
                    anim.keepOriginalPositionXZ = true;
                }
            );
        }
    }
}