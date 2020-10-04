using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Services
{
    //To solve the problem with jwt token being in an httponly cookie and cant use automatic authentication
    //The token should be appended in httpheader
    public class JWTInHeaderMiddleware
    {
        private RequestDelegate _next;

        public JWTInHeaderMiddleware(RequestDelegate next) //RequestDelegate is a class that include a funtion to the http request pipeline
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string cookie = context.Request.Cookies["LoginJwt"]; //cookie that contain the jwt token

            if(cookie != null)
            {
                context.Request.Headers.Append("Authorization", $"Bearer {cookie}");
            }

            await _next.Invoke(context);
        }
    }
}
