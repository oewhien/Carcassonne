using Carcassonne.Classes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Classes.Helper
{
    public class CardGrid
    {
        public List<IntPoint> GridPos;
        private List<CardBase> _cardPos;

        public CardGrid()
        {
            GridPos = new List<IntPoint>();
            _cardPos = new List<CardBase>();   
        }

        private void FillGridPos(int row, int col, CardBase card)
        {
            GridPos.Add(new IntPoint(col, row));    // Remember: point(x,y)!
            _cardPos.Add(card);
        }

        public CardBase GetCardAt(int row, int col)
        {
            int index = -1;            
            for (int i = 0; i < GridPos.Count; i++)
            {
                IntPoint pos = GridPos[i];
                if ((pos.X - col) == 0 && (pos.Y - row) == 0)
                    index = i;
            }

            if (index == -1)
                throw new Exception("There is no card here.");

            return _cardPos[index];
        }

        public CardBase GetLastCard()
        {
            return _cardPos[_cardPos.Count - 1];
        }

        public void FillGridPos(CardBase card)
        {
            int row = card.GridPosition.Y;
            int col = card.GridPosition.X;
            FillGridPos(row, col, card);
        }

        public bool IsOccupied(int row, int col)
        {                       
            foreach (IntPoint pos in GridPos)
            {
                if ((pos.X - col) == 0 && (pos.Y - row) == 0)
                    return true;                        
            }
            return false;
        }

        public CardNeighbours GetNeighbours(int row, int col)
        {
            CardNeighbours neighbours = new CardNeighbours();

            int nCards = GridPos.Count;
            for (int i = 0; i < nCards; i++)
            {
                IntPoint pos = GridPos[i];
                CardBase card = _cardPos[i];
                int dRow = pos.Y - row;
                int dCol = pos.X - col;

                if (dRow == 1 && dCol == 0)
                    neighbours.South = card;
                if (dRow == -1 && dCol == 0)
                    neighbours.North = card;
                if (dRow == 0 && dCol == 1)
                    neighbours.East = card;
                if (dRow == 0 && dCol == -1)
                    neighbours.West = card;
            }            
            return neighbours;
        }

        


        //public bool IsNeighbourOccupied(int row, int col)
        //{
        //    foreach (IntPoint pos in GridPos)
        //    {
        //        int dRow = Math.Abs(pos.Y - row);
        //        int dCol = Math.Abs(pos.X - col);
        //        if ((dRow == 1 && dCol == 0) || (dRow == 0 && dCol == 1))
        //            return true;                
        //    }
        //    return false;
        //}
    }

    public class CardNeighbours
    {
        public CardBase North { get; set; }
        public CardBase East { get; set; }
        public CardBase South { get; set; }
        public CardBase West { get; set; }
        

        public CardNeighbours()
        {
        }

        public int NumNeighbours()
        {
            int count = 0;
            if (North != null)
                count++;
            if (East != null)
                count++;
            if (South != null)
                count++;
            if (West != null)
                count++;
            return count;
        }
    }
}
