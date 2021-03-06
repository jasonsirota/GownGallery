using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Ketchup;
using Ketchup.Commands;

namespace GownGallery
{
	public class HybridController : Controller
	{
		public ActionResult Index()
		{
			var sw = new Stopwatch();
			sw.Start();

			//create the ketchup client
			var bucket = new KetchupClient("localhost",11211).DefaultBucket;
			var keylist = "gowns-hybrid";

			//check to see if the list of ids is in the cache:
			
			var ids = bucket.Get(keylist) as List<int>;
			if(ids == null)
			{
				ids = GalleryImage.QueryList();
				bucket.Set(keylist, ids);
			}
			
			//create the model
			var galleryImages = new List<GalleryImage>();
			foreach(var id in ids)
			{
				var key = keylist + "-" + id;
				
				var galleryImage = bucket.Get(key) as GalleryImage;
				if(galleryImage == null)
				{
					galleryImage = GalleryImage.QueryOne(id);
					bucket.Set(key, galleryImage);
				}
				
				//process the data after retrieving it from the DB or cache
				galleryImage.ImageData = galleryImage.ProcessDataFor20ms();

				//add it to the model collection
				galleryImages.Add(galleryImage);
				
				//why do we add it to the model collection after instead of just caching processed data and the whole collection?
				//1. limits in memcached size, not as big a deal anymore
				//2. may want to use this cache object later, could still cache both
				//3. not as cool for my asynchronous demo.
			}
			
			sw.Stop();
			ViewData["Time"] = sw.ElapsedMilliseconds;
			bucket.Flush();
			return View("Grid", galleryImages);
		}	
	}
}

