using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class MessageWindowController : MonoBehaviour
    {
        enum ChildObj
        {
            Message,
            Title,
            Ok,
            Cancel,
            Close,

            Count
        }

        GameObject _obj;
        RectTransform _tra;
        GameObject[] _objAry;
        RectTransform[] _traAry;
        TextMeshProUGUI[] _txtAry;

        public Action OkEvent { get; set; }
        public Action CancelEvent { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _obj = gameObject;
            _tra = GetComponent<RectTransform>();
            _objAry = new GameObject[(int)ChildObj.Count];
            _traAry = new RectTransform[(int)ChildObj.Count];
            _txtAry = new TextMeshProUGUI[(int)ChildObj.Count];
            for (int i = 0; i < (int)ChildObj.Count; i++)
            {
                var tra = _tra.GetChild(i);
                _objAry[i] = tra.gameObject;
                _traAry[i] = tra.GetComponent<RectTransform>();
                _txtAry[i] = tra.GetChild(0).GetComponent<TextMeshProUGUI>();

                var btn = _traAry[i].GetComponent<Button>();
                switch (i)
                {
                    case (int)ChildObj.Ok:
                        btn.onClick.AddListener(() => OkEvent?.Invoke());
                        break;

                    case (int)ChildObj.Cancel:
                        btn.onClick.AddListener(() => CancelEvent?.Invoke());
                        break;

                    case (int)ChildObj.Close:
                        btn.onClick.AddListener(() => CancelEvent?.Invoke());
                        break;
                }
            }
        }

        /// <summary>
        /// UI設定(Yes or No)
        /// </summary>
        public void SetOkCancelUi(string message, string okTxt, string cancelTxt, string title = "")
        {
            _objAry[(int)ChildObj.Close].SetActive(false);
            _txtAry[(int)ChildObj.Message].text = message;
            _txtAry[(int)ChildObj.Ok].text = okTxt;
            _txtAry[(int)ChildObj.Cancel].text = cancelTxt;
            if (title == "")
            {
                _objAry[(int)ChildObj.Title].SetActive(false);
            }
            else
            {
                _txtAry[(int)ChildObj.Title].text = title;
            }
        }

        /// <summary>
        /// UI設定(とじるのみ)
        /// </summary>
        public void SetCloseUi(string message, string closeTxt, string title = "")
        {
            _objAry[(int)ChildObj.Ok].SetActive(false);
            _objAry[(int)ChildObj.Cancel].SetActive(false);
            _txtAry[(int)ChildObj.Message].text = message;
            _txtAry[(int)ChildObj.Close].text = closeTxt;
            if (title == "")
            {
                _objAry[(int)ChildObj.Title].SetActive(false);
            }
            else
            {
                _txtAry[(int)ChildObj.Title].text = title;
            }
        }

        /// <summary>
        /// 表示切替
        /// </summary>
        public void SetActive(bool active) => _obj.SetActive(active);

        /// <summary>
        /// ウィンドウをとじる
        /// </summary>
        public void Close() => Destroy(_obj);
    }
}
