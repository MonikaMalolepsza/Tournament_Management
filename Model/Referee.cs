using System;
namespace Tournament_Management.Model
{
    public class Referee : Person

    {
        #region Attributes

        /**
            Assistant Referee. (minimum age 12) ...
            Regional Referee. (minimum age 12) ...
            Intermediate Referee. (minimum age 14) ...
            Advanced Referee. (minimum age 16) ...
            National Referee. (minimum age 18)
         */
        private string _certificate;

        #endregion

        #region Properties


        public string Certificate
        {
            get => _certificate;
            set => _certificate = value;
        }

        #endregion

        #region Constructors

        public Referee()
        {
            this.Certificate = "";
        }

        public Referee(string name) : base(name)
        {
            this.Certificate = "";
        }

        public Referee(string name, string certificate) : base(name)
        {
            this.Certificate = certificate;
        }

        #endregion

        #region Methods

        public string GiveCert()
        {
            return this.Certificate;
        }

        public override string GiveInfo()
        {
            return base.GiveInfo() + ", " + $"Certificate: {Certificate}";
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
