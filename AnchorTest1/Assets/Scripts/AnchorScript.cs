using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;

public class AnchorScript : MonoBehaviour
{
    public GameObject rootGameObject;
    WorldAnchorStore worldAnchorStore = null;
    bool savedRoot = false;
    string[] ids = null;
    bool storeLoaded = false;
    WorldAnchor anchor;
    // Start is called before the first frame update
    void Start()
    {
        WorldAnchorStore.GetAsync(StoreLoaded);
    }

    // Update is called once per frame
    void Update()
    {
        if (storeLoaded)
        {
            EnumerateIDS();
        }
    }

    void EnumerateIDS()
    {
        Debug.Log("Enumerating IDS");
        storeLoaded = false;
        if (worldAnchorStore != null)
        {
            ids = this.worldAnchorStore.GetAllIds();
            Debug.Log("id length of anchors: " + ids.Length);
            for(int index = 0; index < ids.Length; index++)
            {
                Debug.Log(ids[index]);
            }
            LoadAnchor();
        }
        else
        {
            Debug.Log("Enumerating IDs: worldanchorstore null");
        }
    }

    private void StoreLoaded(WorldAnchorStore store)
    {
        this.worldAnchorStore = store;
        Debug.Log("StoreLoaded: World anchor store loaded successfully");
        storeLoaded = true;
    }

    private void LoadAnchor()
    {
        this.savedRoot = this.worldAnchorStore.Load("Root", rootGameObject);
        if (!this.savedRoot)
        {
            Debug.Log("Loadgame: no rootobject anchor saved previously");
        }

    }

    public void SaveAnchor()
    {
        anchor = rootGameObject.AddComponent<WorldAnchor>();
        //save data about holograms positioned by this world anchor
        if (!this.savedRoot && anchor != null)//Save the root only once
        {
            this.savedRoot = this.worldAnchorStore.Save("root", anchor);
            Debug.Log("Saved anchor: " + this.savedRoot);
        }
        else
        {
            Debug.Log("Root has already been saved");
        }
    }

    public void DestroyExistingAnchor()
    {
        anchor = rootGameObject.GetComponent<WorldAnchor>();
        if (anchor != null)
        {
            Destroy(anchor);
            worldAnchorStore.Delete("root");
            this.savedRoot = false;
        }
    }
}
