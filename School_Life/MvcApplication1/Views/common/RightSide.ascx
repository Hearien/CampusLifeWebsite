<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Model.Goods>>" %>

    <style type="text/css">
        .tempra{overflow:hidden;}
        .goods li{margin:5px auto;}
    </style>

    <%
        
         %>

    <div class="panel panel-default tempra">
		<div class="panel-heading">
			最近天气
		</div>
		<div class="panel-body">
			<iframe width="225" scrolling="no" height="80" frameborder="0" allowtransparency="true" src="http://i.tianqi.com/index.php?c=code&id=8&icon=1&num=3"></iframe>
		</div>
	</div>
    <a class="thumbnail" href="#">
		<img src="../../img/boot.jpg" height="40" />
	</a>
    <div class="panel panel-default">
					<div class="panel-heading">
						热卖商品
						<a href="#" class="text-muted pull-right">>></a>
					</div>
					<ul class="media-list goods">
                    <%for (var i = 0; i < 4; i++)
                      {
                          var id = Model.ElementAt(i).id.ToString();
                          %> 
                      
						<li>
							<div class="media">
								<div class="media-left">
									<img src="<%=Session["root"] %><%=Html.Encode(Model.ElementAt(i).thumb) %>.jpg" width="100" />
								</div>
								<div class="media-body">
									<a href="Detail?goodid=<%= id %>">
                                        <strong class="media-heading">
                                        <%=Html.Encode(Model.ElementAt(i).title.Length > 12 ? Model.ElementAt(i).title.Substring(0, 10) : Model.ElementAt(i).title)%>
                                        </strong>
									</a>
                                    <p><%=Model.ElementAt(i).price %>元 </p>
									
								</div>
							</div>
						</li>
                        <%} %>
					</ul>
				</div>

