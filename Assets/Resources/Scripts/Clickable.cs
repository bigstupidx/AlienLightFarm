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

    Color startColor;

    void Start()
    {
        library = GameObject.FindObjectOfType<Library>();

        if (num < 10)
            floor = 0;
        else if (num < 18)
            floor = 1;
        else if (num < 26)
            floor = 2;
        else if (num < 34)
            floor = 3;

        startColor = GetComponent<Image>().color;
        SetOffHighlight();
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

    public GameObject BuildPusher()
    {
        //  MoveAllAlienInClickable();
        currentPusher = Build(BuildingType.Pusher).GetComponent<Building>();
        
        SetAliensToCurrentPusher();

        return currentPusher.gameObject;
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

        Vector3 tempPos = GetComponent<RectTransform>().position;

        go.GetComponent<RectTransform>().position = tempPos;

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
        return IsFree();
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
            && !buildingType.Contains(BuildingType.Pusher))
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

    public Vector3 GetLocalPosition(bool isFinalTarget)
    {
        if (isFinalTarget)
        {
            float temp = GetComponent<RectTransform>().rect.width / 2f - 10;
            float randomTemp = Random.Range(-temp, temp);
            return transform.position + new Vector3(randomTemp, 0, 0) * library.canvas.scaleFactor;
        }
        else
        {
            if (num == 0 || num == 14 || num == 17 || num == 18 || num == 21 || num == 33) 
            {
                return transform.position + new Vector3(25, 0f, 0f) * library.canvas.scaleFactor;
            }
            else if (num == 9 || num == 10 || num == 13 || num == 22 || num == 25 || num == 26)
            {
                return transform.position + new Vector3(-25f, 0f, 0f) * library.canvas.scaleFactor;
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

    void SetAliensToCurrentPusher()
    {
        foreach (Alien alien in library.aliens.GetComponent<AlienController>().GetAliensInCell(this))
        {
            ((Pusher)currentPusher).AddAlien(alien);
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

}
