using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Types = Model.Types;

namespace UI.Game.Popup
{
    public class Controller : MonoBehaviour, IHidenable
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Image center;
        [SerializeField] private TextMeshProUGUI weightText;
        [SerializeField] private TextMeshProUGUI applyText;
        [SerializeField] private Image imageApply;
        [SerializeField] private Sprite[] applySprites;
        [SerializeField] private Button apply;
        [SerializeField] private Button delete;
        private Types _currentType;
        private int _currentID;
        public event Action<Types, int> ApplyEvent;
        public event Action<Types, int> DeleteEvent;
        public void Initialize(string name, Sprite sprite, string weight, string apply, Types types, int id)
        {
            this.name.text = name;
            center.sprite = sprite;
            weightText.text = weight;
            applyText.text = apply;
            _currentType = types;
            _currentID = id;
            if (_currentType == Types.Body || _currentType == Types.Head)
            {
                imageApply.sprite = applySprites[0];
                buttonText.text = "Экипировать(надеть на себя это говнище)";
            }
            else if (_currentType == Types.Medicine)
            {
                imageApply.sprite = applySprites[1];
                buttonText.text = "Лечиться";
            }
            else if (_currentType == Types.Projectiles)
            {
                imageApply.sprite = applySprites[2];
                buttonText.text = "Купить";
            }
        }

        public void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.ignoreParentGroups = true;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.ignoreParentGroups = false;
        }

        private void Awake()
        {
            apply.onClick.AddListener(OnApply);
            delete.onClick.AddListener(OnDelete);
        }

        private void OnApply()
        {
            ApplyEvent?.Invoke(_currentType, _currentID);
        }

        private void OnDelete()
        {
            DeleteEvent?.Invoke(_currentType, _currentID);
        }
    }
}