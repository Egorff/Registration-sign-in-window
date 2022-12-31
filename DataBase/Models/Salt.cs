using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Salt
    {
        #region Prop

        public string SaltEntity { get; set; }

        public Guid Id { get; set; }

        #region NavigationsProp

        public Guid UserId { get; set; }

        public User User { get; set; }

        #endregion

        #endregion



        #region Ctor

        public Salt(Guid id, string saltEntity)
        {
            Id = id;
            SaltEntity = saltEntity;
        }

        public Salt()
        {

        }

        #endregion

        public byte[] ToBytes(Encoding enc)
        {
            return enc.GetBytes(SaltEntity);
        }
    }
}
