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
  //  public BgController bgController;
    public AgroLineController agroLineController;
    public GameController gameController;
    public UIButtonsController uiButtonsController;
    public ScreenController screenController;

    public Money money;
    public AdController adController;
    public BgController bgController;

    public PlayGameServices playGameServices;

    public VKController vkController;
}
