using System;
using MySql.Data.MySqlClient;

namespace Tournament_Management.Model
{
    public abstract class Player : Participant
    {
        #region Attributes

        private double _speed;
        private bool _active;
        private string _surname;
        private int _age;

        #endregion

        #region Properties

        public double Speed
        {
            get => _speed;
            set => _speed = value;
        }

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

        #endregion

        #region Constructors

        public Player()
        {
            this.Speed = 0;
        }

        public Player(string name) : base(name)
        {
            this.Speed = 0;
        }

        public Player(string name, double speed, bool active) : base(name)
        {
            this.Speed = speed;
            this.Active = active;
        }

        #endregion

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
            return base.GiveInfo() + $"{Surname}" + ", " + $"Speed: {Speed}" + ", " + $"{(Active ? "Ja" : "Nein")}";
        }

        public abstract override void Update();

        public abstract override void Put();

        public abstract override void Delete();

        public abstract override void Get(int id);

        #endregion
    }
}
