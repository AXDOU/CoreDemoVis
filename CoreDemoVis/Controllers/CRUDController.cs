using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CfoDAL.DataEntity;
using CfoMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
namespace CoreDemoVis.Controllers
{
    public class CRUDController : Controller
    {
        private IUserService _userService;
        public CRUDController(IUserService userService)
        {
            this._userService = userService;
        }
        // GET: CRUD
        public ActionResult Index()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        // GET: CRUD/Details/5
        public ActionResult Details(int id)
        {
            var user = _userService.GetUser(id) ?? new CoreUser();
            return View(user);
        }

        // GET: CRUD/Create
        public ActionResult Create()
        {
            return View(new CoreUser());
        }

        // POST: CRUD/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var entity = new CoreUser
                {
                    FullName = collection["FullName"],
                    Phone = collection["Phone"],
                    Password = collection["Password"],
                    Email = collection["Email"],
                    Address = collection["Address"]
                };
                bool result = _userService.Insert(entity);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CRUD/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _userService.GetUser(id) ?? new CoreUser();
            return View(user);
        }

        // POST: CRUD/Edit/5
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit(int id, IFormCollection collection)
         {
             try
             {
                 CoreUser coreUser = new CoreUser();
                 //PropertyInfo[] properties = typeof(CoreUser).GetProperties();
                 //foreach (var item in properties)
                 //{
                 //    var first = collection.FirstOrDefault(x => x.Key == item.Name);
                 //    collection.TryGetValue(item.Name, out Microsoft.Extensions.Primitives.StringValues obj);
                 //    if (item.Name.Equals("ID"))
                 //        item.SetValue(coreUser, id, null);
                 //    else if (first.Key != null)
                 //    {
                 //        if (item.MemberType.Equals(typeof(string)))
                 //            item.SetValue(coreUser, first.Value.ToString(), null);
                 //    }
                 //}
                 var entity = new CoreUser
                 {
                     ID = id,
                     Password = collection["Password"],
                     FullName = collection["FullName"],
                     Phone = collection["Phone"],
                     Email = collection["Email"],
                     Address = collection["Address"]
                 };
                 _userService.Update(coreUser);
                 //TODO: Add update logic here

                 return RedirectToAction(nameof(Index));
             }
             catch (Exception ex)
             {
                 return View();
             }
         }
         */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Phone,ID,FullName,Password,Address")] CoreUser coreUser)
        {
            try
            {
                var entity = _userService.GetUser(coreUser.ID);
                if (ModelState.IsValid && entity != null)
                {
                    PropertyInfo[] propertyInfos = typeof(CoreUser).GetProperties();
                    foreach (var item in propertyInfos)
                    {
                        item.SetValue(entity, item.GetValue(coreUser, null), null);
                    }
                    await _userService.Edit(entity);
                    return RedirectToAction(nameof(Index));
                }
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: CRUD/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        [HttpGet]
        // POST: CRUD/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _userService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}