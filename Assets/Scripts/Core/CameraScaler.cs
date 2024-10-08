using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private float baseAspect = 1080f / 1920f;
    private float baseOrthographicSize = 11f;
    private void Awake()
    {
        float currentAspect = (float)Screen.width / Screen.height;

        Camera.main.orthographicSize = baseOrthographicSize * (baseAspect / currentAspect);

        float scaleHeight = currentAspect / baseAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            // 가로가 남음 (레터박스)
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // 세로가 남음 (필러박스)
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

}
