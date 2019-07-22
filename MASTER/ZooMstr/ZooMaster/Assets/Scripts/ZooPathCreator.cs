/*
Class:          ZooPathCreater
Copyright:      Philipp Wolf - Zoorino 2019
Date:           2019-07-18
Version:        1.0
Description:    Class scans unityscene for objects from below mentioned assets, to calculate and mark the shortest path
                between a start Node and and endnode. 
                Bezierpaths are the curves (splines) which will be later on marked by color to show the was
                Nodes are for shortest way calculation and need to be placed along the bezier curves. 
                Linear Connections need one Spline and two nodes. Curves depending on their angles more to come as close as possible
                to the length of the spline.

Use:            1. Add Curve to the scene, place nodes at the start Node, beginning and along the path if it is not linear.
                2. Instanciation:   ZooPathCreator zpc = new ZooPathCreator();
                                    zpc.createPathFromCurrentPositionToFirstNode(rigi.transform.position);
                                    zpc.scanScene();
                                    zpc.getShortestPath("B");
                                    zpc.markShortestPathInScene();

Necessities:    - If there are more nodes then single symbols ("A",...), name them by two characters, each different e.g. "AA", "AB",..
                - All nodes in the scene must have the tag "Node" and need to be named (as above mentioned)
                - Pathnames (setted in scene) are combinations from node-names. Path between "AA" and "AB" needs to be named either "AAAB" or "ABAA" 
                - All paths in the scene must have the tag "Path"
                - The graph containing all nodes in the scene must have the name "NodeGraph"
                - all nodes which are not start Node and end Node points of a path need a name with the first letter "0" e.g. "0xdf"

Dependencies:   - Two assets from the unity asset store are beeing used. Both necessary to add.
                - Dijkstra Node -   https://github.com/EmpireWorld/unity-dijkstras-pathfinding
                - Bezier Curves -   https://assetstore.unity.com/packages/tools/level-design/bezier-solution-113074
                                    https://github.com/yasirkula/UnityBezierSolution
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;

public class ZooPathCreator
{
    private GameObject[] pathFragments; // container for all path fragments, to be colored later on
    private GameObject[] nodes; // container for ALL nodes in scene
    private Graph ingameGraph = new GameObject().AddComponent<Graph>(); // graph holds all nodes together in scene
    private Path resultPath; // will contain shortest path nodes in correct order after calculation
    
    private Node startNode; // start node for shortest path
    private Node endNode; // end node for shortest path
    private BezierSpline splineCurrentPosToFirstNode;

    private Dictionary<string, Node> nodeSet = new Dictionary<string, Node>(); // container holding all nodes in scene after scan
    private Dictionary<string, BezierSpline> pathSet = new Dictionary<string, BezierSpline>(); // container holding all paths in scene after scan
    private List<Node> shortestPathNodeSet = new List<Node>(); // container holding all non-gapfiller-nodes in the scene after calculation
    
    // Constructor
    public ZooPathCreator() { }

    // method scans the scene for various objects, necessary for the pathfinding
    public void scanScene() {
        scanSceneForNodes();
        scanSceneForGraph();
        scanSceneForPaths();
    }

    // method calculates closest node and builds marked path to it
    // paramenter: 
    public void createPathFromCurrentPositionToFirstNode(Vector3 currentPosition){
        checkForClosestNode(currentPosition); // Retieves closeset Node
        createPathToFirstNode(currentPosition);
    }

    // method checks all node objects within the scene for which is the closest
    private void checkForClosestNode(Vector3 currentPosition){
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Node");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos){
            if (go.name.StartsWith("0") != true){
                Vector3 diff = go.transform.position - currentPosition;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance) {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        startNode = closest.GetComponent<Node>();
    }

    private void createPathToFirstNode(Vector3 currentPosition){
                // Create new path from players current Position
        BezierSpline pathToFirstNode = new GameObject().AddComponent<BezierSpline>();
        pathToFirstNode.Initialize(2); // Gives spline two points (start,end)
        
        // Set first point to the current position of the player
        pathToFirstNode[0].position = new Vector3(0,0,0);
        pathToFirstNode[0].localPosition = currentPosition;
        
        // Set second point to the position of the closest node
        pathToFirstNode[1].position = new Vector3(0,0,0);
        pathToFirstNode[1].localPosition = startNode.transform.position;
        pathToFirstNode.DrawGizmos(Color.red, 10);
        splineCurrentPosToFirstNode = pathToFirstNode;
    }

    // method scans scene for all available gameObjects that go by the tag "Node"
    private void scanSceneForNodes(){
        nodes = GameObject.FindGameObjectsWithTag("Node"); // search in scene for all node-objects by their tags!
        foreach(GameObject einzelNode in nodes){ // loop through them
            Node tmpResultNode = einzelNode.GetComponent<Node>(); // extract eacg Node from its GameObject
            nodeSet.Add(einzelNode.name, tmpResultNode); // hold each node by its name in a dictionary
            Debug.Log("ZooPathCreator.scanSceneForNodes: " + tmpResultNode);
        }
    }

    // method scans scene for the node holding graph
    private void scanSceneForGraph(){
        GameObject graph = GameObject.Find("NodeGraph"); // search in scene for GameObject by its name
        ingameGraph = graph.GetComponent<Graph>(); // hold the Graph for later calculation
    }

    // method scans scene for paths (BezierCurves)
    private void scanSceneForPaths(){
        pathFragments = GameObject.FindGameObjectsWithTag("Path"); // add all gameObjects with tag "Path"
        foreach (GameObject pathFragment in pathFragments){  // Iteration through all catched "Path" Objects
            BezierSpline tmpPathFragment = pathFragment.GetComponent<BezierSpline>(); // extract and hold all "curves" from their gameObjects
            Debug.Log("ZooPathCreator.scanSceneForPaths: " + pathFragment.name);
            pathSet.Add(pathFragment.name, tmpPathFragment); // adds all edges to a dictionary
        } 
    }

    // method calculates the shortest way between two nodes in a net given the names of startNode and net
    // Arguments: 1. string with the name of the startNode node         
    //            2. string with the name of the endNode node
    public void getShortestPath(string endNodeName){
        resultPath = ingameGraph.GetShortestPath(nodeSet[startNode.name], nodeSet[endNodeName]);
        Debug.Log("ZooPathCreator.getShortestPath() -> Kürzeste Distanz" + resultPath);
        
        foreach (Node resNode in resultPath.nodes){ // Iteration through all resultNodes
            Node tmpResultNode = resNode.GetComponent<Node>();
            if (tmpResultNode.name.StartsWith("0") != true){
                shortestPathNodeSet.Add(tmpResultNode); // keeps all names and their nodes
                //Debug.Log("ZooPathCreator.getShortestPath: " + tmpResultNode);
            } else {
                // Do nothing, don't add node to nodeSet
            }
        }
    }

    // method that colors the shorotest path
    public void markShortestPathInScene(){
        // Due to the pathnames are a combination of nodenames both options (AB or BA) must be checked (direction of way)
        string firstPathPartAB = shortestPathNodeSet[0].name + shortestPathNodeSet[1].name + "";
        string firstPathPartBA = shortestPathNodeSet[1].name + shortestPathNodeSet[0].name + ""; 

        if (pathSet.ContainsKey(firstPathPartAB) == true){
            for (int i = 0; i<shortestPathNodeSet.Count; i++){
                string tmpStrAB =  shortestPathNodeSet[i].name + shortestPathNodeSet[i+1].name + "";
                Debug.Log("ZooPathCreator: " + tmpStrAB);
                pathSet[tmpStrAB].DrawGizmos(Color.red, 10);
            }

        } else if (pathSet.ContainsKey(firstPathPartBA) == true){
            for (int i = 0; i<shortestPathNodeSet.Count; i++){
                string tmpStrBA =  shortestPathNodeSet[i+1].name + shortestPathNodeSet[i].name + "";
                Debug.Log("ZooPathCreator: " + tmpStrBA);
                pathSet[tmpStrBA].DrawGizmos(Color.red, 10);
            }
        } else { 
            Debug.Log("ZooPathCreator.markShortestPathInScene() -> NO PATH IS MARKED");
        }
    }

    // method unmarks the colored way in scene
    public void unmarkShortestPathInScene(){
        for (int i = 0; i<shortestPathNodeSet.Count; i++){
                string tmpStrBA =  shortestPathNodeSet[1].name + shortestPathNodeSet[0].name + "";
                pathSet[tmpStrBA].HideGizmos();
        }
        splineCurrentPosToFirstNode.HideGizmos();
    }
}
