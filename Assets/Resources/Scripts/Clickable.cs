using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Clickable : MonoBehaviour {


    public enum BuildingType
    {
        
        Fountain,
        Wall,
        SafeCupol,
        BlackHole,
        Pusher,
        Healing,
        Born
    }

    public int num;

    int floor;

    List<BuildingType> buildingType = new List<BuildingType>();// BuildingType.Free;

    Library library;

    //List<Building> currentBuildings = new List<Building>();
    Building currentFountain;
    Building currentCupol;
    Building currentBlackHole;
    Building currentPusher;
    Building currentHealing;

    Color startColor;

    void Awake()
    {
        library = GameObject.FindObjectOfType<Library>();
    }


    void Start()
    {

        floor = library.map.GetFloorByClickableNum(num);
        
       
        startColor = GetComponent<Image>().color;
        ToDefault();
    }


    void Update()
    {
        if (currentFountain != null && currentFountain.IsDestroyed())
        {

            if(buildingType.Contains(BuildingType.Fountain))
            {
                //library.buildings.RemoveFountain(currentFountain.gameObject.GetComponent<Fountain>());
                buildingType.Remove(BuildingType.Fountain);    
            }

            Destroy(currentFountain.gameObject);
            currentFountain = null;
            library.map.LandWasChanged();
        }

        if (currentCupol != null && currentCupol.IsDestroyed())
        {

            if (buildingType.Contains(BuildingType.Wall))
            {
              //  library.buildings.RemoveWall(currentCupol.gameObject.GetComponent<Wall>());
                buildingType.Remove(BuildingType.Wall);
            }
            else if(buildingType.Contains(BuildingType.SafeCupol))
            {
              //  library.buildings.RemoveWall(currentCupol.gameObject.GetComponent<Wall>());
                buildingType.Remove(BuildingType.SafeCupol);
            }

            Destroy(currentCupol.gameObject);
            currentCupol = null;
            library.map.LandWasChanged();
        }

        if (currentBlackHole != null && currentBlackHole.IsDestroyed())
        {
            if (buildingType.Contains(BuildingType.BlackHole))
            {
                //  library.buildings.RemoveWall(currentCupol.gameObject.GetComponent<Wall>());
                buildingType.Remove(BuildingType.BlackHole);
            }
            Destroy(currentBlackHole.gameObject);
            currentBlackHole = null;
            library.map.LandWasChanged();
        }

        if (currentPusher != null && currentPusher.IsDestroyed())
        {
            if (buildingType.Contains(BuildingType.Pusher))
            {
                //  library.buildings.RemoveWall(currentCupol.gameObject.GetComponent<Wall>());
                buildingType.Remove(BuildingType.Pusher);
            }
            Destroy(currentPusher.gameObject);
            currentPusher = null;
            library.map.LandWasChanged();
        }

        if (currentHealing != null && currentHealing.IsDestroyed())
        {
            if (buildingType.Contains(BuildingType.Healing))
            {
                //  library.buildings.RemoveWall(currentCupol.gameObject.GetComponent<Wall>());
                buildingType.Remove(BuildingType.Healing);
            }
            Destroy(currentHealing.gameObject);
            currentHealing = null;
        }
    }

    public GameObject BuildFountain()
    {
        currentFountain = Build(BuildingType.Fountain).GetComponent<Building>();
        return currentFountain.gameObject;
    }

    public GameObject BuildWall()
    {
        //  MoveAllAlienInClickable();
        currentCupol = Build(BuildingType.Wall).GetComponent<Building>();
         return currentCupol.gameObject;
    }

    public GameObject BuildSafeCupol()
    {
        //  MoveAllAlienInClickable();
        currentCupol = Build(BuildingType.SafeCupol).GetComponent<Building>();
        return currentCupol.gameObject;
    }

    public GameObject BuildBlackHole()
    {
        //  MoveAllAlienInClickable();
        currentBlackHole = Build(BuildingType.BlackHole).GetComponent<Building>();

        SetAliensToCurrentBlackHole();

        return currentBlackHole.gameObject;
    }

    /*
    public GameObject BuildPusher()
    {
        //  MoveAllAlienInClickable();
        currentPusher = Build(BuildingType.Pusher).GetComponent<Building>();
        
        SetAliensToCurrentPusher();

        return currentPusher.gameObject;
    }*/

    public GameObject BuildHealing()
    {
        //  MoveAllAlienInClickable();
        currentHealing = Build(BuildingType.Healing).GetComponent<Building>();

        SetAliensToCurrentHealing();

        return currentHealing.gameObject;
    }

    GameObject Build(BuildingType bt)
    {     
        buildingType.Add(bt);

        GameObject go = Instantiate(Resources.Load("Prefabs/Buildings/"+bt.ToString())) as GameObject;
        
        Transform parentTransform = null;
        switch(bt)
        {
            case BuildingType.Fountain: parentTransform = library.buildings.fountains;  break;
            case BuildingType.SafeCupol: parentTransform = library.buildings.safeCupols; break;
            case BuildingType.Pusher: parentTransform = library.buildings.pushers; break;
            case BuildingType.Healing: parentTransform = library.buildings.healings; break;
            case BuildingType.Wall: parentTransform = library.buildings.walls; break;
            case BuildingType.BlackHole: parentTransform = library.buildings.blackHoles; break;
        }

        go.transform.SetParent(parentTransform, false);
        go.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

        return go;   
    }

    public Alien CreateAlien()
    {
        buildingType.Add(BuildingType.Born);

        GameObject go = Instantiate(Resources.Load("Prefabs/Alien")) as GameObject;
        go.transform.SetParent(library.aliens.transform, false);

        Vector3 tempPos = GetRandomPositionInClickable();
        //  tempPos.z = 1;
        go.GetComponent<RectTransform>().anchoredPosition = tempPos;

     //   go.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, Alien.DeltaHeight);

        Alien alien = go.GetComponent<Alien>();
        alien.StartBorn(this);

        return alien;   
    }

    /*
    public bool IsFree()
    {
        if (buildingType.Equals(BuildingType.Free))
            return true;
        else
            return false;
    }*/

    public bool CanBuildFountain()
    {
        return !IsFountain() && !IsBlackHole() && !IsPusher();
    }

    public bool CanBuildWall()
    {
        return !IsCupol() && !IsBlackHole() && !IsPusher();
    }

    public bool CanBuildSafeCupol()
    {
        return !IsCupol() && !IsBlackHole() && !IsPusher();
    }

    public bool CanBuildBlackHole()
    {
        return IsFree();
    }

    public bool CanBuildPusher()
    {
        return !IsCupol() && !IsBlackHole() && !IsPusher();
    }

    public bool CanBuildHealing()
    {
        return true;
    }

    public bool IsBlackHole()
    {
        if (buildingType.Contains(BuildingType.BlackHole))
            return true;
        else
            return false;
               
    }

    public bool IsPusher()
    {
        if (buildingType.Contains(BuildingType.Pusher))
            return true;
        else
            return false;

    }


    public bool IsFree()
    {
        if (!buildingType.Contains(BuildingType.BlackHole) 
            && !buildingType.Contains(BuildingType.Fountain) 
            && !buildingType.Contains(BuildingType.SafeCupol) 
            && !buildingType.Contains(BuildingType.Wall)
            && !buildingType.Contains(BuildingType.Pusher)
            && !buildingType.Contains(BuildingType.Healing))
            return true;
        else
            return false;
    }

    public bool IsWall()
    {
        if (buildingType.Contains(BuildingType.Wall))
            return true;
        else
            return false;
    }

    public bool IsSafeCupol()
    {
        if (buildingType.Contains(BuildingType.SafeCupol))
            return true;
        else
            return false;
    }

    public bool IsFountain()
    {
        if (buildingType.Contains(BuildingType.Fountain))
            return true;
        else
            return false;
    }
    

    public bool IsCupol()
    {
        if (currentCupol != null)
            return true;
        else
            return false;
    }

    public bool IsAvailableFountain()
    {
        if (buildingType.Contains(BuildingType.Fountain) && !buildingType.Contains(BuildingType.Wall))
            return true;
        else
            return false;

    }

    public bool IsFreeForBorn()
    {
        if (buildingType.Count == 0)
            return true;
        else
            return false;
    }

    public void AlienWasBorning()
    {
        if (buildingType.Contains(BuildingType.Born))
            buildingType.Remove(BuildingType.Born);
    }

    public bool IsFreeForMove()
    {
        if (!buildingType.Equals(BuildingType.Wall))
            return true;
        else
            return false;
    }

    /*
    public void SetFree()
    {
        buildingType = BuildingType.Free;
    }*/

    public int GetFloor()
    {
        return floor;
    }

    public Vector2 GetLocalPosition(bool isFinalTarget)
    {
        if (isFinalTarget)
        {
            float temp = GetComponent<RectTransform>().rect.width / 2f - 10;
            float randomTemp = Random.Range(-temp, temp);
            return GetComponent<RectTransform>().anchoredPosition + new Vector2(randomTemp, 0 );
        }
        else
        {
            if (library.map.IsRightJump(num))
            {
                return GetComponent<RectTransform>().anchoredPosition + new Vector2(33, 0f) ;
            }
            else if (library.map.IsLeftJump(num))
            {
                return GetComponent<RectTransform>().anchoredPosition + new Vector2(-33f, 0f);
            }

        }

        return new Vector3();
    }

    /*
    public BuildingType GetBuildingType()
    {
        return buildingType;
    }*/

    public void MoveAllAlienInClickable()
    {
        library.aliens.GetComponent<AlienController>().MoveAllAlienInClickable(this);
    }

    public void UseFountain()
    {
        if(currentFountain != null)
            (currentFountain as Fountain).Use();
        
    }

    public bool FountainExist()
    {
        if (currentFountain != null)
            return true;
        else return false;
        /*

        if (currentBuildings.Contains(typeof(Fountain)))
            return true;
        else
            return false;*/
    }

    void SetAliensToCurrentBlackHole()
    {
        foreach(Alien alien in library.aliens.GetComponent<AlienController>().GetAliensInCell(this))
        {
            ((BlackHole) currentBlackHole).AddAlien(alien);
        }
    }

    /*
    void SetAliensToCurrentPusher()
    {
        foreach (Alien alien in library.aliens.GetComponent<AlienController>().GetAliensInCell(this))
        {
            ((Pusher)currentPusher).AddAlien(alien);
        }
    }*/

    void SetAliensToCurrentHealing()
    {
        foreach (Alien alien in library.aliens.GetComponent<AlienController>().GetAliensInCell(this))
        {
            ((Healing)currentHealing).AddAlien(alien);
        }
    }

    public void OnHighlight(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.Fountain:
                if(CanBuildFountain())
                    SetOnHighlight();
                

                break;
            case BuildingType.Wall:
                if (CanBuildWall())
                    SetOnHighlight();
                

                break;
            case BuildingType.SafeCupol:
                if (CanBuildSafeCupol())
                    SetOnHighlight();
                

                break;
            case BuildingType.BlackHole:
                if (CanBuildBlackHole())
                    SetOnHighlight();
                

                break;

            case BuildingType.Pusher:
                if (CanBuildPusher())
                    SetOnHighlight();


                break;
            case BuildingType.Healing:
                if (CanBuildHealing())
                    SetOnHighlight();


                break;
        }
    }

    public void OffHighlight()
    {
        SetOffHighlight();
    }

    void SetOnHighlight()
    {
        GetComponent<Image>().color = startColor;
    }

    void SetOffHighlight()
    {
        GetComponent<Image>().color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }

    public void ToDefault()
    {
        buildingType.Clear();

        if (currentFountain != null)
            Destroy(currentFountain.gameObject);

        if (currentCupol != null)
            Destroy(currentCupol.gameObject);

        if (currentBlackHole != null)
            Destroy(currentBlackHole.gameObject);

        if (currentPusher != null)
            Destroy(currentPusher.gameObject);

        if (currentHealing != null)
            Destroy(currentHealing.gameObject);

     //   startColor = GetComponent<Image>().color;
        SetOffHighlight();
    }

    public Vector2 GetRandomPositionInClickable()
    {
        RectTransform rt = GetComponent<RectTransform>();

        float treshold = rt.rect.width  * 0.8f  / 2f;

   //     Debug.Log(library.canvas.scaleFactor);

        return rt.anchoredPosition +  new Vector2(Random.Range(-treshold, treshold),0);
    }
}
