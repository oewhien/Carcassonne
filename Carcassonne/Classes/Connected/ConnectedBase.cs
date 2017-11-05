using Carcassonne.Classes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Classes.Connected
{
    public class ConnectedBase
    {
        public List<CardWrapper> CardMembers { get; set; }
        public bool IsClosed { get; set; }

        public ConnectedBase(CardBase card, int index = 0)
        {
            IsClosed = false;
            CardMembers = new List<CardWrapper>();

            CardMembers.Add(new CardWrapper(card, index));
        }

        public class CardWrapper
        {
            public CardBase Card { get; }
            public int Index { get; }
            public CardWrapper(CardBase card, int index = 0)
            {
                Card = card;
                Index = index;
            }
        }
    }

    
}
