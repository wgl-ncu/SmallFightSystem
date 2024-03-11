using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonCSharp<UIManager>
{
    public Transform root => SceneNodeManager.Instance.UIRoot;

    public Dictionary<string, Page> pageDict;

    public void Init()
    {
        pageDict = new Dictionary<string, Page>();
    }

    public void Uninit()
    {
        pageDict.Clear();
    }

    public void OpenPage(string path, object param = null)
    {
        if (!pageDict.ContainsKey(path))
        {
            var pageObj = Resources.Load<Page>(path);
            var page = UnityEngine.Object.Instantiate(pageObj, root);
            page.pageName = path;
            page.gameObject.SetActive(true);
            page.Open(param);
            pageDict.Add(path, page);
        }
    }

    public void ClosePage(string path)
    {
        if(pageDict.TryGetValue(path, out var page))
        {
            page.OnClose();
            GameObject.Destroy(page.gameObject);
            pageDict.Remove(path);
        }
    }

    public void ClosePage(Page page)
    {
        ClosePage(page.pageName);
    }

    public Page GetPage(string name)
    {
        return pageDict[name];
    }
}
