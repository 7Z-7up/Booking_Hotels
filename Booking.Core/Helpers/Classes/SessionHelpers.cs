using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Booking.Core.Helpers.Classes
{
    public static class SessionHelpers
    {
        public static void SetObjectAsJson(this ISession session, string key, string value)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));
            session.SetString(key, value);
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static dynamic GetObjectFromJson<T>(object session, string v)
        {
            throw new NotImplementedException();
        }
    }
}
