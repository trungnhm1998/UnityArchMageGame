using System.Collections;
using System.Globalization;
using GameplayAbilitySystem.AttributeSystem;
using GameplayAbilitySystem.AttributeSystem.Components;
using GameplayAbilitySystem.AttributeSystem.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArchMageTest.Gameplay.UI
{
    public class CharacterAttributeVisualizer : MonoBehaviour
    {
        [SerializeField] private AttributeSystemBehaviour _attributeSystem;
        [SerializeField] private AttributeScriptableObject _maxAttribute;
        [SerializeField] private AttributeScriptableObject _attributeToView;

        [Header("UI")]
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _current;
        [SerializeField] private TMP_Text _max;

        private IEnumerator Start()
        {
            yield return null; // skip a frame to make sure all attributes are initialized
            UpdateUI();
        }

        private void OnEnable()
        {
            _attributeSystem.PostAttributeChange += OnAttributeChange; // event based for performance
        }

        private void OnDisable()
        {
            _attributeSystem.PostAttributeChange -= OnAttributeChange;
        }

        private void OnAttributeChange(AttributeScriptableObject attribute, AttributeValue oldValue,
            AttributeValue newValue)
        {
            if (attribute != _attributeToView) return;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _attributeSystem.TryGetAttributeValue(_attributeToView, out var attributeValue);
            _attributeSystem.TryGetAttributeValue(_maxAttribute, out var maxAttributeValue);

            _slider.value = attributeValue.CurrentValue / maxAttributeValue.CurrentValue;
            _current.text = attributeValue.CurrentValue.ToString(CultureInfo.InvariantCulture);
            _max.text = maxAttributeValue.CurrentValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}