using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace AutoChek.Helpers
{
    public class Helper
    {
        public static readonly string adminCookie = "autoChekAdmin";

        public static string DecryptUserCookie(string cookieName)
        {
            var bytes = Convert.FromBase64String(HttpContext.Current.Request.Cookies[cookieName].Value);
            var output = MachineKey.Unprotect(bytes, "info");
            return Encoding.UTF8.GetString(output);
        }
        public static void EncryptUserCookie(string text, string name, int expiry = 1)
        { 
            byte[] encoded  = Encoding.UTF8.GetBytes(text);
            var encryptedValue = Convert.ToBase64String(MachineKey.Protect(encoded, "info"));

            HttpCookie userCookie = new HttpCookie(name, encryptedValue);
            if (expiry > 0)
            {
                userCookie.Expires = DateTime.Now.AddDays(expiry);
            }
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }
        public static int GetAdminId()
        {
            string cookie = DecryptUserCookie(adminCookie);
            return Convert.ToInt32(cookie[0]);
        }

    }
}