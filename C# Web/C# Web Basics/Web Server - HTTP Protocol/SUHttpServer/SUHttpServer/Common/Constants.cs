using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUHttpServer.Common
{
    public class Constants
    {
        public const string redirectUrl = "http://softuni.org";
        public const string HtmlForm = @"<form action='/HTML' method='POST'>
   Name: <input type='text' name='Name'/>
   Age: <input type='number' name='Age'/>
<input type='submit' value='Save'/>
</form>";

        public const string DownloadForm = @"<form action='/Content' method='POST'>
   <input type='submit' value ='Download Sites Content' /> 
</form>";

        public const string LoginForm = @"<form action='/Login' method='POST'>
   Username: <input type='text' name='Username'/>
   Password: <input type='text' name='Password'/>
   <input type='submit' value ='Log In' /> 
</form>";


        public const string FileName = "content.txt";

        public const string Username = "user";

        public const string Password = "user123";
    }
}
