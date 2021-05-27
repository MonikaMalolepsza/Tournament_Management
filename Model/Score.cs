using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_Management.Model
{
    public class Score
    {
        #region Attributes

        private int _points;
        private int _team;

        #endregion Attributes

        #region Properties

        public int Points { get => _points; set => _points = value; }
        public int Team { get => _team; set => _team = value; }

        #endregion Properties

        #region Constructors

        public Score()
        {
            this.Points = 0;
            this.Team = -1;
        }

        public Score(int t, int points)
        {
            this.Points = points;
            this.Team = t;
        }

        #endregion Constructors
    }
}