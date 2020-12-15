using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : MonoBehaviour
{
    private GameObject[] allCardHoldersGO;
  
    private List<int> numbers = new List<int>();
    private List<Card> allCards = new List<Card>();
    private List<Card> inGameCards = new List<Card>();
    [SerializeField] GameObject cardObj;
    private CardHolder[] allCardHolders = new CardHolder[9];
    [SerializeField] Card cardPrefab1;
    [SerializeField] Card cardPrefab2;
    [SerializeField] Card cardPrefab3;
    [SerializeField] Card cardPrefab4;
    [SerializeField] Card cardPrefab5;
    [SerializeField] Card cardPrefab6;
    [SerializeField] Card cardPrefab7;
    [SerializeField] Card cardPrefab8;
    [SerializeField] Card cardPrefab9;
    [SerializeField] Card cardPrefab10;
    [SerializeField] Card cardPrefab11;
    [SerializeField] Card cardPrefab12;
    [SerializeField] Card cardPrefab13;

    // Start is called before the first frame update
    void Start()
    {
        cardObj = GameObject.Find("Cards");
        numbers = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13};
        allCards = new List<Card> { cardPrefab1, cardPrefab2 , cardPrefab3 , cardPrefab4 , cardPrefab5 , cardPrefab6 , cardPrefab7,
        cardPrefab8,cardPrefab9,cardPrefab10,cardPrefab11,cardPrefab12,cardPrefab13};
        allCardHoldersGO = GameObject.FindGameObjectsWithTag("CardHolder");
        int counter = 0;
        foreach(GameObject GO in allCardHoldersGO)
        {
            allCardHolders[counter] = GO.GetComponent<CardHolder>();
            counter++;
        }
        Shuffle();
    }
    public void Shuffle()
    {
        for(int i = 0; i < 9; i++)
        {
            int random = Random.Range(1, numbers.Count);
            allCardHolders[i].SetCurrentCard(allCards[random]);
            numbers.RemoveAt(random);
            allCards.RemoveAt(random);
        }

        foreach(CardHolder cardHolder in allCardHolders)
        {
            Card cardGO = Instantiate(cardHolder.GetCurrentCard(), cardHolder.transform);
            cardHolder.SetCurrentCard(cardGO);
            cardGO.SetCurrentCardHolder(cardHolder);
            inGameCards.Add(cardGO);
            cardGO.transform.SetParent(cardObj.transform);
        }
    }
   
    public CardHolder[] GetAllCardHolders()
    {
        return this.allCardHolders;
    }
    public List<Card> AllCards()
    {
        return this.inGameCards;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
