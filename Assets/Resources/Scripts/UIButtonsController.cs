using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class UIButtonsController : MonoBehaviour {

    Library library;
    public GameObject fountainButton;
    public GameObject wallButton;
    public GameObject safeCupolButton;
    public GameObject blackHoleButton;
    GameObject currentButton;
    GameObject lastSelected;


    // Use this for initialization
    void Start () {
        library = GameObject.FindObjectOfType<Library>();
	}
	
	// Update is called once per frame
	void Update ()
    {


        if(currentButton != null)
        {
            GameObject currentSelectObject = library.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
            if (currentSelectObject != null &&
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
            }
        }

        lastSelected = library.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
    }

    public void ButtonOnClick(GameObject go)
    {
        currentButton = go;
    }

    public void UpdateCurrentAndNextElements()
    {

    }



    void BuildFountain(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();


        if (!clickable.IsFountain())
        {
            GameObject go = clickable.BuildFountain();
            library.aliens.GetComponent<AlienController>().LandWasChanged();
            //library.buildings.AddFoutain(go.GetComponent<Fountain>());
            
            currentButton.GetComponent<UIButton>().SetReload(GetReloadFountainTime());
        }
    }

    void BuildWall(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();
        if (!clickable.IsCupol())
        {
            GameObject go = clickable.BuildWall();
           // library.buildings.AddWall(go.GetComponent<Wall>());
            library.aliens.GetComponent<AlienController>().LandWasChanged();

            currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
        }

    }

    void BuildSafeCupol(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();
        if (!clickable.IsCupol())
        {
            GameObject go = clickable.BuildSafeCupol();
            //library.buildings.AddSafeCupol(go.GetComponent<SafeCupol>());
            library.aliens.GetComponent<AlienController>().LandWasChanged();

            currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
        }
    }

    void BuildBlackHole(GameObject currentSelectObject)
    {
        Clickable clickable = currentSelectObject.GetComponent<Clickable>();
        if (clickable.CanBuildBlackHole())
        {


            GameObject go = clickable.BuildBlackHole();
            //library.buildings.AddSafeCupol(go.GetComponent<SafeCupol>());

            currentButton.GetComponent<UIButton>().SetReload(GameplayConstants.WallButtonReloadTime);
        }
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
}
