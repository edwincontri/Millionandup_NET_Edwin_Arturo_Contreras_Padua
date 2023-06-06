using Api_Properties.Interfaces;
using Api_Properties.Models;
using Api_Properties.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Properties.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class PropertyController : Controller
    {
        private readonly IDatabaseService _database;        

        public PropertyController(IDatabaseService database)
        {           
            _database = database;
        }

        [HttpPost("CreateProperty")]
        public async Task<IActionResult> CreateProperty(Property property)
        {
            try
            {
                if (property != null)
                {
                    await _database.InsertProperty(property);
                    return HttpResponseHelper.Response(StatusCodes.Status201Created, new ResponseResult() { IsError = false, Message = "created property" });
                }
                else
                {
                    return HttpResponseHelper.Response(StatusCodes.Status400BadRequest, new ResponseResult() { IsError = true, Message = "No information to enter" });
                }
            }
            catch (Exception ex)
            {
                return HttpResponseHelper.Response(StatusCodes.Status500InternalServerError, new ResponseResult() { IsError = true, Message = ex.Message });
            }
        }

        [HttpPost("CreateImageProperty")]
        public async Task<IActionResult> CreateImageProperty(PropertyImage property)
        {
            try
            {
                if (property != null)
                {
                    await _database.InsertImageProperty(property);
                    return HttpResponseHelper.Response(StatusCodes.Status201Created, new ResponseResult() { IsError = false, Message = "created image property" });
                }
                else
                {
                    return HttpResponseHelper.Response(StatusCodes.Status400BadRequest, new ResponseResult() { IsError = true, Message = "No information to enter" });
                }
            }
            catch (Exception ex)
            {
                return HttpResponseHelper.Response(StatusCodes.Status500InternalServerError, new ResponseResult() { IsError = true, Message = ex.Message });
            }
        }

        [HttpPut("ChangePrice")]
        public async Task<IActionResult> PriceProperty(UpdatePrice property)
        {
            try
            {
                if (property != null)
                {
                    var existIdProperty = await _database.ExistsProperty(property.idProperty);

                    if (existIdProperty)
                    {
                        await _database.UpdatePriceProperty(property);
                        return HttpResponseHelper.Response(StatusCodes.Status201Created, new ResponseResult() { IsError = false, Message = "Update price property" });
                    }
                    else
                    {
                        return HttpResponseHelper.Response(StatusCodes.Status400BadRequest, new ResponseResult() { IsError = true, Message = "There is no information for the Id" });
                    }
                }
                else
                {
                    return HttpResponseHelper.Response(StatusCodes.Status400BadRequest, new ResponseResult() { IsError = true, Message = "No information to enter" });
                }
            }
            catch (Exception ex)
            {
                return HttpResponseHelper.Response(StatusCodes.Status500InternalServerError, new ResponseResult() { IsError = true, Message = ex.Message });
            }
        }

        [HttpPut("UpdateProperty")]
        public async Task<IActionResult> UpdatePropertyAll(Property property)
        {
            try
            {
                if (property != null)
                {
                    var existIdProperty = await _database.ExistsProperty(property.PkProperty);

                    if (existIdProperty)
                    {
                        await _database.UpdateAllProperty(property);
                        return HttpResponseHelper.Response(StatusCodes.Status201Created, new ResponseResult() { IsError = false, Message = "Update property" });
                    }
                    else
                    {
                        return HttpResponseHelper.Response(StatusCodes.Status400BadRequest, new ResponseResult() { IsError = true, Message = "There is no information for the Id" });
                    }
                }
                else
                {
                    return HttpResponseHelper.Response(StatusCodes.Status400BadRequest, new ResponseResult() { IsError = true, Message = "No information to enter" });
                }
            }
            catch (Exception ex)
            {
                return HttpResponseHelper.Response(StatusCodes.Status500InternalServerError, new ResponseResult() { IsError = true, Message = ex.Message });
            }
        }
        
        [HttpGet("ListNameProperty/{name}")]
        public async Task<IActionResult> ListProperty(string name)
        {
            try
            {
                List<Property> result = await _database.GetProperty(name);
                if(result.Any())
                    return HttpResponseHelper.Response(StatusCodes.Status200OK, result);

                return HttpResponseHelper.Response(StatusCodes.Status409Conflict, new ResponseResult() { IsError = true, Message = "There is no information for the record entered" });
            }
            catch(Exception ex)
            {
                return HttpResponseHelper.Response(StatusCodes.Status500InternalServerError, new ResponseResult() { IsError = true, Message = ex.Message });
            }            
        }        
    }
}
