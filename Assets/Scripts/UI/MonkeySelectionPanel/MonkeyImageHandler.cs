using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        private RectTransform rectTransform;
        private Vector3 originalAnchoredPosition;

        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
        }

        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;
            rectTransform = GetComponent<RectTransform>();
            //originalPosition = rectTransform.position;
            originalAnchoredPosition = rectTransform.anchoredPosition;
        }
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta;
            owner.MonkeyDraggedAt(rectTransform.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetMonkey();
            owner.MonkeyDroppedAt(eventData.position);
        }
        private void ResetMonkey()
        {
            monkeyImage.color = new Color(1f, 1f, 1f, 1f); // Reset the image color to fully opaque
            rectTransform.anchoredPosition = originalAnchoredPosition;
            LayoutElement layoutElement = GetComponent<LayoutElement>();
            layoutElement.enabled = false;
            layoutElement.enabled = true; // Re-enable to update layout
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            monkeyImage.color = new Color(1f, 1f, 1f, 0.5f); // Make the image semi-transparent on click
        }
    }
}