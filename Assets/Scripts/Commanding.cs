﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Commanding {

    public List<SpaceUnit> activeUnits;
    public Octree space;
    public Graph spaceGraph;

    public Commanding(World world) {
        activeUnits = new List<SpaceUnit>();
        this.space = world.space;
        this.spaceGraph = world.spaceGraph;
    }
    public Commanding(Octree space, Graph spaceGraph) {
        activeUnits = new List<SpaceUnit>();
        this.space = space;
        this.spaceGraph = spaceGraph;
    }

    public void MoveOrder(Vector3 target) {
        List<Vector3> pathFindingDest = new List<Vector3>();
        foreach (SpaceUnit unit in activeUnits) {
            pathFindingDest.Add(unit.position);
        }
        if (pathFindingDest.Count > 0) {
            List<List<Node>> allWayPoints = spaceGraph.FindPath(spaceGraph.LazyThetaStar, target, pathFindingDest, space);
            for (int i = 0; i < activeUnits.Count; i++) {
                activeUnits[i].MoveOrder(U.InverseList(allWayPoints[i]), Main.defaultWaypointSize * Mathf.Pow(activeUnits.Count, 0.333f)); // * Mathf.Pow(activeUnits.Count, 0.333f)
            }
        }
    }
}
