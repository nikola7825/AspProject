using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Responses;
using Application.Searches;

namespace WebMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IAddCategoryCommand _addCategory;
        private readonly IGetCategoriesCommand _getCategories;
        private readonly IGetCategoryCommand _getCategory;
        private readonly IEditCategoryCommand _editCategory;
        private readonly IDeleteCategoryCommand _deleteCategory;

        public CategoriesController(IAddCategoryCommand addCategory,
                                    IGetCategoriesCommand getCategories,
                                    IGetCategoryCommand getCategory,
                                    IEditCategoryCommand editCategory,
                                    IDeleteCategoryCommand deleteCategory)
        {
            _addCategory = addCategory;
            _getCategories = getCategories;
            _getCategory = getCategory;
            _editCategory = editCategory;
            _deleteCategory = deleteCategory;
        }


        // GET: Categories
        public ActionResult Index(string sortOrder, string searchString, CategoryQuery query)
        {
            ViewBag.CategorySortParam = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;

            var categoryList = _getCategories.Execute(query);
            return View(categoryList);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var dto = _getCategory.Execute(id);
                return View(dto);
            }
            catch (Exception)
            {
                return View();
            }
        }
        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                // TODO: Add insert logic here
                _addCategory.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch(EntityAlreadyExistsException)
            {
                TempData["error"] = "Category with the same name already exists.";
            }
            catch(Exception)
            {
                TempData["error"] = "Some error occurred. Please try again.";
            }

            return View();
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _getCategory.Execute(id);
                return View(dto);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _editCategory.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException)
            {
                TempData["error"] = "Category with the same name already exist.";
                return View(dto);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteCategory.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "Some error occurred. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}