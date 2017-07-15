using System.Net;
using UnityEngine;

/// <summary>
/// Base class for HTTP responses.
/// </summary>
public abstract class HttpResponseBehaviour : MonoBehaviour
{
    /// <summary>
    /// The name of the responder.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Responder action
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public abstract string GetResponse(HttpListenerRequest request);
}
