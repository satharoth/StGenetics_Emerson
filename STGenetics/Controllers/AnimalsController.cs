using Microsoft.AspNetCore.Mvc;
using STGenetics.DTO;
using STGenetics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STGenetics.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {

        private readonly IAnimals _animalsRepo;
        private readonly ILogin _loginRepo;
        private readonly IAnimalPruchase _animalsPur;

        public AnimalsController(IAnimals animalsRepo, ILogin loginRepo, IAnimalPruchase animalsPurs)
        {
            _animalsRepo = animalsRepo;
            _loginRepo = loginRepo;
            _animalsPur = animalsPurs;
        }

        // GET: api/<AnimalsController>
        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromHeader] string token)
        {
            try
            {
                var user = _loginRepo.VerifyToken(token);
                if (user.Result != null)
                {
                    var animals = await _animalsRepo.GetAnimals();
                    return Ok(animals);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    return Unauthorized();
                }
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<AnimalsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromHeader] string token)
        {
            try
            {
                var user = _loginRepo.VerifyToken(token);
                if (user.Result != null)
                {
                    var animal = await _animalsRepo.GetAnimal(id);
                    return Ok(animal);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<AnimalsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Animals animalsPost, [FromHeader] string token)
        {
            try
            {
                var user = _loginRepo.VerifyToken(token);
                if (user.Result != null)
                {
                    var animals = await _animalsRepo.CreateAnimals(animalsPost);
                    return Ok(animals);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<AnimalsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Animals animalsPost, [FromHeader] string token)
        {
            try
            {
                var user = _loginRepo.VerifyToken(token);
                if (user.Result != null)
                {
                    var animals = await _animalsRepo.UpdateAnimals(id, animalsPost);
                    return Ok(animals);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<AnimalsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromHeader] string token)
        {
            try
            {
                var user = _loginRepo.VerifyToken(token);
                if (user.Result != null)
                {
                    var animals = await _animalsRepo.DeleteAnimals(id);
                    return Ok(animals);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{name}/{sex}/{state}")]
        public async Task<IActionResult> GetAnimalsByParameter(string name, int sex, int state, [FromHeader] string token)
        {
            try
            {
                var user = _loginRepo.VerifyToken(token);
                if (user.Result != null)
                {
                    var animalsPost = new AnimalsForParameter();
                    animalsPost.Name = name;
                    animalsPost.Sex = sex;
                    animalsPost.Status = state;
                    var result = await _animalsRepo.GetAnimalsByParameter(animalsPost);
                    return Ok(result);
                }
                else
                {
                    return Unauthorized();
                }


            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        // POST api/<AnimalsController>
        [HttpPost]
        [Route("PostAnimalPruchase")]
        public async Task<IActionResult> PostAnimalPruchase([FromBody] AnimalPruchase animalsPur, [FromHeader] string token)
        {
            try
            {
                var user = _loginRepo.VerifyToken(token);
                if (user.Result != null)
                {
                    var animalspur = await _animalsPur.CreateAnimalsPurchase(animalsPur);
                    return Ok(animalspur);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



    }
}
