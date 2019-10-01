using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Employee.Domain.Entities;
using Employee.Infrastructure;

using Employee.Web.ViewModels;

namespace Employee.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public SalesController(UnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;
        }
        
        public ActionResult Index()
        {
            var salesOrders = _unitOfWork.SalesOrders.All();
            return View(salesOrders.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SalesOrder salesOrder = _unitOfWork.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            var sovm = new SalesOrderViewModel
            {
                SalesOrderId = salesOrder.Id,
                CustomerName = salesOrder.CustomerName,
                PONumber = salesOrder.PONumber,
                MessageToClient = "I originated from the ViewModel."
            };


            return View(sovm);
        }
                
        public ActionResult Create()
        {
            var sovm = new SalesOrderViewModel();
            return View(sovm);
        }

        public async Task<JsonResult> SaveAsync(SalesOrderViewModel salesOrderViewModel)
        {
            var salesOrder = new SalesOrder
            {
                CustomerName = salesOrderViewModel.CustomerName,
                PONumber = salesOrderViewModel.PONumber
            };

            _unitOfWork.SalesOrders.Insert(salesOrder);
            await _unitOfWork.SaveAsync();            

            salesOrderViewModel.MessageToClient = $"{salesOrderViewModel.CustomerName}'s order has been saved successfully!";

            return Json(new { salesOrderViewModel });
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _unitOfWork.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        } 
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _unitOfWork.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        }       

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
