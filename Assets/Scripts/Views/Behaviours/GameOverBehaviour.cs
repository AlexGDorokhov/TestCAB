using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Behaviours
{
    public class GameOverBehaviour : BaseBehaviour
    {

        public Action NewGameAction;
        public Action ExitAction;
        
        [SerializeField] private ScoreBehaviour _scorePrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _exitButton;

        private RectTransform _gridLayoutGroupRectTransform;
        private RectTransform _gridLayoutGroupParentRectTransform;

        public void AddListeners()
        {
            _newGameButton.onClick.AddListener(OnNewGameButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            
        }

        public void RemoveListeners()
        {
            _newGameButton.onClick.RemoveListener(OnNewGameButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
            
        }

        private void Awake()
        {
            _gridLayoutGroupRectTransform = _gridLayoutGroup.GetComponent<RectTransform>();
            _gridLayoutGroupParentRectTransform = _gridLayoutGroup.transform.parent.GetComponent<RectTransform>();
        }

        private void Update()
        {
            var pRect = _gridLayoutGroupParentRectTransform.rect;
            _gridLayoutGroupRectTransform.SetWidth(pRect.width);
            _gridLayoutGroup.cellSize = new Vector2(pRect.width, _gridLayoutGroup.cellSize.y);
        }

        private void OnNewGameButtonClick()
        {
            NewGameAction?.Invoke();
        }

        private void OnExitButtonClick()
        {
            ExitAction?.Invoke();
        }


        public void CreateGrid(List<int> list, int currentResultIndex)
        {
            CleanGrid();
            for (int i = 0; i < list.Count; i++)
            {
                var score = Instantiate(_scorePrefab, _gridLayoutGroup.transform);
                var scoreBh = score.GetComponent<ScoreBehaviour>();
                scoreBh.SetScore(list[i]);
                scoreBh.SetPosition(i + 1);
                if (currentResultIndex == i)
                {
                    score.GetComponent<Image>().color = Color.magenta;
                }
            }

            StartCoroutine(Rebuild(currentResultIndex));
        }
        
        private IEnumerator Rebuild(int currentResultIndex)
        {
            yield return new WaitForSeconds(0.1f);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_gridLayoutGroup.GetComponent<RectTransform>());
            MoveToCurrent(currentResultIndex);
        }
        
        private void MoveToCurrent(int currentResultIndex)
        {
            var pos = _gridLayoutGroup.transform.localPosition;
            var gHeight = _gridLayoutGroup.GetComponent<RectTransform>().rect.height;
            var sHeight = _scorePrefab.GetComponent<RectTransform>().rect.height;
            var newY = -(gHeight - (_gridLayoutGroup.spacing.y + sHeight) * currentResultIndex);
            
            _gridLayoutGroup.gameObject.GetComponent<RectTransform>().SetPositionY(newY);
        }
        
        

        public void CleanGrid()
        {
            for (int i = _gridLayoutGroup.transform.childCount - 1; i >= 0; i--)
            {
                var child = _gridLayoutGroup.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }

    }
}