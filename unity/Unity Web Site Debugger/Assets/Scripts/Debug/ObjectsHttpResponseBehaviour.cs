using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

/// <summary>
/// Returns a list of objects to an HTTP request.
/// </summary>
public class ObjectsHttpResponseBehaviour : HttpResponseBehaviour
{
    /// <summary>
    /// Lock used for multi-threaded goodness.
    /// </summary>
    object jsonLock = new object();

    /// <summary>
    /// The JSON string that contains the list of objects.
    /// </summary>
    string json = "";

    /// <summary>
    /// The JSON string that contains the list of objects. Wrapped around a lock so it's thread-safe.
    /// </summary>
    private string Json
    {
        get
        {
            lock (jsonLock) { return json; }
        }
        set
        {
            lock (jsonLock) { json = value; }
        }
    }

    /// <summary>
    /// Serializable object description.
    /// </summary>
    [Serializable]
    class ObjDesc
    {
        public string name;

        public Vector3 pos;
    }

    /// <summary>
    /// Serializable list of objects.
    /// </summary>
    [Serializable]
    class ObjList
    {
        public List<ObjDesc> objects = new List<ObjDesc>();
    }

    [Tooltip("Game objects to keep track of")]
    public GameObject[] objects;

    /// <summary>
    /// The name of the api entry point.
    /// </summary>
    public override string Name { get { return "objects"; } }

    /// <summary>
    /// Get the HTML response.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override string GetResponse(HttpListenerRequest request)
    {
        return Json;
    }

    /// <summary>
    /// Update this instance!
    /// </summary>
    void Update()
    {
        // Get the list of objects - runs on the Unity main thread
        ObjList list = new ObjList();
        foreach (var o in objects)
        {
            ObjDesc desc = new ObjDesc() { name = o.name, pos = o.transform.position };
            list.objects.Add(desc);
        }

        // Set the JSON string (uses thread-safe property)
        Json = JsonUtility.ToJson(list);
    }
}
