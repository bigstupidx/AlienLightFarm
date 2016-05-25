using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIButtonsController : MonoBehaviour {

    Library library;
    public GameObject fountainButton;
 //   public GameObject wallButton;
    public GameObject safeCupolButton;
   // public GameObject blackHoleButton;
    public GameObject pusherButton;
    public GameObject healingButton;

    GameObject currentButton;
    GameObject lastSelected;
    GameObject currentSelectObject;
    bool highlightIsOn;
    GameObject buttonForHighlight;
    // Use this for initialization
    void Start() {
        library = GameObject.FindObjectOfType<Library>();
        SetCurrentButton(fountainButton);
    }

    // Update is called once per frame
    void Update()
    {
        ActivateHighlight();
        UpdateActiveButtons();

        if (currentButton != null /*&& currentSelectObject != null*/)
        {
            //   GameObject currentSelectObject = library.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;

            if (currentSelectObject != null && currentSelectObject.GetComponent<Clickable>() != null)
            {
                if (currentButton.Equals(fountainButton))
                {
                    BuildFountain(currentSelectObject);
                }/*
                else if (currentButton.Equals(wallButton))
                {
                    BuildWall(currentSelectObject);
                }*/
                else if (currentButton.Equals(safeCupolButton))
                {
                    BuildSafeCupol(currentSelectObject);
                }
                /*
                else if (currentButton.Equals(blackHoleButton))
                {
                    BuildBlackHole(currentSelectObject);
                }*/
                else if (currentButton.Equals(pusherButton))
                {
                    BuildPusher(currentSelectObject);
                }
                else if (currentButton.Equals(healingButton))
                {
                    BuildHealing(currentSelectObject);
                }

                currentSelectObject = null;

            }
            //     currentSelectObject = null;
            //  GameObject currentSelectObject = library.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
            /*     if (currentSelectObject != null &&
                     currentSelectObject.GetComponent<Clickable>() != null)
                 {   
                     if(currentButton.Equals(fountainButton))
                     {
                         BuildFountain(currentSelectObject);
                     }
                     else if(currentButton.Equals(wallButton))
                     {
                         BuildWall(currentSelectObject);        
                     }
                     else if(currentButton.Equals(safeCupolButton))
                     {
                         BuildSafeCupol(currentSelectObject);

                     }
                     else if (currentButton.Equals(blackHoleButton))
                     {

                         BuildBlackHole(currentSelectObject);

                     }
                     currentButton = null;
                 }
                 else if(currentSelectObject == null)
                 {
                     currentButton = null;
                 }*/
        }

        //  lastSelected = library.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
    }

    void UpdateActiveButtons()
    {
        int alienCount = library.aliens.GetComponent<AlienController>().GetAlienCount();

        List<UIButton> uiButtons = GetUIButtonList();

        foreach(UIButton uiButton in uiButtons)
        {
            int treshold = 0;

            if (uiButton.gameObject.Equals(fountainButton))
                treshold = GameplayConstants.FountainTreshold;
            else if (uiButton.gameObject.Equals(safeCupolButton))
                treshold = GameplayConstants.SafeCupolTreshold;
            else if (uiButton.gameObject.Equals(pusherButton))
                treshold = GameplayConstants.PusherTreshold;
            else if (uiButton.gameObject.Equals(healingButton))
                treshold = GameplayConstants.HealingTreshold;

            if (alienCount >= treshold)
                uiButton.SetActive();
            else
            {
                uiButton.SetDeactive();

                if(currentButton.GetComponent<UIButton>().Equals(uiButton))
                SetCurrentButton(fountainButton);
            }
        }
    }

    void ActivateHighlight()
    {
        if(buttonForHighlight != currentButton)
        {
            library.map.OffHighlightAllActiveClickable();

            highlightIsOn = false;
        }

        if (!highlightIsOn  && !currentButton.GetComponent<UIButton>().IsReload() /*&& currentElement != null*/)
        {
            buttonForHighlight = currentButton;

            library.map.OnHighlightAllActiveClickable();

            highlightIsOn = true;

            return;
        }
        
        if(highlightIsOn && currentButton.GetComponent<UIButton>().IsReload())
        {
            highlightIsOn = false;

            library.map.OffHighlightAllActiveClickable();

            return;
        }
    }

    public void ButtonOnClick(GameObject go)
    {
        SetCurrentButton(go);
    }

    public void ClickableOnClick(Clickable clickable)
    {
       // if (reloadTime == 0)
       if(!currentButton.GetComponent<UIButton>().IsReload())
            currentSelectObject = clickable.gameObject;
    }

    void SetCurrentButton(GameObject go)
    {
        currentButton = go;

        List<UIButton> listUIButtons = GetUIButtonList();


        if (currentButton != null)
        {
            currentButton.GetComponent<UIButton>().SetChecked();
            listUIButtons.Remove(currentButton.GetComponent<UIButton>());
        }

        foreach (UIButton uiButton in listUIButtons)
            uiButton.SetUnchecked();
    }

    List<UIButton> GetUIButtonList()
    {
        List<UIButton> listUIButtons = new List<UIButton>();

        listUIButtons.Add(fountainButton.GetComponent<UIButton>());
        listUIButtons.Add(safeCupolButton.GetComponent<UIButton>());
        listUIButtons.Add(pusherButton.GetComponent<UIButton>());
        listUIButtons.Add(healingButton.GetComponent<UIButton>());

        return listUIButtons;
    }

    bool BuildFountain(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();


        if (clickable.CanBuildFountain())
        {
            GameObject go = clickable.BuildFountain();
            LandWasChanged();
            currentButton.GetComponent<UIButton>().SetReload(GetReloadFountainTime());

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
            currentButton.GetComponent<UIButton>().SetReload(2);


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
            currentButton.GetComponent<UIButton>().SetReload(2);


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
            currentButton.GetComponent<UIButton>().SetReload(2);

            //library.buildings.AddSafeCupol(go.GetComponent<SafeCupol>());

            // currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
            return true;
        }
        else
            return false;
    }

    bool BuildHealing(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();
        if (clickable.CanBuildHealing())
        {
            GameObject go = clickable.BuildHealing();
            currentButton.GetComponent<UIButton>().SetReload(2);

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
            currentButton.GetComponent<UIButton>().SetReload(2);

            //library.buildings.AddSafeCupol(go.GetComponent<SafeCupol>());

            // currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
            return true;
        }
        else
            return false;
    }

    float GetReloadFountainTime()
    {
        for(int i = GameplayConstants.FountainTresholdI.Length-1; i >= 0; i--)
            if(library.buildings.GetFountainCount() >= GameplayConstants.FountainTresholdI[i])
            {
                return GameplayConstants.FountainButtonReloadTime[i];
            }

        return 0;
    }

    public Clickable.BuildingType GetCurrentElementType()
    {
        Clickable.BuildingType buildType;


        if (currentButton.Equals(fountainButton))
            buildType = Clickable.BuildingType.Fountain;
  /*      else if (currentButton.Equals(wallButton))
            buildType = Clickable.BuildingType.Wall;
            */
        else if (currentButton.Equals(safeCupolButton))
            buildType = Clickable.BuildingType.SafeCupol;

       /* else if (currentButton.Equals(blackHoleButton))
            buildType = Clickable.BuildingType.BlackHole;*/
        else if (currentButton.Equals(pusherButton))
            buildType = Clickable.BuildingType.Pusher;
        else if (currentButton.Equals(healingButton))
            buildType = Clickable.BuildingType.Healing;

        else
            buildType = Clickable.BuildingType.Fountain;
        return buildType;
    }

    void LandWasChanged()
    {
        library.aliens.GetComponent<AlienController>().LandWasChanged();
        library.map.LandWasChanged();
    }
}
