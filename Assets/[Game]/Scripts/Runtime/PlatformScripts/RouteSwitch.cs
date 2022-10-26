using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using Random = UnityEngine.Random;


public class RouteSwitch : MonoBehaviour
{
    private SplineFollower _follower;

    private void Start()
    {
        _follower = GetComponent<SplineFollower>();
        _follower.onNode += OnNodePassed;

       
    }
    
    private void OnNodePassed(List<SplineTracer.NodeConnection> passed)
    {
        SplineTracer.NodeConnection nodeConnection = passed[0];
        Debug.Log(nodeConnection.node.name + "at point " + nodeConnection.point);

        double nodePercent = (double)nodeConnection.point / (_follower.spline.pointCount - 1);
        double followerPercent = _follower.result.percent;
        float distancePastNode = _follower.spline.CalculateLength(nodePercent, followerPercent);
        Debug.Log(nodePercent);

        Node.Connection[] connections = nodeConnection.node.GetConnections();
        
        int newConnection = Random.Range(0, connections.Length);
        int currentConnection = 0;
        if (newConnection == currentConnection)
        {
            newConnection++;
            if (newConnection >= connections.Length) newConnection = 0;
        }


        _follower.spline = connections[newConnection].spline;
        double newNodePercent = (double)connections[newConnection].pointIndex / (connections[newConnection].spline.pointCount);
        double newPercent = connections[newConnection].spline.Travel(newNodePercent, distancePastNode, _follower.direction);
        _follower.SetPercent(newPercent);
    }
}
