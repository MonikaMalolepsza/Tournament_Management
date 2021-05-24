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
        private Team _team;

        #endregion Attributes

        #region Properties

        public int Points { get => _points; set => _points = value; }
        public Team Team { get => _team; set => _team = value; }

        #endregion Properties

        #region Constructors

        public Score()
        {
            this.Points = 0;
            this.Team = new Team();
        }

        public Score(Team t, int points)
        {
            this.Points = points;
            this.Team = t;
        }

        #endregion Constructors
    }
}