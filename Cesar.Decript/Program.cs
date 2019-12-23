using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;

namespace Cesar.Decript
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            int numeroDeCriptografia = 6;
            string textoCriptografado = "znk znxkk mxkgz boxzaky ul g vxumxgsskx: rgfotkyy, osvgzoktik, gtj nahxoy. rgxxe cgrr";
            string textoDescriptografado;


            char[] Char = textoCriptografado.ToCharArray();
            for (int i = 0; i < Char.Length; i++)
            {
                if ((int)Char[i] > 96 && (int)Char[i] < 123)
                {
                    int value = (int)Char[i] - numeroDeCriptografia;
                    if (value < 97)
                    {
                        value = 123 - (97 - value);
                    }
                    Char[i] = (char)value;
                }
            }
            textoDescriptografado = new string(Char);


            Console.WriteLine(textoDescriptografado);
            Console.WriteLine(Hash(textoDescriptografado));

            var restClient = new RestSharp.RestClient("https://api.codenation.dev");
            var request = new RestSharp.RestRequest($"/v1/challenge/dev-ps/submit-solution", RestSharp.Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddHeader("Content-Type", "multipart/form-data");

            request.AddQueryParameter("token", "e2e34c6b93d38dbd1d03d540ba81e7a5dfd3f7c2");

            request.AddFile("answer", "./answer.json");

            var response = restClient.Execute(request);
        }
        static string Hash(string input)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}
