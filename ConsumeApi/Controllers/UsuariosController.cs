using ConsumeApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumeApi.Controllers
{
    public class UsuariosController : Controller
    {
        //URL API
        string baseUrl = "http://localhost:63170/";
        HttpClient cliente = new HttpClient();
        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            List<Usuario> EmpInfo = new List<Usuario>();
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //Llamar a todos los usuarios con el HttpClient
            HttpResponseMessage res = await cliente.GetAsync("api/usuarios/");
            if (res.IsSuccessStatusCode)
            {
                //Asignamos los datos
                var EmpResponse = res.Content.ReadAsStringAsync().Result;
                //Deserializar API y mostrar los datos
                EmpInfo = JsonConvert.DeserializeObject<List<Usuario>>(EmpResponse);
                
            }
            cliente.Dispose();
            return View(EmpInfo);
        }
        //Crear Usuario
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Usuario usuario)
        {
            cliente.BaseAddress = new Uri("http://localhost:63170/api/Usuarios");
            var postTask = cliente.PostAsJsonAsync<Usuario>("usuarios", usuario);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Error, contacta con el administrador.");
            cliente.Dispose();
            return View(usuario);
        }

        //Modifica Usuario
        //extraer el usuario a modificar
        public ActionResult Editar(int id)
        {
            Usuario usuario = null;
            cliente.BaseAddress = new Uri(baseUrl);
            var responseTask = cliente.GetAsync("api/usuarios/" + id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Usuario>();
                readTask.Wait();
                usuario = readTask.Result;
            }
            cliente.Dispose();
            return View(usuario);
        }

        //modificar usuario
        [HttpPost]
        public ActionResult Editar(Usuario usuario)
        {
            cliente.BaseAddress = new Uri(baseUrl);
            var putTask = cliente.PutAsJsonAsync($"api/usuarios/{usuario.ID}", usuario);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            cliente.Dispose();
            return RedirectToAction("Index");
        }

        //Eliminar Usuario
        //obtener usuario
        public ActionResult Eliminar(int id)
        {
            Usuario usuario = null;
            cliente.BaseAddress = new Uri(baseUrl);
            var responseTask = cliente.GetAsync("api/usuarios/" + id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Usuario>();
                readTask.Wait();
                usuario = readTask.Result;
            }
            cliente.Dispose();
            return View(usuario);
        }
       
        //borrar usuario
        [HttpPost]
        public ActionResult Eliminar(Usuario usuario, int id)
        {
            cliente.BaseAddress = new Uri(baseUrl);
            var deleteTask = cliente.DeleteAsync($"api/usuarios/" + id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            cliente.Dispose();
            return View(usuario);
        }
    }

    
}