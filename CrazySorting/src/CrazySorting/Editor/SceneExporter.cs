using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

class SceneExporter : Editor
{
    [MenuItem("CreamTeam/ExportLevelNames")]
    public static void ExportLevelNames()
    {
        var scenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes.Where(s => s.enabled))
        {
            scenes.Add(Path.GetFileNameWithoutExtension(scene.path));
        }

        var fileName = Application.dataPath + "/Resources/scene_names.txt";
        if (!File.Exists(fileName))
        {
            var file = File.Create(fileName);
            file.Close();
        }
        File.WriteAllLines(fileName, scenes.ToArray());
    }
}