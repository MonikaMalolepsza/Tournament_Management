using System;
using System.Collections.Generic;

namespace Tournament_Management.Model
{
    public class Team : Participant
    {
        #region Attributes

        private List<Participant> _list;

        #endregion

        #region Properties

        public List<Participant> List { get => _list; set => _list = value; }

        #endregion

        #region Constructors

        public Team()
        {
            List = new List<Participant>();
        }

        public Team(string name) : base(name)
        {
            List = new List<Participant>();
        }

        public Team(string name, List<Participant> team) : base(name)
        {
            List = team;
        }



        #endregion

        #region Methods

        public void NewMember(Participant teilnehmer)
        {
            List.Add(teilnehmer);
        }

        public string OutputTeamInformation()
        {
            string res = $"Team: {Name}\r\n";
            foreach (Participant t in List)
            {
                res += t.GiveInfo();
            }
            return res;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Put()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Get(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
