using System.IO;
using System.Net;
using UnityEngine;

/// <summary>
/// File response HTML.
/// </summary>
[FileResponder]
public class FileHttpResponseBehaviour : HttpResponseBehaviour
{
    // Application.dataPath
    string dataPath;
    
    /// <summary>
    /// The name of the response behaviour
    /// </summary>
    public override string Name { get { return "htdocs"; } }

    /// <summary>
    /// Get the response.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override string GetResponse(HttpListenerRequest request)
    {
        string path = Path.Combine(dataPath, "htdocs");

        // /mygame/index.html

        int pos = request.Url.AbsolutePath.IndexOf('/', 1) + 1;
        path = Path.Combine(path, request.Url.AbsolutePath.Substring(pos));

        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }

        return "<h3>404 - File not found</h3>";
    }

    void Awake()
    {
        // Cache the application's datapath because it's not allowed to be accessed cross-thread
        dataPath = Application.dataPath;
    }
}
