using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImageEffect
{
    public class Effect : MonoBehaviour
    {
        private static Effect instance = null;
        /*public Effect getInstance()
        {
            if (instance == null) 
            {
                instance = new Effect();
            }
            return instance;
        }*/
        public Effect getInstance()
        {
            if (instance == null)
            {
                instance = this;
            }
            return instance;
        }

        /// <summary>
        /// 物体缩放渐变协程
        /// 限制：最低帧数应大于30帧
        /// </summary>
        /// <param name="gameObject">渐变对象</param>
        /// <param name="fromScale">开始时大小scale</param>
        /// <param name="toScale">结束时大小scale</param>
        /// <param name="effect_time_s">渐变的时间，秒为单位</param>
        /// <param name="action">回调函数，可无</param>
        public void gradualChange(GameObject gameObject, Vector3 fromScale, Vector3 toScale, float effect_time_s = 1f, Action action = null)
        {
            StartCoroutine(gradualChangeEnum(gameObject,fromScale,toScale,effect_time_s,action));
        }


        /// <summary>
        /// 物体缩放渐变协程
        /// 参数说明：渐变对象 起始大小，终止大小，渐变时间(秒为单位)，回调函数（可无）
        /// 帧数限制：最低帧数大于30帧
        /// </summary>
        private IEnumerator gradualChangeEnum(GameObject gameObject, Vector3 fromScale, Vector3 toScale, float effect_time_s = 1f, Action action = null)
        {
            if(gameObject == null)
            {
                Debug.LogError("gradual change error: gameObject is null");
                yield return null;
            }
            if(effect_time_s == 0f)
            {
                Debug.LogError("gradual change error:  effect time is zero");
                yield return null;
            }

            float lastFrameTime = Time.time - 0.01f;
            float totalTime = 0;
            while (true)
            {
                totalTime += (Time.time - lastFrameTime);

                //缩放变化，依据经过的时间进行线性插值
                gameObject.transform.localScale = Vector3.Lerp(fromScale, toScale, totalTime / effect_time_s);
                lastFrameTime = Time.time;


                //如果当前点达到了目标位置，并且在最小误差范围内，则归位，结束循环
                if (Vector3.Distance(toScale, fromScale) / effect_time_s / 30 > Vector3.Distance(toScale, gameObject.transform.localScale))
                {
                    gameObject.transform.localScale = toScale;
                    break;
                }
                //yield return new WaitForSeconds(0.04f);
                yield return null;
            }
            if (action != null)
                action();
        }
    }
}
