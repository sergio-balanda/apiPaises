using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Route("api/PaisApi/{PaisId}/Provincias")]
    [ApiController]
    public class ProvinciasApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProvinciasApiController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> Get(int paisId)
        {
            return context.Provincias
                .Where(m=>m.PaisId==paisId)
                .ToList();
        }

        [HttpGet("{id}", Name = "provinciaById")]
        public IActionResult GetById(int id)
        {
            var provincias = context.Provincias.FirstOrDefault(m => m.Id == id);
            if (provincias == null)
            {
                return NotFound();
            }
            return Ok(provincias);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                context.Provincias.Add(provincia);
                context.SaveChanges();
                return new CreatedAtRouteResult("provinciaById", new { id = provincia.Id }, provincia);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Provincia provincia, int id)
        {
            if (provincia.Id != id)
            {
                return BadRequest();
            }
            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(m => m.Id == id);
            if (provincia == null)
            {
                return NotFound();
            }
            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return Ok();
        }
    }
}