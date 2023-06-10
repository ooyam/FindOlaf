using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class OlafManager : MonoBehaviour
    {
        [SerializeField, Header("ÉIÉâÉtêe")]
        Transform _parent;
        
        [Header("ÉIÉâÉtÇÃÉvÉåÉnÉu")]
        public GameObject[] OlafPreAry;

        GameObject[] _olafObjAry;
        OlafController[] _olafCtlAry;

        static readonly Vector2 OLAF_DEF_SIZE = new Vector2(256f, 256f);
        const float OLAF_DEF_MARGIN = 20f;

        public enum Olaf
        {
            Normal,     // í èÌ
            CloseEyes,  // ñ⁄Çï¬Ç∂ÇÈ
            LieIn,      // êQì]Ç‘
            Eat,        // êHÇ◊ÇÈ
            Hide,       // âBÇÍÇÈ
            Question,   // ã^ñ‚
            LookUp,     // å©è„Ç∞ÇÈ
            Walk,       // ï‡Ç≠
            Sleep,      // êQÇÈ
            Back,       // îwñ 

            Count
        }

        public void Initialize()
        {
            
        }

        public void CreateOlaf(Olaf themeOlaf)
        {
            int lineCnt = 2;
            int columnCnt = 3;
            float scale = 1f;
            float margin = OLAF_DEF_MARGIN * scale;
            Vector2[] posAry = ComCalc.GetSquarePosAry(lineCnt, columnCnt, OLAF_DEF_SIZE * scale, new Vector2(margin, margin));
            posAry.Shuffle();

            int createCnt = lineCnt * columnCnt;
            _olafObjAry = new GameObject[createCnt];
            _olafCtlAry = new OlafController[createCnt];
            for (int i = 0; i < createCnt; i++)
            {
                Olaf olaf = themeOlaf;
                if (i != 0)
                {
                    do
                    {
                        olaf = (Olaf)Random.Range(0, (int)Olaf.Count);
                        if (olaf != themeOlaf) break;
                    } while (true);
                }
                _olafObjAry[i] = UIControl.instance.OpenUI(OlafPreAry[(int)olaf], _parent);
                _olafCtlAry[i] = _olafObjAry[i].GetComponent<OlafController>();
                _olafCtlAry[i].Initialize();
                _olafCtlAry[i].Tra.localPosition = posAry[i];
            }
        }
    }
}