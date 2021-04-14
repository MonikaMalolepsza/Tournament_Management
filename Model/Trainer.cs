using System;
namespace Tournament_Management.Model
{
    public class Trainer : Person
    {

        #region Attributes

        private int _type;

        #endregion

        #region Properties

        public int Type { get => _type; set => _type = value; }

        #endregion

        #region Constructors

        public Trainer()
        {
            this.Type = 0;
        }

        public Trainer(string name) : base(name)
        {
            this.Type = 0;

        }

        public Trainer(string name, int type) : base(name)
        {
            this.Type = type;
        }

        #endregion

        #region Methods

        public int HowOld()
        {
            return this.Age;
        }

        public override string GiveInfo()
        {
            return base.GiveInfo() + ", " + $"Age: {Age}";
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
