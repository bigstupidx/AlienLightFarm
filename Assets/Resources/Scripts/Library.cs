using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Library : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject aliens;
    public Map map;
    public Canvas canvas;
    public AlienCount alienCount;
    public Buildings buildings;
    public BgController bgController;
    public AgroLineController agroLineController;
    public GameController gameController;
    public UIButtonsController uiButtonsController;
}
