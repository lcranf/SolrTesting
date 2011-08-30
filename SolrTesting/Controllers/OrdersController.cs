using System.Linq;
using System.Web.Mvc;
using SolrNet;
using SolrNet.Commands.Parameters;

namespace SolrTesting.Controllers
{   
    public class OrdersController : Controller
    {
        private readonly ISolrOperations<SolrTesting.Models.Order> _repository;

        public OrdersController(ISolrOperations<SolrTesting.Models.Order> repository)
        {
            _repository = repository;
        }

        //
        // GET: /Orders/

        public ViewResult Index()
        {
            return View(_repository.Query(SolrQuery.All,
                new[] { new SortOrder("Id", Order.ASC) }).ToList());
        }

        //
        // GET: /Orders/Details/5

        public ViewResult Details(int id)
        {
            var order = _repository.Query(new SolrQueryByField("Id", id.ToString())).FirstOrDefault();
            return View(order);
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(SolrTesting.Models.Order order)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(order);
                _repository.Commit();
                return RedirectToAction("Index");  
            }

            return View(order);
        }
        
        //
        // GET: /Orders/Edit/5
 
        public ActionResult Edit(int id)
        {
            var order = _repository.Query(new SolrQueryByField("Id", id.ToString())).FirstOrDefault();

            return View(order);
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(SolrTesting.Models.Order order)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(order);
                _repository.Commit();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        //
        // GET: /Orders/Delete/5
 
        public ActionResult Delete(int id)
        {
            var order = _repository.Query(new SolrQueryByField("Id", id.ToString())).FirstOrDefault();
            return View(order);
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id.ToString());
            _repository.Commit();

            return RedirectToAction("Index");
        }
    }
}