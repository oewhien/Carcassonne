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

        public CardGrid()
        {
            GridPos = new List<IntPoint>();            
        }

        public void FillGridPos(int row, int col)
        {
            GridPos.Add(new IntPoint(col, row));    // Remember: point(x,y)!
        }

        public void FillGridPos(CardBase card)
        {
            int row = card.GridPosition.Y;
            int col = card.GridPosition.X;
            FillGridPos(row, col);
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

        public bool IsNeighbourOccupied(int row, int col)
        {
            foreach (IntPoint pos in GridPos)
            {
                int dRow = Math.Abs(pos.Y - row);
                int dCol = Math.Abs(pos.X - col);
                if ((dRow == 1 && dCol == 0) || (dRow == 0 && dCol == 1))
                    return true;                
            }
            return false;
        }
    }
}
