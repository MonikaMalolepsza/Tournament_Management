using System;
namespace Tournament_Management.Model
{
    public class Trainer : Person
    {

        #region Attributes

        private string _surname;
        private int _age;


        #endregion

        #region Properties

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        #endregion

        #region Constructors

        public Trainer()
        {
            this.Age = 0;
        }

        public Trainer(string name) : base(name)
        {
            this.Age = 0;

        }

        public Trainer(string name, int age) : base(name)
        {
            this.Age = age;
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
