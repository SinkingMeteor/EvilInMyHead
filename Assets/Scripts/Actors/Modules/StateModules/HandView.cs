using System;
using UnityEngine;

namespace Sheldier.Actors
{
    public class HandView : MonoBehaviour
    {
        public SpriteRenderer ItemBody => spriteRenderer;
        public Transform Aim => aim;
        public Transform NearPoint => nearPoint;
        public Transform FarPoint => farPoint;
        
        [SerializeField] private Transform aim;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform nearPoint;
        [SerializeField] private Transform farPoint;
        public void AddItem(Sprite itemSprite) => spriteRenderer.sprite = itemSprite;
        public void ClearItem() => spriteRenderer.sprite = null;
        
    }
}