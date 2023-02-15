using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkable;
    public GameObject a;
    public int width;
    public int height;
    public Node[,] nodes;
    Transform floor;

    AStar aStar;
    private void Awake()
    {
        aStar = GetComponent<AStar>();     
        Gridd();
    }

    /// <summary>
    /// Creates a grid of nodes
    /// It checks if is walkable. It detects it with Physics.CheckBox if collides with unwalkable layer
    /// </summary>
    void Gridd()
    {
        floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Transform>();
        nodes = new Node[width, height];

        for (int x = 0; x < nodes.GetLength(0); x++)
        {
            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                //bool walkable = true;
                bool walkable = !(Physics.CheckBox(new Vector3((x) - floor.localScale.x * 5, 1, (y) -
                        floor.localScale.z * 5), new Vector3(.49f, .49f, .49f), Quaternion.identity, unwalkable));


                if (walkable)
                {
                    nodes[x, y] = new Node(new Vector3Int((int)((x) - floor.localScale.x * 5), (int)(1), (int)((y) -
                    floor.localScale.z * 5)), walkable);

                    aStar.openDictionary.Add(new Vector3Int(x - 25, 1, y - 25), nodes[x, y]);
                    Instantiate(a, nodes[x, y].worldPos, Quaternion.identity);
                }                
            }
        }
    }
}
