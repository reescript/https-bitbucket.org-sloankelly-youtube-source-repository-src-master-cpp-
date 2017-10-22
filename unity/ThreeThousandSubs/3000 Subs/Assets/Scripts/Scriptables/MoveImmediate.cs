using System.Collections;
using UnityEngine;

public class MoveImmediate : ScriptableBehaviour
{
    public Transform objectToMove;

    public Transform target;

    void Awake()
    {
        if (objectToMove == null)
        {
            objectToMove = transform;
        }
    }

    public override IEnumerator Run()
    {
        objectToMove.position = target.position;
        yield return null;
    }
}
