using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[Serializable]
public class BlockData
{
    public Sprite blockIcon;
    public BlockType type;
    public RuntimeAnimatorController animatorController;


}
public abstract class Block : MonoBehaviour
{
    [SerializeField]
    protected BlockData blockData;

    protected Vector2Int blockPos;
    protected bool isDraggable;
    public event Action<Vector2Int> onBeginDrag;
    public event Action<Vector2Int> onDrop;
    public bool isMoveable { get; protected set; }
    public BlockType blockType { get; protected set; }


    public void Setup(Vector2Int blockPos)
    {
        this.blockPos = blockPos;
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag == gameObject)
        {
            Debug.Log("begindrag : " + gameObject.name);
            Debug.Log("pos :" + blockPos);

            onBeginDrag?.Invoke(blockPos);
        }
    }
    public void Move(Vector2Int position)
    {

    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //if (eventData.pointerEnter == gameObject)
        //{
        //    Debug.Log("enddrag : " + gameObject.name);
        //    Debug.Log("pos :" + blockPos);

        //    onBeginDrag?.Invoke(blockPos);
        //}
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }

    private void SwapBlock(Block block)
    {
        if (block == null)
            return;

        if (block is SpecialBlock)
        {

        }
        else
        {

        }
    }
    protected abstract bool FindMatches();

    protected abstract void DestroyBlock();
    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        onDrop?.Invoke(blockPos);
    }
}
