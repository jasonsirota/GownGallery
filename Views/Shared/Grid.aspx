<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<GownGallery.GalleryImage>>" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" /> 
	<title>Modest Wedding Dresses - Wedding Dresses - Wedding Gowns</title>
    <meta name="keywords" content="modest Wedding Dresses, modest wedding dress, modest wedding gowns, modest wedding gown, modest bridal gowns, modest bridal gown, modest wedding dresses, modest wedding dresses.," /> 
	<meta name="description" content="modest Wedding Dresses - Find modest Wedding Gowns and modest wedding dresses on TheKnot.com. Browse through hundreds of wedding dresses and wedding gowns.," /> 
	<meta name="abstract" content="modest Wedding Dresses - Find modest Wedding Gowns and modest wedding dresses on TheKnot.com." /> 
</head>
<body>
	Elapsed Time: <%= ViewData["Time"] %> ms<br />
	<table border="1" cellpadding="5" cellspacing="0" >
		<tr>
			<%
				for(var i = 0; i < Model.Count; i++)
				{
					if(i % 4 == 0)
					{
					%>
		</tr>
		<tr>
					<%
					}
				%>
			<td>
				<%= Model[i].Name %><br />
				<img src="<%= Model[i].ImageData %>" /><br />
				Created: <%= Model[i].DateCreated.ToShortDateString() %>
			</td>
				<%
				}
			%>
		</tr>		
	</table>
	<br />
	<br />
</body>
</html>
