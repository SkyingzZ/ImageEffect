using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImageEffect;
using System;

//测试渐变函数
public class testGradualChange : MonoBehaviour {
    
    //定义各种特效的时间常量
    const float EFFECT_ENTER_TIME = 0.25f;
    const float EFFECT_ARRIVE_TIME = 0.5f;
    const float EFFECT_OUT_TIME = 0.25f;

    //从外界把ImageEffectHolder对象拖过来
    public Effect effect;
  
    private void OnEnable()
    {
        playEffect();
    }

    public void playEffect()
    {
        effectEnter();
    }
    
    //开始渐入
    private void effectEnter()
    {
        //参数说明：要渐入或渐出的物体的gameObject，初始大小即Scale，结束时候大小即Scale，渐入或渐出的时间，回调函数
        effect.gradualChange(this.gameObject, new Vector3(0, 0, 0), new Vector3(1, 1, 1), EFFECT_ENTER_TIME, effectArrive);
    }
    //渐入完成
    private void effectArrive()
    {
        StartCoroutine(waitForTime(EFFECT_ARRIVE_TIME, effectOut));
    }
    //开始渐出
    private void effectOut()
    {
        effect.gradualChange(this.gameObject, new Vector3(1, 1, 1), new Vector3(0, 0, 0), EFFECT_OUT_TIME, null);
    }

    //等待n秒调用回调函数
    IEnumerator waitForTime(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        if(action!=null)
            action();
    }
}
