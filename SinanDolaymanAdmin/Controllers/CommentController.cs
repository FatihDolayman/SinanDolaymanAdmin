using DAL;
using Entities;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SinanDolaymanAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CommentController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Comment
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);



            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }





        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
