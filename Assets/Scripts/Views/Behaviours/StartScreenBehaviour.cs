using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Behaviours
{
    public class StartScreenBehaviour : BaseBehaviour
    {

        public Action<string> ChangeMoveSpeedAction;
        public Action<string> ChangeEnemiesCountAction;
        public Action<string> ChangeFruitsCountAction;
        public Action<string> ChangeLivesAction;
        public Action ButtonPlayClickedAction;

        [SerializeField] private TMP_InputField MoveSpeed;
        [SerializeField] private TMP_InputField EnemiesCount;
        [SerializeField] private TMP_InputField FruitsCount;
        [SerializeField] private TMP_InputField Lives;
        [SerializeField] private Button ButtonPlay;

        private bool _listenersAdded = false;
        
        public void Init(float moveSpeed, int enemiesCount, int fruitsCount, int lives)
        {
            if (!_listenersAdded)
            {
                _listenersAdded = true;
                
                MoveSpeed.onEndEdit.AddListener(ChangeMoveSpeed);
                EnemiesCount.onEndEdit.AddListener(ChangeEnemiesCount);
                FruitsCount.onEndEdit.AddListener(ChangeFruitsCount);
                Lives.onEndEdit.AddListener(ChangeLives);
                ButtonPlay.onClick.AddListener(OnButtonPlayClicked);
            }

            MoveSpeed.interactable = true;
            MoveSpeed.text = moveSpeed.ToString();
            EnemiesCount.text = enemiesCount.ToString();
            FruitsCount.text = fruitsCount.ToString();
            Lives.text = lives.ToString();
        }

        public void RemoveListeners()
        {
            MoveSpeed.onEndEdit.RemoveListener(ChangeMoveSpeed);
            EnemiesCount.onEndEdit.RemoveListener(ChangeEnemiesCount);
            FruitsCount.onEndEdit.RemoveListener(ChangeFruitsCount);
            Lives.onEndEdit.RemoveListener(ChangeLives);
            ButtonPlay.onClick.RemoveListener(OnButtonPlayClicked);
        }

        private void ChangeMoveSpeed(string value)
        {
            ChangeMoveSpeedAction?.Invoke(value);
        }

        private void ChangeEnemiesCount(string value)
        {
            ChangeEnemiesCountAction?.Invoke(value);
        }

        private void ChangeFruitsCount(string value)
        {
            ChangeFruitsCountAction?.Invoke(value);
        }

        private void ChangeLives(string value)
        {
            ChangeLivesAction?.Invoke(value);
        }

        private void OnButtonPlayClicked()
        {
            ButtonPlayClickedAction?.Invoke();
        }

    }
}