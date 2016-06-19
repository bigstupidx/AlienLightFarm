using UnityEngine;
using System.Collections;

public class MarketScreen : MonoBehaviour {

    public MarketElement[] marketElements;

    public GameObject scrollBox;

    Library library;
    // Use this for initialization
    void Awake() {
        library = GameObject.FindObjectOfType<Library>();
	}


    public void OnOpenScreen()
    {
        for (int i = 0; i < marketElements.Length; i++)
        {
            if (i == 0 || PreferencesSaver.IsOpenElementInMarket(i))
                marketElements[i].Open();
            else
                marketElements[i].Close();
        }
        
        int currentElementNum = PreferencesSaver.GetCurrentElementInMarket();

        SelectCurrentElement(marketElements[currentElementNum]);
    }

	
    public void SelectCurrentElement(MarketElement currentMarketElement)
    {
        if (currentMarketElement.IsOpen())
        {

            foreach (MarketElement marketElement in marketElements)
                marketElement.SetNotCurrent();


            currentMarketElement.SetCurrent();

            for (int i = 0; i < marketElements.Length; i++)
                if (marketElements[i].Equals(currentMarketElement))
                {
                    PreferencesSaver.SetCurrentElementInMarket(i);
                    break;
                }

        }

    }

    public void OnSelectClick(MarketElement currentMarketElement)
    {
        if (currentMarketElement.IsOpen())
            SelectCurrentElement(currentMarketElement);
        else
        {
            if(library.money.GetMoney() >= GameplayConstants.PurchasePrice/*если хватает бабосов, то открываем, выбирраем, снимаем бабосы*/)
            {
                PreferencesSaver.SetOpenElementInMarket(currentMarketElement.num);
                currentMarketElement.Open();
                SelectCurrentElement(currentMarketElement);
                library.money.AddMoney(-GameplayConstants.PurchasePrice);
                library.money.SaveMoney();
            }

        }
//            library.screenController.HideMarketScreen();

    }



}
