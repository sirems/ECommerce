using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using ECommerce.DataAccess.IMainRepository;
using ECommerce.Models.DbModels;
using ECommerce.Models.ViewModels;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ProjectConstant.Role_Admin)]
    public class ProductController : Controller
    {

        #region Variables
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;     //www de erişim sağlar root a erişim özelliği resim ekleme!
        //AmazonS3Client client;
        //public BasicAWSCredentials credentials = new BasicAWSCredentials(AWSSettings.AccessKeyID, AWSSettings.SecretKeyID);
        //string bucketName = "mytestudemybucketapp1";

        #endregion

        #region CTOR
        public ProductController(IUnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;
            //client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.EUWest1);

        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region API CALLS
        public IActionResult GetAll()
        {
            var allObj = _uow.Product.GetAll(includeProperties: "Category");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _uow.Product.Get(id);
            if (deleteData == null)
                return Json(new { success = false, message = "Data Not Found!" });

            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, deleteData.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _uow.Product.Remove(deleteData);
            _uow.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });
        }

        #endregion

        /// <summary>
        /// Create Or Update Get Method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _uow.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _uow.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
                return View(productVM);

            productVM.Product = _uow.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null)
                return NotFound();
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (productVM.Product.ImageUrl != null)
                    {
                        var imageUrl = productVM.Product.ImageUrl;
                        var imagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    };

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    productVM.Product.ImageUrl = @"images\products\" + fileName + extension;

                }
                else
                {
                    if (productVM.Product.Id != 0)
                    {
                        var productData = _uow.Product.Get(productVM.Product.Id);
                        productVM.Product.ImageUrl = productData.ImageUrl;
                    }
                }
                if (productVM.Product.Id == 0)
                {
                    //create
                    _uow.Product.Add(productVM.Product);
                }
                else
                {
                    //update
                    _uow.Product.Update(productVM.Product);
                }
                _uow.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _uow.Category.GetAll().Select(a => new SelectListItem
                {
                    Text = a.CategoryName,
                    Value = a.Id.ToString()
                });

                productVM.CoverTypeList = _uow.CoverType.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                });

                if (productVM.Product.Id != 0)
                {
                    productVM.Product = _uow.Product.Get(productVM.Product.Id);
                }
            }
            return View(productVM.Product);
        }
    }
}