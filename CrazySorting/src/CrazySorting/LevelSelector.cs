using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazySorting
{
    class LevelSelector : MonoBehaviour
    {
        IList<string> mScenes = new List<string>();

        private void Awake()
        {
            var sceneTextAsset = Resources.Load<TextAsset>("scene_names");
            mScenes = sceneTextAsset.text.Split('\n')
                .Where(s=>!string.IsNullOrEmpty(s) && s != "LevelSelectorScreen")
                .ToList();
        }

        private void OnGUI()
        {
            var yPos = 0f;
            var height = Screen.height / 10f;
            var xPadding = 5;
            var yPadding = 5;
            var guiStyle = new GUIStyle("button");
            guiStyle.fontSize = 30;

            foreach(var scene in mScenes)
            {

                if (GUI.Button(new Rect(new Vector2(xPadding, yPos + yPadding), new Vector2(Screen.width - 2 * xPadding, height)), scene, guiStyle))
                {
                    SceneManager.LoadScene(scene);
                }

                yPos += height + yPadding;
            }
        }
    }

    
}
