using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsManager : SingletonUnity<TipsManager>
{
    private class TipsRequest
    {
        string msg;
        float time;
        float passTime;
        public bool isShow;
        public TipsRequest(string msg, float time)
        {
            this.msg = msg;
            this.time = time;
            passTime = 0;
            isShow = false;
        }

        public bool Update(float dt)
        {
            if (isShow)
            {
                passTime += dt;
                if (passTime >= time)
                {
                    UIManager.Instance.ClosePage(PageDefine.TipPage);
                    return true;
                }
            }
            return false ;
        }

        public void Show()
        {
            if (!isShow)
            {
                isShow = true;
                UIManager.Instance.OpenPage(PageDefine.TipPage, msg);
            }
        }
    }

    private List<TipsRequest> requests = new List<TipsRequest>();
    public void CreateTips(string msg, float time)
    {
        requests.Add(new TipsRequest(msg, time));
    }

    public void ClearAll()
    {
        requests.Clear();
        UIManager.Instance.ClosePage(PageDefine.TipPage);
        
    }

    private void Update()
    {
        bool hasRequestShow = false;
        int finishIndex = -1;
        for (int i = 0; i < requests.Count; ++i)
        {
            bool isFinish = requests[i].Update(Time.deltaTime);
            if (isFinish)
            {
                finishIndex = i;
            }
            hasRequestShow |= requests[i].isShow;
        }
        if(finishIndex != -1)
        {
            requests.RemoveAt(finishIndex);
        }
        if (!hasRequestShow && requests.Count > 0)
        {
            requests[0].Show();
        }
    }

}
