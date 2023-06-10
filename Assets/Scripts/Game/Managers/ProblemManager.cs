using System.Collections;
using System.Collections.Generic;
using Common;
using TMPro;
using UnityEngine;

namespace Game
{
    public class ProblemManager : MonoBehaviour
    {
        [SerializeField, Header("ñ‚ëËêîUI")]
        Transform _tra;

        TextMeshProUGUI _txt;
        int _problemNum = 0;

        const int PROBLEM_COUNT = 9;
        const string LEFT = "écÇË";
        const string PROBLEM = "ñ‚";

        public void Initialize()
        {
            _txt = _tra.GetChild(0).GetComponent<TextMeshProUGUI>();
            UpdateUi(PROBLEM_COUNT);
        }

        void UpdateUi(int count)
        {
            _txt.text = LEFT + count + PROBLEM;
        }

        public void UpdateProblemNum()
        {
            _problemNum++;
            switch (Global.instance.GameMode)
            {
                case ComDefine.GameMode.Speed:
                    UpdateUi(PROBLEM_COUNT - _problemNum);
                    if (PROBLEM_COUNT <= _problemNum)
                    {
                        StartCoroutine(GameMain.instance.GameEnd());
                    }
                    break;

                case ComDefine.GameMode.NoMiss:
                    UpdateUi(_problemNum);
                    break;
            }
        }
    }
}
