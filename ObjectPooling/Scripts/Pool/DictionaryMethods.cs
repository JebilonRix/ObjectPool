using System.Collections.Generic;
using UnityEngine;

namespace RedPanda.ObjectPooling
{
    public static class DictionaryMethods
    {
        public static void ToDictionary(this Dictionary<string, Queue<GameObject>> dictionary, string tag, GameObject obj) => dictionary[tag].Enqueue(obj);
        public static GameObject FromDictionary(this Dictionary<string, Queue<GameObject>> dictionary, string tag) => dictionary[tag].Count <= 0 ? null : dictionary[tag].Dequeue();
    }
}