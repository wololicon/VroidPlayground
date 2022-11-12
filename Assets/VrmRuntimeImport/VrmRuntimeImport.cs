using System.IO;
using UnityEngine;
using UniGLTF;
using System;
using System.Collections.Generic;
using System.Linq;
using VRMShaders;
using Object = UnityEngine.Object;
using VRM;
using UnityEngine.UI;

public class VrmRuntimeImport
{
    public static GameObject ImportRuntime(string path)
    {
        // load into scene
        var data = new GlbFileParser(path).Parse();
        // VRM extension を parse します
        var vrm = new VRMData(data);

        using (var context = new VRMImporterContext(vrm))
        {
            var loaded = context.Load();
            loaded.EnableUpdateWhenOffscreen();
            loaded.ShowMeshes();
            data.Dispose();
            return loaded.gameObject;
        }
       
    } 
    
    public static GameObject ImportRuntime(VrmInfo vrminfo)
    {
        // load into scene
        var vrm = new VRMData(vrminfo._content.Data);
        using (var context = new VRMImporterContext(vrm))
        {
            var loaded = context.Load();
            loaded.EnableUpdateWhenOffscreen();
            loaded.ShowMeshes();
            return loaded.gameObject;
        }
    }



    public static VrmInfo[] GetVrmList()
    {
        VrmInfo[] _vrmInfo;
        #if UNITY_EDITOR
        String path = Application.dataPath.Replace("Assets", "") + @"\VrmData";
        #else
        String path = Application.dataPath + @"\VrmData";
        #endif
        var files = Directory.GetFiles(path, "*.vrm");
        _vrmInfo = new VrmInfo[files.Length];

        for (int a=0;a<files.Length;a++)
        {
            // load into scene
            var data = new GlbFileParser(files[a]).Parse();
            // VRM extension を parse します
            var vrm = new VRMData(data);
            using (var context = new VRMImporterContext(vrm))
            {
                _vrmInfo[a] = new VrmInfo();

                VRMMetaObject meta = context.ReadMeta(true);
                
                _vrmInfo[a].name = meta.Title;
                _vrmInfo[a].icon = meta.Thumbnail;
                _vrmInfo[a]._content = context;
                _vrmInfo[a].path = files[a];
                //var loaded = context.Load();
            }
            data.Dispose();
        }
        return _vrmInfo;
    }
}

public class VrmInfo
{
    public string name;
    public Texture2D icon;
    public string path;
    public VRMImporterContext _content;
}
