using EMSSP.DAL;
using EMSSP.Models;
using Microsoft.AspNetCore.Mvc;

namespace EMSSP.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _dal;

        public EmployeeController(Employee_DAL dal)
        {
            _dal = dal;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = " Model data is invalid";
                }
                bool result = _dal.Insert(model);
                if (!result)
                {
                    TempData["errorMessage"] = " Unable to save the data";
                    return View();
                }
                TempData["successMessage"] = " Employee details saved";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            try
            {
                Employee employee = _dal.GetById(id);
                if (employee.EmployeeId == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with id: {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = " Employee data is invalid";
                    return View();
                }
                bool result = _dal.Update(model);
                if (!result)
                {
                    TempData["errorMessage"] = " Unable to update the data";
                    return View();
                }
                TempData["successMessage"] = " Employee details updated";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Employee employee = _dal.GetById(id);
                if (employee.EmployeeId == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with id: {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Employee model)
        {
            try
            {
                bool result = _dal.Delete(model.EmployeeId);
                if (!result)
                {
                    TempData["errorMessage"] = " Unable to delete the data";
                    return View();
                }
                TempData["successMessage"] = " Employee details deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int id=0)
        {
            try
            {
                if(id == 0)
                {
                    return View(new Employee());
                }
                Employee employee = _dal.GetById(id);
                if (employee.EmployeeId == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with id: {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult CreateOrEdit(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = " Employee data is invalid";
                    return View();
                }
                bool result = _dal.CreateOrEdit(model);
                if (!result)
                {
                    if(model.EmployeeId == 0) 
                    {
                        TempData["errorMessage"] = " Unable to insert the data";
                    }
                    else 
                    {
                        TempData["errorMessage"] = " Unable to update the data";
                    }
                    return View();
                }
                if(model.EmployeeId == 0) 
                {
                    TempData["successMessage"] = " Employee details inserted";
                }
                else
                {
                    TempData["successMessage"] = " Employee details updated";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
