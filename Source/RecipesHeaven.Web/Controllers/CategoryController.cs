﻿using RecipesHeaven.Data.Contracts;
using RecipesHeaven.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RecipesHeaven.Web.ViewModels.Category;
using RecipesHeaven.Web.ViewModels.Recipe;

namespace RecipesHeaven.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private ICategoriesServices categoryService;
        private IRecipesServices recipeService;


        public CategoryController(IRecipesHeavenData data, ICategoriesServices categoryService, IRecipesServices recipeService)
           : base(data)
        {
            this.categoryService = categoryService;
            this.recipeService = recipeService;
        }

        // GET: Category
        public ActionResult Category(int id, int pageIndex = 0, int pageSize = 10)
        {
            var model = new CategoryViewModel();
            var sourceCat = categoryService.GetCategoryById(id);

            model = Mapper.Map<Models.Category, CategoryViewModel>(sourceCat);            

            return View(model);
        }

        public ActionResult GetCategories()
        {
            var categories = categoryService.GetAllCategories()
                .AsQueryable()
                .Project()
                .To<CategoryViewModel>()
                .ToList();

            return PartialView("_Categories", categories);
        }
    }
}