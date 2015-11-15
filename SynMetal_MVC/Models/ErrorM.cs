using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SynMetal_MVC.Models
{
    public enum ErrEnums {HttpError,MaxFileError};
    [Serializable]
   public class ErrorM : ISerializable
    {
        string ReturnUrl;
        string ErrName;
        string ErrDescription;
        ErrEnums err;

        public ErrorM(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new System.ArgumentNullException("info");
            this.ReturnUrl = info.GetString("ru");
            this.ErrName = info.GetString("eName");
            this.ErrDescription = info.GetString("ErrDesc");
            this.err = (ErrEnums) info.GetValue("ErrEnum", typeof(ErrEnums));
        }
      
        public ErrorM()
        {
            ReturnUrl = "";
            ErrName = "";
            ErrDescription = "";
            
        }
        public ErrorM(string returnUrl, string errName, string errDescription, ErrEnums err)
        {
            ReturnUrl = returnUrl;
            ErrName = errName;
            ErrDescription = errDescription;
            this.err = err;
        }
        [SecurityPermission(SecurityAction.LinkDemand,
    Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new System.ArgumentNullException("info");
            info.AddValue("eName", this.ErrName);
            info.AddValue("ErrDesc", this.ErrDescription);
            info.AddValue("ru", this.ReturnUrl);
            info.AddValue("ErrEnum", this.err,typeof(ErrEnums));

        }

        #region SettersAndGetters


        public void setReturnUrl(string Url)
        {
            this.ReturnUrl = Url;
        }

        public string getReturnUrl()
        {
            return ReturnUrl;
        }

        public void setErrName(string ErrName)
        {
            this.ErrName = ErrName;
        }

        public string getErrName ()
        {
            return this.ErrName;
        }

        public void setErrDescription(string ErrDesc)
        {
            this.ErrDescription = ErrDesc;
        }

        public string getErrDescription()
        {
            return this.ErrDescription;
        }

        public void setErr(ErrEnums Err)
        {
            this.err = Err;
        }

        public ErrEnums getErr()
        {
            return this.err;
        }
        #endregion
        
       


    }
}
