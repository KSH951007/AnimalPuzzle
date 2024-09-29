using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StageGenerator : EditorWindow
{

    private Vector2Int stageSize;
    private GameObject[,] grid;
    private Texture2D stageBackground;


    [MenuItem("Windows/StageEditor")]
    public static void ShowWindow()
    {
        GetWindow<StageGenerator>("Stage Editor");
    }
    private void OnGUI()
    {
        GUILayout.Label("Stage Settings", EditorStyles.boldLabel);

        // Stage 크기 설정
        stageSize.x = EditorGUILayout.IntField("Width", stageSize.x);
        stageSize.y = EditorGUILayout.IntField("Height", stageSize.y);
        stageBackground = (Texture2D)EditorGUILayout.ObjectField("Stage Background", stageBackground, typeof(Texture2D), false);

        if (GUILayout.Button("Create Grid"))
        {
            CreateGrid();
        }

        if (grid != null)
        {
            DrawGrid();
        }
    }

    private void CreateGrid()
    {
        grid = new GameObject[stageSize.x, stageSize.y];
        for (int x = 0; x < stageSize.x; x++)
        {
            for (int y = 0; y < stageSize.y; y++)
            {
                grid[x, y] = null; // 추후 각 좌표에 맞는 게임 오브젝트 배치
            }
        }
    }

    private void DrawGrid()
    {
        if (stageBackground != null)
        {
            GUILayout.Label("Stage Preview");
            GUILayout.Box(stageBackground);
            //GUI.BeginGroup(new Rect(10, 200, 500, 500),stageBackground);
            //GUI.DrawTexture(new Rect(10, 150, 100, 100), stageBackground);
            // GUI.EndGroup();
        }
        GUILayout.Label("Stage Grid", EditorStyles.boldLabel);

        for (int x = 0; x < stageSize.x; x++)
        {
            GUILayout.BeginHorizontal();
            for (int y = 0; y < stageSize.y; y++)
            {
                grid[x, y] = (GameObject)EditorGUILayout.ObjectField(grid[x, y], typeof(GameObject), false);
            }
            GUILayout.EndHorizontal();
        }

    }

}
