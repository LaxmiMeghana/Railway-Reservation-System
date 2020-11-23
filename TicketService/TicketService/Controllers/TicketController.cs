using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketService.Models;
using TicketService.Repository;


namespace TicketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TicketController));
        private readonly ITicketRepository _ITicketRepository;
        public TicketController(ITicketRepository Repo)
        {
            this._ITicketRepository = Repo;
        }

    

    [HttpGet]

    public IActionResult GetAllTickets()
    {
        try
        {
            _log4net.Info("Http GET is accesed");
            IEnumerable<Ticket> Tlist = _ITicketRepository.GetAll();
            return Ok(Tlist);
        }
        catch
        {
            _log4net.Error("Http GET is requested");
            return new NoContentResult();

        }
    }
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            try
            {
                _log4net.Info("Http GET is accessed"+id);
                var Tlist = _ITicketRepository.GetById(id);
                return new OkObjectResult(Tlist);

            }
            catch
            {
                _log4net.Error("Error in get by id Request");
                return new NoContentResult();
                
            }
        }

    }

}
