using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;
using static System.Net.WebRequestMethods;
using System.Text;

public class StageGenerator : EditorWindow
{
    private int stageLevel = 1;
    private Vector2Int CellSize;
    private int stepCount;

    private GameObject[,] grid;
    private Texture2D cellTex;
    private bool[,] isActiveCells;
    private List<Texture2D> blockImages = new List<Texture2D>();
    private List<bool> selectBlockList = new List<bool>();
    private Vector2 BlockItemScrollPos;

    [MenuItem("Custom/StageEditor")]
    public static void ShowWindow()
    {

        GetWindow<StageGenerator>("Stage Editor");
    }
    private void OnEnable()
    {
        LoadBlockIconImage();
        LoadCellImage();

    }
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.Width(350), GUILayout.Height(position.height));
        GUILayout.Label("Stage Settings", EditorStyles.boldLabel);

        // Stage 크기 설정
        stageLevel = EditorGUILayout.IntField("StageLevel", stageLevel);
        stageLevel = stageLevel < 0 ? 0 : stageLevel;
        stepCount = EditorGUILayout.IntSlider("StepCount", stepCount, 0, 50);
        GUILayout.Space(10);
        GUILayout.Label("Board Settings", EditorStyles.boldLabel);
        CellSize.x = EditorGUILayout.IntField("Width", CellSize.x);
        CellSize.x = Mathf.Clamp(CellSize.x, 0, 9);

        CellSize.y = EditorGUILayout.IntField("Height", CellSize.y);
        CellSize.y = Mathf.Clamp(CellSize.y, 0, 9);

        SettingBlockItem();

        if (GUILayout.Button("Create Grid"))
        {
            CreateGrid();
        }
        if (GUILayout.Button("Clear Grid"))
        {
        }
        if (GUILayout.Button("Regist Board"))
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(SaveToDatabase());
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box");
        if (grid != null)
        {
            DrawGrid();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
    private void LoadCellImage()
    {
        string FilePath = "Assets/Imports/Cell/Cell.png";  // 실제 경로로 변경해야 함


        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(FilePath);
        if (texture != null)
        {
            cellTex = texture;
            Debug.Log(cellTex);
        }

    }
    private void SettingBlockItem()
    {
        GUILayout.Space(10);
        GUILayout.Label("Appearance Blocks", EditorStyles.boldLabel);


        Rect iconBoxRect = GUILayoutUtility.GetLastRect();
        BlockItemScrollPos = GUILayout.BeginScrollView(BlockItemScrollPos);


        Vector2Int iconLayout = new Vector2Int(5, 5);

        float iconWidth = 60;
        float iconHeight = 60;


        for (int i = 0; i < iconLayout.y; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < iconLayout.x; j++)
            {
                int index = i * iconLayout.x + j;
                if (blockImages.Count <= index)
                {
                    break;
                }
                Texture2D icon = blockImages[index];
                if (icon != null)
                {
                    GUILayout.BeginVertical(GUILayout.Width(iconWidth), GUILayout.Height(iconHeight));
                    Rect rect = GUILayoutUtility.GetRect(iconWidth, iconHeight, GUILayout.ExpandWidth(false));
                    // GUI.DrawTexture(rect, icon, ScaleMode.ScaleToFit);
                    if (selectBlockList.Count > index)
                    {
                        selectBlockList[index] = GUI.Toggle(rect, selectBlockList[index], icon);


                    }

                    GUILayout.EndVertical();
                }
                else
                {
                    GUILayout.Space(iconWidth);
                }

            }
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }
    private void LoadBlockIconImage()
    {
        string filePathFormat = @"Assets/Imports/Blocks/AnimalBlock/{0}.png";
        // 특정 경로에 있는 이미지 에셋을 불러옴
        for (int i = 0; i < (int)BlockID.End; i++)
        {
            string filePath = string.Format(filePathFormat, ((BlockID)i).ToString());
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(filePath);
            if (texture != null)
            {
                blockImages.Add(texture);
                Debug.Log(texture.name);
                selectBlockList.Add(false);
            }
        }


    }
    private void CreateGrid()
    {
        isActiveCells = new bool[CellSize.x, CellSize.y];

        grid = new GameObject[CellSize.x, CellSize.y];
        for (int x = 0; x < CellSize.x; x++)
        {
            for (int y = 0; y < CellSize.y; y++)
            {
                isActiveCells[x, y] = true;
            }
        }
    }

    private void DrawGrid()
    {

        int size = 50;
        for (int i = 0; i < CellSize.x; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < CellSize.y; j++)
            {

                int index = i * CellSize.y + j;

                GUILayout.BeginVertical(GUILayout.Width(size), GUILayout.Height(size));
                Rect rect = GUILayoutUtility.GetRect(size, size, GUILayout.ExpandWidth(false));
                if (GUI.Button(rect, isActiveCells[i, j] == true ? cellTex : null))
                {
                    isActiveCells[i, j] = !isActiveCells[i, j];
                }
                GUILayout.EndVertical();



            }
            GUILayout.EndHorizontal();


        }

    }
    public IEnumerator SaveToDatabase()
    {
        string url = $"https://localhost:7004/api/Stage/{stageLevel}";

        List<BlockID> blockList = new List<BlockID>(selectBlockList.Count);
        for (int i = 0; i < selectBlockList.Count; i++)
        {

            if (selectBlockList[i] == true)
            {
               
                blockList.Add((BlockID)i);

            }
        }

        StageDBData stageData = new StageDBData()
        {
            StageLevel = stageLevel,
            StepCount = stepCount,
            BoardRowCount = CellSize.x,
            BoardHeightCount = CellSize.y,
            BlockList = JsonConvert.SerializeObject(blockList),
            IsPresenceCells = JsonConvert.SerializeObject(isActiveCells)
        };


        string jsonData = JsonConvert.SerializeObject(stageData);
        Debug.Log(jsonData);
        UnityWebRequest request = new UnityWebRequest(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData)),
        };
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("성공");
        }
        else
        {
            Debug.Log("실패");

        }
    }

}
