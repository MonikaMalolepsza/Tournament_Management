using System;
namespace Tournament_Management.Model
{
    public class Physio : Participant
    {
        #region Attributes

        private int _experience;


        #endregion

        #region Properties

        public int Experience
        {
            get => _experience;
            set => _experience = value;
        }

        #endregion

        #region Constructors

        public Physio()
        {
            this.Experience = 0;
        }

        public Physio(string name) : base(name)
        {
            this.Experience = 0;

        }

        public Physio(string name, int experience) : base(name)
        {
            this.Experience = experience;
        }

        #endregion

        #region Methods

        public int HowExperienced()
        {
            return this.Experience;
        }

        public override string GiveInfo()
        {
            return base.GiveInfo() + ", " + $"Experience: {Experience}";
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
