using DepartemenIT.Models;
using DepartemenIT.ViewModel;
using DepartemenIT.Repository;
using DepartemenIT.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DepartemenIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleRepository repository;
        public SaleController(SaleRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Sale>> Get()
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
                var entity = repository.Get(ID);
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
        public virtual ActionResult Insert(SaleVM sale)
        {
            try
            {
                var entity = repository.Insert(sale);

                return ResponseFormatter.CreateResponse(HttpStatusCode.OK, "Data berhasil ditambahkan.");
            }
            catch (Exception ex)
            {
                return ResponseFormatter.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public virtual ActionResult Update(UpdateSaleVM sale)
        {
            try
            {
                var entity = repository.Update(sale);

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
