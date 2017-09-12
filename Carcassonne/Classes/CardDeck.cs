using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Carcassonne.Classes
{
    public class CardDeck
    {
        public List<CardBase> CardDeckList { get; set; }


        public CardDeck()
        {
            List<CardBase> card0s = Enumerable.Repeat(0, 3).Select(x => (CardBase) new Card0()).ToList<CardBase>();
            List<CardBase> card1s = Enumerable.Repeat(0, 3).Select(x => (CardBase)new Card1()).ToList<CardBase>();
            List<CardBase> card2s = Enumerable.Repeat(0, 3).Select(x => (CardBase)new Card2()).ToList<CardBase>();
            List<CardBase> card3s = Enumerable.Repeat(0, 3).Select(x => (CardBase)new Card3()).ToList<CardBase>();

            CardDeckList = new List<CardBase>();
            CardDeckList.AddRange(card0s);
            CardDeckList.AddRange(card1s);
            CardDeckList.AddRange(card2s);
            CardDeckList.AddRange(card3s);

            CardDeckList.Shuffle();
        }

        public CardBase DrawCard()
        {
            int n = CardDeckList.Count;
            if (n <= 0)
                return new CardNull();

            CardBase card = CardDeckList[n - 1];
            CardDeckList.RemoveAt(n - 1);

            return card;

        }

        

    }
    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
