using System;
using MySql.Data.MySqlClient;

namespace Tournament_Management.Model
{
    [Serializable]
    public abstract class Person : Participant
    {
        #region Attributes

        private bool _active;
        private string _surname;
        private int _age;

        #endregion Attributes

        #region Properties

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        public int Age { get => _age; set => _age = value; }

        #endregion Properties

        #region Constructors

        public Person()
        {
            this.Age = 0;
        }

        public Person(string name) : base(name)
        {
            this.Age = 0;
        }

        public Person(string name, bool active) : base(name)
        {
            this.Active = active;
        }

        #endregion Constructors

        #region Methods

        public bool IsActive()
        {
            return this.Active;
        }

        public void SwitchActive()
        {
            this.Active = !this.Active;
        }

        public override string GiveInfo()
        {
            return base.GiveInfo() + $"{Surname}" + ", " + $"{(Active ? "Ja" : "Nein")}";
        }

        public override string ToString()
        {
            return Name.ToString() + " " + Surname.ToString();
        }

        public abstract override void Update();

        public abstract override void Put();

        public abstract override void Delete();

        public abstract override void Get(int id);
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        #endregion Methods
    }
}