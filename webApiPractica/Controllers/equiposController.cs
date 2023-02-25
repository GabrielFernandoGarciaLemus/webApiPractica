using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get() {
            List<equipos> listadoequipo = (from e in _equiposContexto.equipos
                                            select e).ToList();

            if (listadoequipo.Count == 0)
            {
                return NotFound();

            }
            return Ok(listadoequipo);
            
        }

        [HttpGet]
        [Route("getbyid/{id}")]

        public IActionResult GetById(int id)
        {
            //El signo ? es porque acepta nulos

            equipos? equipo = (from e in _equiposContexto.equipos
                          where e.id_auto == id
                          select e).FirstOrDefault();

            if (equipo == null) { 
            return NotFound();
            } return Ok(equipo);


        }

        [HttpGet]
        [Route("find/")]

        public IActionResult GetByName(string filtro)
        {

           List <equipos> equipo = (from e in _equiposContexto.equipos
                                   where e.nombre.Contains(filtro)
                                   select e).ToList();

            if (equipo == null){
                return NotFound();
            }
            return Ok(equipo);
        }
        // Se usa Post para crear un registro

        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody]equipos equipo)
        {
            try {
                //Agrega en el contexto
                _equiposContexto.equipos.Add(equipo);
                //Agrega en la base de datos
                _equiposContexto.SaveChanges();
                return Ok(equipo);

            } catch(Exception ex){

                return BadRequest(ex.Message);  
            }
        }

        [HttpPut]
        [Route("update/{id}")]

        public IActionResult Actualizar(int id, [FromBody] equipos equipo_Actualizar)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_auto == id
                               select e).FirstOrDefault();

            if (equipo == null) return NotFound();

            equipo.nombre = equipo_Actualizar.nombre;
            equipo.modelo = equipo_Actualizar.modelo;

            _equiposContexto.Entry(equipo).State = EntityState.Modified;

            return Ok(equipo);


        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult Delete( int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_auto == id
                               select e).FirstOrDefault();

            if (equipo == null) return NotFound();



            _equiposContexto.equipos.Attach(equipo);
            _equiposContexto.equipos.Remove(equipo);
            _equiposContexto.SaveChanges();

            return Ok(equipo);  

        }
    }
}
