using System;
using System.Collections;
using Sheldier.Constants;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sheldier.Setup
{
    public class LoadingViewer : MonoBehaviour
    {
        [SerializeField] private Canvas loadingCanvas;
        [SerializeField] private Image loadingBarImage;
        [SerializeField] private TextMeshProUGUI descriptionTMP; 
        
        private int _fillAmountID = Shader.PropertyToID(Constants.ShaderGlobalVariables.LOADING_BAR_FILL_AMOUNT);
        private float _currentFillAmount;
        private float _desirableFillAmount;

        private const float BAR_FILLING_SPEED = 10.0f;

        public void SetProgress(float progressValue) => _desirableFillAmount = _fillAmountID;

        public void ResetProgress() => SetBarFill(0.0f);
        public void SetDescription(string loadingOperationLoadLabel) => descriptionTMP.text = loadingOperationLoadLabel;
        
        public void EnableCanvas()
        {
            loadingCanvas.enabled = true;
            _currentFillAmount = 0.0f;
            StartCoroutine(UpdateLoadingBarCoroutine());
        }
        private IEnumerator UpdateLoadingBarCoroutine()
        {
            while (loadingCanvas.enabled)
            {
                SetBarFill(Mathf.Lerp(_currentFillAmount, _desirableFillAmount, Time.deltaTime * BAR_FILLING_SPEED));
                yield return null;
            }
        }
        private void SetBarFill(float fillAmount) => loadingBarImage.material.SetFloat(_fillAmountID, fillAmount);
    }
    
    
}