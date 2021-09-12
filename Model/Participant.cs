using System;
using System.Xml.Serialization;
using Tournament_Management.Helper;

namespace Tournament_Management.Model
{
    [Serializable]
    [XmlInclude(typeof(Team))]
    [XmlInclude(typeof(Person))]
    public abstract class Participant
    {
        #region Attributes

        private string _name;
        private int _id;

        #endregion Attributes

        #region Properties

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        #endregion Properties

        #region Constructors

        public Participant()
        {
            Name = "";
        }

        public Participant(string name)
        {
            Name = name;
        }

        #endregion Constructors

        #region Methods

        public virtual string GiveInfo()
        {
            return this.Name;
            // Console.WriteLine(this.Name);
        }

        public abstract void Update();

        public abstract void Put();

        public abstract void Delete();

        public abstract void Get(int id);

        public override bool Equals(object obj)
            => obj != null && GetType().Equals(obj.GetType()) && ((Participant)obj).Id == Id;

        public override int GetHashCode()
            => Id.GetHashCode();

        #endregion Methods
    }
}