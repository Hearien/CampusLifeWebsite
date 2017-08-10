<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	关于我们
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style type="text/css">
.contact-form div{
	padding:5px 0;
}
.contact-form span{
	display:block;
	font-size:0.85em;
	color:#888;
	padding-bottom:5px;
	margin-right:1em;
}
.contact-form input[type="text"],.contact-form textarea{
		    padding:10px;
			display:block;
			width:100%;
			background:none;
			border:1px solid #8fc74a;
			outline:none;
			color:#C5C5C5;
			font-size:14px;
			 font-family: 'Open Sans', sans-serif;
			-webkit-appearance:none;
}
.contact-form textarea{
		resize:none;
		height:105px;		
}
.contact-form input[type="text"]:hover,.contact-form input[type="text"]:focus,.contact-form textarea:hover,.contact-form textarea:focus{
	border-color:#C5C5C5;
}
.myButton{
    background:#8fc74a;
	color: #FFF;
	text-shadow:0px 1px 0px rgba(0, 0, 0, 0.34);
	font-size:1em;
	padding:10px 20px;
	border:none;
	text-decoration: none;
	outline: 0;
	-webkit-transition: all 0.5s ease-in-out;
	-moz-transition: all 0.5s ease-in-out;
	-o-transition: all 0.5s ease-in-out;
	transition: all 0.5s ease-in-out;
    display:inline-block;
    font-family: 'Open Sans', sans-serif;
    cursor:pointer;
    -webkit-appearance:none;
    float:right;
}
.myButton:hover{
		background:#333;
}
.company_address{
	 float:left;
	 width:25%;
}
.company_address p{
	color:#888;
	font-size:0.85em;
	padding:5px 0;
}
.company_address p span a{
	text-decoration:underline;
	color:#CCC;
}
.company_address p span a:hover{
	text-decoration:none;
	color:#8fc74a;
}
.contact_info{
	float:right;
	width:70%;
	margin-left:5%;
}

.contact-data h4{
	color: #8fc74a;
	font-size: 20px;
	text-transform: uppercase;
	padding: 15px 0;
	font-weight: 400;
	border-bottom: 1px solid rgba(245, 238, 243, 0.15);
	margin:0;
}
</style>
  
		<div class="row col-md-12 contact-data">
            <div class="content_bottom">
				 	<div class="col-md-6 col-sm-12 company_address">
				     	<h4>Location</h4>
						    	<p>中国</p>
						   		<p>重庆市 南岸区 南山街道</p>
						   		<p>重庆第二师范学院</p>
				   		<p>Phone:(00) 222 666 444</p>
				   		<p>Fax: (000) 000 00 00 0</p>
				 	 	<p>Email: <span><a href="mailto:1251447688@qq.com">info(at)mycompany.com</a></span></p>
				   		<p>Follow on: <span><a href="#">Facebook</a></span>, <span><a href="#">Twitter</a></span></p>
				     </div>
				       <div class="col-md-6 col-sm-12 contact_info">
    	 				<h4>Find Us Here</h4>
					    	  <div class="map">
							   	    
                                       
                                        <script type="text/javascript" src="http://api.map.baidu.com/api?key=&v=1.1&services=true"></script>
                                        

                                          <!--百度地图容器-->
                                          <div style="width:697px;height:550px;border:#ccc solid 1px;" id="dituContent"></div>
                                        </body>
                                        <script type="text/javascript">
                                            //创建和初始化地图函数：
                                            function initMap() {
                                                createMap(); //创建地图
                                                setMapEvent(); //设置地图事件
                                                addMapControl(); //向地图添加控件
                                                addMarker(); //向地图中添加marker
                                            }

                                            //创建地图函数：
                                            function createMap() {
                                                var map = new BMap.Map("dituContent"); //在百度地图容器中创建一个地图
                                                var point = new BMap.Point(106.609087, 29.522494); //定义一个中心点坐标
                                                map.centerAndZoom(point, 14); //设定地图的中心点和坐标并将地图显示在地图容器中
                                                window.map = map; //将map变量存储在全局
                                            }

                                            //地图事件设置函数：
                                            function setMapEvent() {
                                                map.enableDragging(); //启用地图拖拽事件，默认启用(可不写)
                                                map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
                                                map.enableDoubleClickZoom(); //启用鼠标双击放大，默认启用(可不写)
                                                map.enableKeyboard(); //启用键盘上下左右键移动地图
                                            }

                                            //地图控件添加函数：
                                            function addMapControl() {
                                                //向地图中添加缩放控件
                                                var ctrl_nav = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_LARGE });
                                                map.addControl(ctrl_nav);
                                                //向地图中添加缩略图控件
                                                var ctrl_ove = new BMap.OverviewMapControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, isOpen: 1 });
                                                map.addControl(ctrl_ove);
                                                //向地图中添加比例尺控件
                                                var ctrl_sca = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT });
                                                map.addControl(ctrl_sca);
                                            }

                                            //标注点数组
                                            var markerArr = [{ title: "重庆第二师范学院", content: "重庆第二师范学院", point: "106.582371|29.517371", isOpen: 1, icon: { w: 21, h: 21, l: 0, t: 0, x: 6, lb: 5} }
		                                         ];
                                            //创建marker
                                            function addMarker() {
                                                for (var i = 0; i < markerArr.length; i++) {
                                                    var json = markerArr[i];
                                                    var p0 = json.point.split("|")[0];
                                                    var p1 = json.point.split("|")[1];
                                                    var point = new BMap.Point(p0, p1);
                                                    var iconImg = createIcon(json.icon);
                                                    var marker = new BMap.Marker(point, { icon: iconImg });
                                                    var iw = createInfoWindow(i);
                                                    var label = new BMap.Label(json.title, { "offset": new BMap.Size(json.icon.lb - json.icon.x + 10, -20) });
                                                    marker.setLabel(label);
                                                    map.addOverlay(marker);
                                                    label.setStyle({
                                                        borderColor: "#808080",
                                                        color: "#333",
                                                        cursor: "pointer"
                                                    });

                                                    (function () {
                                                        var index = i;
                                                        var _iw = createInfoWindow(i);
                                                        var _marker = marker;
                                                        _marker.addEventListener("click", function () {
                                                            this.openInfoWindow(_iw);
                                                        });
                                                        _iw.addEventListener("open", function () {
                                                            _marker.getLabel().hide();
                                                        })
                                                        _iw.addEventListener("close", function () {
                                                            _marker.getLabel().show();
                                                        })
                                                        label.addEventListener("click", function () {
                                                            _marker.openInfoWindow(_iw);
                                                        })
                                                        if (!!json.isOpen) {
                                                            label.hide();
                                                            _marker.openInfoWindow(_iw);
                                                        }
                                                    })()
                                                }
                                            }
                                            //创建InfoWindow
                                            function createInfoWindow(i) {
                                                var json = markerArr[i];
                                                var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>" + json.content + "</div>");
                                                return iw;
                                            }
                                            //创建一个Icon
                                            function createIcon(json) {
                                                var icon = new BMap.Icon("http://app.baidu.com/map/images/us_mk_icon.png", new BMap.Size(json.w, json.h), { imageOffset: new BMap.Size(-json.l, -json.t), infoWindowOffset: new BMap.Size(json.lb + 5, 1), offset: new BMap.Size(json.x, json.h) })
                                                return icon;
                                            }

                                            initMap(); //创建和初始化地图
                                        </script>
                                    
                                    <br><small><a href="https://maps.google.co.in/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=Lighthouse+Point,+FL,+United+States&amp;aq=4&amp;oq=light&amp;sll=26.275636,-80.087265&amp;sspn=0.04941,0.104628&amp;ie=UTF8&amp;hq=&amp;hnear=Lighthouse+Point,+Broward,+Florida,+United+States&amp;t=m&amp;z=14&amp;ll=26.275636,-80.087265" style="color:#999;text-align:left;font-size:13px">View Larger Map</a></small>
							  </div>
      				   </div>
				      
	        </div>
			
		    <div class="col-md-12 contact-form">
                  <h4>Get In Touch</h4>
					     <form method="post" action="/Campus/Suggest">
					    	<div>
						    	<span><label>NAME</label></span>
						    	<span><input name="userName" type="text" class="textbox"></span>
						    </div>
						    <div>
						    	<span><label>E-MAIL</label></span>
						    	<span><input name="userEmail" type="text" class="textbox"></span>
						    </div>
					        <div>					    	
						    	<span><label>SUBJECT</label></span>
						    	<span><textarea name="opinion"> </textarea></span>
						    </div>
						   <div>
						   		<span><input type="submit" value="Submit" class="myButton"></span>
						  </div>
					    </form>
				  </div>
				  
		</div>
		

</asp:Content>
