using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DebugWebServer : MonoBehaviour
{
    string entryPointUrl;
    HttpResponseBehaviour fileResponder = null;
    Dictionary<string, HttpResponseBehaviour> responders = new Dictionary<string, HttpResponseBehaviour>();

    [Tooltip("Port number the web server is going to run on")]
    public int portNumber = 8080;

    [Tooltip("Web server prefix")]
    public string prefix = "mygame";

    void Awake()
    {
        HttpResponseBehaviour[] behaviours = GetComponents<HttpResponseBehaviour>();
        foreach(var behaviour in behaviours)
        {
            responders[behaviour.Name] = behaviour;

            object[] result = behaviour.GetType().GetCustomAttributes(typeof(FileResponderAttribute), false);
            if (result != null && result.Length == 1)
            {
                fileResponder = behaviour;
            }
        }
    }

    void Start ()
    {
        entryPointUrl = string.Format("http://localhost:{0}/{1}/", portNumber, prefix);
        WebServer ws = new WebServer(SendResponse, entryPointUrl);
        ws.Run();
    }
	
    public string SendResponse(HttpListenerRequest request)
    {
        // http://localhost:8080/mygame/htdocs
        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXhtdocs
        string entryPoint = request.Url.AbsoluteUri.Substring(entryPointUrl.Length);

        HttpResponseBehaviour responder = null;
        if (responders.TryGetValue(entryPoint, out responder))
        {
            return responder.GetResponse(request);
        }
        else if (entryPoint.EndsWith(".html") || entryPoint.EndsWith(".htm"))
        {
            if (fileResponder != null)
            {
                return fileResponder.GetResponse(request);
            }
        }

        return string.Format("<HTML><BODY>Invalid entry point. Unknown API call {0}<br>{1}</BODY></HTML>", entryPoint, DateTime.Now);
    }
}
