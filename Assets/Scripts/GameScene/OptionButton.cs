﻿using System;
using RTLTMPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene
{
    public class OptionButton : MonoBehaviour
    {
        public static event Action<OptionProp> ButtonClickEvent;
        [SerializeField] private Button button;
        [SerializeField] private RTLTextMeshPro text;
        [SerializeField] private OptionProp optionProp;
        [SerializeField] private bool isAnswer;


        public void SetData(OptionProp option)
        {
            Debug.Log(option.GetOption().Key);
            optionProp = option;
            text.text = option.GetOption().Key;
            isAnswer = option.GetOption().Value;
            button.interactable = true;
            button.image.color = Color.white;
        }

        private void Awake()
        {
            ButtonClickEvent += OnButtonClickEvent;
        }

        private void Start()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (optionProp is null)
                return;
            ButtonClickEvent?.Invoke(optionProp);
        }

        private void OnButtonClickEvent(OptionProp prop)
        {
            button.interactable = false;
            if (optionProp == prop)
                button.image.color = isAnswer ? Color.green : Color.red;
            else if (isAnswer)
                button.image.color = Color.green;
        }
    }
}