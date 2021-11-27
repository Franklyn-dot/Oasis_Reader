using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oasis_Reader.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Oasis_Reader.Data
{
    /// <summary>
    /// Clase para counicarse con el Servicio de una REST Api 
    /// </summary>
    public class RestService
    {




        public Ta_inventario_producto_copy Aux { get; set; }

        HttpClient client;

    
        public RestService()
        {
            HttpClient httpClient = new HttpClient();
            client = httpClient;



            client.MaxResponseContentBufferSize = 256000;

            
        }



        /// <summary>
        /// Envia el encabezado Tm_conteo
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="IP_URL"></param>
        /// <param name="isNewItem"></param>
        /// <returns></returns>
        public async Task SendEncabezado(Tm_conteo encabezado, string IP_URL, bool isNewItem = true)
        {

            var uri = new Uri(string.Format(IP_URL, string.Empty));


            try
            {
                //Convertir a formato JSON en minúsculas
                var json = JsonConvert.SerializeObject(encabezado).ToLower();

                /*//----------------------- Remover Id

                var jArr = JArray.Parse(json);

                jArr.Descendants().OfType<JProperty>()
                                  .Where(p => p.Name == "id")
                                  .ToList()
                                  .ForEach(att => att.Remove());



                json = jArr.ToString();

                //------------------------*/

                var content = new StringContent(json, Encoding.UTF8, "application/json");




                HttpResponseMessage response = null;
                if (!isNewItem)
                {
                    response = await client.PutAsync(uri, content);
                }
                else
                {
                    response = await client.PostAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Item successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				HUBO UN ERROR {0}", ex.Message);
            }


        }






        /// <summary>
        /// Envia una lista de encabezados Tm_conteo
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="IP_URL"></param>
        /// <param name="isNewItem"></param>
        /// <returns></returns>
        public async Task SendEncabezadoList(List<Tm_conteo> encabezados, string IP_URL,  bool isNewItem = true)
        {

            var uri = new Uri(string.Format(IP_URL, string.Empty));
            int N = encabezados.Count;


            Console.WriteLine("NUMERO DE ENCABEZADOS = " + N);

            for (int i = 0; i < N; i++)
            {

                try
                {
                    //Convertir a formato JSON en minúsculas
                    var json = JsonConvert.SerializeObject(encabezados[i]).ToLower();

                    /*//----------------------- Remover Id

                    var jArr = JArray.Parse("[" + json + "]");

                    jArr.Descendants().OfType<JProperty>()
                                      .Where(p => p.Name.Equals("id"))
                                      .ToList()
                                      .ForEach(att => att.Remove());





                    json = jArr.ToString().Substring(1, jArr.ToString().Length - 2); ;

                    //------------------------*/

                    

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    


                    HttpResponseMessage response = null;
                    if (!isNewItem)
                    {
                        response = await client.PutAsync(uri, content);
                    }
                    else
                    {
                        response = await client.PostAsync(uri, content);
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine(@"				Item successfully saved.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"				HUBO UN ERROR {0}", ex.Message);
                }

            }
        }




        /// <summary>
        /// Función para enviar la información de un producto tomado para el inventario en formato JSON
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="IP_URL"></param>
        /// <param name="isNewItem"></param>
        /// <returns></returns>
        public async Task SendProducto(Ta_inventario_producto_copy producto, string IP_URL, bool isNewItem = true)
        {

            var uri = new Uri(string.Format(IP_URL, string.Empty));


            try

            {
                //Convertir a formato JSON en minúsculas
                var json = JsonConvert.SerializeObject(producto).ToLower();

                //----------------------- Remover Id
                //cambio Franklyn Tinoco
               // var jArr = JArray.Parse(json);

              //  jArr.Descendants().OfType<JProperty>()
              //                   .Where(p => p.Name == "id")
              //                   .ToList()
              //                   .ForEach(att => att.Remove());



               // json = jArr.ToString();

                //------------------------

                var content = new StringContent(json, Encoding.UTF8, "application/json");




                HttpResponseMessage response = null;
                if (!isNewItem)
                {
                    response = await client.PutAsync(uri, content);
                }
                else
                {
                    response = await client.PostAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                   Debug.WriteLine(@"				Item successfully saved.");
                }
                }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				HUBO UN ERROR {0}", ex.Message);
            }

            

        }



        /// <summary>
        /// Función para enviar la información de una lista de productos tomados para el inventario en formato JSON
        /// </summary>
        /// <param name="productos"></param>
        /// <param name="IP_URL"></param>
        /// <param name="pagina"> Diseñado para trabajar con una instancia de PaginaEnviarArchivo </param>
        /// <param name="isNewItem"></param>
        /// <returns></returns>
        public async Task SendProductoList(List<Ta_inventario_producto_copy> productos, string IP_URL,PaginaEnviarArchivo pagina, bool isNewItem = true)
        {

            IP_URL = "http://173.173.26.249:8082/ws_inventario?99";

            var uri = new Uri(string.Format(IP_URL, string.Empty));
            int N = productos.Count;


            

            //Uri uri2;
            for (int i = 0; i < N; i++)
            {
                try
                {

                    //Convertir a formato JSON en minúsculas
                    var json = JsonConvert.SerializeObject(productos[i]).ToLower();

                    //---------------------- Remover Id

                   // var jArr = JArray.Parse("[" + json + "]");

                   // jArr.Descendants().OfType<JProperty>()
                   //                   .Where(p => p.Name.Equals("id"))
                   //                   .ToList()
                   //                   .ForEach(att => att.Remove());





                    //json = jArr.ToString().Substring(1, jArr.ToString().Length - 2); ;

                    //------------------------


                    Console.WriteLine(json);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    // var content = new StringContent(json, Encoding.UTF8, "application/json");

                    Console.WriteLine(content);

                    HttpResponseMessage response = null;
                    if (!isNewItem)
                    {
                        //codigo Franklyn Tinoco

                        response = await client.PutAsync(uri, content);
                        
                    }
                    else
                    {
                        //response = await client.PutAsync(uri, content);
                       //response = await client.PostAsync(uri, content);
                        
                        response = await client.PostAsync(uri, content);
                        

                        var result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    }

                    if (response.IsSuccessStatusCode)
                    {

                        Debug.WriteLine(@"				Item successfully saved.");
                        
                        //Aux = Login.Ta_inventario_producto_copyDatabase.GetTa_inventario_producto_copy(productos[i].Id)[0];
                        //Console.WriteLine("Producto numero "+ i);
                        

                    }



                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"				HUBO UN ERROR {0}", ex.Message);
                    pagina.Display_Error_Carga(ex.Message);
                    break;
                }
                if (i == N - 1)
                {
                    pagina.Display_Carga_Completa();
                }
            }

            


        }

        /// <summary>
        /// Función para eliminar producto del servicio
        /// </summary>
        /// <param name="IP_URL"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteProducto( string IP_URL, string id)
        {
            
            var uri = new Uri(string.Format(IP_URL, id));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Item successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }






    }
}