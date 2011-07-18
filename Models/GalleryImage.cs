using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ketchup;
using Ketchup.Async;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace GownGallery
{
	[Serializable]
	public class GalleryImage
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public string Name { get; set; }
		public string FullPath { get; set; }
		public string Description { get; set; }
		public string ImageData { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }

		public GalleryImage ()
		{
		}
		
		public static List<int> QueryList()
		{
			var results = new List<int>();
			for(var i = 1; i<=12; i++)
			{
				results.Add(i);
			}

			//pretend we're actually getting this from a SQL Server, is this number correct?
			//who knows, it's fake.
			Thread.Sleep(300);
			return results;
		}
		
		public static GalleryImage QueryOne(int id)
		{
			var directory = HttpContext.Current.Server.MapPath("~/content/img/thumbs/");
			var s = id.ToString();
			s = s.Length == 1 ? "00" + s : s.Length == 2 ? "0" + s : s;
			var fullPath = directory + "thumbnail_" + s + ".jpg";
			var datetime = new DateTime(2011,07,11);
			
			var galleryImage =  new GalleryImage {
					Id = id,
					Name = Path.GetFileName(fullPath),
					FullPath = fullPath,
					Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
					DateCreated = datetime,
					DateModified = datetime,
				};
			
			Thread.Sleep(100);
			return galleryImage;
		}
		
		public string ProcessDataFor20ms()
		{
			var image = Image.FromFile(FullPath);
			var format = System.Drawing.Imaging.ImageFormat.Jpeg;
			var imageData = "";
			
			using (MemoryStream ms = new MemoryStream())
	  		{
	    		image.Save(ms, format);
	    		var imageBytes = ms.ToArray();
				imageData = "data:image/jpg;base64," + Convert.ToBase64String(imageBytes);
  			}
			
			Thread.Sleep(20);
			return imageData;
		}

	}
}

