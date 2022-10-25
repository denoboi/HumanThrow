using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class JunctionHandler : MonoBehaviour
{
    SplineTracer tracer;

    private void Awake()
    {
        tracer = GetComponent<SplineTracer>();
    }

    private void OnEnable()
    {
        tracer.onNode += OnNode; //onNode is called every time the tracer passes by a Node
    }

    private void OnDisable()
    {
        tracer.onNode -= OnNode;
    }

    private void OnNode(List<SplineTracer.NodeConnection> passed)
    {
        Debug.Log("Reached node " + passed[0].node.name + " connected at point " +
                  passed[0].point);

        Node.Connection[] connections = passed[0].node.GetConnections();

        if (connections.Length == 1) return;

        int currentConnection = 0;
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].spline == tracer.spline && connections[i].pointIndex ==
                passed[0].point)
            {
                currentConnection = i;
                break;
            }
        }


        int newConnection = Random.Range(0, connections.Length);

        if (newConnection == currentConnection)
        {
            newConnection++;
            if (newConnection >= connections.Length) newConnection = 0;
        }


        SwitchSpline(connections[currentConnection], connections[newConnection]);
    }

    void SwitchSpline(Node.Connection from, Node.Connection to)
    {
        float excessDistance =
            tracer.spline.CalculateLength(tracer.spline.GetPointPercent(from.pointIndex),
                tracer.UnclipPercent(tracer.result.percent));

        tracer.spline = to.spline;
        tracer.RebuildImmediate();

        double startpercent = tracer.ClipPercent(to.spline.GetPointPercent(to.pointIndex));
        if (Vector3.Dot(from.spline.Evaluate(from.pointIndex).forward,
                to.spline.Evaluate(to.pointIndex).forward) < 0f)
        {
            if (tracer.direction == Spline.Direction.Forward)
                tracer.direction =
                    Spline.Direction.Backward;
            else tracer.direction = Spline.Direction.Forward;
        }


        tracer.SetPercent(tracer.Travel(startpercent, excessDistance, tracer.direction));
    }
}