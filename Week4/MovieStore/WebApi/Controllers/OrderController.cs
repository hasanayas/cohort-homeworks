using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.Application.OrderOperations.Model;
using WebApi.Application.OrderOperations.Queries.GetByIdOrder;
using WebApi.Application.OrderOperations.Queries.GetListOrder;
using WebApi.DbOprations;

namespace WebApi.Controllers
{
    [Authorize] // Bu controller'a sadece oturumu açık olan kullanıcılar erişebilir.
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderController(IMovieStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        // Tüm siparişleri listeler.
        [HttpGet]
        public IActionResult GetLists()
        {
            GetListOrderQuery query = new GetListOrderQuery(_dbContext, _mapper);
            var response = query.Handle();

            return Ok(response);
        }

        // Belirli bir siparişi getirir.
        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            GetByIdOrderQuery query = new GetByIdOrderQuery(_dbContext, _mapper);
            query.OrderId = id;

            
            var response = query.Handle();

            return Ok(response);
        }

        // Yeni bir sipariş oluşturur.
        [HttpPost]
        public IActionResult Create([FromBody] CreateOrderModel model)
        {
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            command.Model = model;

          
            command.Handle();

            return Ok();
        }

        // Belirli bir siparişin bilgilerini günceller.
        [HttpPut("{Id}")]
        public IActionResult Update([FromBody] UpdateOrderModel model, int Id)
        {
            UpdateOrderCommand command = new UpdateOrderCommand(_dbContext, _mapper);
            command.Model = model;
            command.OrderId = Id;
            
            command.Handle();

            return Ok();
        }

        // Belirli bir siparişi siler.
        [HttpPut("softDelete/{Id}")]
        public IActionResult SoftDelete([FromRoute] int Id)
        {
            SoftDeleteOrderCommand command = new SoftDeleteOrderCommand(_dbContext);

            command.OrderId = Id;
            command.Handle();

            return Ok();
        }
    }
}
