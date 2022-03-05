using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    #region Singleton
    private static MapGen _instance;
    public static MapGen Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<MapGen>();
            }
            return _instance;

        }
    }
    #endregion

    public Vector2Int[] edges;

    public Transform points;
    public Transform lines;

    public GameObject linePrefab;

    private void Start()
    {
        RegenerateEdges();
    }


    [ContextMenu("Regenerate edges")]
    public void RegenerateEdges()
    {
        for (int i = lines.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(lines.GetChild(i).gameObject);
        }
        foreach (var e in edges) 
        {
            var p1 = SearchPoint(e.x);
            var p2 = SearchPoint(e.y);
            p1.connectedTo.Add(p2);
            p2.connectedTo.Add(p1);

            var line = Instantiate(linePrefab, lines);
            var lr = line.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, p1.transform.position);
            lr.SetPosition(1, p2.transform.position);

        }
    }

    private Point SearchPoint(int id)
    {
        foreach (Transform p in points) 
        {
            var point = p.GetComponent<Point>();
            if(point.GetId() == id)
            {
                return point;
            }
        }
        return null;
    }

    public void AfterPlayerMoved()
    {
        foreach (Transform p in points)
        {
            var point = p.GetComponent<Point>();
            point.AfterPlayerTrun();
        }
    }
}
