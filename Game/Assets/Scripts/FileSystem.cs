using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileSystem : MonoBehaviour {
        
    public static T[] FromJson<T>(string file)
    {
        string Path = Application.dataPath + file;
        string fileData = File.ReadAllText(Path);

        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(fileData);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }


    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
