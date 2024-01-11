using DepartemenIT.Models;
using DepartemenIT.Repository;
using DepartemenIT.Utils;
using DepartemenIT.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DepartemenIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository repository;
        public ProductController(ProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                var get = repository.GetAll();

                int total = get.Count();
                if (total == 0)
                {
                    return ResponseFormatter.CreateResponse(HttpStatusCode.NotFound, "Tidak ada data.");
                }

                return ResponseFormatter.CreateResponse(HttpStatusCode.OK, total + " data berhasil diambil.", get);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{ID}")]
        public virtual ActionResult Get(int ID)
        {
            try
            {
                var entity = repository.GetById(ID);
                if (entity == null)
                {
                    return ResponseFormatter.CreateResponse(HttpStatusCode.NotFound, "Data tidak ditemukan.");
                }

                return ResponseFormatter.CreateResponse(HttpStatusCode.OK, "Data berhasil ditemukan.", entity);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public virtual ActionResult Insert(ProductVM product)
        {
            try
            {
                var entity = repository.Insert(product);

                return ResponseFormatter.CreateResponse(HttpStatusCode.OK, "Data berhasil ditambahkan.");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public virtual ActionResult Update(Product product)
        {
            try
            {
                var entity = repository.Update(product);

                return ResponseFormatter.CreateResponse(HttpStatusCode.OK, "Data berhasil diubah.");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        public virtual ActionResult Delete(int ID)
        {
            try
            {
                var delete = repository.Delete(ID);

                return ResponseFormatter.CreateResponse(HttpStatusCode.OK, "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
