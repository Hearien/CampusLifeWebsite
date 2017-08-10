<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/campusAdmin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	校园生活管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/echarts.min.js" type="text/javascript"></script>
    <style>
	    .titlehead{
		    width: 104%;
	    }
	    .systitle h1{
		    color: #222;
		    font-family: arial, 'Hiragino Sans GB', 'Microsoft Yahei', 微软雅黑, 宋体, Tahoma, Arial, Helvetica, STHeiti;
		    letter-spacing: 12px;
	    }
	    .icos{
		    margin: 27px 0;
		    text-align:right;
		    position:relative;
	    }
	    .icos a{
	        display: inline-block;
		    background-color: #fff;
	       height: 50px;
	       border: 1px solid #ccc;
	       border-radius: 36px;
	       width: 50px;
	       font-size: 24px;
	       cursor:pointer;
	    }
	    .icos a span{
		    text-align: center;
	        display: block;
	        line-height: 43px;
	    }
	    .icos a:last-of-type:hover{
		    color:#ff0000;
	    }
    </style>
    <div class="row col-md-12">
        <div class="page-header row col-md-12 titlehead">
	    <div class="col-md-4 systitle">
		    <h1>校园生活后台管理</h1><small>Campus Management System</small>
	    </div>
	    <div class="col-md-8 icos">
	 	    <a title="当前位置：中国 重庆">
	 		    <span class="glyphicon glyphicon-map-marker"></span>
	 	    </a>
	 	    <a title="存为书签">
	 		    <SPAN class="glyphicon glyphicon-bookmark"></SPAN>
	 	    </a>
	 	    <a title="修改"><span class="glyphicon glyphicon-pencil"></span></a>
		    <a title="喜欢"><span class="glyphicon glyphicon-heart"></span></a>
	    </div>
    </div>


        <div id="container" style="height:400px"></div>
    <script type="text/javascript">
        var dom = document.getElementById("container");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        option = {
            "series": [
            	        {
            	            "center": [
            	                "20.0%",
            	                "50%"
            	            ],
            	            "radius": [
            	                "49%",
            	                "50%"
            	            ],
            	            "clockWise": false,
            	            "hoverAnimation": false,
            	            "type": "pie",
            	            "itemStyle": {
            	                "normal": {
            	                    "label": {
            	                        "show": true,
            	                        "textStyle": {
            	                            "fontSize": 15,
            	                            "fontWeight": "bold"
            	                        },
            	                        "position": "center"
            	                    },
            	                    "labelLine": {
            	                        "show": false
            	                    },
            	                    "color": "#5886f0",
            	                    "borderColor": "#5886f0",
            	                    "borderWidth": 25
            	                },
            	                "emphasis": {
            	                    "label": {
            	                        "textStyle": {
            	                            "fontSize": 15,
            	                            "fontWeight": "bold"
            	                        }
            	                    },
            	                    "color": "#5886f0",
            	                    "borderColor": "#5886f0",
            	                    "borderWidth": 25
            	                }
            	            },
            	            "data": [
            	                {
            	                    "value": 52.7,
            	                    "name": "用户  674"
            	                },
            	                {
            	                    "name": " ",
            	                    "value": 47.3,
            	                    "itemStyle": {
            	                        "normal": {
            	                            "label": {
            	                                "show": false
            	                            },
            	                            "labelLine": {
            	                                "show": false
            	                            },
            	                            "color": "#5886f0",
            	                            "borderColor": "#5886f0",
            	                            "borderWidth": 0
            	                        },
            	                        "emphasis": {
            	                            "color": "#5886f0",
            	                            "borderColor": "#5886f0",
            	                            "borderWidth": 0
            	                        }
            	                    }
            	                }
            	            ]
            	        },
            	        {
            	            "center": [
            	                "80.0%",
            	                "50%"
            	            ],
            	            "radius": [
            	                "49%",
            	                "50%"
            	            ],
            	            "clockWise": false,
            	            "hoverAnimation": false,
            	            "type": "pie",
            	            "itemStyle": {
            	                "normal": {
            	                    "label": {
            	                        "show": true,
            	                        "textStyle": {
            	                            "fontSize": 15,
            	                            "fontWeight": "bold"
            	                        },
            	                        "position": "center"
            	                    },
            	                    "labelLine": {
            	                        "show": false
            	                    },
            	                    "color": "#ee3a3a",
            	                    "borderColor": "#ee3a3a",
            	                    "borderWidth": 25
            	                },
            	                "emphasis": {
            	                    "label": {
            	                        "textStyle": {
            	                            "fontSize": 15,
            	                            "fontWeight": "bold"
            	                        }
            	                    },
            	                    "color": "#ee3a3a",
            	                    "borderColor": "#ee3a3a",
            	                    "borderWidth": 25
            	                }
            	            },
            	            "data": [
            	                {
            	                    "value": 30,
            	                    "name": "流量情况  35G"
            	                },
            	                {
            	                    "name": " ",
            	                    "value": 52.7,
            	                    "itemStyle": {
            	                        "normal": {
            	                            "label": {
            	                                "show": false
            	                            },
            	                            "labelLine": {
            	                                "show": false
            	                            },
            	                            "color": "#ee3a3a",
            	                            "borderColor": "#ee3a3a",
            	                            "borderWidth": 0
            	                        },
            	                        "emphasis": {
            	                            "color": "#ee3a3a",
            	                            "borderColor": "#ee3a3a",
            	                            "borderWidth": 0
            	                        }
            	                    }
            	                }
            	            ]
            	        },
            	        {
            	            "center": [
                "50%",
                "50%"
            ],
            	            "radius": [
                "49%",
                "50%"
            ],
            	            "clockWise": false,
            	            "hoverAnimation": false,
            	            "type": "pie",
            	            "itemStyle": {
            	                "normal": {
            	                    "label": {
            	                        "show": true,
            	                        "textStyle": {
            	                            "fontSize": 15,
            	                            "fontWeight": "bold"
            	                        },
            	                        "position": "center"
            	                    },
            	                    "labelLine": {
            	                        "show": false
            	                    },
            	                    "color": "#44A3BB",
            	                    "borderColor": "#44A3BB",
            	                    "borderWidth": 25
            	                },
            	                "emphasis": {
            	                    "label": {
            	                        "textStyle": {
            	                            "fontSize": 15,
            	                            "fontWeight": "bold"
            	                        }
            	                    },
            	                    "color": "#44A3BB",
            	                    "borderColor": "#44A3BB",
            	                    "borderWidth": 25
            	                }
            	            },
            	            "data": [
                {
                    "value": 30,
                    "name": "访问量  1252"
                },
                {
                    "name": " ",
                    "value": 47.3,
                    "itemStyle": {
                        "normal": {
                            "label": {
                                "show": false
                            },
                            "labelLine": {
                                "show": false
                            },
                            "color": "#44A3BB",
                            "borderColor": "#44A3BB",
                            "borderWidth": 0
                        },
                        "emphasis": {
                            "color": "#44A3BB",
                            "borderColor": "#44A3BB",
                            "borderWidth": 0
                        }
                    }
                }
            ]
            	        }
            	    ]
        };
        if (option && typeof option === "object") {
            var startTime = +new Date();
            myChart.setOption(option, true);
            var endTime = +new Date();
            var updateTime = endTime - startTime;
            console.log("Time used:", updateTime);
        }  
        </script> 
		</div>
    </div>
    
</asp:Content>
