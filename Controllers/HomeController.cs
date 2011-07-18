using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace GownGallery.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var sw = new Stopwatch();
			sw.Start();

			var ids = GalleryImage.QueryList();
			var galleryImages = new List<GalleryImage>();

			foreach(var id in ids)
			{
				var galleryImage = GalleryImage.QueryOne(id);

				//process the data after retrieving it from the DB
				galleryImage.ImageData = galleryImage.ProcessDataFor20ms();
				
				//add it to the model collection
				galleryImages.Add(galleryImage);
			}
			
			sw.Stop();
			ViewData["Time"] = sw.ElapsedMilliseconds;
			return View("Grid", galleryImages);
		}
	}
}

