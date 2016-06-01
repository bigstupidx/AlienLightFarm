using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILentaController : MonoBehaviour
{
    /*
    Library library;
    public GameObject fountainButton;
    public GameObject wallButton;
    public GameObject safeCupolButton;
    public GameObject blackHoleButton;
    public GameObject pusherButton;


    // GameObject currentButton;
    //   GameObject lastSelected;

    public GameObject currentElementGO;
    public GameObject nextElementGO;

   // public Text time;

    GameObject currentElement;
    GameObject nextElement;
    GameObject currentSelectObject;

    float reloadTime = 0;
    bool hightlightIsOn;
    // Use this for initialization
    void Start()
    {
        library = GameObject.FindObjectOfType<Library>();
    }

    // Update is called once per frame
    void Update()
    {
        reloadTime = Mathf.Max(0, reloadTime - Time.deltaTime);

        UpdateCurrentAndNextElements();

        ActivateHighlight();

        if (currentElement != null && currentSelectObject != null && reloadTime == 0)
        {
            //  GameObject currentSelectObject = library.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;


            if (currentElement.Equals(fountainButton))
            {
                if (BuildFountain(currentSelectObject))
                {
                    ClearCurrentElement();
                    SetReloadTime();
                    DeactivateHighlight();
                }
            }
            else if (currentElement.Equals(wallButton))
            {
                if (BuildWall(currentSelectObject))
                {
                    ClearCurrentElement();
                    SetReloadTime();
                    DeactivateHighlight();
                }
            }
            else if (currentElement.Equals(safeCupolButton))
            {
                if (BuildSafeCupol(currentSelectObject))
                {
                    ClearCurrentElement();
                    SetReloadTime();
                    DeactivateHighlight();
                }
            }
            else if (currentElement.Equals(blackHoleButton))
            {
                if (BuildBlackHole(currentSelectObject))
                {
                    ClearCurrentElement();
                    SetReloadTime();
                    DeactivateHighlight();
                }
            }
            else if (currentElement.Equals(pusherButton))
            {
                if (BuildPusher(currentSelectObject))
                {
                    ClearCurrentElement();
                    SetReloadTime();
                    DeactivateHighlight();
                }
            }
                
            currentSelectObject = null;     
        }
       // lastSelected = library.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
    }

    public void SetCurrentToNext()
    {
        if (reloadTime == 0)
        {
            ClearCurrentElement();
            UpdateCurrentAndNextElements();
        }
    }

    void ActivateHighlight()
    {
        if (!hightlightIsOn && reloadTime == 0 && currentElement != null)
        {
            library.map.OnHighlightAllActiveClickable();
            hightlightIsOn = true;
        }
    }

    void DeactivateHighlight()
    {
        hightlightIsOn = false;

        library.map.OffHighlightAllActiveClickable();
    }

    void ClearCurrentElement()
    {
        currentElement = null;

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in currentElementGO.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    void ClearNextElement()
    {
        nextElement = null;

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in nextElementGO.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }
    
    public void ButtonOnClick(Clickable clickable)
    {
        if(reloadTime == 0)
        currentSelectObject = clickable.gameObject;
    }


    bool BuildFountain(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();


        if (clickable.CanBuildFountain())
        {
            GameObject go = clickable.BuildFountain();
            LandWasChanged();
            //library.buildings.AddFoutain(go.GetComponent<Fountain>());

            //currentButton.GetComponent<UIButton>().SetReload(GetReloadFountainTime());
            return true;
        }
        else
            return false;
    }



    bool BuildWall(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();
        if (clickable.CanBuildWall())
        {
            GameObject go = clickable.BuildWall();
            // library.buildings.AddWall(go.GetComponent<Wall>());
            LandWasChanged();

            // currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
            return true;
        }
        else
            return false;

    }

    bool BuildSafeCupol(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();
        if (clickable.CanBuildSafeCupol())
        {
            GameObject go = clickable.BuildSafeCupol();
            //library.buildings.AddSafeCupol(go.GetComponent<SafeCupol>());
            LandWasChanged();

            // currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
            return true;
        }
        else
            return false;
    }

    bool BuildBlackHole(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();
        if (clickable.CanBuildBlackHole())
        { 
            GameObject go = clickable.BuildBlackHole();
            //library.buildings.AddSafeCupol(go.GetComponent<SafeCupol>());

            // currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
            return true;
        }
        else
            return false;
    }

    bool BuildPusher(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();
        if (clickable.CanBuildPusher())
        {
            GameObject go = clickable.BuildPusher();
            //library.buildings.AddSafeCupol(go.GetComponent<SafeCupol>());

            // currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
            return true;
        }
        else
            return false;
    }


    void LandWasChanged()
    {
        library.aliens.GetComponent<AlienController>().LandWasChanged();
        library.map.LandWasChanged();
    }

    public void UpdateCurrentAndNextElements()
    {
        if (currentElement == null && nextElement != null)
        {
            currentElement = nextElement;

            nextElement = null;

            GameObject go =
            (Instantiate(currentElement) as GameObject);
            go.transform.SetParent(currentElementGO.transform, false);
            go.GetComponent<UIButton>().SetReload(reloadTime);

        }

        if (nextElement == null)
        {
            ClearNextElement();

            nextElement = GetNextElement();

            (Instantiate(nextElement) as GameObject).transform.SetParent(nextElementGO.transform, false);
        }
    }

    public GameObject GetNextElement()
    {
        int alienCount = library.aliens.GetComponent<AlienController>().GetAlienCount();

        List<GameObject> tempArr = new List<GameObject>();

        if (alienCount >= GameplayConstants.FountainTreshold)
            tempArr.Add((GameObject)Resources.Load("Prefabs/LentaElements/Fountain"));

        if(alienCount >= GameplayConstants.WallTreshold)
            tempArr.Add((GameObject)Resources.Load("Prefabs/LentaElements/Wall"));

        if (alienCount >= GameplayConstants.SafeCupolTreshold)
            tempArr.Add((GameObject)Resources.Load("Prefabs/LentaElements/SafeCupol"));

        if (alienCount >= GameplayConstants.BlackHoleTreshold)
            tempArr.Add((GameObject)Resources.Load("Prefabs/LentaElements/BlackHole"));

        if (alienCount >= GameplayConstants.PusherTreshold)
            tempArr.Add((GameObject)Resources.Load("Prefabs/LentaElements/Pusher"));

        return tempArr[Random.Range(0, tempArr.Count)];
    }

    void SetReloadTime()
    {
        reloadTime = GameplayConstants.LentaReloadTime;

        
    }

    public Clickable.BuildingType GetCurrentElementType()
    {
        Clickable.BuildingType buildType;


        if (currentElement.Equals(fountainButton))
            buildType = Clickable.BuildingType.Fountain;
        else if (currentElement.Equals(wallButton))
            buildType = Clickable.BuildingType.Wall;

        else if (currentElement.Equals(safeCupolButton))
            buildType = Clickable.BuildingType.SafeCupol;

        else if (currentElement.Equals(blackHoleButton))
            buildType = Clickable.BuildingType.BlackHole;
        else if (currentElement.Equals(pusherButton))
            buildType = Clickable.BuildingType.Pusher;

        else
            buildType = Clickable.BuildingType.Fountain;
        return buildType;
    }

    
    /*
    float GetReloadFountainTime()
    {
        for (int i = GameplayConstants.FountainTreshold.Length - 1; i >= 0; i--)
            if (library.buildings.GetFountainCount() >= GameplayConstants.FountainTreshold[i])
            {
                return GameplayConstants.FountainButtonReloadTime[i];
            }

        return 0;
    }*/
}

