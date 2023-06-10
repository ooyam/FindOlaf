using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField, Header("éûä‘UI")]
        Transform _tra;

        TextMeshProUGUI _txt;
        float _time = 0f;

        public void Initialize()
        {
            _txt = _tra.GetChild(0).GetComponent<TextMeshProUGUI>();
            UpdateUi();
        }

        void Update()
        {
            if (GameMain.instance.GetFlag(GameMain.GameFlag.GameEnd) ||
                !GameMain.instance.GetFlag(GameMain.GameFlag.GameStart))
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            _time += deltaTime;
            UpdateUi();

        }

        /// <summary>
        /// ï\é¶ÇÃçXêV
        /// </summary>
        void UpdateUi()
        {
            _txt.text = _time.ToString("F2");
        }
    }
}
