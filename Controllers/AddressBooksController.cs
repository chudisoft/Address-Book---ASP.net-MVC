using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Address_Book.Data;
using Address_Book.Models;

namespace Address_Book.Controllers
{
    public class AddressBooksController : Controller
    {
        private DBContext db = new DBContext();

        // GET: AddressBook
        public async Task<ActionResult> Index()
        {
            return View(await db.Address_Book.ToListAsync());
        }

        // GET: AddressBook/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressBook address_Book = await db.Address_Book.FindAsync(id);
            if (address_Book == null)
            {
                return HttpNotFound();
            }
            return View(address_Book);
        }

        // GET: AddressBook/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddressBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddressBook address_Book)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                    if (Request.Files["file"] != null)
                        if (Request.Files["file"].ContentLength > 0)
                {
                    if (!System.IO.Directory.Exists(Server.MapPath("~/Uploads")))
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads"));

                    if (!System.IO.Directory.Exists(Server.MapPath("~/Uploads/Contacts")))
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads/Contacts"));

                    address_Book.FileType = Request.Files[0].ContentType;
                    string ImageName = $"~/Uploads/Contacts/{Guid.NewGuid().ToString()}" +
                        Request.Files[0].FileName.Substring(Request.Files[0].FileName.LastIndexOf("."));
                    address_Book.FileName = Request.Files[0].FileName;
                    address_Book.FilePath = ImageName;
                    Request.Files[0].SaveAs(Server.MapPath(ImageName));
                }
                address_Book.CreationDate = DateTime.Now;

                db.Address_Book.Add(address_Book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(address_Book);
        }

        // GET: AddressBook/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressBook address_Book = await db.Address_Book.FindAsync(id);
            if (address_Book == null)
            {
                return HttpNotFound();
            }
            return View(address_Book);
        }

        // POST: AddressBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AddressBook address_Book)
        {
            if (ModelState.IsValid)
            {
                AddressBook addressBook = db.Address_Book.Find(address_Book.Id);

                if (Request.Files.Count > 0)
                    if (Request.Files["file"] != null)
                        if (Request.Files["file"].ContentLength > 0)
                {
                    if (!System.IO.Directory.Exists(Server.MapPath("~/Uploads")))
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads"));

                    if (!System.IO.Directory.Exists(Server.MapPath("~/Uploads/Contacts")))
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads/Contacts"));


                    if (!string.IsNullOrEmpty(addressBook.FilePath))
                    {
                        if (System.IO.File.Exists(Server.MapPath(addressBook.FilePath)))
                            System.IO.File.Delete(Server.MapPath(addressBook.FilePath));
                    }

                    addressBook.FileType = Request.Files[0].ContentType;
                    string ImageName = $"~/Uploads/Contacts/{Guid.NewGuid().ToString()}" +
                        Request.Files[0].FileName.Substring(Request.Files[0].FileName.LastIndexOf("."));
                    addressBook.FileName = Request.Files[0].FileName;
                    addressBook.FilePath = ImageName;
                    Request.Files[0].SaveAs(Server.MapPath(ImageName));
                }

                addressBook.OfficeNumber = address_Book.OfficeNumber;
                addressBook.Name = address_Book.Name;
                addressBook.MobileNumber = address_Book.MobileNumber;
                addressBook.Email = address_Book.Email;
                addressBook.Hometown = address_Book.Hometown;

                db.Entry(addressBook).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(address_Book);
        }

        // GET: AddressBook/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressBook address_Book = await db.Address_Book.FindAsync(id);
            if (address_Book == null)
            {
                return HttpNotFound();
            }
            return View(address_Book);
        }

        // POST: AddressBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AddressBook address_Book = await db.Address_Book.FindAsync(id);


            if (!string.IsNullOrEmpty(address_Book.FilePath))
            {
                if (System.IO.File.Exists(Server.MapPath(address_Book.FilePath)))
                    System.IO.File.Delete(Server.MapPath(address_Book.FilePath));
            }


            db.Address_Book.Remove(address_Book);
            await db.SaveChangesAsync();
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
