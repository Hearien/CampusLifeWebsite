<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campus.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	��������
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
						    	<p>�й�</p>
						   		<p>������ �ϰ��� ��ɽ�ֵ�</p>
						   		<p>����ڶ�ʦ��ѧԺ</p>
				   		<p>Phone:(00) 222 666 444</p>
				   		<p>Fax: (000) 000 00 00 0</p>
				 	 	<p>Email: <span><a href="mailto:1251447688@qq.com">info(at)mycompany.com</a></span></p>
				   		<p>Follow on: <span><a href="#">Facebook</a></span>, <span><a href="#">Twitter</a></span></p>
				     </div>
				       <div class="col-md-6 col-sm-12 contact_info">
    	 				<h4>Find Us Here</h4>
					    	  <div class="map">
							   	    
                                       
                                        <script type="text/javascript" src="http://api.map.baidu.com/api?key=&v=1.1&services=true"></script>
                                        

                                          <!--�ٶȵ�ͼ����-->
                                          <div style="width:697px;height:550px;border:#ccc solid 1px;" id="dituContent"></div>
                                        </body>
                                        <script type="text/javascript">
                                            //�����ͳ�ʼ����ͼ������
                                            function initMap() {
                                                createMap(); //������ͼ
                                                setMapEvent(); //���õ�ͼ�¼�
                                                addMapControl(); //���ͼ��ӿؼ�
                                                addMarker(); //���ͼ�����marker
                                            }

                                            //������ͼ������
                                            function createMap() {
                                                var map = new BMap.Map("dituContent"); //�ڰٶȵ�ͼ�����д���һ����ͼ
                                                var point = new BMap.Point(106.609087, 29.522494); //����һ�����ĵ�����
                                                map.centerAndZoom(point, 14); //�趨��ͼ�����ĵ�����겢����ͼ��ʾ�ڵ�ͼ������
                                                window.map = map; //��map�����洢��ȫ��
                                            }

                                            //��ͼ�¼����ú�����
                                            function setMapEvent() {
                                                map.enableDragging(); //���õ�ͼ��ק�¼���Ĭ������(�ɲ�д)
                                                map.enableScrollWheelZoom(); //���õ�ͼ���ַŴ���С
                                                map.enableDoubleClickZoom(); //�������˫���Ŵ�Ĭ������(�ɲ�д)
                                                map.enableKeyboard(); //���ü����������Ҽ��ƶ���ͼ
                                            }

                                            //��ͼ�ؼ���Ӻ�����
                                            function addMapControl() {
                                                //���ͼ��������ſؼ�
                                                var ctrl_nav = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_LARGE });
                                                map.addControl(ctrl_nav);
                                                //���ͼ���������ͼ�ؼ�
                                                var ctrl_ove = new BMap.OverviewMapControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, isOpen: 1 });
                                                map.addControl(ctrl_ove);
                                                //���ͼ����ӱ����߿ؼ�
                                                var ctrl_sca = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT });
                                                map.addControl(ctrl_sca);
                                            }

                                            //��ע������
                                            var markerArr = [{ title: "����ڶ�ʦ��ѧԺ", content: "����ڶ�ʦ��ѧԺ", point: "106.582371|29.517371", isOpen: 1, icon: { w: 21, h: 21, l: 0, t: 0, x: 6, lb: 5} }
		                                         ];
                                            //����marker
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
                                            //����InfoWindow
                                            function createInfoWindow(i) {
                                                var json = markerArr[i];
                                                var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>" + json.content + "</div>");
                                                return iw;
                                            }
                                            //����һ��Icon
                                            function createIcon(json) {
                                                var icon = new BMap.Icon("http://app.baidu.com/map/images/us_mk_icon.png", new BMap.Size(json.w, json.h), { imageOffset: new BMap.Size(-json.l, -json.t), infoWindowOffset: new BMap.Size(json.lb + 5, 1), offset: new BMap.Size(json.x, json.h) })
                                                return icon;
                                            }

                                            initMap(); //�����ͳ�ʼ����ͼ
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
