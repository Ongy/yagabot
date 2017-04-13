using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchBot {

    public class Authorization {
        public List<string> scopes;
        public string created_at;
        public string updated_at;
    }

    public class Token {
        public bool valid;
        public Authorization authorization;
        public string user_name;
        public string user_id;
        public string client_id;
    }

    public class Info {
        public Token token;
    }

    class TwitchApi {

        private static string authRegex = ".split('=')[1].split('&').shift()";
        private static string redirectJS = "<script> window.location.replace(\"http://localhost:8001/submit?key=\" + window.location.hash" + authRegex + "); </script>";
        private static string noFavIco = "<link rel=\"icon\" href=\"data:,\">";
        private static string redirectHTML = "<!DOCTYPE html>\n<html> " + noFavIco + " <head> <meta charset=\"UTF-8\"> </head> <body> </body> " + redirectJS + "</html>";

        private static void openBrowser() {
            string url = "https://api.twitch.tv/kraken/oauth2/authorize/?response_type=token&client_id=rk7p4tmq6gbo29bwgmbajvfra5zrhi&redirect_uri=http://localhost:8001/&scope=user_read+chat_login";
            System.Diagnostics.Process.Start(url);
        }

        public static string requestOAuth() {
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add("http://localhost:8001/");
                listener.Start();
                openBrowser();

                { /* Redirect with javascript, since we don't get the key */
                    HttpListenerContext cxt = listener.GetContext();
                    HttpListenerResponse response = cxt.Response;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(redirectHTML);
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }

                {
                    HttpListenerContext cxt = listener.GetContext();
                    return cxt.Request.QueryString["key"];
                }
            }
        }

        private string oauth;
        private HttpClient client;

        public TwitchApi(string oauth) {
            this.oauth = oauth;

            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.twitch.tv/kraken/");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
            client.DefaultRequestHeaders.Add("Authorization", "OAuth " + this.oauth);
        }

        public async Task<string> getUsername() {
            HttpResponseMessage msg = await this.client.GetAsync("");
            Stream s = await msg.Content.ReadAsStreamAsync();

            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader r = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(r))
            {
                Info info = serializer.Deserialize<Info>(reader);
                return info.token.user_name;
            }
        }
    }
}
